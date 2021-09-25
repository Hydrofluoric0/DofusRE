# D2o files

## Purpose

D2o files stores every static information about the game. For example, we can find:

* NPCs: ids, types, dialogs, etc.
* Items: ids, types, effects, etc.
* ...

## Structure

A d2o file contains three distinct parts:

* The indexes table. This tells us where are the data in the file for a specified class
* The class definitions. This is the structure of our classes this will give us the fields and the name of the class
* The "processors" which should be seen as a way to read the classes fields without having to read the whole file



**Class data indexes table**.

This table will be used at the end to read the data of the classes present in the d2o file.

To read this table you must:

1. Retrieve its position a the beginning of the file by reading the first four bytes as an integer.
2. Go to the position indicated by the previous step, and read another integer to get the length of the table. Be careful, it is the length, not the count of elements.
3. Iterate to read and store the indexes until you reach the length previously retrieved. To read an index:
   1. key (int) => is the key (id) used to refer to the class definition
   2. pointer (int) => represents the address of the text's string in the file

Pseudo-code:

```c#
private void readIndexes()
{
    var indexesPointer = this.m_reader.ReadInt();
    this.m_reader.Seek(indexesPointer, SeekOrigin.Begin);
    var indexesLength = this.m_reader.ReadInt();
    for (int i = 0; i < indexesLength; i += sizeof(int) + sizeof(int))
    {
        var key = this.m_reader.ReadInt();
        var pointer = this.m_reader.ReadInt();
        this.m_indexes.Add(key, pointer);
     }
}
```

**Class definitions**

As said previously, this will tell us what are the classes we can read from the file and what they contain.

To read the definitions, you must :

* Read an int representing the amount of classes contained in the file
* Iterate until you've read the previously gathered amount of classes. This is structured as:
  * id (int) => the id of the class identifier
  * name (UTF) => the name of the class identifier
  * package (UTF) => the "namespace" of the class identifier
  * fields
    * count (int) => the amount of fields in the class definition
    * Iterate until you've read the previously gathered amount of fields. This is structured as:
      * name (UTF) => name of the field
      * type (int) => type of the field. 
        **warning:** if the type is VECTOR, you have additional reading to do. In order to let this documentation clear and concise, you can find the details of it at the end of this page, in the section "Appendix".

Code:

```C#
        private void readClassesDefinitions()
        {
            var classesCount = this.m_reader.ReadInt();

            for (int i = 0; i < classesCount; i++)
            {
                var id = this.m_reader.ReadInt();
                var name = this.m_reader.ReadUTF();
                var package = this.m_reader.ReadUTF();

                var classDef = new GameDataClassDefinition(id, name, package);
                readClassDefinitionFields(classDef);
                this.m_classesDefinitions.Add(id, classDef);
            }
        }

        private void readClassDefinitionFields(GameDataClassDefinition classDef)
        {
            var fieldsCount = this.m_reader.ReadInt();
            for (int j = 0; j < fieldsCount; j++)
            {
                var fieldName = this.m_reader.ReadUTF();
                var fieldType = (GameDataFieldType)this.m_reader.ReadInt();

                classDef.AddField(new GameDataField(fieldName, fieldType, this.m_reader, this.m_classesDefinitions));
            }
        }
```

````c#
using DofusRE.d2i;
using DofusRE.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2o
{
    public class GameDataField
    {
        public const int NULL_IDENTIFIER = -1431655766;

        private string m_name;
        private GameDataFieldType m_type;
        private BigEndianReader m_reader;
        private Func<int, object> m_readFunction;
        private Dictionary<int, GameDataClassDefinition> m_classDefinitions;

        private List<GameDataFieldType> m_innerTypes;
        private List<Func<int, object>> m_innerReadFunctions;

        public string Name => this.m_name;
        public GameDataFieldType Type => this.m_type;

        public GameDataField(string name, GameDataFieldType type, BigEndianReader reader, Dictionary<int, GameDataClassDefinition> classDefinitions)
        {
            this.m_name = name;
            this.m_type = type;
            this.m_reader = reader;
            this.m_classDefinitions = classDefinitions;

            this.m_innerTypes = new List<GameDataFieldType>();
            this.m_innerReadFunctions = new List<Func<int, object>>();

            this.m_readFunction = defineReadFunction(type);
        }

        private Func<int, object> defineReadFunction(GameDataFieldType type)
        {
            switch (type)
            {
                case GameDataFieldType.INT:
                case GameDataFieldType.I18N:
                    return new Func<int, object>(_ => this.m_reader.ReadInt());
                case GameDataFieldType.BOOL:
                    return new Func<int, object>(_ => this.m_reader.ReadBoolean());
                case GameDataFieldType.STRING:
                    return new Func<int, object>(_ => this.m_reader.ReadUTF());
                case GameDataFieldType.NUMBER:
                    return new Func<int, object>(_ => this.m_reader.ReadDouble());
                case GameDataFieldType.UINT:
                    return new Func<int, object>(_ => this.m_reader.ReadUInt());
                case GameDataFieldType.VECTOR:
                    var typeName = this.m_reader.ReadUTF();
                    var innerType = (GameDataFieldType)this.m_reader.ReadInt();
                    this.m_innerTypes.Add(innerType);
                    var innerReadFunction = this.defineReadFunction(innerType);
                    this.m_innerReadFunctions.Insert(0, innerReadFunction);
                    return readVector;
                default:
                    if (type > 0)
                        return new Func<int, object>((_) => readObject());
                    throw new Exception($"unknown GameDataField type: {type}");
            }
        }

        private object readVector(int depth = 0)
        {
            var length = this.m_reader.ReadInt();
            var type = this.m_innerTypes[depth];
            var list = new List<object>();

            for (int i = 0; i < length; i++)
            {
                var element = this.m_innerReadFunctions[depth](depth + 1);
                list.Add(element);
            }
            return list;
        }

        private object readObject()
        {
            var classId = this.m_reader.ReadInt();
            if (classId == NULL_IDENTIFIER)
            {
                return null;
            }
            var classDefinition = this.m_classDefinitions[classId];
            return classDefinition.Read();
        }

        public object Read()
        {
            return this.m_readFunction(0);
        }
    }
}

````



**Processors**

Processors are structured the following way:

* fieldName (UTF) => the name of the field targeted
* fieldIndex (int) => index of the field
* fieldType (int) => type of field
* fieldCount (int) => ?

Pseudo-code:

````c#
			var length = this.m_reader.ReadInt();
            var offset = this.m_reader.Position + length + 4;
            var read = 0;
            while (read < length)
            {
                var beginPosition = this.m_reader.Position;
                
                var fieldName = this.m_reader.ReadUTF();
                var fieldIndex = this.m_reader.ReadInt() + offset;
                var fieldType = this.m_reader.ReadInt();
                var fieldCount = this.m_reader.ReadInt();

                var endPosition = this.m_reader.Position;
                read += (int)(endPosition - beginPosition);
            }
        }
````

TODO: more details on processors



## **Reading the classes datas**

````c#
// m_indexes contains the indexes from the indexes table
// m_classesDefinitions contains every classes definitions contained in the d2o file
foreach (var entry in this.m_indexes)
            {
                var key = entry.Key;
                var position = entry.Value;

                this.m_reader.Seek(position, SeekOrigin.Begin);
                
                var classDefinitionIdentifier = this.m_reader.ReadInt();
                var classDef = this.m_classesDefinitions[classDefinitionIdentifier];
                var fields = classDef.Read();
            }
````

```c#
public Dictionary<string, object> Read()
        {
            var result = new Dictionary<string, object>();

            foreach (var field in this.m_fields)
            {
                var value = field.Read();
                result.Add(field.Name, value);
            }

            return result;
        }
```




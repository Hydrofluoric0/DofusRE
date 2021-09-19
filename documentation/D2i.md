# D2i files

## Purpose

D2i files aims to provide every texts to the Dofus client. Basically, it stores every text and their id. This allows the client to receive only the key of the text to show instead of the full text.

## Structure

A d2i files contains four distinct parts:

**Text indexes table**.

To read this table you must:

1. Retrieve its position a the beginning of the file by reading the first four bytes as an integer.
2. Go to the position indicated by the previous step, and read another integer to get the length of the table. Be careful, it is the length, not the count of elements.
3. Iterate to read the indexes until you reach the length previously retrieved. To read an index:
   1. key (int) => is the key (id) used to refer to the text
   2. isDiacritical (bool) => tells if the text contains diacritical character
   3. pointer (int) => represents the address of the text's string in the file
   4. (only if isDiacritical is true) undiacriticalPointer => represents the addresse of the undiacritical text's string in the file.

Pseudo-code:

````
var textIndexes = new Dictionary<int, int>();
var undiacriticalTextIndexes = new Dictionary<int, int>();
var tableLen = bigEndianReader.readInt();
while (tableLen > 0) {
	var key = bigEndianReader.readInt();
	var isDiacritical = bigEndianReader.readBoolean();
	var pointer = bigEndianReader.readInt();
	if (isDiacritical) {
		var unDiacriticalPointer = bigEndianReader.readInt();
		undiacriticalTextIndexes[key] = unDiacriticalPointer;
	}
	
	tableLen -= sizeof(int) + sizeof(bool) + sizeof(int);
	//if isDiacritical is true, we have read one additional pointer
	if (isDiacritical) {
		tableLen -= sizeof(int);
	}
}
````





**Named text indexes table**

The named text indexes table start just after the Text indexes table.

To read this table, do as follow:

1. Read an integer representing the length of this table.
2. Iterate to read the named text indexes until you've read the length of the table. A named index is represented as: 
   1. key (utf) => is the key (id) used to refer to the text
   2. pointer (int) => represents the address of the text's string in the file

Pseudo-code:

```
var sortedTextIndexes = new Dictionary<int, int>();
var tableLen = bigEndianReader.readInt();
while (tableLen > 0) {
	var key = bigEndianReader.readUTF();
	var pointer = bigEndianReader.readInt();
	
	sortedTextIndexes[key] = pointer;
	// 2 = the int16 read to know the string key length. See how are written UTF in Dofus resources.
	tableLen -= (2 + key.length + sizeof(pointer));
}
```



**Sorted text indexes table**

The Sorted text indexes table starts just after the Named text indexes table.

To read this table, do as follow :

1. Read an integer representing the length of this table
2. Iterate to read the named text indexes until you've read the length of the table and by incrementing a variable. We will call this variable *idx*. An index is represented as:
   1. key (int) => is the key (id) used to refer to the text
   2. the sorted index, represented by our variable *idx*.

Pseudo code :

```
var sortedTextIndexes = new Dictionary<int, int>();
var tableLen = bigEndianReader.readInt();
var idx = 0;
while (tableLen > 0) {
  var key = bigEndianReader.readInt();
  sortedTextIndexes[key] = idx;
  
  idx += 1;
  tableLen -= sizeof(int);
}
```


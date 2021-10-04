using DofusRE.d2o;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DofusRE.Tests
{
    public class GameDataTests
    {
        [Fact]
        public void GameData_Tests()
        {
            var uuid = Guid.NewGuid();
            var output = Path.Combine(Path.GetTempPath(), $"output-{uuid}.d2o");
            File.Create(output).Close();

            var refReader = new D2oReader(@"./assets/items.d2o");
            refReader.Read();

            var writer = new D2oWriter(output);
            writer.Write(refReader.ClassesDefinitions, refReader.Classes);

            var reader = new D2oReader(output);
            reader.Read();

            Assert.True(reader.Classes.Count == refReader.Classes.Count);

            var refClasses = refReader.Classes.Values.ToList();
            var testClasses = reader.Classes.Values.ToList();

            for (int i = 0; i < refReader.Classes.Count; i++)
            {
                var refClass = refClasses[i];
                var testClass = testClasses[i];

                var fields = refClass.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();

                for (int j = 0; j < fields.Count; j++)
                {
                    var f = fields[j];
                    if (f.FieldType == typeof(int))
                    {
                        var vA = (int)f.GetValue(refClass);
                        var vB = (int)f.GetValue(testClass);
                        Assert.True(vA == vB);
                    }
                    else if (f.FieldType == typeof(string))
                    {
                        var vA = (string)f.GetValue(refClass);
                        var vB = (string)f.GetValue(testClass);
                        Assert.True(vA == vB);
                    }
                }
            }
        }
    }
}

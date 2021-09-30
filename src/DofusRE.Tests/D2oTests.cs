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
    public class D2oTests
    {
        [Fact]
        public void D2o_Tests()
        {
            var uuid = Guid.NewGuid();
            var output = Path.Combine(Path.GetTempPath(), $"output-{uuid}.d2i");

            var refReader = new D2oReader(@"./assets/items.d2o");
            refReader.Read();

            var writer = new D2oWriter(output, true);
            writer.Write(refReader.ClassesDefinitions, refReader.Classes);


            var reader = new D2oReader(output);
            reader.Read();
        }
    }
}

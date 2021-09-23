using DofusRE.d2o;
using System;
using System.Collections.Generic;
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
            var reader = new D2oReader(@"./assets/items.d2o");
            reader.Read();
        }
    }
}

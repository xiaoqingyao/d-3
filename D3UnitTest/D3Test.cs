using D3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3UnitTest
{
    [TestClass]
    public class D3Test
    {
        [TestMethod]
        public void TestD3EveryDay()
        {
            var rel = D3Manager.D3(2021, 1, 6);
            Assert.AreEqual(true, rel);
        }
    }
}

using D3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3UnitTest
{
    /// <summary>
    /// D-3整体测试
    /// </summary>
    [TestClass]
    public class D3Test
    {
        /// <summary>
        /// d-3测试调用
        /// </summary>
        [TestMethod]
        public void TestD3EveryDay()
        {
            var rel = D3Manager.D3(2021, 1, 7);
            Assert.AreEqual(true, rel.Result);
        }
        //d-3执行后数据库断言

        //d-3内
        //教室释放
        //增加教室
        //调课-增加课-有匹配
        //调课-增加课-没有匹配
        //调课-释放课-有匹配进入
        //调课-时方可-无匹配进入
    }
}

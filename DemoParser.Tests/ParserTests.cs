using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoParser.Tests
{
    [TestClass]
    public class ParserTests
    {
        private Parser _parser = new Parser();

        [TestMethod]
        public void TestMethod1()
        {
            _parser.ParseHeader(@"C:\Users\roder\AppData\Roaming\Ryada\RyaUploader\Demos\CSGO-3KuQo-jScCH-KNDdV-oUiZ2-Rz23L.dem");
            Assert.IsTrue(true);
        }

        //[TestMethod]
        //public void TestMethod2()
        //{
        //    _parser.PlayDemoFile(@"C:\Users\roder\AppData\Roaming\Ryada\RyaUploader\Demos\CSGO-3KuQo-jScCH-KNDdV-oUiZ2-Rz23L.dem");
        //    Assert.IsTrue(true);
        //}
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using concurrentProgramming;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 c = new Class1();
            Assert.AreEqual(-1, c.func());
        }
    }
}
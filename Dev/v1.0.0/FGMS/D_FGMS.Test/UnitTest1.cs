using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace D_FGMS.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestMethod2() 
        {
            Assert.IsTrue(1 + 1 == 2);
        }

    }
}
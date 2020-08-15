using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Logs;

namespace Tests
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void TestLog()
        {
            Log.GravarLog(new System.Exception("Teste da classe de Logs!"));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

using WordpressAutomation;

namespace WordpressTests
{
    public class WordpressTest
    {
        [TestInitialize]
        public void Init()
        {
            Driver.Initialize();
            LoginPage.GoTo();
            LoginPage.LoginAs("admin").WithPassword("admin").Login();
        }
       
        [TestCleanup]
        public void CleanUp()
        {
            Driver.Close();
        }
    }
}

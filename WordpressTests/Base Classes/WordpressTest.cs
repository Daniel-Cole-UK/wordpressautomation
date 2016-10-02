using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordpressAutomation;
using WordpressAutomation.Workflows;

namespace WordpressTests
{
    // Base class for common actions
    public class WordpressTest
    {
        [TestInitialize]
        public void Init()
        {
            // Initialize WebDriver
            Driver.Initialize();
            
            // Sets previousTitle, previousBody & previousComment to null
            PostCreator.Initialize();

            // Login to wp dashboard
            LoginPage.GoTo();
            LoginPage.LoginAs("admin").WithPassword("admin").Login();
        }
        
        [TestCleanup]
        public void CleanUp()
        {
            // Close the browser
            Driver.Close();
        }

    }
}

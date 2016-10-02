using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordpressAutomation;

namespace WordpressTests
{
    [TestClass]
    public class LoginTests : WordpressTest
    {
        [TestMethod]
        public void AdminUserCanLogin()
        {
            // Check if login was successful by checking if browser is at dashboard page
            Assert.IsTrue(DashboardPage.IsAt, "Failed to login.");
        }
    }
}

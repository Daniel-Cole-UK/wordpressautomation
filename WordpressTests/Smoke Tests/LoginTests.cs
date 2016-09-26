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
            Assert.IsTrue(DashboardPage.IsAt, "Failed to login.");
        }
    }
}

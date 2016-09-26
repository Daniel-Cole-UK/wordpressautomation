using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WordpressAutomation
{
    public class LoginPage
    {
        public static void GoTo()
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseURL + "wp-login.php");
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            wait.Until(d => d.SwitchTo().ActiveElement().GetAttribute("id") == "user_login");
        }


        public static LoginCommand LoginAs(string userName)
        {
            return new LoginCommand(userName);
        }
    }

    public class LoginCommand
    {
        private string password;
        private string userName;

        public LoginCommand(string userName)
        {
            this.userName = userName;
        }


        public LoginCommand WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public void Login()
        {
            Driver.Instance.FindElement(By.Id("user_login")).SendKeys(userName);

            Driver.Instance.FindElement(By.Id("user_pass")).SendKeys(password);

            Driver.Instance.FindElement(By.Id("wp-submit")).Click();
        }
    }
}

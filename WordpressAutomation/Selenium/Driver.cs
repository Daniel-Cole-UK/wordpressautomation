using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace WordpressAutomation
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }
        // property to store base url for wordpress instance
        public static string BaseURL
        {
            get
            {
                return "http://localhost/wordpress/";
            }  
        }

        // Create new instance of driver, switch on implicit wait, maximize browser window
        public static void Initialize()
        {
            Instance = new ChromeDriver();
            TurnOnWait();
            Instance.Manage().Window.Maximize();
        }

        // Method to close driver instance
        public static void Close()
        {
            Instance.Close();
        }

        // Method to click the browser back button a provided number of times
        public static void GoBack(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Instance.Navigate().Back();
            }
        }

        // Method to wait the current thread for provided time
        public static void Wait(TimeSpan timeSpan)
        {
            Thread.Sleep((int) (timeSpan.TotalSeconds * 1000));
        }

        // Method to switch off wait, carry out provided action, then switch wait back on
        public static void NoWait(Action action)
        {
            TurnOffWait();
            action();
            TurnOnWait();
        }

        // Method to set implicit wait of 10 seconds
        private static void TurnOnWait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        // Method to remove implicit wait
        private static void TurnOffWait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
        }
    }
}

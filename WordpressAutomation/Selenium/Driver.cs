using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace WordpressAutomation
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }
        public static string BaseURL
        {
            get
            {
                return "http://localhost/wordpress/";
            }  
        }

        public static void Initialize()
        {
            Instance = new ChromeDriver();
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            Instance.Manage().Window.Maximize();
        }

        public static void Close()
        {
            Instance.Close();
        }

        public static void Wait(TimeSpan timeSpan)
        {
            Thread.Sleep((int) (timeSpan.TotalSeconds * 1000));
        }
    }
}
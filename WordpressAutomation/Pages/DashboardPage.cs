﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordpressAutomation
{
    public class DashboardPage
    {
        // Returns true if browser is at Dashboard page
        public static bool IsAt
        {
            get
            {
                var h1s = Driver.Instance.FindElements(By.TagName("H1"));
                if (h1s.Count > 0)
                {
                    return h1s[0].Text == "Dashboard";
                } else
                {
                    return false;
                }
            }
        }
    }
}

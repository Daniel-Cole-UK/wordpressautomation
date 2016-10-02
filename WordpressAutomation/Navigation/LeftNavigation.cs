using System;
using OpenQA.Selenium;

namespace WordpressAutomation
{
    // Classes for selection of left navigation buttons
    class LeftNavigation
    {
        public class Posts
        {
            public class AddNew
            {
                public static void Select()
                {
                    MenuSelector.Select("menu-posts", "Add New");
                }
            }

            public class AllPosts
            {
                public static void Select()
                {
                    MenuSelector.Select("menu-posts", "All Posts");
                }
            }
        }

        public class Pages
        {
            public class AddNew
            {
                public static void Select()
                {
                    MenuSelector.Select("menu-pages", "Add New");
                }
            }

            public class AllPages
            {
                public static void Select()
                {
                    MenuSelector.Select("menu-pages", "All Pages");
                }
            }
        }

        public class Comments
        {
            public class AllComments
            {
                public static void Select()
                {
                    Driver.Instance.FindElement(By.Id("menu-comments")).Click();
                }
            }
        }
    }
}

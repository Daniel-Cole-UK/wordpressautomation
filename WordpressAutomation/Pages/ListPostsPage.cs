using System;
using OpenQA.Selenium;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;

namespace WordpressAutomation
{
    public class ListPostsPage
    {
        private static int lastCount;

        public static int CurrentPostCount
        {
            get
            {
                return GetPostCount();
            }
        }

        public static int PreviousPostCount
        {
            get
            {
                return lastCount;
            }
        }

        public static void GoTo(PostType postType)
        {
            switch (postType)
            {
                case PostType.Page:
                    LeftNavigation.Pages.AllPages.Select();
                    break;
                case PostType.Posts:
                    LeftNavigation.Posts.AllPosts.Select();
                    break;
            }
        }

        public static void SelectPost(string title)
        {
            Driver.Instance.FindElement(By.LinkText("Sample Page")).Click();
        }

        public static void StoreCount()
        {
            lastCount = GetPostCount();
        }

        public static bool DoesPostExistWithTitle(string title)
        {
            return Driver.Instance.FindElements(By.LinkText(title)).Any();
        }

        public static void RemovePost(string title)
        {
            var link = Driver.Instance.FindElement(By.LinkText(title));
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(link);
            action.Perform();
            Driver.Instance.FindElement(By.ClassName("submitdelete")).Click();
        }

        public static void SearchForPost(string searchString)
        {
            Driver.Instance.FindElement(By.Id("post-search-input")).SendKeys(searchString);
            Driver.Instance.FindElement(By.Id("search-submit")).Click();
        }

        public static int GetPostCount()
        {
            var countText = Driver.Instance.FindElement(By.ClassName("displaying-num")).Text;
            return int.Parse(countText.Split(' ')[0]);
        }
    }

    public enum PostType
    {
        Page,
        Posts
    }
}

using System;
using OpenQA.Selenium;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using WordpressAutomation.Workflows;

namespace WordpressAutomation
{
    public class ListPostsPage
    {
        // Property to store last count of posts
        private static int lastCount;

        // Property to store current post count
        public static int CurrentPostCount
        { get { return GetPostCount(); } }

        // Property to store previous post count
        public static int PreviousPostCount
        { get { return lastCount; } }

        // Method to store count of posts
        public static void StoreCount()
        {
            lastCount = GetPostCount();
        }

        // Method to return the current post count
        public static int GetPostCount()
        {
            var countText = Driver.Instance.FindElement(By.ClassName("displaying-num")).Text;
            return int.Parse(countText.Split(' ')[0]);
        }

        // Method to go to posts or pages
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

        // Method to select post with title matching provided string
        public static void SelectPost(string title)
        {
            Driver.Instance.FindElement(By.LinkText(title)).Click();
        }

        // Method to check for existence of post matching provided string
        public static bool DoesPostExistWithTitle(string title)
        {
            return Driver.Instance.FindElements(By.LinkText(title)).Any();
        }

        // Method to remove post with title matching provided string
        public static void RemovePost(string title)
        {
            var link = Driver.Instance.FindElement(By.LinkText(title));
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(link);
            action.Perform();
            Driver.Instance.FindElement(By.ClassName("submitdelete")).Click();
        }

        // Method to search for post using provided string
        public static void SearchForPost(string searchString)
        {
            Driver.Instance.FindElement(By.Id("post-search-input")).SendKeys(searchString);
            Driver.Instance.FindElement(By.Id("search-submit")).Click();
        }

        // Method to view published version of post with title matching provided string
        public static void ViewPublishedPost(string title)
        {
            var link = Driver.Instance.FindElement(By.LinkText(title));
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(link);
            action.Perform();
            Driver.Instance.FindElement(By.LinkText("View")).Click();
        }

        // Method to enter quick edit mode for post with title matching provided string
        public static void QuickEditPost(string title)
        {
            var link = Driver.Instance.FindElement(By.LinkText(title));
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(link);
            action.Perform();
            Driver.Instance.FindElement(By.LinkText("Quick Edit")).Click();
        }

        // Method to make an edit when in quick edit mode (appends text)
        public static void MakeEdit()
        {
            Driver.Instance.FindElement(By.ClassName("input-text-wrap")).FindElement(By.ClassName("ptitle")).SendKeys(" edited"); ;
            Driver.Instance.FindElement(By.ClassName("save")).Click();
            PostCreator.UpdateTitle(PostCreator.PreviousTitle + " edited");
        }

        // Method to check if currently in quick edit mode
        public static bool IsInQuickEdit(string title)
        {
            var editLegend = Driver.Instance.FindElement(By.ClassName("inline-edit-legend")).Displayed;
            var titleInput = Driver.Instance.FindElement(By.ClassName("input-text-wrap")).FindElement(By.ClassName("ptitle")).GetAttribute("value");
            return editLegend == true && titleInput == title;
        }
    }

    // Enumeration to store constants for post type
    public enum PostType
    {
        Page,
        Posts
    }
}

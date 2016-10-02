using System;
using OpenQA.Selenium;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using WordpressAutomation.Workflows;

namespace WordpressAutomation
{
    public class ListCommentsPage
    {
        // Property to store count of posts or comments for comparison in assertions
        private static int lastCount;

        // Property to store the current count of comments for comparison in assertions
        public static int CurrentCommentCount
        { get { return GetCommentCount(); } }

        // Property to store the previous count of comments for comparison in assertions
        public static int PreviousCommentCount
        { get { return lastCount; } }

        public static int GetCommentCount()
        {
            var countText = Driver.Instance.FindElement(By.ClassName("displaying-num")).Text;
            return int.Parse(countText.Split(' ')[0]);
        }

        // Method to get the comment count and store in the lastCount property
        public static void StoreCount()
        {
            lastCount = GetCommentCount();
        }

        // Method to go to list of all comments
        public static void GoTo()
        {
            LeftNavigation.Comments.AllComments.Select();
        }

        // Method to check for the existence of a comment with a text content which matches a provided string
        public static bool DoesCommentExistWithText(string text)
        {
            return Driver.Instance.FindElement(By.Id("the-comment-list")).FindElement(By.TagName("p")).Text == text;
        }

        // Method to remove comment which matches provided text
        public static void RemoveComment(string text)
        {
            var comment = Driver.Instance.FindElement(By.Id("the-comment-list")).FindElement(By.TagName("p"));
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(comment);
            action.Perform();
            Driver.Instance.FindElement(By.ClassName("delete")).Click();
        }

        // Method to search for comment(s) using provided search string
        public static void SearchForComment(string searchString)
        {
            Driver.Instance.FindElement(By.Id("comment-search-input")).SendKeys(searchString);
            Driver.Instance.FindElement(By.Id("search-submit")).Click();
        }

        // Method to enter quick edit mode for the last created comment
        public static void QuickEditComment()
        {
            var comment = Driver.Instance.FindElement(By.Id("the-comment-list")).FindElement(By.TagName("p"));
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(comment);
            action.Perform();
            Driver.Instance.FindElement(By.LinkText("Quick Edit")).Click();
        }

        // Method to check if in quick edit mode
        public static bool IsInQuickEdit(string text)
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            var editLegend = Driver.Instance.FindElement(By.Id("editlegend")).Text == "Edit Comment";
            var commentInput = Driver.Instance.FindElement(By.ClassName("wp-editor-area")).GetAttribute("value");
            return editLegend == true && commentInput == text;
        }

        // Method to edit comment which is currently in quick edit mode (appends text to existing content)
        public static void MakeEdit()
        {
            Driver.Instance.FindElement(By.ClassName("wp-editor-area")).SendKeys(" edited"); ;
            Driver.Instance.FindElement(By.Id("savebtn")).Click();
            PostCreator.UpdateComment(PostCreator.PreviousComment + " edited");

        }
    }
}

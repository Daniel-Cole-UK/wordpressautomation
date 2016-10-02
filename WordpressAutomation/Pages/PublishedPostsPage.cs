using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using WordpressAutomation.Workflows;

namespace WordpressAutomation
{
    public class PublishedPostsPage
    {
        // Method to check for existence of published post with matching title and body text
        public static bool DoesPostExist(string title, string body)
        {
            var postTitle = Driver.Instance.FindElement(By.ClassName("entry-title")).Text;
            var postBody = Driver.Instance.FindElement(By.ClassName("entry-content")).FindElement(By.TagName("p")).Text;
            return postTitle == title && postBody == body;
        }

        // Method to leave a randomly generated comment
        public static void LeaveComment()
        {
            PostCreator.CreateComment();
            Driver.Instance.FindElement(By.Id("comment")).SendKeys(PostCreator.PreviousComment);
            Driver.Instance.FindElement(By.Id("submit")).Submit();
        }

        // Method to check for the existence of a comment with matching body text to provided string
        public static bool DoesCommentExist(string comment)
        {
            var commentText = Driver.Instance.FindElement(By.ClassName("comment-content")).FindElement(By.TagName("p")).Text;
            return commentText == comment;
        }
    }
}

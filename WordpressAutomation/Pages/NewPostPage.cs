using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;

namespace WordpressAutomation
{
    public class NewPostPage
    {
        public static string Title {
                    get {
                            var title = Driver.Instance.FindElement(By.Id("title"));
                            if (title != null)
                            {
                                return title.GetAttribute("value");
                            } else
                            {
                                return string.Empty;
                            }
                        }
                }

        // Method to go to add new post screen
        public static void GoTo()
        {
            LeftNavigation.Posts.AddNew.Select();
        }

        // Method to return new instance of CreatePostCommand class
        public static CreatePostCommand CreatePost(string title)
        {
            return new CreatePostCommand(title);
        }

        // Method to go to newly created post
        public static void GoToNewPost()
        {
            Driver.Instance.FindElement(By.Id("message")).FindElements(By.TagName("a"))[0].Click();
        }

        // Method to check if post is in edit mode
        public static bool IsInEditMode()
        {
            var firstH1 = Driver.Instance.FindElements(By.TagName("H1"))[0].Text;
            return firstH1 == "Edit Page Add New";
        }
    }

    // Class to handle creation of posts
    public class CreatePostCommand
    {
        private string title;
        private string body;

        public CreatePostCommand(string title)
        {
            this.title = title;
        }

        public CreatePostCommand WithBody(string body)
        {
            this.body = body;
            return this;
        }

        public void Publish()
        {
            Driver.Instance.FindElement(By.Id("title")).SendKeys(title);

            Driver.Instance.SwitchTo().Frame("content_ifr");
            Driver.Instance.SwitchTo().ActiveElement().SendKeys(body);
            Driver.Instance.SwitchTo().DefaultContent();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Instance.FindElement(By.Id("publish")).Click();
        }
    }
}

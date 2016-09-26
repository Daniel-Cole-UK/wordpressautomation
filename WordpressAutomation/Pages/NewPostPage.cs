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
                    get
                    {
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

        public static void GoTo()
        {
            // General menu navigation
            LeftNavigation.Posts.AddNew.Select();
        }

        public static CreatePostCommand CreatePost(string title)
        {
            return new CreatePostCommand(title);
        }

        public static void GoToNewPost()
        {
            Driver.Instance.FindElement(By.Id("message")).FindElements(By.TagName("a"))[0].Click();
        }

        public static bool IsInEditMode()
        {
            var firstH1 = Driver.Instance.FindElements(By.TagName("H1"))[0].Text;
            return firstH1 == "Edit Page Add New";
        }
    }

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

            Driver.Wait(TimeSpan.FromSeconds(3));

            Driver.Instance.FindElement(By.Id("publish")).Click();
        }
    }
}

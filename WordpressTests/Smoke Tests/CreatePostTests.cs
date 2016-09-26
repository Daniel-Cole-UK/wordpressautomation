using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordpressAutomation;

namespace WordpressTests
{
    [TestClass]
    public class CreatePostTests : WordpressTest
    {
        [TestMethod]
        public void CanCreateBasicPost()
        {
            NewPostPage.GoTo();
            NewPostPage.CreatePost("This is the test post title").WithBody("This is the body text").Publish();

            NewPostPage.GoToNewPost();

            Assert.AreEqual(PostPage.Title, "This is the test post title", "Title did not match new post title");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordpressAutomation;
using WordpressAutomation.Workflows;

namespace WordpressTests
{
    [TestClass]
    public class CreatePostTests : WordpressTest
    {
        [TestMethod]
        public void CanCreateBasicPost()
        {
            // Create a post and check it appears
            NewPostPage.GoTo();
            NewPostPage.CreatePost("This is the test post title").WithBody("This is the body text").Publish();
            NewPostPage.GoToNewPost();
            Assert.AreEqual(PostPage.Title, "This is the test post title", "Title did not match new post title");

            // Navigate to posts page and remove created post
            Driver.GoBack(1);
            ListPostsPage.GoTo(PostType.Posts);
            ListPostsPage.RemovePost("This is the test post title");
        }
    }
}

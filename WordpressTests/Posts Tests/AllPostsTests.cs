using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordpressAutomation;
using WordpressAutomation.Workflows;

namespace WordpressTests
{
    [TestClass]
    // Derive from base class (initialize, login, cleanup)
    public class AllPostsTests : WordpressTest
    {
        [TestMethod]
        public void AddedPostsAppear()
        {
            // Go to posts, get post count, store
            ListPostsPage.GoTo(PostType.Posts);
            ListPostsPage.StoreCount();

            // Add a new post
            PostCreator.CreatePost();

            // Go to posts, get new post count
            ListPostsPage.GoTo(PostType.Posts);
            Assert.AreEqual(ListPostsPage.PreviousPostCount + 1, ListPostsPage.CurrentPostCount, "Count of posts did not increase");

            // Check for added post
            Assert.IsTrue(ListPostsPage.DoesPostExistWithTitle(PostCreator.PreviousTitle));

            // Remove created post
            PostCreator.Cleanup(PostCreator.Type.post);
        }

        [TestMethod]
        public void CanSearchPosts()
        {
            // Create a new post
            PostCreator.CreatePost();

            // Go to list posts
            ListPostsPage.GoTo(PostType.Posts);

            // Search for the post
            ListPostsPage.SearchForPost(PostCreator.PreviousTitle);

            // Check post appears in results
            Assert.IsTrue(ListPostsPage.DoesPostExistWithTitle(PostCreator.PreviousTitle));

            // Remove created post
            PostCreator.Cleanup(PostCreator.Type.post);
        }

        [TestMethod]
        public void CanViewPublishedPost()
        {
            // Create a new post
            PostCreator.CreatePost();

            // Go to list posts
            ListPostsPage.GoTo(PostType.Posts);

            // Select view link
            ListPostsPage.ViewPublishedPost(PostCreator.PreviousTitle);

            // Check published post is shown
            Assert.IsTrue(PublishedPostsPage.DoesPostExist(PostCreator.PreviousTitle, PostCreator.PreviousBody));

            // Remove created post
            Driver.GoBack(1);
            PostCreator.Cleanup(PostCreator.Type.post);
        }

        [TestMethod]
        public void CanQuickEditPost()
        {
            // Create a new post
            PostCreator.CreatePost();

            // Go to list posts
            ListPostsPage.GoTo(PostType.Posts);

            // Select quick edit link
            ListPostsPage.QuickEditPost(PostCreator.PreviousTitle);

            // Check quick edit mode is activated
            Assert.IsTrue(ListPostsPage.IsInQuickEdit(PostCreator.PreviousTitle));

            // Make an edit and check it updates
            ListPostsPage.MakeEdit();
            ListPostsPage.GoTo(PostType.Posts);
            Assert.IsTrue(ListPostsPage.DoesPostExistWithTitle(PostCreator.PreviousTitle));

            // Remove created post
            PostCreator.Cleanup(PostCreator.Type.post); ;
        }
    }
}

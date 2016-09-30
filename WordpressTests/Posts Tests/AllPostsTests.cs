using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordpressAutomation;

namespace WordpressTests
{
    [TestClass]
    public class AllPostsTests : WordpressTest
    {
        [TestMethod]
        public void AddedPostsAppear()
        {
            // Go to posts, get post count, store
            ListPostsPage.GoTo(PostType.Posts);
            ListPostsPage.StoreCount();

            // Add a new post
            NewPostPage.GoTo();
            NewPostPage.CreatePost("Added posts appear, title").WithBody("Added posts appear, body").Publish();

            // Go to posts, get new post count
            ListPostsPage.GoTo(PostType.Posts);
            Assert.AreEqual(ListPostsPage.PreviousPostCount + 1, ListPostsPage.CurrentPostCount, "Count of posts did not increase");

            // Check for added post
            Assert.IsTrue(ListPostsPage.DoesPostExistWithTitle("Added posts appear, title"));
            
            // Bin post (clean up)
            ListPostsPage.RemovePost("Added posts appear, title");
            Assert.AreEqual(ListPostsPage.PreviousPostCount, ListPostsPage.CurrentPostCount, "Couldn't remove post");
        }

        [TestMethod]
        public void CanSearchPosts()
        {
            // Create a new post
            NewPostPage.GoTo();
            NewPostPage.CreatePost("Searching posts, title").WithBody("Searching posts, body").Publish();

            // Go to list posts
            ListPostsPage.GoTo(PostType.Posts);

            // Search for the post
            ListPostsPage.SearchForPost("Searching posts, title");

            // Post appears in results
            Assert.IsTrue(ListPostsPage.DoesPostExistWithTitle("Searching posts, title"));

            // Bin post (clean up)
            ListPostsPage.RemovePost("Searching posts, title");
        }
    }
}

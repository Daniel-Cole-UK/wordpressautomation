using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordpressAutomation;
using WordpressAutomation.Workflows;

namespace WordpressTests
{
    [TestClass]
    public class CommentsTests : WordpressTest
    {
        [TestMethod]
        public void CanCreateAndDeleteComment()
        {
            // Go to comments, get comment count, store
            ListCommentsPage.GoTo();
            ListCommentsPage.StoreCount();

            // Create post
            PostCreator.CreatePost();

            // Navigate to post and leave a new comment
            ListPostsPage.GoTo(PostType.Posts);
            ListPostsPage.ViewPublishedPost(PostCreator.PreviousTitle);
            PublishedPostsPage.LeaveComment();

            // Check comment is visible on published post
            Assert.IsTrue(PublishedPostsPage.DoesCommentExist(PostCreator.PreviousComment));

            // Navigate to comments and check new comment appears
            Driver.GoBack(2);
            ListCommentsPage.GoTo();
            Assert.IsTrue(ListCommentsPage.DoesCommentExistWithText(PostCreator.PreviousComment));
            Assert.AreEqual(ListCommentsPage.PreviousCommentCount + 1, ListCommentsPage.CurrentCommentCount, "Count of comments did not increase");

            // Delete comment
            PostCreator.Cleanup(PostCreator.Type.comment);

            // Check comment is deleted
            Assert.AreNotEqual(ListCommentsPage.PreviousCommentCount + 1, ListCommentsPage.CurrentCommentCount, "Count of comments did not decrease");

            // Delete post
            ListPostsPage.GoTo(PostType.Posts);
            PostCreator.Cleanup(PostCreator.Type.post);
        }

        [TestMethod]
        public void CanSearchForComment()
        {
            // Create post
            PostCreator.CreatePost();

            // Navigate to post and leave a new comment
            ListPostsPage.GoTo(PostType.Posts);
            ListPostsPage.ViewPublishedPost(PostCreator.PreviousTitle);
            PublishedPostsPage.LeaveComment();

            // Navigate to comments
            Driver.GoBack(2);
            ListCommentsPage.GoTo();

            // Search for comment & check it appears
            ListCommentsPage.SearchForComment(PostCreator.PreviousComment);
            Assert.IsTrue(ListCommentsPage.DoesCommentExistWithText(PostCreator.PreviousComment));

            // Delete post
            ListPostsPage.GoTo(PostType.Posts);
            PostCreator.Cleanup(PostCreator.Type.post);
        }

        [TestMethod]
        public void CanQuickEditComment()
        {
            // Create post
            PostCreator.CreatePost();

            // Navigate to post and leave a new comment
            ListPostsPage.GoTo(PostType.Posts);
            ListPostsPage.ViewPublishedPost(PostCreator.PreviousTitle);
            PublishedPostsPage.LeaveComment();

            // Navigate to comments
            Driver.GoBack(2);
            ListCommentsPage.GoTo();

            // Select quick edit link
            ListCommentsPage.QuickEditComment();

            // Check quick edit mode is activated
            Assert.IsTrue(ListCommentsPage.IsInQuickEdit(PostCreator.PreviousComment));

            // Make an edit and check it updates
            ListCommentsPage.MakeEdit();
            ListCommentsPage.GoTo();
            Assert.IsTrue(ListCommentsPage.DoesCommentExistWithText(PostCreator.PreviousComment));

            // Remove created post
            ListPostsPage.GoTo(PostType.Posts);
            PostCreator.Cleanup(PostCreator.Type.post);
        }
    }
}

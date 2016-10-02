using System;
using System.Text;

namespace WordpressAutomation.Workflows

{
    public class PostCreator
    {
        // Properties to store strings used for body, title or comment
        public static string PreviousBody { get; set; }
        public static string PreviousTitle { get; set; }
        public static string PreviousComment { get; set; }

        // returns true if a stored post exists
        public static bool CreatedAPost
        {
            get { return !String.IsNullOrEmpty(PreviousTitle); }
        }

        // returns true if a stored comment exists
        public static bool CreatedAComment
        {
            get { return !String.IsNullOrEmpty(PreviousComment); }
        }

        // Update the stored title string
        public static void UpdateTitle (string newTitle)
        {
            PreviousTitle = newTitle;
        }
        // Update the stored comment string
        public static void UpdateComment(string newComment)
        {
            PreviousComment = newComment;
        }

        // Generate title and body strings then create a post using them
        public static void CreatePost()
        {
            NewPostPage.GoTo();

            PreviousTitle = CreateString("title");
            PreviousBody = CreateString("body");

            NewPostPage.CreatePost(PreviousTitle).WithBody(PreviousBody).Publish();
        }

        // Create a comment string
        public static string CreateComment()
        {
            PreviousComment = CreateString("comment");
            return PreviousComment;
        }

        // Remove any stored strings
        public static void Initialize()
        {
            PreviousTitle = null;
            PreviousBody = null;
            PreviousComment = null;
        }

        // Remove comment or post
        public static void Cleanup(Type type)
        {
            if (type == Type.post)
            {
                if (CreatedAPost)
                {
                    RemovePost();
                    Driver.Wait(TimeSpan.FromSeconds(3));
                }
            }
            if (type == Type.comment)
            {
                if (CreatedAComment)
                {
                    RemoveComment();
                    Driver.Wait(TimeSpan.FromSeconds(3));
                }
            }
        }

        // Remove the last stored post
        private static void RemovePost()
        {
            ListPostsPage.RemovePost(PreviousTitle);
            Initialize();
        }

        // Remove the last stored comment
        private static void RemoveComment()
        {
            ListCommentsPage.RemoveComment(PreviousComment);
            PreviousComment = null;
        }

        // Create a random string and append the provided type string
        private static string CreateString(string type)
        {
            return CreateRandomString() + ", " + type;
        }

        // Build a string by randomly selecting words and connectors and random amount of times
        private static string CreateRandomString()
        {
            var str = new StringBuilder();
            var rand = new Random();
            var cycle = rand.Next(1, 10);

            for (int i = 0; i < cycle; i++)
            {
                str.Append(Words[rand.Next(Words.Length)]);
                str.Append(" ");
                str.Append(Connectors[rand.Next(Connectors.Length)]);
                str.Append(" ");
                str.Append(Words[rand.Next(Words.Length)]);
                str.Append(" ");
            }

            return str.ToString();
        }

        // Array of words to be randomly selected from
        private static string[] Words = new[]
                                        {
                                            "fan", "weather", "table", "movie", "table", "praise", "burger", "sauce", "hilarious"
                                        };
        // Array of connective words to be selected from
        private static string[] Connectors = new[]
                                {
                                            "the", "an", "and", "a", "of", "to", "it", "as"
                                        };
        // Enumeration for post, page and comment constants
        public enum Type
        {
            post, page, comment
        }
    }
}

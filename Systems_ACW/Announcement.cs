using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class Announcement
    {
        private string title;
        private string body;
        private User poster;
        private DateTime dateTimePosted;
        private DateTime timeSinceLastComment;
        private List<Comment> comments;

        public User Poster { get { return poster; } }
        public string Title { get { return title; } }

        public string Body { get { return body; } }
        public List<Comment> Comments { get { return comments; } }
        public Announcement(string pTitle, string pBody, User pPoster)
        {
            title = pTitle;
            body = pBody;
            poster = pPoster;
            timeSinceLastComment = DateTime.Now;
            comments = new List<Comment>();
        }

        public bool addComment(string pBody, User pUser)
        {
            try
            {
                Comment newComment = new Comment(pBody, pUser);
                comments.Add(newComment);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class Announcement
    {
        string title;
        string body;
        DateTime timeSinceLastComment;
        List<Comment> comments;

        public string Title { get { return title; } }
        public List<Comment> Comments { get { return comments; } }
        public Announcement(string pTitle, string pBody)
        {
            title = pTitle;
            body = pBody;
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

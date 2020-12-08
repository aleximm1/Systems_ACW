using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class Comment
    {
        private User poster;
        private string body;
        private int announcementCommentedOnID;
        private DateTime datePosted;

        public string Body { get { return body; } }
        public User Poster { get { return poster; } }
        public DateTime DatePosted { get { return datePosted; } }

        public Comment(string pBody, User pPoster, Announcement pAnnouncement)
        {
            body = pBody;
            poster = pPoster;
            announcementCommentedOnID = pAnnouncement.ID;
        }

        public Comment(string pBody, User pPoster, Announcement pAnnouncement, DateTime pDatePosted)
        {
            body = pBody;
            poster = pPoster;
            announcementCommentedOnID = pAnnouncement.ID;
            datePosted = pDatePosted;
        }
    }
}

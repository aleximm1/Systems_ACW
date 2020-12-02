using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Systems_ACW
{
    public class Announcement
    {
        private int id;
        private string title;
        private string body;
        private User poster;
        private DateTime dateTimePosted;
        private DateTime timeSinceLastComment;
        private List<Comment> comments;

        public User Poster { get { return poster; } }
        public string Title { get { return title; } }
        public int ID { get { return id; } }
        public string Body { get { return body; } }
        public List<Comment> Comments { get { return comments; } }
        public Announcement(string pTitle, string pBody, User pPoster)
        {
            int lastID = 0;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(".\\XML_Files\\Announcements.xml");
            foreach (XmlNode node in xDoc.DocumentElement)
            {
                string announcementIdString = node.Attributes[0].InnerText;
                try
                {
                    lastID = Convert.ToInt32(announcementIdString);
                }
                catch
                {
                }
            }
            id = lastID++;
            title = pTitle;
            body = pBody;
            poster = pPoster;
            timeSinceLastComment = DateTime.Now;
            comments = new List<Comment>();
        }

        public Announcement(int pId, string pTitle, string pBody, int pPosterID, DateTime pDateTime)
        {
            int lastID = 0;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(".\\XML_Files\\Announcements.xml");
            foreach (XmlNode node in xDoc.DocumentElement)
            {
                string announcementIdString = node.Attributes[0].InnerText;
                try
                {
                    lastID = Convert.ToInt32(announcementIdString);
                }
                catch
                {
                }
            }
            id = pId;
            title = pTitle;
            body = pBody;
            poster = FindUser(pPosterID);
            timeSinceLastComment = DateTime.Now;
            comments = new List<Comment>();
        }

        public bool addComment(string pBody, User pUser)
        {
            try
            {
                Comment newComment = new Comment(pBody, pUser, this);
                comments.Add(newComment);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private User FindUser(int pUserId)
        {
            User dummyUser = new User("test", "test", "Student");
            return dummyUser;
        }
    }
}

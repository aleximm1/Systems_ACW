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
        private DateTime timeOfLastComment;
        private List<Comment> comments;

        public User Poster { get { return poster; } }
        public string Title { get { return title; } }
        public int ID { get { return id; } }
        public string Body { get { return body; } }
        public DateTime DateTimePosted { get { return dateTimePosted; } }
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
            dateTimePosted = DateTime.Now;
            timeOfLastComment = DateTime.Now;
            comments = new List<Comment>();
        }

        public Announcement(string pTitle, string pBody, int pPosterID, DateTime pDateTime)
        {
            int highestID = 0;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(".\\XML_Files\\Announcements.xml");
            int announcementId = 0;
            foreach (XmlNode node in xDoc.DocumentElement)
            {
                string announcementIdString = node.Attributes[0].InnerText;
                try
                {
                    announcementId = Convert.ToInt32(announcementIdString);
                    if (announcementId>highestID)
                    {
                        highestID = announcementId;
                    }
                }
                catch
                {

                }
            }
            id = highestID+1;
            title = pTitle;
            body = pBody;
            poster = FindUser(pPosterID);
            dateTimePosted = pDateTime;
            comments = new List<Comment>();
        }

        public Announcement(int pId, string pTitle, string pBody, int pPosterID, DateTime pDateTime)
        {
            id = pId;
            title = pTitle;
            body = pBody;
            poster = FindUser(pPosterID);
            dateTimePosted = pDateTime;
            comments = new List<Comment>();
        }

        public bool AddComment(Comment pComment)
        {
            try
            {
                comments.Add(pComment);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private User FindUser(int pUserId)
        {
            string name = null;
            string accessLevel = null;
            XmlDocument usersDoc = new XmlDocument();
            usersDoc.Load("XML_Files\\Users.xml");
            foreach (XmlNode node in usersDoc.DocumentElement)
            {
                if (node.Attributes["id"].Value == pUserId.ToString())
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "name")
                        {
                            name = childNode.InnerText;
                        }
                        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "accessLevel")
                        {
                            accessLevel = childNode.InnerText;
                            break;
                        }
                    }
                }
            }
            User user = new User(pUserId, name, accessLevel);
            return user;
        }
    }
}

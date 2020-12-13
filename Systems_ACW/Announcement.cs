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
        private int moduleId;
        private string title;
        private string body;
        private User poster;
        private DateTime dateTimePosted;
        private DateTime timeOfLastComment;
        private List<Comment> comments;

        public User Poster { get { return poster; } }
        public string Title { get { return title; } }
        public int ID { get { return id; } }
        public int ModuleID { get { return moduleId; } }
        public string Body { get { return body; } }
        public DateTime DateTimePosted { get { return dateTimePosted; } }
        public List<Comment> Comments { get { return comments; } }

        //Constructor for when a new announcement is being added
        public Announcement(string pTitle, string pBody, int pPosterID, int pModuleID, DateTime pDateTime)
        {
            int highestID = 0;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("..\\..\\XML_Files\\Announcements.xml");
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
            moduleId = pModuleID;
            dateTimePosted = pDateTime;
            comments = new List<Comment>();
        }

        //Constructor for when an existing announcement is loaded from the Announcements.xml file
        public Announcement(int pId, string pTitle, string pBody, int pPosterID, int pModuleId, DateTime pDateTime)
        {
            id = pId;
            title = pTitle;
            body = pBody;
            poster = FindUser(pPosterID);
            moduleId = pModuleId;
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
            usersDoc.Load("..\\..\\XML_Files\\Users.xml");
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

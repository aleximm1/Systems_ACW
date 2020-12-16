using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Systems_ACW
{
    public class Comment
    {
        private int id;
        private User poster;
        private string body;
        private int announcementCommentedOnID;
        private DateTime datePosted;

        public int ID { get { return id; } }
        public string Body { get { return body; } }
        public User Poster { get { return poster; } }
        public DateTime DatePosted { get { return datePosted; } }

        public Comment(string pBody, User pPoster, Announcement pAnnouncement)
        {
            int highestID = 0;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("..\\..\\XML_Files\\comments.xml");
            int commentId = 0;
            foreach (XmlNode node in xDoc.DocumentElement)
            {
                string commentIdString = node.Attributes[0].InnerText;
                try
                {
                    commentId = Convert.ToInt32(commentIdString);
                    if (commentId > highestID)
                    {
                        highestID = commentId;
                    }
                }
                catch
                {

                }
            }
            id = highestID + 1;
            body = pBody;
            poster = pPoster;
            announcementCommentedOnID = pAnnouncement.ID;
            datePosted = DateTime.Now;
            SaveComment();
        }

        public Comment(int pID, string pBody, User pPoster, int pAnnouncementID, DateTime pDatePosted)
        {
            id = pID;
            body = pBody;
            poster = pPoster;
            announcementCommentedOnID = pAnnouncementID;
            datePosted = pDatePosted;
        }

        private void SaveComment()
        {
            XmlDocument commentsDoc = new XmlDocument();
            commentsDoc.Load("..\\..\\XML_Files\\Comments.xml");
            XmlElement root = commentsDoc.DocumentElement;
            XmlElement commentElem = commentsDoc.CreateElement("comment");
            commentElem.SetAttribute("id", id.ToString());
            XmlElement bodyElem = commentsDoc.CreateElement("body");
            bodyElem.InnerText = body;
            XmlElement posterIdElem = commentsDoc.CreateElement("posterId");
            posterIdElem.InnerText = Poster.Id.ToString();
            XmlElement dateTimePostedElem = commentsDoc.CreateElement("datePosted");
            dateTimePostedElem.InnerText = datePosted.ToString();
            commentElem.AppendChild(bodyElem);
            commentElem.AppendChild(posterIdElem);
            commentElem.AppendChild(dateTimePostedElem);
            root.AppendChild(commentElem);
            commentsDoc.Save("..\\..\\XML_Files\\Comments.xml");
            XmlDocument announcementsDoc = new XmlDocument();
            announcementsDoc.Load("..\\..\\XML_Files\\Announcements.xml");
            XmlElement root2 = announcementsDoc.DocumentElement;
            XmlNodeList announcementsNodeList = announcementsDoc.GetElementsByTagName("announcement");
            XmlNode announcementNode = announcementsNodeList[0];
            foreach (XmlNode node in announcementsNodeList)
            {
                if(node.Attributes[0].InnerText == announcementCommentedOnID.ToString())
                {
                    announcementNode = node;
                    break;
                }
            }
            XmlNode commentsElem = announcementNode.LastChild;
            XmlElement commentIdElem = announcementsDoc.CreateElement("commentID");
            commentIdElem.InnerText = id.ToString();
            commentsElem.AppendChild(commentIdElem);
            announcementNode.AppendChild(commentsElem);
            root2.AppendChild(announcementNode);
            announcementsDoc.Save("..\\..\\XML_Files\\Announcements.xml");
        }
    }
}

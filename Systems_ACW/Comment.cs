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
            commentsDoc.Load("XML_Files\\Comments.xml");
            XmlElement root = commentsDoc.DocumentElement;
            XmlElement commentElem = commentsDoc.CreateElement("comment");
            commentElem.SetAttribute("id", id.ToString());
            XmlElement bodyElem = commentsDoc.CreateElement("body");
            bodyElem.InnerText = body;
            XmlElement posterIdElem = commentsDoc.CreateElement("posterID");
            posterIdElem.InnerText = Poster.Id.ToString();
            XmlElement dateTimePostedElem = commentsDoc.CreateElement("datePosted");
            dateTimePostedElem.InnerText = datePosted.ToString();
            commentElem.AppendChild(bodyElem);
            commentElem.AppendChild(posterIdElem);
            commentElem.AppendChild(dateTimePostedElem);
            root.AppendChild(commentElem);
            commentsDoc.Save("..\\..\\XML_Files\\Comments.xml");
            XmlDocument announcementsDoc = new XmlDocument();
            root = announcementsDoc.DocumentElement;
            announcementsDoc.Load("XML_Files\\Announcements.xml");
            //XmlElement announcementElem = announcementsDoc.GetElementById(announcementCommentedOnID.ToString());
            XmlNodeList announcementsNodeList = announcementsDoc.GetElementsByTagName("announcement");
            XmlNode announcementNode = announcementsDoc.CreateNode("Element","fillerNode", "");
            foreach (XmlNode node in announcementsNodeList)
            {
                if(node.Attributes[0].InnerText == announcementCommentedOnID.ToString())
                {
                    announcementNode = node;
                    break;
                }
            }
            XmlNodeList commentsNodeList = announcementNode.GetElementsByTagName("comments");
            XmlNode commentsElem = commentsNodeList[0];
            XmlElement commentIdElem = announcementsDoc.CreateElement("commentID");
            commentIdElem.InnerText = id.ToString();
            commentsElem.AppendChild(commentIdElem);
            announcementNode.AppendChild(commentsElem);
            root.AppendChild(announcementNode);
            announcementsDoc.Save("..\\..\\XML_Files\\Announcements.xml");
        }
    }
}

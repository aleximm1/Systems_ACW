﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Systems_ACW
{
    public class Module
    {
        List<User> members;
        List<Announcement> announcements;
        string name;
        int id;

        public string Name { get { return name; } }
        public int ID { get { return id; } }
        public List<Announcement> Announcements { get { return announcements; } }
        public Module(string pName, int pID)
        {
            name = pName;
            id = pID;
            announcements = new List<Announcement>();
        }

        public void AddPerson(User pUser)
        {
            members.Add(pUser);
        }

        public void RemoveStudent(User pUser)
        {
            members.Remove(pUser);
        }

        public void LoadAnnouncement(Announcement pAnnouncement)
        {
            announcements.Add(pAnnouncement);
        }
        public void ResetAnnouncements()
        {
            announcements = new List<Announcement>();
        }

        public void SaveAnnouncement(Announcement pAnnouncement)
        {
            XmlDocument announcementsDoc = new XmlDocument();
            announcementsDoc.Load("..\\..\\XML_Files\\Announcements.xml");
            XmlElement root = announcementsDoc.DocumentElement;
            XmlElement announcementElem = announcementsDoc.CreateElement("announcement");
            announcementElem.SetAttribute("id", pAnnouncement.ID.ToString());
            announcementElem.SetAttribute("moduleId", id.ToString());
            XmlElement titleElem = announcementsDoc.CreateElement("title");
            titleElem.InnerText = pAnnouncement.Title;
            XmlElement bodyElem = announcementsDoc.CreateElement("body");
            bodyElem.InnerText = pAnnouncement.Body;
            XmlElement posterIdElem = announcementsDoc.CreateElement("posterID");
            posterIdElem.InnerText = pAnnouncement.Poster.Id.ToString();
            XmlElement dateTimePostedElem = announcementsDoc.CreateElement("dateTimePosted");
            dateTimePostedElem.InnerText = pAnnouncement.DateTimePosted.ToString();
            XmlElement commentsElem = announcementsDoc.CreateElement("comments");
            announcementElem.AppendChild(titleElem);
            announcementElem.AppendChild(bodyElem);
            announcementElem.AppendChild(posterIdElem);
            announcementElem.AppendChild(dateTimePostedElem);
            announcementElem.AppendChild(commentsElem);
            root.AppendChild(announcementElem);
            announcementsDoc.Save("..\\..\\XML_Files\\Announcements.xml");
        }
    }
}

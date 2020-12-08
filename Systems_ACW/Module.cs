using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class Module
    {
        List<User> members;
        List<Announcement> announcements;
        string name;
        int id;

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

        public bool addAnnouncement(string pTitle, string pBody, User user)
        {
            try
            {
                Announcement newAnnouncement = new Announcement(pTitle, pBody, user);
                announcements.Add(newAnnouncement);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void loadAnnouncement(Announcement pAnnouncement)
        {
            announcements.Add(pAnnouncement);
        }
    }
}

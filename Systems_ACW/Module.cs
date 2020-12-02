using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class Module
    {
        List<Student> students;
        List<Teacher> teachers;
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

        public void AddPerson(Student student)
        {
            students.Add(student);
        }

        public void AddPerson(Teacher teacher) 
        {
            teachers.Add(teacher);
        }

        public void RemoveStudent(Student student)
        {
            students.Remove(student);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    class Module
    {
        List<Student> students;
        List<Teacher> teachers;
        List<Announcement> announcements;
        string name;
        int id;
        
        public Module(string pName, int pID)
        {
            name = pName;
            id = pID;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class Student
    {
        string name;
        int id;
        private string password;
        private string accessLevel;
        private Module[] modules;

        public Student(string UName, string pPassword)
        {
            name = UName;
            password = pPassword;
            accessLevel = "Student";
            Module aiModule = new Module("AI", 00051);
            Module twoDGraphicsModule = new Module("2D Graphics", 00052);
            Module computerSystemsModule = new Module("Computer Systems", 00053);
            modules[0] = aiModule;
            modules[1] = twoDGraphicsModule;
            modules[2] = computerSystemsModule;
        }
    }
}

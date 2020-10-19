using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    class Student : User
    {
        string name;
        int ID;

        public Student(string pName, int pID)
        {
            name = pName;
            ID = pID;
        }
    }
}

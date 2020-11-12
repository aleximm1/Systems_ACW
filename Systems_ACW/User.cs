using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    class User
    {
        string name;
        int id;
        private string password;
        private string accessLevel;

        public string Name { get { return name; } }
        public int Id { get { return id; } }
        public string AccessLevel { get { return accessLevel; } }

        public User(string pName, string pPassword, string pAccessLevel)
        {
            name = pName;
            password = pPassword;
            accessLevel = pAccessLevel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class User
    {
        private string name;
        private int id;
        private string password;
        private string accessLevel;
        private List<Module> modules = new List<Module>();

        public string Name { get { return name; } }
        public int Id { get { return id; } }
        public string AccessLevel { get { return accessLevel; } }
        public List<Module> Modules { get { return modules; } }

        public User(string pName, string pPassword, string pAccessLevel)
        {
            name = pName;
            password = pPassword;
            accessLevel = pAccessLevel;
            Module aiModule = new Module("AI", 00051);
            Module twoDGraphicsModule = new Module("2D Graphics", 00052);
            Module computerSystemsModule = new Module("Computer Systems", 00053);
            modules.Add(aiModule);
            modules.Add(twoDGraphicsModule);
            modules.Add(computerSystemsModule);
        }
    }
}

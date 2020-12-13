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

        public User(string pId, string pAccessLevel)
        {
            try
            {
                id = Convert.ToInt32(pId);
            }
            catch
            {

            }
            accessLevel = pAccessLevel;
            Module aiModule = new Module("AI", 50051);
            Module twoDGraphicsModule = new Module("2D Graphics", 50052);
            Module computerSystemsModule = new Module("Computer Systems", 50053);
            modules.Add(aiModule);
            modules.Add(twoDGraphicsModule);
            modules.Add(computerSystemsModule);
        }

        public User(int pId, string pName, string pAccessLevel)
        {
            id = pId;
            name = pName;
            accessLevel = pAccessLevel;
            Module aiModule = new Module("AI", 50051);
            Module twoDGraphicsModule = new Module("2D Graphics", 50052);
            Module computerSystemsModule = new Module("Computer Systems", 50053);
            modules.Add(aiModule);
            modules.Add(twoDGraphicsModule);
            modules.Add(computerSystemsModule);
        }

        public List<Module> LoadUserModules()
        {
            List<Module> loadedModules = new List<Module>();

            return loadedModules;
        }
    }
}

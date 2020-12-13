using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Systems_ACW
{
    /// <summary>
    /// Interaction logic for MessagesWindow.xaml
    /// </summary>
    public partial class MessagesWindow : Window
    {
        private User currentUser;
        private User currentlySelectedUser;
        public MessagesWindow(User pCurrentUser)
        {
            InitializeComponent();
            currentUser = pCurrentUser;
            List<User> users = GetUsers();
            currentlySelectedUser = users[0];
            for (int i = 0; i < users.Count(); i++)
            {
                UsersBox.Items.Add(users[i]);
            }
            ChangeSelectedUser();
        }

        private List<User> GetUsers()
        {
            List<User> usersList = new List<User>();
            int userId = 0;
            XmlDocument usersDoc = new XmlDocument();
            usersDoc.Load("..\\..\\XML_Files\\Users.xml");
            foreach (XmlNode node in usersDoc.DocumentElement)
            {
                string userIdString = node.Attributes[0].InnerText;
                try
                {
                    userId = Convert.ToInt32(userIdString);
                }
                catch {}
                usersList.Add(new User(userId ,node.FirstChild.InnerText, "null"));
            }
            return usersList;
        }

        private void ChangeSelectedUser()
        {
            MessagesBox.Items.Clear();
            XmlDocument dmsDoc = new XmlDocument();
            dmsDoc.Load("..\\..\\XML_Files\\DMs.xml");
            XmlNode currentDmNode = dmsDoc.DocumentElement;
            bool dmFound = false;
            foreach (XmlNode dmNode in dmsDoc.DocumentElement)
            {
                if ((dmNode.Attributes[0].InnerText == currentUser.Id.ToString() || dmNode.Attributes[1].InnerText == currentUser.Id.ToString()) && (dmNode.Attributes[0].InnerText == currentlySelectedUser.Id.ToString() || dmNode.Attributes[1].InnerText == currentlySelectedUser.Id.ToString()))
                {
                    dmFound = true;
                    currentDmNode = dmNode;
                }
                else
                {
                    continue;
                }
            }
            if (!dmFound)
            {
                CreateDM();
                dmsDoc.Load("..\\..\\XML_Files\\DMs.xml");
                foreach (XmlNode dmNode in dmsDoc.DocumentElement)
                {
                    if ((dmNode.Attributes[0].InnerText == currentUser.Id.ToString() || dmNode.Attributes[1].InnerText == currentUser.Id.ToString()) && (dmNode.Attributes[0].InnerText == currentlySelectedUser.Id.ToString() || dmNode.Attributes[1].InnerText == currentlySelectedUser.Id.ToString()))
                    {
                        currentDmNode = dmNode;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            foreach(XmlNode messageNode in currentDmNode.ChildNodes)
            {
                Message message = new Message(messageNode.Attributes[0].InnerText, messageNode.FirstChild.InnerText, Convert.ToDateTime(messageNode.LastChild.InnerText));
                MessagesBox.Items.Add(message);
            }
        }

        private void UsersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentlySelectedUser = (User)UsersBox.SelectedItem;
            ChangeSelectedUser();
        }

        private void CreateDM()
        {
            XmlDocument dmsDoc = new XmlDocument();
            dmsDoc.Load("..\\..\\XML_Files\\DMs.xml");
            XmlElement root = dmsDoc.DocumentElement;
            XmlElement DMElem = dmsDoc.CreateElement("DM");
            DMElem.SetAttribute("id1", currentUser.Id.ToString());
            DMElem.SetAttribute("id2", currentlySelectedUser.Id.ToString());
            root.AppendChild(DMElem);
            dmsDoc.Save("..\\..\\XML_Files\\DMs.xml");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

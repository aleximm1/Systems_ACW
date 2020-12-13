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
            
        }

        private void UsersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentlySelectedUser = (User)UsersBox.SelectedItem;
            ChangeSelectedUser();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

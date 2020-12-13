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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Systems_ACW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User currentUser;
        public MainWindow()
        {
            InitializeComponent();
            bool logInFailed = false;
            Visibility = Visibility.Hidden;
            logInFailed = attemptLogIn();
            while(!logInFailed)
            {
                logInFailed = attemptLogIn();
            }
            Visibility = Visibility.Visible;
            if (currentUser.AccessLevel == "Admin")
            {
                AdminMenuButton.Visibility = Visibility.Visible;
                AdminMenuButton.IsEnabled = true;
            }
            else
            {
                AdminMenuButton.Visibility = Visibility.Hidden;
                AdminMenuButton.IsEnabled = false;
            }
        }

        private bool attemptLogIn()
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.ShowDialog();
            //This is where the database check for login happens.
            string name;
            string accessLevel = null;
            bool userFound = false;
            XmlDocument usersDoc = new XmlDocument();
            usersDoc.Load("XML_Files\\Users.xml");
            foreach (XmlNode node in usersDoc.DocumentElement)
            {
                if (node.Attributes["id"].Value == logInWindow.StudentIdTextbox.Text)
                {
                    userFound = true;
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "name")
                        {
                            name = childNode.InnerText;
                        }
                        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "password")
                        {
                            if (childNode.InnerText != logInWindow.PasswordTextbox.Password)
                            {
                                MessageBox.Show("Incorrect Password");
                                return false;
                            }
                        }
                        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "accessLevel")
                        {
                            accessLevel = childNode.InnerText;
                            break;
                        }
                    }
                }
            }
            if (userFound)
            {
                currentUser = new User(logInWindow.StudentIdTextbox.Text, accessLevel);
                return true;
            }
            MessageBox.Show("Student ID not found");
            return false;
            
        }

        private void Module1Button_Click(object sender, RoutedEventArgs e)
        {
            int moduleID = currentUser.Modules[0].ID;
            AnnouncementsWindow announcementsWindow = new AnnouncementsWindow(currentUser, currentUser.Modules[0]);
            Visibility = Visibility.Hidden;
            announcementsWindow.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void Module2Button_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementsWindow announcementsWindow = new AnnouncementsWindow(currentUser, currentUser.Modules[1]);
            Visibility = Visibility.Hidden;
            announcementsWindow.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void Module3Button_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementsWindow announcementsWindow = new AnnouncementsWindow(currentUser, currentUser.Modules[2]);
            Visibility = Visibility.Hidden;
            announcementsWindow.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void AdminMenuButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuWindow adminMenuWindow = new AdminMenuWindow(currentUser);
            Visibility = Visibility.Hidden;
            adminMenuWindow.ShowDialog();
            Visibility = Visibility.Visible;
        }

        

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            bool logInFailed = false;
            Visibility = Visibility.Hidden;
            logInFailed = attemptLogIn();
            while (!logInFailed)
            {
                logInFailed = attemptLogIn();
            }
            Visibility = Visibility.Visible;
            if (currentUser.AccessLevel == "Admin")
            {
                AdminMenuButton.Visibility = Visibility.Visible;
                AdminMenuButton.IsEnabled = true;
            }
            else
            {
                AdminMenuButton.Visibility = Visibility.Hidden;
                AdminMenuButton.IsEnabled = false;
            }
        }

        private void CheckMessagesButton_Click(object sender, RoutedEventArgs e)
        {
            MessagesWindow messagesWindow = new MessagesWindow(currentUser);
            Visibility = Visibility.Hidden;
            messagesWindow.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}

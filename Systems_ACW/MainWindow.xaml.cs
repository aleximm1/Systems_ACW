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
        }

        private bool attemptLogIn()
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.ShowDialog();
            //This is where the database check for login happens.
            if (logInWindow.UsernameTextbox.Text == "Username" && logInWindow.PasswordTextbox.Text == "Password")
            {
                currentUser = new User(logInWindow.UsernameTextbox.Text, logInWindow.PasswordTextbox.Text, "student");
                return true;
            } 
            else
            {
                return false;
            }
        }

        private void Module1Button_Click(object sender, RoutedEventArgs e)
        {
            int moduleID = currentUser.Modules[0].ID;
            //currentUser.Modules[0].addAnnouncement("Title", "Body.");
            //currentUser.Modules[0].addAnnouncement("Title2", "This is the body of the second announcement.");
            //currentUser.Modules[0].addAnnouncement("Title3", "Body of announcement number 3.");
            //currentUser.Modules[0].Announcements[0].addComment("This is a test comment", currentUser);
            //currentUser.Modules[0].Announcements[0].addComment("This is also a test comment", currentUser);
            //currentUser.Modules[0].Announcements[2].addComment("This is also a test commentaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", currentUser);
            //currentUser.Modules[0].Announcements[0].addComment("This is also a test comment", currentUser);
            //currentUser.Modules[0].Announcements[0].addComment("This is also a test comment", currentUser);
            //currentUser.Modules[0].Announcements[1].addComment("This is also a test comment", currentUser);
            //currentUser.Modules[0].Announcements[2].addComment("This is also a test comment", currentUser);
            LoadModulesAnnouncements(currentUser.Modules[0]);
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

        private void LoadModulesAnnouncements(Module pModule)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("XML_Files\\Announcements.xml");
            int announcementId = 50;
            string announcementTitle = null;
            string announcementBody = null;
            int posterId = 50;
            DateTime dateTimePosted = DateTime.Now;
            List<Comment> announcementsComments;
            foreach(XmlNode node in xDoc.DocumentElement)
            {
                string announcementIdString = node.Attributes[0].InnerText;
                try
                {
                    announcementId = Convert.ToInt32(announcementIdString);
                } catch
                {
                    MessageBox.Show("Announcement ID couldn't be converted to int");
                }
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "title")
                    {
                        announcementTitle = childNode.InnerText;
                    }
                    if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "body")
                    {
                        announcementBody = childNode.InnerText;
                    }
                    if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "posterID")
                    {
                        try
                        {
                            posterId = Convert.ToInt32(childNode.InnerText);
                        }
                        catch
                        {
                            MessageBox.Show("Poster ID couldn't be converted to int");
                        }
                    }
                    if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "dateTimePosted")
                    {
                        try
                        {
                            dateTimePosted = Convert.ToDateTime(childNode.InnerText);
                        }
                        catch
                        {
                            MessageBox.Show("DateTime Posted couldn't be converted to DateTime");
                        }
                    }
                    if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Comments")
                    {
                        LoadAnnouncementComments(announcementId);
                    }
                }   
                Announcement loadedAnnouncement = new Announcement(announcementId, announcementTitle, announcementBody, posterId, dateTimePosted);
                pModule.loadAnnouncement(loadedAnnouncement);
            }
        }

        private void LoadAnnouncementComments(int pAnnouncementId)
        {
            
        }
    }
}

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

        private void ModulesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Module1Button_Click(object sender, RoutedEventArgs e)
        {
            currentUser.Modules[0].addAnnouncement("Title", "Body.");
            currentUser.Modules[0].addAnnouncement("Title2", "Body2.");
            currentUser.Modules[0].addAnnouncement("Title3", "Body3.");
            currentUser.Modules[0].Announcements[0].addComment("This is a test comment", currentUser);
            currentUser.Modules[0].Announcements[0].addComment("This is also a test comment", currentUser);
            currentUser.Modules[0].Announcements[2].addComment("This is also a test commentaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", currentUser);
            currentUser.Modules[0].Announcements[0].addComment("This is also a test comment", currentUser);
            currentUser.Modules[0].Announcements[0].addComment("This is also a test comment", currentUser);
            currentUser.Modules[0].Announcements[1].addComment("This is also a test comment", currentUser);
            currentUser.Modules[0].Announcements[2].addComment("This is also a test comment", currentUser);
            AnnouncementsWindow announcementsWindow = new AnnouncementsWindow(currentUser, currentUser.Modules[0]);
            Visibility = Visibility.Hidden;
            announcementsWindow.ShowDialog();
        }

        private void Module2Button_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementsWindow announcementsWindow = new AnnouncementsWindow(currentUser, currentUser.Modules[1]);
            Visibility = Visibility.Hidden;
            announcementsWindow.ShowDialog();
        }

        private void Module3Button_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementsWindow announcementsWindow = new AnnouncementsWindow(currentUser, currentUser.Modules[2]);
            Visibility = Visibility.Hidden;
            announcementsWindow.ShowDialog();
        }
    }
}

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

namespace Systems_ACW
{
    /// <summary>
    /// Interaction logic for NewAnnouncementWindow.xaml
    /// </summary>
    public partial class NewAnnouncementWindow : Window
    {
        User currentUser;
        Module currentModule;
        public Announcement announcement;
        public NewAnnouncementWindow(User pCurrentUser, Module pCurrentModule)
        {
            InitializeComponent();
            currentUser = pCurrentUser;
            currentModule = pCurrentModule;
            PostToTextbox.Text = "Post announcement to module: " + currentModule.Name;
        }

        private void PostAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            announcement = new Announcement(TitleTextbox.Text, BodyTextbox.Text, currentUser.Id, currentModule.ID, DateTime.Now);
            this.DialogResult = true;
        }
    }
}

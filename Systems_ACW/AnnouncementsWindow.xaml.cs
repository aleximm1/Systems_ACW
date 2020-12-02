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
    /// Interaction logic for AnnouncementsWindow.xaml
    /// </summary>
    public partial class AnnouncementsWindow : Window
    {
        public AnnouncementsWindow(User currentUser, Module currentModule)
        {
            InitializeComponent();
            Announcement selectedAnnouncement = currentModule.Announcements[0];
            for (int i = 0; i < currentModule.Announcements.Count(); i++)
            {
                AnnouncementsBox.Items.Add(currentModule.Announcements[i]);
                FillCommentsBox(selectedAnnouncement);
            }
        }

        private void FillCommentsBox(Announcement pAnnouncement)
        {
            CommentsBox.Items.Clear();
            for (int j = 0; j < pAnnouncement.Comments.Count(); j++)
            {
                CommentsBox.Items.Add(pAnnouncement.Comments[j]);
            }
        }

        private void ChangeSelectedAnnouncement(Announcement pAnnouncement)
        {
            PosterTextBlock.Text = pAnnouncement.Poster.Name;
            TitleTextBlock.Text = pAnnouncement.Title;
            BodyTextBlock.Text = pAnnouncement.Body;
        }

        private void AnnouncementsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Announcement selectedAnnouncement = (Announcement)AnnouncementsBox.SelectedItem;
            ChangeSelectedAnnouncement(selectedAnnouncement);
            FillCommentsBox(selectedAnnouncement);
        }

        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

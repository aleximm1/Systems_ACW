﻿using System;
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
        private User currentUser;
        private Module currentModule;
        private Announcement currentlySelectedAnnouncement;
        public AnnouncementsWindow(User pCurrentUser, Module pCurrentModule)
        {
            InitializeComponent();
            currentUser = pCurrentUser;
            currentModule = pCurrentModule;
            currentlySelectedAnnouncement = currentModule.Announcements[0];
            for (int i = 0; i < currentModule.Announcements.Count(); i++)
            {
                if (currentModule.Announcements[i].ModuleID == currentModule.ID)
                {
                    AnnouncementsBox.Items.Add(currentModule.Announcements[i]);
                }
                ChangeSelectedAnnouncement(currentlySelectedAnnouncement);
                FillCommentsBox(currentlySelectedAnnouncement);
            }
            if (currentUser.AccessLevel == "Teacher" || currentUser.AccessLevel == "Admin")
            {
                NewAnnouncementButton.Visibility = Visibility.Visible;
                NewAnnouncementButton.IsEnabled = true;
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
            currentlySelectedAnnouncement = (Announcement)AnnouncementsBox.SelectedItem;
            ChangeSelectedAnnouncement(currentlySelectedAnnouncement);
            FillCommentsBox(currentlySelectedAnnouncement);
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            Comment newComment = new Comment(CommentTextbox.Text, currentUser, currentlySelectedAnnouncement);
        }

        private void NewAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            NewAnnouncementWindow newAnnouncementWindow = new NewAnnouncementWindow(currentUser, currentModule);
            bool? result = newAnnouncementWindow.ShowDialog();
            if (result != true)
            {
                currentModule.LoadAnnouncement(newAnnouncementWindow.announcement);
                currentModule.SaveAnnouncement(newAnnouncementWindow.announcement, currentModule);
            }
        }

        private void CommentsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

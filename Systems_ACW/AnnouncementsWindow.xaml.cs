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
            LoadModulesAnnouncements(currentModule);
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
            MessageBox.Show("Comment Saved");
            CommentTextbox.Clear();
        }

        private void NewAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            NewAnnouncementWindow newAnnouncementWindow = new NewAnnouncementWindow(currentUser, currentModule);
            bool? result = newAnnouncementWindow.ShowDialog();
            if (result == true)
            {
                currentModule.LoadAnnouncement(newAnnouncementWindow.announcement);
                currentModule.SaveAnnouncement(newAnnouncementWindow.announcement);
            }
        }

        private void LoadModulesAnnouncements(Module pModule)
        {
            pModule.ResetAnnouncements();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("..\\..\\XML_Files\\Announcements.xml");
            int announcementId = 50;
            int moduleId = 0;
            string announcementTitle = null;
            string announcementBody = null;
            int posterId = 50;
            DateTime dateTimePosted = DateTime.Now;
            foreach (XmlNode node in xDoc.DocumentElement)
            {
                string announcementIdString = node.Attributes[0].InnerText;
                string moduleIdString = node.Attributes[1].InnerText;
                List<Comment> announcementsComments = new List<Comment>();
                try
                {
                    announcementId = Convert.ToInt32(announcementIdString);
                }
                catch
                {
                    MessageBox.Show("Announcement ID couldn't be converted to int");
                }
                try
                {
                    moduleId = Convert.ToInt32(moduleIdString);
                }
                catch
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
                    if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "comments")
                    {
                        XmlDocument commentsDoc = new XmlDocument();
                        commentsDoc.Load("..\\..\\XML_Files\\Comments.xml");
                        foreach (XmlNode commentNode in childNode.ChildNodes)
                        {
                            Comment loadedComment = LoadAnnouncementComments(commentNode.InnerText, commentsDoc, announcementId);
                            if (loadedComment != null)
                            {
                                announcementsComments.Add(loadedComment);
                            }
                        }
                    }
                }
                Announcement loadedAnnouncement = new Announcement(announcementId, announcementTitle, announcementBody, posterId, moduleId, dateTimePosted);
                foreach (Comment comment in announcementsComments)
                {
                    loadedAnnouncement.AddComment(comment);
                }
                if (pModule.ID == moduleId)
                {
                    pModule.LoadAnnouncement(loadedAnnouncement);
                }
            }
        }

        private Comment LoadAnnouncementComments(string pCommentId, XmlDocument pCommentsDoc, int pAnnouncementId)
        {
            Comment newComment = null;
            foreach (XmlNode node in pCommentsDoc.DocumentElement)
            {
                if (node.Attributes["id"].Value == pCommentId)
                {
                    int id = Convert.ToInt32(pCommentId);
                    string body = null;
                    int posterId = 0;
                    DateTime datePosted = DateTime.Now;
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "body")
                        {
                            body = childNode.InnerText;
                        }
                        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "posterId")
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
                        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "datePosted")
                        {
                            try
                            {
                                datePosted = Convert.ToDateTime(childNode.InnerText);
                            }
                            catch
                            {
                                MessageBox.Show("DateTime Posted couldn't be converted to DateTime");
                            }
                        }
                    }
                    newComment = new Comment(id, body, GetUserFromID(posterId), pAnnouncementId, datePosted);
                    break;
                }
                else
                {
                    continue;
                }
            }
            return newComment;
        }

        private User GetUserFromID(int pUserId)
        {
            string name = null;
            string accessLevel = null;
            XmlDocument usersDoc = new XmlDocument();
            usersDoc.Load("XML_Files\\Users.xml");
            foreach (XmlNode node in usersDoc.DocumentElement)
            {
                if (node.Attributes["id"].Value == pUserId.ToString())
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "name")
                        {
                            name = childNode.InnerText;
                        }
                        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "accessLevel")
                        {
                            accessLevel = childNode.InnerText;
                            break;
                        }
                    }
                }
            }
            User user = new User(pUserId, name, accessLevel);
            return user;
        }

        private void CommentsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

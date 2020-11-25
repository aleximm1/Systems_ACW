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
            for (int i = 0; i < currentModule.Announcements.Count(); i++)
            {
                AnnouncementsBox.Items.Add(currentModule.Announcements[i]);
                //currentModule.Announcements[i].LoadImg(currentPlayer.Hand.Cards[i].cardImage);
            }
        }

        private void AnnouncementsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

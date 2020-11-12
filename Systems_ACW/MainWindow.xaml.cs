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
            Visibility = Visibility.Hidden;
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.ShowDialog();
            currentUser = new User(logInWindow.UsernameTextbox.Text, logInWindow.PasswordTextbox.Text, "Student");
            //This is where the database check for login happens.
            if (currentUser.Name == "test user")
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                logInWindow = new LogInWindow();
                logInWindow.ShowDialog();
            }
        }

        private void ModulesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

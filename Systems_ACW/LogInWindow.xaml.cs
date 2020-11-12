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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User testStudent = new User(UsernameTextbox.Text, PasswordTextbox.Text, "Student");
            User testTeacher = new User(UsernameTextbox.Text, PasswordTextbox.Text, "Teacher");
            User testAdmin = new User(UsernameTextbox.Text, PasswordTextbox.Text, "Main Admin");
            this.DialogResult = true;
        }
    }
}

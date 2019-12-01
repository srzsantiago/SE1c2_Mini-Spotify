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

namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            string Filled_password = filled_password.Password;
            string Filled_email = filled_email.Text;
            Ritmo.Login loginAttemp = new Ritmo.Login(Filled_email, Filled_password);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Colors.LightYellow;
            successblock.Fill = brush;
            if (loginAttemp.loggedin == true)
            {
                succeslabel.Content = "Success";
            }
            else
            {
                succeslabel.Content = "Failed, incorrect email or password";
            }
        }

        private void Newacc_link_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

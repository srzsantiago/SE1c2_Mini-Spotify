using Ritmo.ViewModels;
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

namespace loginscherm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
                // mainwindow tonen
            }
            else
            {
                succeslabel.Content = "Failed, incorrect email or password";
            }
        }

        private void Newacc_link_Click(object sender, RoutedEventArgs e)
        {
            //DisplayRootViewFor<MainWindowViewModel>();
        }

    }
}

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

namespace RegisterTestWPF
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

        private void Artistbutton_Click(object sender, RoutedEventArgs e)
        {
            var currentwindow = new Artist();
            currentwindow.Show();
        }

        private void Cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            loginscherm.MainWindow loginscherm = new loginscherm.MainWindow();
            loginscherm.Show();
            this.Close();
        }

        private void Createbutton_Click(object sender, RoutedEventArgs e)
        {
            string filledName = nameField.Text;
            string filledPassword = passwordField.Password;
            string filledConfirmpw = confirmpwField.Password;
            string filledEmail = emailField.Text;

            Ritmo.Register register = new Ritmo.Register(filledName, filledEmail, filledPassword, filledConfirmpw);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Colors.LightYellow;
            successblock.Fill = brush;
            succeslabel.Content = register.ToString();
        }
    }
}

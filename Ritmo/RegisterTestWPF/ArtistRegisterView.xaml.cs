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

namespace RegisterTestWPF
{
    /// <summary>
    /// Interaction logic for Artist.xaml
    /// </summary>
    public partial class Artist : Window
    {
        public Artist()
        {
            InitializeComponent();
        }

        private void Cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Sendbutton_Click(object sender, RoutedEventArgs e)
        {
            string email = filledmail.Text;
            Ritmo.Register sendArtist = new Ritmo.Register(email);
            MainWindow afterWindow = new MainWindow();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Colors.LightYellow;
            afterWindow.successblock.Fill = brush;
            afterWindow.succeslabel.Content = sendArtist.ToString();
            this.Close();
        }

        private void Cancelbutton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

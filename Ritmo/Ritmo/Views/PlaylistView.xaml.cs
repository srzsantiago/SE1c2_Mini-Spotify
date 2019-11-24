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

namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for MyPlaylistsView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        bool playlistMenuPanel = false;

        public PlaylistView()
        {
            InitializeComponent();
        }

        private void PlaylistMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!playlistMenuPanel)
            {
                PlaylistMenuGrid.Height = +140;
                playlistMenuPanel = true;
            }
            else
            {
                PlaylistMenuGrid.Height = 0;
                playlistMenuPanel = false;
            }
        }
    }
}

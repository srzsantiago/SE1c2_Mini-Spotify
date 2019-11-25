using Microsoft.VisualBasic;
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
        bool menuPenalIsOpen = false;

        public PlaylistView()
        {
            InitializeComponent();
        }

        private void PlaylistMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!playlistMenuPanel)
            {
                PlaylistMenuGrid.Height = +60;
                playlistMenuPanel = true;
            }
            else
            {
                PlaylistMenuGrid.Height = 0;
                playlistMenuPanel = false;
            }
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!menuPenalIsOpen)
            {
                MenuPanel.Height = +90;
                menuPenalIsOpen = true;
            }
            else
            {
                MenuPanel.Height = 0;
                menuPenalIsOpen = false;
            }
        }
        private void DeletePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            PlaylistMenuGrid.Height = 0;
            playlistMenuPanel = false;
        }

        private void ChangeNameButton_Click(object sender, RoutedEventArgs e)
        {
            PlaylistMenuGrid.Height = 0;
            playlistMenuPanel = false;
            string message, title, defaultValue;
            object myValue;

            //Set prompt
            message = "Please enter a name for the playlist.";

            //Set title
            title = "Set name for the playlist.";

            //Set default value
            defaultValue = "Default value";
            myValue = Interaction.InputBox(message, title, defaultValue);

            //Click ok
            var result = myValue.ToString();
            if (myValue.ToString() != "")
            {
                NamePlaylist.Content = result;
            }
            else
            {
                //Do nothing
            }
        }

        private void AddTrackPlaylist_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Height = 0;
            menuPenalIsOpen = false;
        }

        private void DeleteTrack_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Height = 0;
            menuPenalIsOpen = false;
        }

        private void AddTrackQueue_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Height = 0;
            menuPenalIsOpen = false;
        }
    }
}
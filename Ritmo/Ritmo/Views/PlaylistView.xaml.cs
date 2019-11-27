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
        bool playlistMenuIsOpen = false;

        PlaylistController playlistController;

        Track one = new Track("FirstTrack", "Shakira", 125);
        Track two = new Track("Second", "Shakira", 134);
        Track three = new Track("FirstSecond", "Ha", 10);

        public PlaylistView(Playlist playlist)
        {
            InitializeComponent();
        
            playlistController = new PlaylistController(playlist.Name);
            playlistController.AddTrack(one);
            playlistController.AddTrack(two);
            playlistController.AddTrack(three);
            NamePlaylist.Content = playlistController.Playlist.Name.ToString();

            ShowObjects();
        }

        public void ShowObjects()
        {            
            foreach (var item in playlistController.Playlist.Tracks)
            {
                Label l = new Label();
                Label l1 = new Label();
                Label l2 = new Label();
                Label l3 = new Label();
                Button b = new Button();

                b.Content = "X";
                b.FontSize = 16;
               
                DeleteColumn.Children.Add(b);
                b.HorizontalAlignment = HorizontalAlignment.Center;

                l.Content = item.Name;
                NameColumn.Children.Add(l);
                l.HorizontalContentAlignment = HorizontalAlignment.Left;
                
                l1.Content = item.Artist;
                ArtistColumn.Children.Add(l1);
                l1.HorizontalContentAlignment = HorizontalAlignment.Center;

                l2.Content = "Test";
                AlbumColumn.Children.Add(l2);
                l2.HorizontalContentAlignment = HorizontalAlignment.Center;

                l3.Content = item.Duration;
                DurationColumn.Children.Add(l3);
                l3.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
        }

        public void ClearItems()
        {
            DeleteColumn.Children.Clear();
            NameColumn.Children.Clear();
            ArtistColumn.Children.Clear();
            AlbumColumn.Children.Clear();
            DurationColumn.Children.Clear();
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
                playlistController.SetName(result);
            }
            else
            {
                //Do nothing
            }
        }

        private void AddTrackPlaylist_Click(object sender, RoutedEventArgs e)
        {
            playlistMenuIsOpen = true;
            PlaylistMenu.Height = 60;
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

        private void NameAsc_Click(object sender, RoutedEventArgs e)
        {
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, "Name", true);
            ClearItems();
            ShowObjects();
        }

        private void NameDesc_Click(object sender, RoutedEventArgs e)
        {
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, "Name", false);
            ClearItems();
            ShowObjects();
        }

        private void ArtistAsc_Click(object sender, RoutedEventArgs e)
        {
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, "Artist", true);
            ClearItems();
            ShowObjects();
        }

        private void ArtistDesc_Click(object sender, RoutedEventArgs e)
        {
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, "Artist", false);
            ClearItems();
            ShowObjects();
        }

        private void DurationAsc_Click(object sender, RoutedEventArgs e)
        {
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, "Duration", true);
            ClearItems();
            ShowObjects();
        }

        private void DurationDesc_Click(object sender, RoutedEventArgs e)
        {
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, "Duration", false);
            ClearItems();
            ShowObjects();
        }
    }
}
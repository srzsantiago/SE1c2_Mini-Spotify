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
        MyPlaylistsView playlistsView = new MyPlaylistsView();
        bool menuPenalIsOpen = false;
        PlaylistController playlistController;
        Track one = new Track("FirstTrack", "Shakira", 125);
        Track two = new Track("Second", "Shakira", 134);
        Track three = new Track("FirstSecond", "Ha", 10);
        public PlaylistView()
        {
            InitializeComponent();
        }

        public PlaylistView(Playlist playlist): this()
        {
            
            playlistController = new PlaylistController(playlist.Name);
            playlistController.AddTrack(one);
            playlistController.AddTrack(two);
            playlistController.AddTrack(three);
            NamePlaylist.Content = playlistController.Playlist.Name.ToString();

            ShowObjects();
        }

        public void ShowObjects()
        {
            foreach (var item in playlistController.Playlist.Tracks) // goes through all the playlists that exist and adds their name to the list in my playlists
            {
                Label l = new Label();
                l.Content = item.Name;
                NameColumn.Children.Add(l);
                l.HorizontalContentAlignment = HorizontalAlignment.Left;

                Label l1 = new Label();
                l1.Content = item.Artist;
                ArtistColumn.Children.Add(l1);
                l1.HorizontalContentAlignment = HorizontalAlignment.Center;

                Label l2 = new Label();
                l2.Content = "Test";
                AlbumColumn.Children.Add(l2);
                l2.HorizontalContentAlignment = HorizontalAlignment.Center;

                Label l3 = new Label();
                l3.Content = item.Duration;
                DurationColumn.Children.Add(l3);
                l3.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
            }

        public void clearLabels()
        {
            foreach (var item in playlistController.Playlist.Tracks) // goes through all the playlists that exist and adds their name to the list in my playlists
            {
                Label l = new Label();
                l.Content = " ";
                NameColumn.Children.Add(l);
                l.HorizontalContentAlignment = HorizontalAlignment.Left;

                Label l1 = new Label();
                l1.Content = " ";
                ArtistColumn.Children.Add(l1);
                l1.HorizontalContentAlignment = HorizontalAlignment.Center;

                Label l2 = new Label();
                l2.Content = " ";
                AlbumColumn.Children.Add(l2);
                l2.HorizontalContentAlignment = HorizontalAlignment.Center;

                Label l3 = new Label();
                l3.Content = " ";
                DurationColumn.Children.Add(l3);
                l3.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
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

        private void NameAsc_Click(object sender, RoutedEventArgs e)
        {
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, "Name", true);
            clearLabels();
            ShowObjects();
        }

        private void NameDesc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ArtistAsc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ArtistDesc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DurationAsc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DurationDesc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
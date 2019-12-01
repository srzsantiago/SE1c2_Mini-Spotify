using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using Microsoft.VisualBasic;
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


namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for AllPlayListsView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        //Close and open the stackpanels (menu panels)
        bool playlistMenuPanel = false;
        bool menuPenalIsOpen = false;
        bool playlistMenuIsOpen = false;


        PlaylistController playlistController;

        AllPlaylistsController allplaylistscontroller; 

        private PlaylistController _playlistController;

        public PlaylistController PlaylistController
        {
            get { return _playlistController; }
            set { _playlistController = value; }
        }

       

        public PlaylistView()
        {
            InitializeComponent();
            allplaylistscontroller = AllPlaylistsViewModel.AllPlaylistsController;

        }

        public PlaylistView(Playlist playlist):this()
        {
            
            playlistController = new PlaylistController(playlist.Name); //Create a new playlistController with playlist
            NamePlaylist.Content = playlistController.Playlist.Name.ToString(); //Set the content for the name
            TestPlaylist();
            ShowObjects(); //Show the tracks in the playlistView
        }

        public void TestPlaylist()
        {
            //Add tracks
            Track one = new Track(1, "FirstTrack", "Shakira", 125);
            Track two = new Track(2, "Second", "Shakira", 134);
            Track three = new Track(3, "FirstSecond", "Ha", 10);

            playlistController.AddTrack(one); //Add tracks to the playlist
            playlistController.AddTrack(two);
            playlistController.AddTrack(three);
            
        }

        public void ShowObjects()
        {
            ClearItems(); //Clear the stackpanel with labels
            foreach (var item in playlistController.Playlist.Tracks) //Get items from the track linkedList
            {
                //Create new labels and button
                Label l = new Label();
                Label l1 = new Label();
                Label l2 = new Label();
                Label l3 = new Label();
                Button b = new Button();


                // Delete button
                b.Click += DeleteTrack_Click; //Set the click event
                b.Content = "X"; //Content
                b.Tag = item.TrackId; //Set id to the button
                b.FontSize = 16; //Set size of the button
                DeleteColumn.Children.Add(b); //Add button to the assigned column
                b.HorizontalAlignment = HorizontalAlignment.Center; //Center the button horizontal

                //Name label
                l.Content = item.Name;
                NameColumn.Children.Add(l);
                l.HorizontalContentAlignment = HorizontalAlignment.Left;
                
                //Artist label
                l1.Content = item.Artist;
                ArtistColumn.Children.Add(l1);
                l1.HorizontalContentAlignment = HorizontalAlignment.Center;
                 
                //Album label
                l2.Content = "Test";
                AlbumColumn.Children.Add(l2);
                l2.HorizontalContentAlignment = HorizontalAlignment.Center;

                //Duration label
                l3.Content = item.Duration;
                DurationColumn.Children.Add(l3);
                l3.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
        }

        public void ClearItems() //Method to clear the whole stackpanel with labels
        {
            DeleteColumn.Children.Clear();
            NameColumn.Children.Clear();
            ArtistColumn.Children.Clear();
            AlbumColumn.Children.Clear();
            DurationColumn.Children.Clear();
        }

        private void PlaylistMenuButton_Click(object sender, RoutedEventArgs e) //Opens or closes the optionsmenu to set the name of the playlist or delete the playlist
        {
            if (!playlistMenuPanel) //If panel is closed
            {
                PlaylistMenuGrid.Height = +60; //Open the stackpanel
                playlistMenuPanel = true;
            }
            else
            {
                PlaylistMenuGrid.Height = 0;  //Else close the stackpanel
                playlistMenuPanel = false;
            }
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e) //Opens or closes the optiosnmenu to add songs to playlist/queue or delete a song
        {
            if (!menuPenalIsOpen) //if panel is closed
            {
                MenuPanel.Height = +90; //Open the stackpanel
                menuPenalIsOpen = true;
            }
            else
            {
                MenuPanel.Height = 0; //Close the stackpanel
                menuPenalIsOpen = false;
            }
        }
        private void DeletePlaylistButton_Click(object sender, RoutedEventArgs e) //Delete playlist
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            PlaylistMenuGrid.Height = 0;
            playlistMenuPanel = false; //Closes the stackpanel

            //if (MessageBox.Show("Are you sure you want to delete this playlist?", "Deleting playlist", MessageBoxButtons.OKCancel) == DialogResult.OK)
            
            for (int i = 0; i < allplaylistscontroller.allplaylists.playlists.Count; i++)
            {
                if (allplaylistscontroller.allplaylists.playlists[i].TrackListID == playlistController.Playlist.TrackListID) // checks if i is equal to the pressed buttons content
                {
                    allplaylistscontroller.RemovePlaylist(allplaylistscontroller.allplaylists.playlists[i]); // removes the button with the id of i
                    break; // stops the loop, if you count 5 playlists and delete one then the loop still goes on to the 5th playlist, this gives an error
                    
                }
            }
        }

        private void ChangeNameButton_Click(object sender, RoutedEventArgs e) //Change the name of the playlist
        {
            PlaylistMenuGrid.Height = 0;
            playlistMenuPanel = false; //Close the stackpanel

            string message, title, defaultValue;
            object myValue;

            //Pop up  window
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

        private void AddTrackToPlaylist_Click(object sender, RoutedEventArgs e) //Add track to a playlist
        {
            playlistMenuIsOpen = true; //Opens a new stackpanel, to select a playlist
            PlaylistMenu.Height = 60;
        }

        private void DeleteTrack_Click(object sender, RoutedEventArgs e) //Delete a track from the playlist
        {
            MenuPanel.Height = 0;
            menuPenalIsOpen = false; //Close the stackpanel

            Button clickedButton = sender as Button; //Check which button is pressed
            int tracksamount = playlistController.Playlist.Tracks.Count();
            int buttoncontent = (int)clickedButton.Tag; //Pressed button gets an ID

            
                for (int i = 0; i < tracksamount; i++)
            {
                if (playlistController.Playlist.Tracks.ElementAt(i).TrackId == buttoncontent) //Check if the trackId is the same as the buttonId
                {
                    playlistController.RemoveTrack(playlistController.Playlist.Tracks.ElementAt(i)); //Delete the selected track (by trackId)
                    ShowObjects(); //Show the tracks in the playlist
                    break; //Ends the loop when you delete a track
                }
            }
                
        }

        private void AddTrackQueue_Click(object sender, RoutedEventArgs e) //Add track to the queue
        {
            MenuPanel.Height = 0;
            menuPenalIsOpen = false; //Close the stackpanel

            
        }

        private void AscendingSort_Click(object sender, RoutedEventArgs e) 
        {
            Button clickedButton = sender as Button;
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, (string)clickedButton.Tag, true);
            ShowObjects();
        }

        private void DescendingSort_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, (string)clickedButton.Tag, false);
            ShowObjects();
        }

       

        
    }
}
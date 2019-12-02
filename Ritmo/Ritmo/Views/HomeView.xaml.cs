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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        PlayQueueController playqueuecontroller = new PlayQueueController();
        AllPlaylistsController allplaylistcontroller;
        List<Track> allTestTrack = new List<Track>();
        private int clickedbuttonvalue;
        PlaylistController testplaylist1 = new PlaylistController("playlist1");

        public HomeView()
        {
            allplaylistcontroller = AllPlaylistsViewModel.AllPlaylistsController;
            testAllPlayLists();
            InitializeComponent();
            LoadItems();
        }

        public void ClearItems()
        {
            TracknamesColumn.Children.Clear();
            AddPlayListColumn.Children.Clear();
            AddQueueColumn.Children.Clear();
        }


        public void LoadItems()
        {
            //ClearItems();
            foreach (var item in allTestTrack)
            {
                Button songnameButton = new Button();
                Button AddtoPlaylistButton = new Button();
                Button AddtoQueueButton = new Button();

                songnameButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                AddtoPlaylistButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                AddtoQueueButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;

                AddtoQueueButton.Content = "Add to Queue";
                AddtoPlaylistButton.Content = "Add to Playlist";
                songnameButton.Content = item.Name;

                songnameButton.Tag = item.TrackId;
                AddtoQueueButton.Tag = item.TrackId;
                AddtoPlaylistButton.Tag = item.TrackId;

                AddtoPlaylistButton.Click += AddToPlayListClick;
                AddtoQueueButton.Click += AddToQueueClick;


                TracknamesColumn.Children.Add(songnameButton);
                AddPlayListColumn.Children.Add(AddtoPlaylistButton);
                AddQueueColumn.Children.Add(AddtoQueueButton);


            }
        }

        public void testAllPlayLists()
        {
            Track track1 = new Track(1, "track1", "JOHANNES", 10);
            Track track2 = new Track(2, "track2", "Tristan", 10);
            Track track3 = new Track(3, "track3", "ZAPATA", 10);
            Track track4 = new Track(4, "track4", "rodriguez", 10);
            Track track5 = new Track(5, "track5", "santiago", 10);


            allTestTrack.Add(track1);
            allTestTrack.Add(track2);
            allTestTrack.Add(track3);
            allTestTrack.Add(track4);
            allTestTrack.Add(track5);

            
            PlaylistController testplaylist2 = new PlaylistController("playlist2");
            PlaylistController testplaylist3 = new PlaylistController("playlist3");
            PlaylistController testplaylist4 = new PlaylistController("playlist4");
            PlaylistController testplaylist5 = new PlaylistController("playlist5");



            allplaylistcontroller.AddTrackList(testplaylist1.Playlist);
            allplaylistcontroller.AddTrackList(testplaylist2.Playlist);
            allplaylistcontroller.AddTrackList(testplaylist3.Playlist);
            allplaylistcontroller.AddTrackList(testplaylist4.Playlist);
            allplaylistcontroller.AddTrackList(testplaylist5.Playlist);

        }

        private void AddToPlayListClick(object sender, RoutedEventArgs e)
        {
            Button clickedbutton = sender as Button; // looks which button was pressed
            clickedbuttonvalue = (int)clickedbutton.Tag;
            Playlistboxes.Items.Clear();

            foreach (var item in allplaylistcontroller.allplaylists.playlists)
            {
                Playlistboxes.Items.Add(item.Name);
            }
            Playlistboxes.Height = 340;
        }


        private void AddToQueueClick(object sender, RoutedEventArgs e)
        {
            Button clickedbutton = sender as Button; // looks which button was pressed
            foreach (var item in allTestTrack) // goes through the tracks
            {
                if (item.TrackId == (int)clickedbutton.Tag) // looks which buttons tag is the same as the trackid
                {
                    playqueuecontroller.AddTrack(item); // adds the song to the queue
                }

            }

        }

        private void Playlistboxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in allTestTrack)
            {
                if (item.TrackId == clickedbuttonvalue)
                {
                    for (int i = 0; i < allplaylistcontroller.allplaylists.playlists.Count; i++)
                    {
                        var selecteditem = Playlistboxes.SelectedItem;
                        if (selecteditem.Equals(allplaylistcontroller.allplaylists.playlists.ElementAt(i).Name))
                        {
                            allplaylistcontroller.allplaylists.playlists.ElementAt(i).Tracks.AddLast(item);
                        }
                    }

                }

            }
            }
        }
    }

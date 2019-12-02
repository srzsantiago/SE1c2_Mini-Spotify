using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class HomeViewModel : Screen
    {

        //PlayQueueController playqueuecontroller;
        //AllPlaylistsController allplaylistcontroller;
        //PlaylistController playlistcontroller = new PlaylistController("playlistcontroller");
        //private int clickedbuttonvalue;


        //public ListBox Playlistboxes { get; set; }
        //public StackPanel TracknamesColumn { get; set; }

        //public ICommand TrackNamesCommand { get; set; }
        //public StackPanel AddPlayListColumn { get; set; }
        //public ICommand AddToPlayListCommand { get; set; }
        //public StackPanel AddQueueColumn { get; set; }

        //public ICommand AddQueueCommand { get; set; }

        //public HomeViewModel()
        //{
        //    allplaylistcontroller = AllPlaylistsViewModel.AllPlaylistsController;
        //    testAllPlayLists();
        //    LoadItems();
        //}

        
        //public ICommand ChangeTestStringCommand { get; set; }
        //private string _TestString = "Dit is een test!";

        //public string TestString
        //{
        //    get { return _TestString; }
        //    set {
        //        _TestString = value;
        //        NotifyOfPropertyChange();
        //    }
        //}

        //private void ChangeTestString()
        //{
        //    if (!TestString.Equals("Tekst is veranderd!"))
        //        TestString = "Tekst is veranderd!";
        //    else
        //        TestString = "Weer andere tekst!";
        //}

        //public void ClearItems()
        //{
        //    TracknamesColumn.Children.Clear();
        //    AddPlayListColumn.Children.Clear();
        //    AddQueueColumn.Children.Clear();
        //}


        //public void LoadItems()
        //{
        //    //ClearItems();
        //    foreach (var item in playlistcontroller.Playlist.Tracks)
        //    {
        //        Button songnameButton = new Button();
        //        Button AddtoPlaylistButton = new Button();
        //        Button AddtoQueueButton = new Button();

        //        songnameButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
        //        AddtoPlaylistButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
        //        AddtoQueueButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;

        //        AddtoQueueButton.Content = "Add to Queue";
        //        AddtoPlaylistButton.Content = "Add to Playlist";
        //        songnameButton.Content = item.Name;

        //        songnameButton.Tag = item.TrackId;
        //        AddtoQueueButton.Tag = item.TrackId;
        //        AddtoPlaylistButton.Tag = item.TrackId;

        //        AddtoPlaylistButton.Click += AddToPlayListClick;
        //        AddtoQueueButton.Click += AddToQueueClick;


        //        TracknamesColumn.Children.Add(songnameButton);
        //        AddPlayListColumn.Children.Add(AddtoPlaylistButton);
        //        AddQueueColumn.Children.Add(AddtoQueueButton);


        //    }
        //}

        //public void testAllPlayLists()
        //{


        //    Track track1 = new Track(1, "track1", "JOHANNES", 10);
        //    Track track2 = new Track(2, "track2", "Tristan", 10);
        //    Track track3 = new Track(3, "track3", "ZAPATA", 10);
        //    Track track4 = new Track(4, "track4", "rodriguez", 10);
        //    Track track5 = new Track(5, "track5", "santiago", 10);

        //    Playlist testplaylist1 = new Playlist(0, "playlist1", 100, DateTime.Today);
        //    Playlist testplaylist2 = new Playlist(1, "playlist2", 200, DateTime.Today);
        //    Playlist testplaylist3 = new Playlist(2, "playlist3", 400, DateTime.Today.AddDays(1));
        //    Playlist testplaylist4 = new Playlist(3, "playlist4", 5000, DateTime.Today.AddMonths(4));
        //    Playlist testplaylist5 = new Playlist(4, "playlist5", 2222, DateTime.Today);

        //    playlistcontroller.AddTrack(track1);
        //    playlistcontroller.AddTrack(track2);
        //    playlistcontroller.AddTrack(track3);
        //    playlistcontroller.AddTrack(track4);
        //    playlistcontroller.AddTrack(track5);

        //    allplaylistcontroller.AddTrackList(testplaylist1);
        //    allplaylistcontroller.AddTrackList(testplaylist2);
        //    allplaylistcontroller.AddTrackList(testplaylist3);
        //    allplaylistcontroller.AddTrackList(testplaylist4);
        //    allplaylistcontroller.AddTrackList(testplaylist5);

        //}

        //private void AddToPlayListClick(object sender, RoutedEventArgs e)
        //{
        //    Button clickedbutton = sender as Button; // looks which button was pressed
        //    clickedbuttonvalue = (int)clickedbutton.Tag;
        //    Playlistboxes.Items.Clear();

        //    foreach (var item in allplaylistcontroller.allplaylists.playlists)
        //    {
        //        Playlistboxes.Items.Add(item.Name);
        //    }
        //    Playlistboxes.Height = 340;
        //}

        //private void AddToQueueClick(object sender, RoutedEventArgs e)
        //{
        //    Button clickedbutton = sender as Button; // looks which button was pressed
        //    foreach (var item in playlistcontroller.Playlist.Tracks) // goes through the tracks
        //    {
        //        if (item.TrackId == (int)clickedbutton.Tag) // looks which buttons tag is the same as the trackid
        //        {
        //            playqueuecontroller.AddTrack(item); // adds the song to the queue
        //        }

        //    }

        //}

        //private void Playlistboxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    foreach (var track in playlistcontroller.Playlist.Tracks)
        //    {
        //        if (track.TrackId == clickedbuttonvalue) //clickedbuttonvalue
        //        {
        //            foreach (var playlist in allplaylistcontroller.allplaylists.playlists)
        //            {
        //                if (Playlistboxes.SelectedIndex == playlist.TrackListID)
        //                {
        //                    playlistcontroller.Playlist.Tracks.AddLast(track);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}

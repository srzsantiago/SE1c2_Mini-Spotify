using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class HomeViewModel : Screen
    {
        
        MainWindowViewModel mainWindowViewModel;
        private int _clickedButtonValue;

        private int _addtonewplaylistbuttonheight = 0;

        public int AddToNewPlaylisButtontHeight
        {
            get { return _addtonewplaylistbuttonheight; }
            set { _addtonewplaylistbuttonheight = value; NotifyOfPropertyChange(); }
        }


        #region observablecollections
        private ObservableCollection<Track> _allTestTrack;

        public ObservableCollection<Track> AllTestTrack
        {
            get
            {
                if (_allTestTrack == null)
                    _allTestTrack = new ObservableCollection<Track>();
                return _allTestTrack;
            }
            set { _allTestTrack = value; }
        }

        private ObservableCollection<Playlist> _allPlaylist;

        public ObservableCollection<Playlist> AllPlaylist
        {
            get
            {
                if (_allPlaylist == null)
                    _allPlaylist = new ObservableCollection<Playlist>();
                return _allPlaylist;
            }
            set
            {
                _allPlaylist = value;
                NotifyOfPropertyChange("AllPlaylist");
            }
        }
        #endregion

        #region commands and selectedItem
        public ICommand LoadListboxPlaylistCommand { get; set; }
        public ICommand AddToQueueCommand { get; set; }    
        public ICommand AddToNewPlaylistCommand { get; set; }

        private Playlist _selectedItem; //this is used as a ActionListener for when user click on a item in the listbox of playlists.
        public Playlist SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value == _selectedItem)
                    return;

                _selectedItem = value;
                NotifyOfPropertyChange();                
                if(_selectedItem != null) 
                    AddTrackToSelectedPlaylist(); //Add the track to the selected item in the listbox (Selected item is a playlist)
                _selectedItem = null;
            }
        }
        #endregion

        public HomeViewModel() { }

        public HomeViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            AllPlaylist = new ObservableCollection<Playlist>();
            LoadListboxPlaylistCommand = new RelayCommand<object>(this.LoadListboxPlaylist);
            AddToQueueCommand = new RelayCommand<object>(this.AddToQueueClick);
            AddToNewPlaylistCommand = new RelayCommand<object>(this.AddToNewPlaylistClick);
            TestAllPlayLists();

        }
        private void LoadListboxPlaylist(object sender)//When user click on "Add to playlist" the listbox of Playlists is filled.
        {
            //get the trackID of the track clicked, so it can be use in the listbox to identify which tracks is going to be add to the selected item
            _clickedButtonValue = (int)sender;
            AddToNewPlaylisButtontHeight = 30;
            AllPlaylist.Clear();//clear the ObservableCollection of Playlists to avoid repeated playlists

            foreach (var item in mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists)
            {
                AllPlaylist.Add(item);//add all playlist to the OC
            }
        }
        private void AddToQueueClick(object sender)//Add clicked track to queue
        {
            _clickedButtonValue = (int)sender;//get trackID

            foreach (var item in AllTestTrack) // goes through the tracks
            {
                if (item.TrackId == _clickedButtonValue) // looks which trackId match the clicked track
                {
                    Track testTrack = item;
                    mainWindowViewModel.PlayQueueController.AddTrack(testTrack); // adds the song to the queue
                    mainWindowViewModel.MyQueueScreenToViewModel.LoadElements();
                }

            }


        }

        public Track GetTrackDB(int trackid)
        {
            Track track = null;
            int count = 0;
            string sql = $"SELECT idTrack, title, path, duration FROM Track WHERE idTrack = {trackid}";
            List<Dictionary<string, object>> tracks = Database.DatabaseConnector.SelectQueryDB(sql);

            int id = 0;
            string title = "";
            string path = "";
            int duration = 0;

            foreach (var dictionary in tracks)
            {
                foreach (var key in dictionary)
                {
                    if (key.Key.Equals("idTrack"))
                    {
                        id = (int)key.Value;
                        count++;
                    }
                    else if (key.Key.Equals("title"))
                    {
                        title = (string)key.Value;
                        count++;
                    }
                    else if (key.Key.Equals("path"))
                    {
                        path = (string)key.Value;
                        count++;
                    }
                    else if (key.Key.Equals("duration"))
                    {
                        duration = (int)key.Value;
                        count++;
                    }
                }
                if (count % 4 == 0)
                {
                    track = new Track() { TrackId = id, Name = title, Duration = duration, AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + path), Artist = "test", Album = "test"};
                }
            }
            return track;
        }

        private void AddToNewPlaylistClick(object sender) // user story opgeschoven dus word later aan gewerkt
        {
            IWindowManager windowManager = new WindowManager();
            windowManager.ShowDialog(new PopUpWindowViewModel(this, _clickedButtonValue, mainWindowViewModel));
            AddToNewPlaylisButtontHeight = 0;
        }

        private void AddTrackToSelectedPlaylist()//This methode is called from the prop SelectedItem. It adds a track to a clicked playlist.
        {
            bool contains = false;
            foreach (var item in AllTestTrack)//goes through all tracks
            {
                if (item.TrackId == _clickedButtonValue)//find the matching Id
                {
                    for (int i = 0; i < mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.Count; i++)
                    {
                        //find the matching playlist
                        if (SelectedItem.Equals(mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.ElementAt(i)))
                        {
                            Track testTrack = item;
                            int playlistid = mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.ElementAt(i).TrackListID;
                            string selectsql = $"SELECT idTrack FROM Track WHERE idTrack IN (SELECT trackID FROM Track_has_Playlist WHERE playlistID = {playlistid})";
                            List<Dictionary<string, object>> ids = Database.DatabaseConnector.SelectQueryDB(selectsql);

                            foreach (var dictionary in ids)
                            {
                                foreach (var key in dictionary)
                                {
                                    if((int)key.Value == testTrack.TrackId)
                                    {
                                        contains = true;
                                    }
                                }
                            }

                            if (!contains)
                            {
                                mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.ElementAt(i).Tracks.AddLast(testTrack);
                                string sql = $"INSERT INTO Track_has_Playlist VALUES ({item.TrackId}, {SelectedItem.TrackListID} )";
                                Database.DatabaseConnector.InsertQueryDB(sql);
                            } else
                            {
                                IWindowManager windowManager = new WindowManager();
                                windowManager.ShowDialog(new PopUpWindowViewModel(this));
                            }
                        }
                    }
                }
            }
            AllPlaylist.Clear();
        }

        public void TestAllPlayLists()//test method to put all the tracks known in the database on the home page.
        {
            int count = 0;
            string sql = "SELECT idTrack, title, path, genre, date, duration FROM Track";
            List<Dictionary<string, object>> tracks = Database.DatabaseConnector.SelectQueryDB(sql);

            int id = 0;
            string title = "";
            string path = "";
            int duration = 0;

            foreach (var dictionary in tracks)
            {
                foreach (var key in dictionary)
                {
                    if (key.Key.Equals("idTrack"))
                    {
                        id = (int)key.Value;
                        count++;
                    }
                    else if (key.Key.Equals("title"))
                    {
                        title = (string)key.Value;
                        count++;
                    }
                    else if (key.Key.Equals("path"))
                    {
                        path = (string)key.Value;
                        count++;
                    }
                    else if (key.Key.Equals("duration"))
                    {
                        duration = (int)key.Value;
                        count++;
                    }
                }
                if (count % 4 == 0)
                {
                    AllTestTrack.Add(new Track() { TrackId = id, Name = title, Duration = duration, AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + path), Artist = "test", Album = "test" });
                }
            }
        }
    }
   
}

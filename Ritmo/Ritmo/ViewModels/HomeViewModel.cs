using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
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




        public HomeViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            AllPlaylist = new ObservableCollection<Playlist>();
            TestAllPlayLists();
            LoadListboxPlaylistCommand = new RelayCommand<object>(this.LoadListboxPlaylist);
            AddToQueueCommand = new RelayCommand<object>(this.AddToQueueClick);

        }

        public void TestAllPlayLists()
        {


            
            AllTestTrack.Add(new Track()
                {
                    TrackId = 6,
                    Album = "testAlbum",
                    Artist = "JOHANNES",
                    Duration = 10,
                    Name = "Track1",
                    AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3")
                });
            

            AllTestTrack.Add(new Track()
                {
                    TrackId = 7,
                    Album = "testAlbum",
                    Artist = "Tristan",
                    Duration = 10,
                    Name = "Track2",
                    AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                });
            
            AllTestTrack.Add(new Track()
                {
                    TrackId = 8,
                    Album = "testAlbum",
                    Artist = "Zapata",
                    Duration = 10,
                    Name = "Track3",
                    AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                });
            
            AllTestTrack.Add(new Track()
                {
                    TrackId = 9,
                    Album = "testAlbum",
                    Artist = "Rodriguez",
                    Duration = 10,
                    Name = "Track4",
                    AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                });
            
            AllTestTrack.Add(new Track()
                {
                    TrackId = 10,
                    Album = "testAlbum",
                    Artist = "Santiago",
                    Duration = 10,
                    Name = "Track5",
                    AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                });


            //PlaylistController testplaylist1 = new PlaylistController("playlist1");
            //PlaylistController testplaylist2 = new PlaylistController("playlist2");
            //PlaylistController testplaylist3 = new PlaylistController("playlist3");
            //PlaylistController testplaylist4 = new PlaylistController("playlist4");
            //PlaylistController testplaylist5 = new PlaylistController("playlist5");


            //mainWindowViewModel.AllPlaylistsController.AddTrackList(testplaylist1.Playlist);
            //mainWindowViewModel.AllPlaylistsController.AddTrackList(testplaylist2.Playlist);
            //mainWindowViewModel.AllPlaylistsController.AddTrackList(testplaylist3.Playlist);
            //mainWindowViewModel.AllPlaylistsController.AddTrackList(testplaylist4.Playlist);
            //mainWindowViewModel.AllPlaylistsController.AddTrackList(testplaylist5.Playlist);

        }


        private void LoadListboxPlaylist(object sender)
        {
            _clickedButtonValue = (int)sender;

            AllPlaylist.Clear();

            foreach (var item in mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists)
            {
                AllPlaylist.Add(item);
            }
        }


        private void AddToQueueClick(object sender)
        {
            _clickedButtonValue = (int)sender;

            foreach (var item in AllTestTrack) // goes through the tracks
            {
                if (item.TrackId == _clickedButtonValue) // looks which buttons tag is the same as the trackid
                {
                    Track testTrack = item;
                    mainWindowViewModel.PlayQueueController.AddTrack(testTrack); // adds the song to the queue
                    mainWindowViewModel.MyQueueScreenToViewModel.ShowElements();
                }

            }


        }

        private void AddTrackToSelectedPlaylist()
        {
            foreach (var item in AllTestTrack)
            {
                if (item.TrackId == _clickedButtonValue)
                {
                    for (int i = 0; i < mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.Count; i++)
                    {
                        if (SelectedItem.Equals(mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.ElementAt(i)))
                        {
                            Track testTrack = item;
                            mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.ElementAt(i).Tracks.AddLast(testTrack);
                        }
                    }

                }

            }
            AllPlaylist.Clear();

        }
    }
   
}

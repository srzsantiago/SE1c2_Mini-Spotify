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
        List<Track> allTestTrack = new List<Track>();
        MainWindowViewModel mainWindowViewModel;
        private int _clickedButtonValue;

        #region stackpanels
        private StackPanel _tracknamesColumn;

        public StackPanel TracknamesColumn
        {
            get
            {
                if (_tracknamesColumn == null)
                    _tracknamesColumn = new StackPanel();
                return _tracknamesColumn;
            }
            set { _tracknamesColumn = value; }
        }

        private StackPanel _addPlayListColumn;

        public StackPanel AddPlayListColumn
        {
            get
            {
                if (_addPlayListColumn == null)
                    _addPlayListColumn = new StackPanel();
                return _addPlayListColumn;
            }
            set { _addPlayListColumn = value; }
        }

        private StackPanel _addQueueColumn;

        public StackPanel AddQueueColumn
        {
            get
            {
                if (_addQueueColumn == null)
                    _addQueueColumn = new StackPanel();
                return _addQueueColumn;
            }
            set { _addQueueColumn = value; }
        }
        #endregion

        private ListBox _playlistboxes;

        public ListBox Playlistboxes
        {
            get
            {
                if (_playlistboxes == null)
                    _playlistboxes = new ListBox();
                return _playlistboxes;
            }
            set
            {
                _playlistboxes = value;
                NotifyOfPropertyChange("Playlistboxes");
            }
        }

        #region observablecollections
        private ObservableCollection<TestItems> _allTestTrack;

        public ObservableCollection<TestItems> AllTestTrack
        {
            get
            {
                if (_allTestTrack == null)
                    _allTestTrack = new ObservableCollection<TestItems>();
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

        #region commands
        private ICommand _addToPlaylistCommand;

        public ICommand AddToPlaylistCommand
        {
            get
            {
                return _addToPlaylistCommand;
            }
            set
            {
                _addToPlaylistCommand = value;
            }
        }

        private ICommand _addToQueueCommand;

        public ICommand AddToQueueCommand
        {
            get
            {
                return _addToQueueCommand;
            }
            set
            {
                _addToQueueCommand = value;
            }
        }
        #endregion




        private Playlist _selectedItem;
        public Playlist SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value == _selectedItem)
                    return;

                _selectedItem = value;
                NotifyOfPropertyChange("SelectedItem");
                Playlistboxes_SelectionChanged();
                _selectedItem = null;

            }
        }




        public HomeViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            AllPlaylist = new ObservableCollection<Playlist>();
            testAllPlayLists();
            _addToPlaylistCommand = new RelayCommand<object>(this.AddToPlayListClick);
            _addToQueueCommand = new RelayCommand<object>(this.AddToQueueClick);

        }

        public void testAllPlayLists()
        {


            Track track1 = new Track(1, "track1", "JOHANNES", 10);
            Track track2 = new Track(2, "track2", "Tristan", 10);
            Track track3 = new Track(3, "track3", "ZAPATA", 10);
            Track track4 = new Track(4, "track4", "rodriguez", 10);
            Track track5 = new Track(5, "track5", "santiago", 10);


            int count = 0;
            AllTestTrack.Add(new TestItems()
            {
                TrackID = 6,
                Album = "testAlbum",
                Artist = "JOHANNES",
                Duration = 10,
                Name = "Track1",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                ButtonID = count,
            });
            count++;
            AllTestTrack.Add(new TestItems()
            {
                TrackID = 7,
                Album = "testAlbum",
                Artist = "Tristan",
                Duration = 10,
                Name = "Track2",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                ButtonID = count,
            });
            count++;
            AllTestTrack.Add(new TestItems()
            {
                TrackID = 8,
                Album = "testAlbum",
                Artist = "Zapata",
                Duration = 10,
                Name = "Track3",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                ButtonID = count,
            });
            count++;
            AllTestTrack.Add(new TestItems()
            {
                TrackID = 9,
                Album = "testAlbum",
                Artist = "Rodriguez",
                Duration = 10,
                Name = "Track4",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                ButtonID = count,
            });
            count++;
            AllTestTrack.Add(new TestItems()
            {
                TrackID = 10,
                Album = "testAlbum",
                Artist = "Santiago",
                Duration = 10,
                Name = "Track5",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
                ButtonID = count,
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


        private void AddToPlayListClick(object sender)
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
                if (item.TrackID == _clickedButtonValue) // looks which buttons tag is the same as the trackid
                {
                    Track testTrack = new Track() { Name = item.Name, Artist = item.Artist, AudioFile = item.AudioFile, Duration = item.Duration, TrackId = item.TrackID };
                    mainWindowViewModel.PlayQueueController.AddTrack(testTrack); // adds the song to the queue
                    mainWindowViewModel.MyQueueScreenToViewModel.ShowElements();
                }

            }


        }

        private void Playlistboxes_SelectionChanged()
        {
            foreach (var item in AllTestTrack)
            {
                if (item.TrackID == _clickedButtonValue)
                {
                    for (int i = 0; i < mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.Count; i++)
                    {
                        if (_selectedItem.Equals(mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.ElementAt(i)))
                        {
                            Track testTrack = new Track() { Name = item.Name, Artist = item.Artist, AudioFile = item.AudioFile, Duration = item.Duration, TrackId = item.TrackID };
                            mainWindowViewModel.AllPlaylistsController.AllPlaylists.Playlists.ElementAt(i).Tracks.AddLast(testTrack);
                        }
                    }

                }

            }
            AllPlaylist.Clear();

        }
    }
    public class TestItems
    {
        public int ButtonID { get; set; } //composition of a type and an Index
        public int TrackID { get; set; }
        public String Name { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public int Duration { get; set; }
        public Uri AudioFile { get; set; }

    }
}

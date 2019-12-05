using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class PlaylistViewModel : Screen
    {
        #region boolForBoxes
        private bool _isChangeNameBoxOpen;

        public bool IsChangeNameBoxOpen
        {
            get { return _isChangeNameBoxOpen; }
            set { _isChangeNameBoxOpen = value;
                NotifyOfPropertyChange("IsChangeNameBoxOpen");
            }
        }


        private bool _isDeletePlaylistBoxOpen;

        public bool IsDeletePlaylistBoxOpen
        {
            get { return _isDeletePlaylistBoxOpen; }
            set
            {
                _isDeletePlaylistBoxOpen = value;
                NotifyOfPropertyChange("IsDeletePlaylistBoxOpen");
            }
        }
        #endregion

        #region StringForLabels
        private string _changeName;
        private string _errorMessage;
        private string _playlistName;
        public string ChangeName
        {
            get
            {
                if (_changeName == null)
                    _changeName = "";
                return _changeName;
            }
            set { _changeName = value;
                NotifyOfPropertyChange("ChangeName");
            }
        }

        public string ErrorMessage
        {
            get { if (_errorMessage == null)
                    _errorMessage = "";
                return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange("ErrorMessage");
            }
        }

        public string PlaylistName
        {
            get { return _playlistName; }
            set { _playlistName = value;
                NotifyOfPropertyChange("PlaylistName");
            }
        }
        #endregion

        #region ObservableCollections
        private ObservableCollection<Track> _playListTracksOC;

        public ObservableCollection<Track> PlayListTracksOC
        {
            get
            {
                if (_playListTracksOC == null)
                    _playListTracksOC = new ObservableCollection<Track>();
                return _playListTracksOC;
            }
            set { _playListTracksOC = value;
                NotifyOfPropertyChange("PlayListTracksOC");
            }
        }
        #endregion

        #region CommandForPopUpScreens
        private ICommand _openChangeNameCommand;
        public ICommand OpenChangeNameCommand
        {
            get
            {
                return _openChangeNameCommand;
            }
            set
            {
                _openChangeNameCommand = value;
                
            }
        }


        private ICommand _openDeletePlaylistCommand;
        public ICommand OpenDeletePlaylistCommand
        {
            get
            {
                return _openDeletePlaylistCommand;
            }
            set
            {
                _openDeletePlaylistCommand = value;

            }
        }
        #endregion

        #region Command
        private ICommand _deletePlaylistCommand;
        public ICommand DeletePlaylistCommand
        {
            get
            {
                return _deletePlaylistCommand;
            }
            set
            {
                _deletePlaylistCommand = value;
            }
        }
        
        private ICommand _changeNameCommand;
        public ICommand ChangeNameCommand
        {
            get
            {
                return _changeNameCommand;
            }
            set
            {
                _changeNameCommand = value;
                
            }
        }

        private ICommand _ascendingSortCommand;
        public ICommand AscendingSortCommand
        {
            get
            {
                return _ascendingSortCommand;
            }
            set
            {
                _ascendingSortCommand = value;

            }
        }

        private ICommand _descendingSortCommand;
        public ICommand DescendingSortCommand
        {
            get
            {
                return _descendingSortCommand;
            }
            set
            {
                _descendingSortCommand = value;

            }
        }

        #endregion

        public PlaylistViewModel()
        {
            TestMethode();
            InitializeCommands();
            PlaylistName = "test";

            
            
        }

        public void InitializeCommands()
        {
            _openChangeNameCommand = new RelayCommand(this.OpenChangeNameClick);
            _openDeletePlaylistCommand = new RelayCommand(this.OpenDeletePlaylistClick);
            _changeNameCommand = new RelayCommand<object>(this.ChangeNameClick);
            _deletePlaylistCommand = new RelayCommand<object>(this.DeletePlaylistClick);
            _ascendingSortCommand = new RelayCommand<object>(this.AscendingSortClick);
            _descendingSortCommand = new RelayCommand<object>(this.DescendingSortClick);
        }

        public void TestMethode()
        {
            PlayListTracksOC.Add(new Track
            {
                TrackId = 1,
                Album = "testAlbum",
                Artist = "Santi",
                Duration = 10,
                Name = "Track1",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });
            PlayListTracksOC.Add(new Track
            {
                TrackId = 2,
                Album = "testAlbum",
                Artist = "Tristan",
                Duration = 10,
                Name = "Track2",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });
            PlayListTracksOC.Add(new Track
            {
                TrackId = 3,
                Album = "testAlbum",
                Artist = "Stefan",
                Duration = 10,
                Name = "Track3",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });
            PlayListTracksOC.Add(new Track
            {
                TrackId = 4,
                Album = "testAlbum",
                Artist = "Susan",
                Duration = 10,
                Name = "Track4",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });
            PlayListTracksOC.Add(new Track
            {
                TrackId = 5,
                Album = "testAlbum",
                Artist = "Marloes",
                Duration = 10,
                Name = "Track5",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });

        }

        private void OpenChangeNameClick() //Change the name of the playlist
        {
            IsChangeNameBoxOpen = true;
        }

        public void OpenDeletePlaylistClick()
        {
            IsDeletePlaylistBoxOpen = true;
        }

        private void ChangeNameClick(object sender) //Change the name of the playlist
        {
            string action = (string)sender;

            if (action.Equals("Change"))
            {
                if (ChangeName.Equals(""))
                {
                    ErrorMessage = "Please write a name";
                }
                else if (ChangeName.Equals("SameName"))//dit moet nog aangepakt worden
                {
                    ErrorMessage = "The name must me a new name";
                }
                else
                {
                    if(ChangeName.Length >= 32){
                        ErrorMessage = "The name cannot be longer than 32 characters";
                    }
                    else
                    {
                        //change the name of the playlist
                        IsChangeNameBoxOpen = false;
                    }
                }
                
            }
            else if (action.Equals("Cancel"))
            {
                IsChangeNameBoxOpen = false;
            }
        }

        public void DeletePlaylistClick(object sender)
        {
            string action = (string)sender;

            if (action.Equals("Delete"))
            {
                //Playlist logico om de playlist te verwijderen
                IsDeletePlaylistBoxOpen = false;
            }
            else
            {
                IsDeletePlaylistBoxOpen = false;
            }
        }

        private void AscendingSortClick(object sender)
        {
            string orderBy = (string) sender;

            PlaylistName = orderBy + "ASC";
            //playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, orderBy, true);
            //ShowObjects();
        }

        private void DescendingSortClick(object sender)
        {
            string orderBy = (string)sender;

            PlaylistName = orderBy + "DES";
            //playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, orderBy, false);
            //ShowObjects();
        }


    }
}

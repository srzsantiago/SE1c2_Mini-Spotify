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
        public PlaylistController playlistController;
        public MainWindowViewModel mainWindow;

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
        private string _playlistCreationDate;
        private string _playlistDuration;

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


        public string PlaylistDuration
        {
            get { return _playlistDuration; }
            set { _playlistDuration = value; }
        }


        public string PlaylistCreationDate
        {
            get { return _playlistCreationDate; }
            set { _playlistCreationDate = value; }
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
        
        private ICommand _deleteTrackCommand;
        public ICommand DeleteTrackCommand
        {
            get
            {
                return _deleteTrackCommand;
            }
            set
            {
                _deleteTrackCommand = value;
            }
        }
        #endregion

        public PlaylistViewModel( MainWindowViewModel mainWindow ,Playlist playlist)
        {
            this.mainWindow = mainWindow;
            playlistController = new PlaylistController(playlist);
            LoadElements();
            LoadPlaylistInfo();
            InitializeCommands();

        }

        public void InitializeCommands()
        {
            _openChangeNameCommand = new RelayCommand(this.OpenChangeNameClick);
            _openDeletePlaylistCommand = new RelayCommand(this.OpenDeletePlaylistClick);
            _changeNameCommand = new RelayCommand<object>(this.ChangeNameClick);
            _deletePlaylistCommand = new RelayCommand<object>(this.DeletePlaylistClick);
            _ascendingSortCommand = new RelayCommand<object>(this.AscendingSortClick);
            _descendingSortCommand = new RelayCommand<object>(this.DescendingSortClick);
            _deleteTrackCommand = new RelayCommand<object>(this.DeleteTrackClick);
        }

        public void LoadPlaylistInfo()
        {
            PlaylistName = playlistController.Playlist.Name;
            PlaylistDuration = playlistController.Playlist.TrackListDuration.ToString();
            PlaylistCreationDate = playlistController.Playlist.CreationDate.ToString();
        }
        public void LoadElements()
        {
            PlayListTracksOC = new ObservableCollection<Track>();
            foreach (var item in playlistController.Playlist.Tracks)
            {
                PlayListTracksOC.Add(new Track
                {
                    TrackId = item.TrackId,
                    Name = item.Name,
                    Artist = item.Artist,
                    Album = item.Album,
                    Duration = item.Duration,
                    AudioFile = item.AudioFile,
                });
            }
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
                else if (ChangeName.Equals(PlaylistName))
                {
                    ErrorMessage = "The name must be a new name";
                }
                else
                {
                    if(ChangeName.Length >= 32){
                        ErrorMessage = "The name cannot be longer than 32 characters";
                    }
                    else
                    {
                        playlistController.SetName(ChangeName);
                        //must be changed in the database aswel
                        PlaylistName = ChangeName;
                        ChangeName = "";
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
                //Playlist logica om de playlist te verwijderen
                mainWindow.ChangeViewModel(mainWindow.HomeViewModel);
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

            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, orderBy, true);
            LoadElements();
        }

        private void DescendingSortClick(object sender)
        {
            string orderBy = (string)sender;

            playlistController.Playlist.SortTrackList(playlistController.Playlist.Tracks, orderBy, false);
            LoadElements();
        }

        public void DeleteTrackClick(object sender)
        {
            int index = (int)sender;
            int count = playlistController.Playlist.Tracks.Count;
            for (int i = 0; i < count; i++)
            {
                if (playlistController.Playlist.Tracks.ElementAt(i).TrackId == index)
                {
                    Track track=playlistController.Playlist.Tracks.ElementAt(i);
                    playlistController.RemoveTrack(track);
                    LoadElements();
                    break;
                }
            }
        }


    }
}

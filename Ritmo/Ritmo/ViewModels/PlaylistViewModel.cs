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
        public PlaylistController PlaylistController;
        public MainWindowViewModel MainWindow;

        #region boolForBoxes
        private bool _isChangeNameBoxOpen;

        public bool IsChangeNameBoxOpen
        {
            get { return _isChangeNameBoxOpen; }
            set { _isChangeNameBoxOpen = value;
                NotifyOfPropertyChange();
            }
        }


        private bool _isDeletePlaylistBoxOpen;

        public bool IsDeletePlaylistBoxOpen
        {
            get { return _isDeletePlaylistBoxOpen; }
            set
            {
                _isDeletePlaylistBoxOpen = value;
                NotifyOfPropertyChange();
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
                return _playListTracksOC;
            }
            set 
            { 
                _playListTracksOC = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region CommandForPopUpScreens
        public ICommand OpenChangeNameCommand { get; set; }

        public ICommand OpenDeletePlaylistCommand { get; set; }
       
        #endregion

        #region Command

        public ICommand DeletePlaylistCommand { get; set; }

        public ICommand ChangeNameCommand { get; set; }

        public ICommand AscendingSortCommand { get; set; }

        public ICommand DescendingSortCommand { get; set; }
        
        public ICommand DeleteTrackCommand { get; set; }
        #endregion

        public PlaylistViewModel( MainWindowViewModel mainWindow ,Playlist playlist)
        {
            MainWindow = mainWindow;
            PlaylistController = new PlaylistController(playlist);
            PlayListTracksOC = new ObservableCollection<Track>();

            LoadElements();
            LoadPlaylistInfo();
            InitializeCommands();

        }

        public void InitializeCommands()
        {
            OpenChangeNameCommand = new RelayCommand(OpenChangeNameClick);
            OpenDeletePlaylistCommand = new RelayCommand(OpenDeletePlaylistClick);
            ChangeNameCommand = new RelayCommand<object>(ChangeNameClick);
            DeletePlaylistCommand = new RelayCommand<object>(DeletePlaylistClick);
            AscendingSortCommand = new RelayCommand<object>(AscendingSortClick);
            DescendingSortCommand = new RelayCommand<object>(DescendingSortClick);
            DeleteTrackCommand = new RelayCommand<object>(DeleteTrackClick);
        }

        public void LoadPlaylistInfo()
        {
            PlaylistName = PlaylistController.Playlist.Name;
            PlaylistDuration = PlaylistController.Playlist.TrackListDuration.ToString();
            PlaylistCreationDate = PlaylistController.Playlist.CreationDate.ToString();
        }
        public void LoadElements()
        {
            PlayListTracksOC.Clear();

            foreach (var track in PlaylistController.Playlist.Tracks)
                PlayListTracksOC.Add(track);
        }


        private void OpenChangeNameClick() //Open popup grid to change the name of the playlist
        {
            IsChangeNameBoxOpen = true;
        }

        public void OpenDeletePlaylistClick()//Open popup grid to delete playlist
        {
            IsDeletePlaylistBoxOpen = true;
        }

        private void ChangeNameClick(object sender) //Change the name of the playlist
        {
            string action = (string)sender; //Sets the chosen action in the popup menu

            if (action.Equals("Change"))//User clicked Change button
            {
                if (ChangeName.Equals(""))//If name is an empty string
                {
                    ErrorMessage = "Please write a name";
                }
                else if (ChangeName.Equals(PlaylistName))//If the name didn't change
                {
                    ErrorMessage = "The name must be a new name";
                }
                else
                {
                    if(ChangeName.Length >= 32){//Name is longer than 32 characters
                        ErrorMessage = "The name cannot be longer than 32 characters";
                    }
                    else//Name fullfill all constrains 
                    {
                        PlaylistController.SetName(ChangeName);
                        //must be changed in the database aswel
                        PlaylistName = ChangeName;
                        ChangeName = "";
                        IsChangeNameBoxOpen = false;
                    }
                }
                
            }
            else if (action.Equals("Cancel")) //User clicked the Cancel button
            {
                IsChangeNameBoxOpen = false;
            }
        }

        public void DeletePlaylistClick(object sender)
        {
            string action = (string)sender; //Sets the chosen action in the popup menu

            if (action.Equals("Delete"))//user clicked delete button
            {
                //Playlist logica om de playlist te verwijderen
                Navigation.ToViewModel(MainWindow.HomeViewModel);
                IsDeletePlaylistBoxOpen = false;
            }
            else //User clicked cancel
            {
                IsDeletePlaylistBoxOpen = false;
            }
        }

        private void AscendingSortClick(object sender)//order playlist Ascending by <sender> as (string) column name
        {
            string orderBy = (string) sender;

            PlaylistController.Playlist.SortTrackList(PlaylistController.Playlist.Tracks, orderBy, true);
            LoadElements();
        }

        private void DescendingSortClick(object sender)//order playlist Descending by <sender> as (string) column name
        {
            string orderBy = (string)sender;

            PlaylistController.Playlist.SortTrackList(PlaylistController.Playlist.Tracks, orderBy, false);
            LoadElements();
        }

        public void DeleteTrackClick(object sender)
        {
            int index = (int)sender; //get the ID of the track to be deleted
            int count = PlaylistController.Playlist.Tracks.Count;
            for (int i = 0; i < count; i++)
            {
                if (PlaylistController.Playlist.Tracks.ElementAt(i).TrackId == index)//compare the index to all ID in the playlist collection
                {
                    Track track=PlaylistController.Playlist.Tracks.ElementAt(i);
                    PlaylistController.RemoveTrack(track);
                    LoadElements();
                    break;
                }
            }
        }


    }
}

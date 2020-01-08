using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Ritmo.Database;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class PopUpWindowViewModel : Screen
    {
        IWindowManager WindowManager = new WindowManager();
        MainWindowViewModel MainWindow;

        public Playlist Playlist;
        #region Command
        public ICommand OnOkayCommand { get; set; }

        #endregion

        #region Popupwindow Attributes
        public PlaylistViewModel _playlistviewmodel;
        private string _popupwarning;

        public int OkButtonWidth { get; set; }

        public int OkButtonHeight { get; set; }

        public string PopUpWarning
        {
            get
            {
                return _popupwarning;
            }
            set
            {
                _popupwarning = value;
                NotifyOfPropertyChange();
            }
        }

        public string Title { get; set; }

        public string TextInput { get; set; }

        public int TextBoxHeight { get; set; }

        public string ButtonContent { get; set; }

        public string TextMessage { get; set; }

        public AllPlaylistsViewModel AllPlaylistViewModel { get; set; }
        public PlaylistViewModel PlayListViewModel { get { return _playlistviewmodel; } set { _playlistviewmodel = value; } }

        public HomeViewModel HomeViewModel { get; set; }
        public int PlaylistID { get; set; }
        public int Trackid { get; set; }
        #endregion

        #region PlaylistViewModel constructors + click methods

        //Constructor to set the Add Playlist functionality
        public PopUpWindowViewModel(PlaylistViewModel viewModel)
        {
            ButtonContent = "Change name";
            TextMessage = "What name do you want to give the playlist";
            Title = "Change playlist name";
            TextBoxHeight = 40;
            OkButtonWidth = 110;
            OkButtonHeight = 35;

            PlayListViewModel = viewModel;
            PlaylistID = viewModel.PlaylistController.Playlist.TrackListID;
            OnOkayCommand = new RelayCommand<object>(ChangePlaylistNameClick);
        }

        //Constructor to set the Delete Playlist functionality
        public PopUpWindowViewModel(PlaylistViewModel viewModel, Playlist playlist, MainWindowViewModel mainwindow) // used when deleting a playlist from the inside of the playlist
        {
            ButtonContent = "Delete this playlist";
            Title = "Delete playlist";
            TextMessage = "Are you sure you want to delete this playlist?";
            PlayListViewModel = viewModel;
            Playlist = playlist;
            MainWindow = mainwindow;
            PlaylistID = viewModel.PlaylistController.Playlist.TrackListID;
            OkButtonWidth = 140;
            OkButtonHeight = 30;
            OnOkayCommand = new RelayCommand<object>(DeleteThisPlaylistClick);
        }

        public void ChangePlaylistNameClick(object param)
        {
            if (TextInput == null)
            {
                PopUpWarning = "Please insert a valid name"; // warning appears when the textbox was left empty
            }
            else if (TextInput.Length >= 32)
            {
                PopUpWarning = "Name can't be longer than 32 characters"; // warning appears when the name is 32 characters long
            }
            else if (TextInput.Equals(PlayListViewModel.PlaylistName)) //If the name didn't change
            {
                PopUpWarning = "The name can't be the same as the old name";
            }
            else
            {
                string sqlquery = $"UPDATE Playlist SET name = '{TextInput}' WHERE {PlaylistID} = idPlaylist"; // also updates the database name
                DatabaseConnector.UpdateQueryDB(sqlquery);
                PlayListViewModel.ChangeName(TextInput); // changes the name on the application
                TryClose();
            }
        }

        public void DeleteThisPlaylistClick(object param)
        {
            Navigation.ChangeViewModel(MainWindow.AllPlaylistsViewModel);
            Navigation.RemovePlaylistViewModel(PlaylistID); //Removes playlist from navigation
            AllPlaylistsViewModel.AllPlaylistsController.RemovePlaylist(Playlist);
            TryClose();
        }
        #endregion

        #region AllPlaylistsViewModel constructors + click methods
        // gets called when you want to delete a playlist
        public PopUpWindowViewModel(AllPlaylistsViewModel viewModel, int playlistID)
        {
            ButtonContent = "Delete playlist";
            TextMessage = "Are you sure you want to delete this playlist?";
            Title = "Delete playlist";
            OkButtonWidth = 110;
            OkButtonHeight = 30;

            AllPlaylistViewModel = viewModel;
            OnOkayCommand = new RelayCommand<object>(DeletePlaylistOnAllPlaylistsClick);
            PlaylistID = playlistID;
        }

        // gets called when you want to add a new playlist
        public PopUpWindowViewModel(AllPlaylistsViewModel viewModel, MainWindowViewModel mainWindow)
        {
            TextBoxHeight = 25;
            ButtonContent = "Add playlist";
            TextMessage = "Give the new playlist a name:";
            Title = "Add playlist";
            OkButtonWidth = 110;
            OkButtonHeight = 30;

            OnOkayCommand = new RelayCommand<object>(AddNewPlaylistClick);
            AllPlaylistViewModel = viewModel;
            MainWindow = mainWindow;
        }

        public void DeletePlaylistOnAllPlaylistsClick(object param) // the method that deletes the playlist
        {
            Playlist playlist = AllPlaylistsViewModel.AllPlaylistsController.GetPlaylist(PlaylistID); //Removes playlist with playlistID
            AllPlaylistsViewModel.AllPlaylistsController.RemovePlaylist(playlist); //Removes playlist with playlistID
            Navigation.RemovePlaylistViewModel(PlaylistID); //Removes playlist from navigationd
            TryClose();
        }

        public void AddNewPlaylistClick(object param) // the method that adds a new playlist
        {
            if (TextInput == null)
            {
                PopUpWarning = "Please insert a valid name"; // warning appears when the textbox was left empty
            }
            else if (TextInput.Length >= 32)
            {
                PopUpWarning = "Name can't be longer than 32 characters"; // warning appears when the name is 32 characters long
            }
            else
            {
                AllPlaylistsViewModel.AllPlaylistsController.AddTrackList(new Playlist(TextInput) { TrackListID = GetLastID(), OwnerID = MainWindow.User.User.ConsumerID }); //Create playlist and add it to all playlists
                TryClose();
            }
        }
        #endregion

        #region HomeViewModel constructors + click methods
        public PopUpWindowViewModel(HomeViewModel viewModel)
        {
            OkButtonHeight = 0;
            OkButtonWidth = 0;
            TextMessage = "You can't add 2 of the same tracks to one playlist";
            Title = "Error";
        }

        public PopUpWindowViewModel(HomeViewModel ViewModel, int trackid, MainWindowViewModel mainWindow)
        {
            TextBoxHeight = 25;
            ButtonContent = "Add playlist";
            TextMessage = "Give the new playlist a name:";
            Title = "Add playlist";
            OkButtonWidth = 110;
            OkButtonHeight = 30;

            MainWindow = mainWindow;
            HomeViewModel = ViewModel;
            Trackid = trackid;
            OnOkayCommand = new RelayCommand<object>(AddNewPlaylistWithTrackClick);
        }

        public void AddNewPlaylistWithTrackClick(object param) // the method that adds a new playlist with the song
        {
            if (TextInput == null)
            {
                PopUpWarning = "Please insert a valid name"; // warning appears when the textbox was left empty
            }
            else if (TextInput.Length >= 32)
            {
                PopUpWarning = "Name can't be longer than 32 characters"; // warning appears when the name is 32 characters long
            }
            else
            {
                Track test = HomeViewModel.GetTrackDB(Trackid);
                Playlist playlist = new Playlist(TextInput) { TrackListID = GetLastID(), OwnerID = MainWindow.User.User.ConsumerID };
                AllPlaylistsViewModel.AllPlaylistsController.AddTrackList(playlist); //Create playlist and add it to allplaylists
                foreach (var _playlist in AllPlaylistsViewModel.AllPlaylistsController.AllPlaylists.Playlists)
                {
                    if (_playlist.TrackListID == playlist.TrackListID)
                    {
                        string sql = $"INSERT INTO Track_has_Playlist VALUES ({test.TrackId}, {playlist.TrackListID} )"; // inserts the trackid and playlistid into the tabel
                        DatabaseConnector.InsertQueryDB(sql);
                    }
                }
                string message = "done";
                Messenger.Default.Send<string>(message);
                TryClose();
            }
        }

        #endregion

        #region methods

        public int GetLastID() // gets the last id from the playlistid column, then adds one to return the new id of the playlist.
        {

            int lastID = 0;
            string sqlquery = "SELECT IDENT_CURRENT('Playlist')"; // gets the last PK id from playlist
            List<Dictionary<string, object>> result = DatabaseConnector.SelectQueryDB(sqlquery); // executing the query

            foreach (var item in result)
            {
                foreach (var key in item)
                {
                    lastID = Convert.ToInt32(key.Value) + 1; // adds one to the last id
                }
            }
            return lastID;
        }
        #endregion
    }
}

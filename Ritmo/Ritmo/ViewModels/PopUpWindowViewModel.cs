using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class PopUpWindowViewModel : Screen
    {
        IWindowManager WindowManager = new WindowManager();
        MainWindowViewModel MainWindow;

        static Playlist Playlist;
        #region Command
        public ICommand OnOkayCommand { get; set; }

        #endregion

        #region Popupwindow Attributes
        private AllPlaylistsViewModel _allplaylistviewModel;
        public PlaylistViewModel _playlistviewmodel;
        private string _buttonContent;
        private string _textMessage;
        private int _textboxheight;
        private string _textinput;
        static int PlaylistID;
        private string _popupwarning;
        private string _title;

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

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string TextInput
        {
            get { return _textinput; }
            set { _textinput = value;}
        }

        public int TextBoxHeight
        {
            get { return _textboxheight; }
            set { _textboxheight = value; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
            set { _buttonContent = value; }
        }

        public string TextMessage
        {
            get { return _textMessage; }
            set { _textMessage = value; }
        }

        public AllPlaylistsViewModel allPlaylistViewModel
        {
            get { return _allplaylistviewModel; }
            set { _allplaylistviewModel = value; }
        }
        public PlaylistViewModel playListViewModel { get { return _playlistviewmodel; } set { _playlistviewmodel = value; } }

        #endregion

        #region PlaylistViewModel constructors + click methods

        public PopUpWindowViewModel(PlaylistViewModel viewModel)
        {
            ButtonContent = "Change name";
            TextMessage = "What name do you want to give the playlist";
            Title = "Change playlist name";
            TextBoxHeight = 25;
            
            playListViewModel = viewModel;
            PlaylistID = viewModel.PlaylistController.Playlist.TrackListID;
            OnOkayCommand = new RelayCommand<object>(ChangeNameClick);
        }

        public PopUpWindowViewModel(PlaylistViewModel viewModel, Playlist playlist, MainWindowViewModel mainwindow)
        {
            ButtonContent = "Delete this playlist";
            Title = "Delete playlist";
            playListViewModel = viewModel;
            Playlist = playlist;
            MainWindow = mainwindow;

            OnOkayCommand = new RelayCommand<object>(DeletePlaylistClick);
        }

        public void ChangeNameClick(object param)
        {
            if (TextInput == null)
            {
                PopUpWarning = "Please insert a valid name"; // warning appears when the textbox was left empty
            }
            else if (TextInput.Length >= 32)
            {
                PopUpWarning = "Name can't be longer than 32 characters"; // warning appears when the name is 32 characters long
            }
            else if (TextInput.Equals(playListViewModel.PlaylistName))//If the name didn't change
            {
                PopUpWarning = "The name can't be the same as the old name";
            }
            else
            {
                string sqlquery = $"UPDATE Playlist SET name = '{TextInput}' WHERE {PlaylistID} = idPlaylist"; // also updates the database name
                Database.DatabaseConnector.UpdateQueryDB(sqlquery);
                playListViewModel.PlaylistName = TextInput; // changes the name on the application
                this.TryClose();
            }
        }

        public void DeletePlaylistClick(object param)
        {
            MainWindow.ChangeViewModel(MainWindow.AllPlaylistsViewModel);
            AllPlaylistsViewModel.AllPlaylistsController.RemovePlaylist(Playlist);
            this.TryClose();
        }
        #endregion

        #region AllPlaylistsViewModel constructors + click methods
        public PopUpWindowViewModel(AllPlaylistsViewModel viewModel, int playlistID) // gets called when you want to delete a playlist
        {
            ButtonContent = "Delete playlist";
            TextMessage = "Are you sure you want to delete this playlist?";
            Title = "Delete playlist";

            allPlaylistViewModel = viewModel;
            OnOkayCommand = new RelayCommand<object>(OnOkayDeleteClick);
            PlaylistID = playlistID;
        }
        
        public PopUpWindowViewModel(AllPlaylistsViewModel viewModel) // gets called when you want to add a new playlist
        {
            TextBoxHeight = 25;
            ButtonContent = "Add playlist";
            TextMessage = "Give the new playlist a name:";
            Title = "Add playlist";

            OnOkayCommand = new RelayCommand<object>(OnOkayAddPlaylistClick);
            allPlaylistViewModel = viewModel;
        }

        public void OnOkayDeleteClick(object param) // the method that deletes the playlist
        {
            Playlist playlist = AllPlaylistsViewModel.AllPlaylistsController.GetPlaylist(PlaylistID); //Removes playlist with playlistID
            AllPlaylistsViewModel.AllPlaylistsController.RemovePlaylist(playlist); //Removes playlist with playlistID
            this.TryClose();
        }

        public void OnOkayAddPlaylistClick(object param) // the method that adds a new playlist
        {
                int lastID = 0;
                string sqlquery = "SELECT IDENT_CURRENT('Playlist')"; // gets the last PK id from playlist
                List<Dictionary<string, object>> result = Database.DatabaseConnector.SelectQueryDB(sqlquery); // executing the query

                foreach (var item in result)
                {
                    foreach (var key in item)
                    {
                        lastID = Convert.ToInt32(key.Value) + 1;
                    }
                }

            if (TextInput == null)
            {
                PopUpWarning = "Please insert a valid name"; // warning appears when the textbox was left empty
            } else if(TextInput.Length >= 32)
            {
                PopUpWarning = "Name can't be longer than 32 characters"; // warning appears when the name is 32 characters long
            } else
            {
                AllPlaylistsViewModel.AllPlaylistsController.AddTrackList(new Playlist(TextInput) { TrackListID = lastID }); //Create playlist and add it to all playlists
                this.TryClose();
            }
        }
        #endregion
    }
}

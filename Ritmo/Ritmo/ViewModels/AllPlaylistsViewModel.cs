using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class AllPlaylistsViewModel : Screen
    {
        #region View Attributes and View Methods
        public MainWindowViewModel MainWindow { get; set; }
        public PlaylistViewModel PlaylistViewModel { get; set; }

        //private bool _popUpIsOpen;
        //private string _popUpWarning;
        //private double _popUpSize = 120;
        //private string _newName;

        //public string NewName
        //{
        //    get { return _newName; }
        //    set
        //    {
        //        _newName = value;
        //        NotifyOfPropertyChange();
        //    }
        //}

        //public double PopUpSize
        //{
        //    get { return _popUpSize; }
        //    set
        //    {
        //        _popUpSize = value;
        //        NotifyOfPropertyChange();
        //    }
        //}

        //public string PopUpWarning
        //{
        //    get { return _popUpWarning; }
        //    set
        //    {
        //        _popUpWarning = value;
        //        NotifyOfPropertyChange();
        //    }
        //}

        //public bool PopUpIsOpen
        //{
        //    get { return _popUpIsOpen; }
        //    set
        //    {
        //        _popUpIsOpen = value;
        //        NotifyOfPropertyChange();
        //    }
        //}

        ////Sets popup to default state
        //private void PopUpMessage()
        //{
        //    PopUpSize = 120;
        //    PopUpWarning = "";
        //    NewName = "";
        //}

        ////Sets message for popup
        //private void PopUpMessage(string warning)
        //{
        //    PopUpSize = 140;
        //    PopUpWarning = warning;
        //}

        ////Enables or disable PopUpMenu
        //private void PopUpControl()
        //{
        //    if (PopUpIsOpen)
        //        PopUpIsOpen = false;
        //    else if (!PopUpIsOpen)
        //        PopUpIsOpen = true;

        //    PopUpMessage();
        //}
        #endregion

        #region AllPlaylist Functionality Attributes
        private ObservableCollection<Playlist> _allPlaylistsCollection = new ObservableCollection<Playlist>();
        private static AllPlaylistsController _allPlaylistsController;

        public ObservableCollection<Playlist> AllPlaylistsCollection
        {
            get { return _allPlaylistsCollection; }
            set
            {
                _allPlaylistsCollection = value;
                NotifyOfPropertyChange("AllPlaylistsCollection");
            }
        }

        public static AllPlaylistsController AllPlaylistsController
        {
            get { return _allPlaylistsController; }
            set
            {
                _allPlaylistsController = value;
            }
        }
        #endregion

        #region Commands
        public ICommand OpenPlaylistViewModelCommand { get; set; }
        public ICommand AddPlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        //public ICommand ControlPopUpCommand { get; set; }
        #endregion

        public AllPlaylistsViewModel(MainWindowViewModel mainWindow)
        {
            InitializeCommands();

            AllPlaylistsController = mainWindow.AllPlaylistsController;
            MainWindow = mainWindow;

            AllPlaylistsController.PlaylistDeleted += SetAllPlaylistsCollection; //Assigns method to event of playlist getting removed

            //TestMethod();
            SetAllPlaylistsCollection();
        }

        #region AllPlaylists methods
        //Gets every playlist from the AllPlaylists class and places them in AllPlaylists ObservableCollection
        public void SetAllPlaylistsCollection()
        {
            AllPlaylistsCollection.Clear();
            AllPlaylistsController.AllPlaylists.Playlists.ForEach(playlist => AllPlaylistsCollection.Add(playlist));
        }

        public void AddPlaylist(string name)
        {
            //int lastID = 0;
            //string sqlquery = "SELECT IDENT_CURRENT('Playlist')"; // gets the last PK id from playlist
            //List<Dictionary<string, object>> result = Database.DatabaseConnector.SelectQueryDB(sqlquery); // executing the query

            //foreach (var item in result)
            //{
            //    foreach (var key in item)
            //    {
            //        lastID = Convert.ToInt32(key.Value) + 1;
            //    }
            //}
            
            //if (name.Equals(""))
            //    PopUpMessage("Invalid Playlist name!");
            //else if (name.Length >= 32)
            //    PopUpMessage("Playlist name must be less than 32 characters!");
            //else
            //{
            //    AllPlaylistsController.AddTrackList(new Playlist(name) { TrackListID = lastID }); //Create playlist and add it to all playlists
            //    SetAllPlaylistsCollection(); //Updates view
            //    //PopUpControl(); //Hides popup menu
            //}

            IWindowManager windowManager = new WindowManager();
            windowManager.ShowDialog(new PopUpWindowViewModel(this));
            SetAllPlaylistsCollection();
        }

        private void DeletePlaylist(int playlistID)
        {
            IWindowManager windowManager = new WindowManager();
            windowManager.ShowDialog(new PopUpWindowViewModel(this, playlistID));
        }
        #endregion        

        #region Initialize Methods
        private void InitializeCommands()
        {
            OpenPlaylistViewModelCommand = new RelayCommand<int>(OpenPlaylistViewModel);
            AddPlaylistCommand = new RelayCommand<string>(AddPlaylist);
            DeletePlaylistCommand = new RelayCommand<int>(DeletePlaylist);
            //ControlPopUpCommand = new RelayCommand(PopUpControl);
        }
        #endregion

        //Sets CurrentViewModel in the MainWindow to a PlaylistViewModel
        private void OpenPlaylistViewModel(int playlistID)
        {
            Playlist playlist = AllPlaylistsController.GetPlaylist(playlistID);

            Navigation.ToClickedViewModel(new PlaylistViewModel(MainWindow, playlist));
        }

        private void TestMethod()
        {
            Playlist kaas = new Playlist("Kaas") { CreationDate = DateTime.Now, TrackListID = 0, TrackListDuration = 3 };
            Playlist hamkaas = new Playlist("HamKaas") { CreationDate = DateTime.Now, TrackListID = 1, TrackListDuration = 6 };
            Playlist dorito = new Playlist("Dorito") { CreationDate = DateTime.Now, TrackListID = 2, TrackListDuration = 4 };

            AllPlaylistsController.AddTrackList(kaas);
            AllPlaylistsController.AddTrackList(hamkaas);
            AllPlaylistsController.AddTrackList(dorito);
        }
    }
}
using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Ritmo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class AllPlaylistsViewModel : Screen
    {
        public MainWindowViewModel MainWindow { get; set; }

        #region AllPlaylist Attributes
        private ObservableCollection<Playlist> _allPlaylistsCollection = new ObservableCollection<Playlist>();
        private static AllPlaylistsController _allPlaylistsController;
        private string _newName;
        private bool _popUpIsOpen;

        public bool PopUpIsOpen
        {
            get { return _popUpIsOpen; }
            set { _popUpIsOpen = value;
                NotifyOfPropertyChange();
            }
        }


        public string NewName
        {
            get { return _newName; }
            set { _newName = value; }
        }

        public ObservableCollection<Playlist> AllPlaylistsCollection
        {
            get { return _allPlaylistsCollection; }
            set { _allPlaylistsCollection = value;
                NotifyOfPropertyChange("AllPlaylistsCollection");
            }
        }

        public static AllPlaylistsController AllPlaylistsController
        {
            get { return _allPlaylistsController; }
            set { _allPlaylistsController = value; }
        }
        #endregion

        #region Commands
        public ICommand OpenPlaylistViewModelCommand { get; set; }
        public ICommand AddPlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        public ICommand ControlPopUpCommand { get; set; }
        #endregion

        public AllPlaylistsViewModel() //MainWindowViewModel mainWindow
        {
            InitializeCommands();
            
            AllPlaylistsController = new AllPlaylistsController();
            //MainWindow = mainWindow;

            TestMethod();

            SetAllPlaylistsCollection();         
        }

        //Gets every playlist from the AllPlaylists class and places them in AllPlaylists ObservableCollection
        private void SetAllPlaylistsCollection()
        {
            AllPlaylistsCollection.Clear();
            AllPlaylistsController.AllPlaylists.Playlists.ForEach(playlist => AllPlaylistsCollection.Add(playlist));
        }

        //Sets CurrentViewModel in the MainWindow to a PlaylistViewModel
        private void OpenPlaylistViewModel(int playlistID)
        {
            Console.WriteLine(playlistID);
            MainWindow.ChangeViewModel(new PlaylistViewModel());
        }

        public void AddPlaylist(string name)
        {
            //Check database for ID's to ascertain which ID to use
            int id = AllPlaylistsCollection.Count();
            //This is for testing

            AllPlaylistsController.AddTrackList(new Playlist($"{name}") { TrackListID = id, CreationDate = DateTime.Now} );
            SetAllPlaylistsCollection();
            ControlPopUp();
        }

        private void DeletePlaylist(int playlistID)
        {
            AllPlaylistsController.RemovePlaylist(AllPlaylistsController.GetPlaylist(playlistID));
        }

        private void ControlPopUp()
        {

            //new Window() { Content = new PopUpWindowViewModel(this) }.Show();

            //IWindowManager windowManager = new WindowManager();
            //windowManager.ShowDialog(new PopUpWindowViewModel(this));

            //MessageBox.Show(new PopUpWindowView(), "kaas");
            //Messenger.Default.Send(new NotificationMessage("ShowAddPlaylist"));

            if (PopUpIsOpen)
                PopUpIsOpen = false;
            else if (!PopUpIsOpen)
                PopUpIsOpen = true;
        }
        

        #region Initialize Methods
        private void InitializeCommands()
        {
            OpenPlaylistViewModelCommand = new RelayCommand<int>(OpenPlaylistViewModel);
            AddPlaylistCommand = new RelayCommand<string>(AddPlaylist);
            DeletePlaylistCommand = new RelayCommand<int>(DeletePlaylist);
            ControlPopUpCommand = new RelayCommand(ControlPopUp);
        }
        #endregion

        private void TestMethod()
        {
            Playlist kaas = new Playlist("Kaas") { CreationDate = DateTime.Now, TrackListID = 1, TrackListDuration=3 };
            Playlist hamkaas = new Playlist("HamKaas") { CreationDate = DateTime.Now, TrackListID = 1, TrackListDuration = 6 };
            Playlist dorito = new Playlist("Dorito") { CreationDate = DateTime.Now, TrackListID = 1, TrackListDuration = 4 };

            AllPlaylistsController.AddTrackList(kaas);
            AllPlaylistsController.AddTrackList(hamkaas);
            AllPlaylistsController.AddTrackList(dorito);
        }
    }
}

using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class AllPlaylistsViewModel : Screen
    {
        public MainWindowViewModel MainWindow { get; set; }

        #region AllPlaylist Attributes
        private ObservableCollection<Playlist> _allPlaylistsCollection = new ObservableCollection<Playlist>();
        private static AllPlaylistsController _allPlaylistsController;

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
        public ICommand DeletePlaylistCommand { get; set; }
        #endregion

        public AllPlaylistsViewModel(MainWindowViewModel mainWindow) 
        {
            InitializeCommands();
            
            AllPlaylistsController = new AllPlaylistsController();
            MainWindow = mainWindow;

            TestMethod();

            SetAllPlaylistsCollection();         
        }

        //Gets every playlist from the AllPlaylists class and places them in AllPlaylists ObservableCollection
        private void SetAllPlaylistsCollection()
        {
            AllPlaylistsController.AllPlaylists.Playlists.ForEach(playlist => AllPlaylistsCollection.Add(playlist));
        }

        //Sets CurrentViewModel in the MainWindow to a PlaylistViewModel
        public void OpenPlaylistViewModel(int playlistID)
        {
            Console.WriteLine(playlistID);
            MainWindow.ChangeViewModel(new PlaylistViewModel());
        }

        public void DeletePlaylist(int playlistID)
        {
            
        }

        

        #region Initialize Methods
        private void InitializeCommands()
        {
            OpenPlaylistViewModelCommand = new RelayCommand<int>(OpenPlaylistViewModel);
        }
        #endregion

        private void TestMethod()
        {
            Playlist kaas = new Playlist("Kaas");
            Playlist hamkaas = new Playlist("HamKaas");
            Playlist dorito = new Playlist("Dorito");

            AllPlaylistsController.AddTrackList(kaas);
            AllPlaylistsController.AddTrackList(hamkaas);
            AllPlaylistsController.AddTrackList(dorito);
        }
    }
}

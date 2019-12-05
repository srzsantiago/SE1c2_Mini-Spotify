﻿using Caliburn.Micro;
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
        #region View Attributes
        public MainWindowViewModel MainWindow { get; set; }

        private bool _popUpIsOpen;

        public bool PopUpIsOpen
        {
            get { return _popUpIsOpen; }
            set
            {
                _popUpIsOpen = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        #region AllPlaylist Functionality Attributes
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
            set { _allPlaylistsController = value; 
            }
        }
        #endregion

        #region Commands
        public ICommand OpenPlaylistViewModelCommand { get; set; }
        public ICommand AddPlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        public ICommand ControlPopUpCommand { get; set; }
        #endregion

        public AllPlaylistsViewModel()//MainWindowViewModel mainWindow
        {
            InitializeCommands();
            
            AllPlaylistsController = new AllPlaylistsController();
            //MainWindow = mainWindow;

            //TestMethod();

            SetAllPlaylistsCollection();         
        }

        #region AllPlaylists methods
        //Gets every playlist from the AllPlaylists class and places them in AllPlaylists ObservableCollection
        private void SetAllPlaylistsCollection()
        {
            AllPlaylistsCollection.Clear();
            AllPlaylistsController.AllPlaylists.Playlists.ForEach(playlist => AllPlaylistsCollection.Add(playlist));
        }

        public void AddPlaylist(string name)
        {
            //Check database for ID's to ascertain which ID to use
            int id = AllPlaylistsCollection.Count();
            //This is for testing

            AllPlaylistsController.AddTrackList(new Playlist($"{name}") { TrackListID = id, CreationDate = DateTime.Now} ); //Create playlist and add it to all playlists
            SetAllPlaylistsCollection(); //Updates view
            ControlPopUp(); //Hides popup menu
        }

        private void DeletePlaylist(int playlistID)
        {
            AllPlaylistsController.RemovePlaylist(AllPlaylistsController.GetPlaylist(playlistID)); //Removes playlist with playlistID
            SetAllPlaylistsCollection(); //Updates view
        }
        #endregion        

        #region Initialize Methods
        private void InitializeCommands()
        {
            OpenPlaylistViewModelCommand = new RelayCommand<int>(OpenPlaylistViewModel);
            AddPlaylistCommand = new RelayCommand<string>(AddPlaylist);
            DeletePlaylistCommand = new RelayCommand<int>(DeletePlaylist);
            ControlPopUpCommand = new RelayCommand(ControlPopUp);
        }
        #endregion

        //Sets CurrentViewModel in the MainWindow to a PlaylistViewModel
        private void OpenPlaylistViewModel(int playlistID)
        {
            Playlist playlist = AllPlaylistsController.GetPlaylist(playlistID);

            MainWindow.ChangeViewModel(new PlaylistViewModel()); 
        }

        //Enables or disable PopUpMenu
        private void ControlPopUp()
        {
            if (PopUpIsOpen)
                PopUpIsOpen = false;
            else if (!PopUpIsOpen)
                PopUpIsOpen = true;
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

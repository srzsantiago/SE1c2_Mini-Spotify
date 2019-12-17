﻿using Caliburn.Micro;
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
        #endregion

        public AllPlaylistsViewModel() { }

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
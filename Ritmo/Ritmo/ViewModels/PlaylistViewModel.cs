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

        #region StringForLabels
        private static string _playlistName;
        private string _playlistCreationDate;
        private string _playlistDuration;

        public string PlaylistName
        {
            get { return _playlistName; }
            set { _playlistName = value;
                NotifyOfPropertyChange();
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

        #region Command

        public ICommand DeletePlaylistCommand { get; set; }

        public ICommand ChangeNameCommand { get; set; }

        public ICommand AscendingSortCommand { get; set; }

        public ICommand DescendingSortCommand { get; set; }
        
        public ICommand DeleteTrackCommand { get; set; }
        #endregion

        public PlaylistViewModel()
        {

        }

        public PlaylistViewModel(MainWindowViewModel mainWindow ,Playlist playlist)
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

        private void ChangeNameClick(object sender) //Change the name of the playlist
        {
            IWindowManager windowManager = new WindowManager();
            windowManager.ShowDialog(new PopUpWindowViewModel(this));
        }

        //Changes name of playlist
        public void ChangeName(string name)
        {
            PlaylistController.SetName(name);
            PlaylistName = name;
        }

        public void DeletePlaylistClick(object sender)
        {
            IWindowManager windowManager = new WindowManager();
            windowManager.ShowDialog(new PopUpWindowViewModel(this, this.PlaylistController.Playlist, MainWindow));
            NotifyOfPropertyChange();
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
                    Track track = PlaylistController.Playlist.Tracks.ElementAt(i);
                    PlaylistController.RemoveTrack(track);
                    LoadElements();
                    break;
                }
            }
        }


    }
}

using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using Ritmo.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class PlaylistViewModel : Screen
    {
        public PlaylistController PlaylistController;
        public MainWindowViewModel MainWindow;
        bool isLoaded = false;

        #region StringForLabels
        private static string _playlistName;

        public string PlaylistName
        {
            get { return _playlistName; }
            set
            {
                _playlistName = value;
                NotifyOfPropertyChange();
            }
        }

        public string PlaylistDuration { get; set; }

        public string PlaylistCreationDate { get; set; }
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

        #region Commands

        public ICommand DeletePlaylistCommand { get; set; }

        public ICommand ChangeNameCommand { get; set; }

        public ICommand AscendingSortCommand { get; set; }

        public ICommand DescendingSortCommand { get; set; }

        public ICommand DeleteTrackCommand { get; set; }
        #endregion

        public PlaylistViewModel() { }

        public PlaylistViewModel(Playlist playlist)
        {
            PlaylistController = new PlaylistController(playlist);
            PlayListTracksOC = new ObservableCollection<Track>();

            LoadElements();
            LoadPlaylistInfo();
            InitializeCommands();
        }

        public PlaylistViewModel(MainWindowViewModel mainWindow, Playlist playlist) : this(playlist)
        {
            MainWindow = mainWindow;
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

            if (!isLoaded)
            {
                int count = 0;
                string sql = $"SELECT idTrack, title, duration FROM Track WHERE idTrack IN (SELECT trackID FROM Track_has_Playlist WHERE playlistID = {PlaylistController.Playlist.TrackListID})";
                List<Dictionary<string, object>> tracks = DatabaseConnector.SelectQueryDB(sql);

                int trackid = 0;
                string name = "";
                int duration = 0;

                foreach (var dictionary in tracks)
                {
                    foreach (var key in dictionary)
                    {
                        if (key.Key.Equals("idTrack"))
                        {
                            trackid = (int)key.Value;
                            count++;
                        }
                        else if (key.Key.Equals("title"))
                        {
                            name = (string)key.Value;
                            count++;
                        }
                        else if (key.Key.Equals("duration"))
                        {
                            duration = (int)key.Value;
                            count++;
                        }
                    }
                    if (count % 3 == 0)
                    {
                        Track track = new Track() { TrackId = trackid, Name = name, Duration = duration };
                        PlayListTracksOC.Add(track);
                        if (!PlaylistController.Playlist.Tracks.Any(item => item.TrackId == track.TrackId))
                        {
                            PlaylistController.Playlist.Tracks.AddLast(track);
                        }

                    }
                }
                isLoaded = true;
            }
            else
            {
                foreach (var item in PlaylistController.Playlist.Tracks)
                {
                    if (!PlayListTracksOC.Contains(item))
                    {
                        PlayListTracksOC.Add(item);
                    }

                }
            }
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
            windowManager.ShowDialog(new PopUpWindowViewModel(this, PlaylistController.Playlist, MainWindow));
            NotifyOfPropertyChange();
        }

        private void AscendingSortClick(object sender)//order playlist Ascending by <sender> as (string) column name
        {
            string orderBy = (string)sender;
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
            int count = PlayListTracksOC.Count;
            for (int i = 0; i < count; i++)
            {
                if (PlayListTracksOC.ElementAt(i).TrackId == index)//compare the index to all ID in the playlist collection
                {
                    Track track = PlayListTracksOC.ElementAt(i);
                    PlaylistController.RemoveTrack(track); // removes the track out of the database
                    LoadElements(); // refreshes the GUI
                    break;
                }
            }
        }


    }
}

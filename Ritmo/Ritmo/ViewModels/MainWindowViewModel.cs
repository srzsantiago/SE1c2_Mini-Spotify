using Caliburn.Micro;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ritmo;



namespace Ritmo.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public PlaylistController PlaylistController = new PlaylistController("TestPlaylist");
        public AllPlaylistsController AllPlaylistsController = new AllPlaylistsController();
        public PlayQueueController PlayQueueController = new PlayQueueController();
        public MyQueueViewModel MyQueueScreenToViewModel;


        #region Commands
        public ICommand ChangeViewModelCommand { get; set; }
        #endregion

        #region ViewModel attributes
        private Screen _currentViewModel;
        public Screen HomeViewModel { get; set; }
        public Screen SearchViewModel { get; set; } = new SearchViewModel();
        public Screen CategoriesViewModel { get; set; } = new CategoriesViewModel();
        public Screen FollowingViewModel { get; set; } = new FollowingViewModel();
        public Screen AllPlaylistsViewModel { get; set; } 
        public Screen MyQueueViewModel { get; set; }

        public Screen CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region CurrentTrack commands and attributes
        public ICommand PlayTrackCommand { get; set; }
        public ICommand NextTrackCommand { get; set; }
        public ICommand PrevTrackCommand { get; set; }
        public ICommand ShuffleWaitinglistCommand { get; set; }

        private MediaElement _currentTrackElement = new MediaElement() { LoadedBehavior = MediaState.Manual};
        private Uri _currentTrackSource; //Unused
        private double _currentTrackVolume = 0.5;
        private Uri _playButtonIcon = new Uri("/ImageResources/playicon.ico", UriKind.RelativeOrAbsolute);

        public MediaElement CurrentTrackElement
        {
            get { return _currentTrackElement; }
            set { _currentTrackElement = value; }
        }
        public Uri CurrentTrackSource
        {
            get { return _currentTrackSource; }
            set { _currentTrackSource = value; }
        } //Unused
        public double CurrentTrackVolume
        {
            get { return _currentTrackVolume; }
            set 
            { 
                _currentTrackVolume = value;
                ChangeMediaVolume(value);
            }
        }
 
        public Uri PlayButtonIcon { get { return _playButtonIcon; } set { _playButtonIcon = value; NotifyOfPropertyChange(); } }
        #endregion

        public MainWindowViewModel()
        {
            InitializeCommands();
            InitializeViewModels();
            InitializeCurrentTrackElement();

            TestTrackMethod();
        }

        //The methods that control or interact with the CurrentTrackElement
        #region CurrentTrackElement Methods
        public void PlayTrack()
        {
            if (CurrentTrackElement.IsLoaded)
            {
                CurrentTrackElement.Play();
                PlayButtonIcon = new Uri(@"\ImageResources\pauseicon.ico", UriKind.Relative);
            }
        }

        //Pauses track and updates play/pausebutton
        public void PauseTrack()
        {
            if (PlayQueueController.PQ.TrackWaitingListEnded)
            {
                CurrentTrackElement.Pause();
                PlayButtonIcon = new Uri(@"\ImageResources\playicon.ico", UriKind.Relative);
            }
        }

        //Changes to next track, set CurrentTrackElement and plays track.
        public void NextTrack()
        {
            PlayQueueController.NextTrack();
            MyQueueScreenToViewModel.ShowElements();
            CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;
            
            if (!PlayQueueController.PQ.TrackWaitingListEnded)
                PlayTrack();
            //else
            //    PauseTrack(); //Bugs out Queue. Now TrackWaitingList will not pause when it's finished.
        }

        ////Changes to the previous track and set CurrentTrackElement
        public void PrevTrack()
        {
            //Checks if CurrentTrack is the first. If it is, it nothing will happen. 
            //Call rewind track here
            if (PlayQueueController.PQ.CurrentTrack.Equals(PlayQueueController.PQ.TrackWaitingList.First.Value)) { }
            else
            {
                PlayQueueController.PreviousTrack();
                MyQueueScreenToViewModel.ShowElements();
                CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;
                PlayTrack();
            }
            
        }

        public void ShuffleWaitinglist()
        {
            PlayQueueController.ShuffleTrackWaitingList();
            MyQueueScreenToViewModel.ShowElements();
        }

        //Runs when the track has ended. The next track will be loaded and played.
        //It's assigned to CurrentTrackElement.MediaEnded in the MainWindowViewModel constructor
        //If the playQueue has played all tracks, CurrentTrack will be set to the first Track in TrackWaitingList and the audio will be paused.
        public void Track_Ended(Object sender, EventArgs e)
        {
            NextTrack();
            PauseTrack();
        }

        //Changes volume based on slider. 0 is muted and 1 is the highest volume.
        public void ChangeMediaVolume(double VolumeSliderValue)
        {
            CurrentTrackElement.Volume = VolumeSliderValue; // gets the slider value and puts it as volume
        }

        #endregion
       
        //Initialize-methods that are used in the constructor of MainWindowViewModel
        #region Initializer methods
        public void InitializeCommands()
        {
            ChangeViewModelCommand = new RelayCommand<Screen>(ChangeViewModel); //Sets the command to the corresponding method
            PlayTrackCommand = new RelayCommand(PlayTrack);
            NextTrackCommand = new RelayCommand(NextTrack);
            PrevTrackCommand = new RelayCommand(PrevTrack);
            ShuffleWaitinglistCommand = new RelayCommand(ShuffleWaitinglist);
        }
        public void InitializeViewModels()
        {
            HomeViewModel = new HomeViewModel(this);
            CurrentViewModel = HomeViewModel;
            AllPlaylistsViewModel = new AllPlaylistsViewModel(this);
            MyQueueViewModel = new MyQueueViewModel(this);
            MyQueueScreenToViewModel = (MyQueueViewModel)MyQueueViewModel;
        }
        public void InitializeCurrentTrackElement()
        {
            //CurrentTrackElement.Source = CurrentTrackSource;
            CurrentTrackElement.MediaEnded += Track_Ended;
            CurrentTrackElement.Volume = CurrentTrackVolume;
        }
        #endregion

        //Changes CurrentViewModel and sets the frame
        public void ChangeViewModel(Screen ViewModel)
        {
            this.CurrentViewModel = ViewModel;
        }

        public void TestTrackMethod()
        {
            
            Track testTrack1 = new Track() {
                TrackId = 1,
                Name = "RingtoneUnatco",
                Artist = "Santi",
                Duration = 120,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneUnatco.mp3"),
            };
            Track testTrack2 = new Track()
            {
                TrackId = 2,
                Name = "RingtoneRoundabout",
                Artist = "Dio",
                Duration = 90,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            };
            Track testTrack3 = new Track()
            {
                TrackId = 3,
                Name = "Powerup1",
                Artist = "Tristan",
                Duration = 100,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack4 = new Track()
            {
                TrackId = 4,
                Name = "Powerup2",
                Artist = "Marloes",
                Duration = 70,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack5 = new Track()
            {
                TrackId = 5,
                Name = "Powerup2",
                Artist = "Susan",
                Duration = 70,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack6 = new Track()
            {
                TrackId = 5,
                Name = "Gun's Roses",
                Artist = "Susan",
                Duration = 30,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack7 = new Track()
            {
                TrackId = 5,
                Name = "Metal Gear",
                Artist = "Solid Snake",
                Duration = 90,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\MetalGearSolid.mp3"),
            };
            //Track testTrack4 = new Track() { AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Powerup1.wav") };
            PlaylistController.AddTrack(testTrack1);
            PlaylistController.AddTrack(testTrack2);
            PlaylistController.AddTrack(testTrack6);
            PlaylistController.AddTrack(testTrack5);
            PlaylistController.AddTrack(testTrack7);


            PlayQueueController.AddTrack(testTrack3);
            PlayQueueController.AddTrack(testTrack4);
            
            


            //Speelt track en zet playlist in wachtrij
            PlayQueueController.PlayTrack(PlaylistController.Playlist.Tracks.First.Value, PlaylistController.Playlist);
            
            //Zet de CurrentTrack als audio die afgespeeld wordt
            CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;             
            
            MyQueueScreenToViewModel.ShowElements();
        }
    }
}

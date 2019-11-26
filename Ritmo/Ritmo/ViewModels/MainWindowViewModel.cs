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
        PlaylistController PlaylistController = new PlaylistController("TestPlaylist");
        PlayQueueController PlayQueueController = new PlayQueueController();


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
        public Screen MyQueueViewModel { get; set; } = new MyQueueViewModel();

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

        private MediaElement _currentTrackElement = new MediaElement() { LoadedBehavior = MediaState.Manual};
        private Uri _currentTrackSource; //Unused
        private double _currentTrackVolume = 0.5;
        private Uri _playButtonIcon = new Uri("/ImageResources/playbutton.png", UriKind.RelativeOrAbsolute);

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
                PlayButtonIcon = new Uri(@"\ImageResources\pausebutton.png", UriKind.Relative);
            }
        }

        //Pauses track and updates play/pausebutton
        public void PauseTrack()
        {
            if (PlayQueueController.PQ.TrackWaitingListEnded)
            {
                CurrentTrackElement.Pause();
                PlayButtonIcon = new Uri(@"\ImageResources\playbutton.png", UriKind.Relative);
            }
        }

        //Changes to next track, set CurrentTrackElement and plays track.
        public void NextTrack()
        {
            PlayQueueController.NextTrack();
            CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;
            if (!PlayQueueController.PQ.TrackWaitingListEnded)
            {
                PlayTrack();
            }
        }

        ////Changes to the previous track and set CurrentTrackElement
        public void PrevTrack()
        {
            PlayQueueController.PreviousTrack();
            CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;
            PlayTrack();
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
        }
        public void InitializeViewModels()
        {
            HomeViewModel = new HomeViewModel();
            CurrentViewModel = HomeViewModel;
            AllPlaylistsViewModel = new AllPlaylistsViewModel(this);
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
            //Een test playlist
            //Track testTrack1 = new Track() { AudioFile = new Uri(@"C:\RingtoneUnatco.mp3") };
            Track testTrack1 = new Track() { AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneUnatco.mp3") };
            Track testTrack2 = new Track() { AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3") };
            Track testTrack3 = new Track() { AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Powerup2.wav") };
            Track testTrack4 = new Track() { AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Powerup1.wav") };
            PlaylistController.AddTrack(testTrack1);
            PlaylistController.AddTrack(testTrack2);
            PlaylistController.AddTrack(testTrack3);
            PlaylistController.AddTrack(testTrack4);

            //Speelt track en zet playlist in wachtrij
            PlayQueueController.PlayTrack(PlaylistController.Playlist.Tracks.First.Value, PlaylistController.Playlist);

            //Zet de CurrentTrack als audio die afgespeeld wordt
            CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;

        }
    }
}

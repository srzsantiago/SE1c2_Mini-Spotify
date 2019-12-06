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
        public PlayQueueController PlayQueueController = new PlayQueueController();
        public AllPlaylistsController AllPlaylistsController = new AllPlaylistsController();

        public PlayQueue PlayQueue = new PlayQueue();

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
        public Screen PlaylistViewModel { get; set; } 

               
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
        public ICommand TrackControlCommand { get; set; }
        public ICommand NextTrackCommand { get; set; }
        public ICommand PrevTrackCommand { get; set; }
        public ICommand MuteTrackCommand { get; set; }
        public ICommand ShuffleWaitinglistCommand { get; set; }
        public ICommand LoopCommand { get; set; }

        private MediaElement _currentTrackElement = new MediaElement() { LoadedBehavior = MediaState.Manual };
        private Uri _currentTrackSource; //Unused
        private Double _currentTrackVolume;
        private Uri _playButtonIcon = new Uri("/ImageResources/playicon.ico", UriKind.RelativeOrAbsolute);
        private Uri _muteButtonIcon = new Uri("/ImageResources/unmute.png", UriKind.RelativeOrAbsolute);

        private Uri _repeatModeIcon = new Uri("/ImageResources/loopOff.png", UriKind.RelativeOrAbsolute);

        private Uri _shuffleButtonIcon = new Uri("/ImageResources/unshuffle.png", UriKind.RelativeOrAbsolute);
        private double oldVolume = 0;

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
        public Double CurrentTrackVolume
        {
            get { return _currentTrackVolume; }
            set
            {
                _currentTrackVolume = value;
                VolumeSlider_ValueChanged(value);
            }
        }

        public Uri PlayButtonIcon { get { return _playButtonIcon; } set { _playButtonIcon = value; NotifyOfPropertyChange(); } }

        public Uri MuteButtonIcon { get { return _muteButtonIcon; } set { _muteButtonIcon = value; NotifyOfPropertyChange(); } }


        public Uri RepeatModeIcon { get { return _repeatModeIcon; } set { _repeatModeIcon = value; NotifyOfPropertyChange("RepeatModeIcon"); } }

        public Uri ShuffleButtonIcon { get { return _shuffleButtonIcon; } set { _shuffleButtonIcon = value; NotifyOfPropertyChange(); } }

        #endregion

        public MainWindowViewModel()
        {
            InitializeCommands();
            InitializeViewModels();
            

            PlayQueue = PlayQueueController.PQ;

            InitializeCurrentTrackElement();

            TestTrackMethod();
            PlaylistViewModel = new PlaylistViewModel(this,PlaylistController.Playlist);
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
            CurrentTrackElement.Pause();
            PlayButtonIcon = new Uri(@"\ImageResources\playicon.ico", UriKind.Relative);
        }

        //Pauses track when the WaitingList has ended.
        private void PauseTrackOnWaitingListEnd()
        {
            if (PlayQueueController.PQ.TrackWaitingListEnded)
            {
                PauseTrack();
            }
        }

        //Alternates between pausing and playing track
        public void TrackControl()
        {
            if (!PlayQueueController.PQ.IsPaused)
            {
                PauseTrack();
            }
            else
                PlayTrack();
            PlayQueueController.PauseTrack();
        }

        //Changes to next track, set CurrentTrackElement and plays track.
        public void NextTrack()
        {
            PlayQueueController.NextTrack();
            MyQueueScreenToViewModel.ShowElements();
            CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;

            if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackRepeat)
            {
                RepeatModeIcon = new Uri("/ImageResources/LoopTrackWaitingList.png", UriKind.RelativeOrAbsolute);
                PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                PlayQueue.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                NextTrack();
            }
            else if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.Off)
            {
                PauseTrackOnWaitingListEnd();
            } 
            else if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackListRepeat)
            {
                PlayQueueController.PQ.TrackWaitingListEnded = false;
            }

            if (!PlayQueueController.PQ.TrackWaitingListEnded)
                PlayTrack();
            else
                PauseTrack(); //Bugs out Queue. Now TrackWaitingList will not pause when it's finished
        }

        //Changes to the previous track and set CurrentTrackElement
        public void PrevTrack()
        {
            TimeSpan timer = new TimeSpan(0, 0, 3); //Set timer for 3 seconds
            if (CurrentTrackElement.Position <= timer) //If the song is under 3 seconds, go to the previous song
            {
                //Checks if CurrentTrack is the first. If it is, the track starts from beginning 
                if (PlayQueueController.PQ.CurrentTrack.Equals(PlayQueueController.PQ.TrackWaitingList.First.Value)) 
                {
                    CurrentTrackElement.Position = new TimeSpan(0, 0, 0); //Set position of song to 0 sec
                }
                else
                {
                    if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackRepeat) //If repeatMode is TrackRepeat set repeatMode to TrackListRepeat
                    {
                        RepeatModeIcon = new Uri("/ImageResources/LoopTrackWaitingList.png", UriKind.RelativeOrAbsolute);
                        PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                        PlayQueue.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                    }
                    PlayQueueController.PreviousTrack();
                    MyQueueScreenToViewModel.ShowElements();
                    CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;
                }
            }
            else //Else play the current song from the start (0 sec)
            {
                CurrentTrackElement.Position = new TimeSpan(0, 0, 0); //Set position of song to 0 sec
            }
            if (!PlayQueueController.PQ.IsPaused)
            {
                PlayTrack();
            }
        }

        public void ShuffleWaitinglist()
        {
            
            if(PlayQueue.IsShuffle == true)
            {
                PlayQueueController.ShuffleTrackWaitingList();
                ShuffleButtonIcon = new Uri("/ImageResources/unshuffle.png", UriKind.RelativeOrAbsolute);
            } else
            {
                PlayQueueController.UnShuffleTrackWaitingList();
                ShuffleButtonIcon = new Uri("/ImageResources/shuffle.png", UriKind.RelativeOrAbsolute);
            }
            MyQueueScreenToViewModel.ShowElements();
        }

        //Runs when the track has ended. The next track will be loaded and played.
        //It's assigned to CurrentTrackElement.MediaEnded in the MainWindowViewModel constructor
        //If the playQueue has played all tracks, CurrentTrack will be set to the first Track in TrackWaitingList and the audio will be paused.
        public void Track_Ended(Object sender, EventArgs e)
        {
            if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackRepeat)
            {
                TimeSpan timerNull = new TimeSpan(0, 0, 0);
                CurrentTrackElement.Position = timerNull;
                CurrentTrackElement.Play();
            }
            else
            {
                NextTrack();
                PauseTrackOnWaitingListEnd();
            }
        }

        public void LoopWaitinglist()
        {
            if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackListRepeat)
            {
                RepeatModeIcon = new Uri("/ImageResources/loopTrack.png", UriKind.RelativeOrAbsolute);
                PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.TrackRepeat;
                PlayQueue.RepeatMode = PlayQueue.RepeatModes.TrackRepeat;
            }
            else if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackRepeat)
            {
                RepeatModeIcon = new Uri("/ImageResources/loopOff.png", UriKind.RelativeOrAbsolute);
                PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.Off;
                PlayQueue.RepeatMode = PlayQueue.RepeatModes.Off;
            }
            else if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.Off)
            {
                RepeatModeIcon = new Uri("/ImageResources/LoopTrackWaitingList.png", UriKind.RelativeOrAbsolute);
                PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                PlayQueue.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
            }
        }

        //Changes volume based on slider. 0 is muted and 100 is highest
        public void VolumeSlider_ValueChanged(double VolumeSliderValue)
        {
            CurrentTrackElement.Volume = VolumeSliderValue; // gets the slider value and puts it as volume
            PlayQueueController.SetVolume(VolumeSliderValue);
            if (CurrentTrackElement.Volume > 0)
            {
                PlayQueue.IsMute = false; // sets bool to false because its not muted
                MuteButtonIcon = new Uri("/ImageResources/unmute.png", UriKind.RelativeOrAbsolute); // puts the volume image to the unmuted png
            }
            else
                MuteButtonIcon = new Uri("/ImageResources/mute.png", UriKind.RelativeOrAbsolute); // puts the volume image to the muted png because the volume is 0
        }

        private void MuteVolume()
        {
            if (PlayQueue.IsMute) // checks if volume is already muted
            {
                VolumeSlider_ValueChanged(oldVolume);
            }
            else // else if volume is not muted
            {
                PlayQueueController.SetMute();
                oldVolume = CurrentTrackElement.Volume; // saves the old volume
                VolumeSlider_ValueChanged(0); // changes the slider volume value to 0
            }
        }


        #endregion

        //Initialize-methods that are used in the constructor of MainWindowViewModel
        #region Initializer methods
        public void InitializeCommands()
        {
            ChangeViewModelCommand = new RelayCommand<Screen>(ChangeViewModel); //Sets the command to the corresponding method
            TrackControlCommand = new RelayCommand(TrackControl);
            NextTrackCommand = new RelayCommand(NextTrack);
            PrevTrackCommand = new RelayCommand(PrevTrack);
            MuteTrackCommand = new RelayCommand(MuteVolume);
            LoopCommand = new RelayCommand(LoopWaitinglist);
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
            CurrentTrackVolume = PlayQueue.CurrentVolume;
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

            Track testTrack1 = new Track()
            {
                TrackId = 1,
                Name = "1",
                Artist = "Santi",
                Album = "testalbum",
                Duration = 120,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack2 = new Track()
            {
                TrackId = 2,
                Name = "2",
                Artist = "Dio",
                Album = "testalbum",
                Duration = 90,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            };
            Track testTrack3 = new Track()
            {
                TrackId = 3,
                Name = "3",
                Artist = "Tristan",
                Album = "testalbum",
                Duration = 100,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack4 = new Track()
            {
                TrackId = 4,
                Name = "4",
                Artist = "Marloes",
                Album = "testalbum",
                Duration = 70,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack5 = new Track()
            {
                TrackId = 5,
                Name = "5",
                Artist = "Susan",
                Album = "testalbum",
                Duration = 70,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack6 = new Track()
            {
                TrackId = 6,
                Name = "Queue1",
                Artist = "Susan",
                Album = "testalbum",
                Duration = 30,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Gun'sRoses.mp3"),
            };
            Track testTrack7 = new Track()
            {
                TrackId = 7,
                Name = "Queue2",
                Artist = "Solid Snake",
                Album = "testalbum",
                Duration = 90,
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\MetalGearSolid.mp3"),
            };
            //Track testTrack4 = new Track() { AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Powerup1.wav") };
            PlaylistController.AddTrack(testTrack1);
            PlaylistController.AddTrack(testTrack2);
            PlaylistController.AddTrack(testTrack3);
            PlaylistController.AddTrack(testTrack4);
            PlaylistController.AddTrack(testTrack5);
            PlaylistController.AddTrack(testTrack6);


            PlayQueueController.AddTrack(testTrack3);
            PlayQueueController.AddTrack(testTrack4);
            
            

            PlayQueueController.AddTrack(testTrack6);
            PlayQueueController.AddTrack(testTrack7);

            //PlayQueueController.AddTrack(testTrack3);
            //PlayQueueController.AddTrack(testTrack4);

            
            //Speelt track en zet playlist in wachtrij
            PlayQueueController.PlayTrack(PlaylistController.Playlist.Tracks.First.Value, PlaylistController.Playlist);

            //Zet de CurrentTrack als audio die afgespeeld wordt
            CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;

            MyQueueScreenToViewModel.ShowElements();
        }
    }
}

﻿using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using System.Collections.Generic;
using Ritmo.Database;

namespace Ritmo.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public Login User { get; set; }

        public PlaylistController PlaylistController = new PlaylistController("testplaylist");
        public PlayQueueController PlayQueueController = new PlayQueueController();
        public AllPlaylistsController AllPlaylistsController;

        public PlayQueue PlayQueue = new PlayQueue();
        public MyQueueViewModel MyQueueScreenToViewModel;

        #region Commands
        public ICommand ToClickedViewModelCommand { get; set; }
        public ICommand ToPreviousViewModelCommand { get; set; }
        public ICommand ToNextViewModelCommand { get; set; }
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

        //Method that called by ViewModelChanged event in Navigation class
        protected virtual void ChangeViewModel(Screen viewModel)
        {
            CurrentViewModel = viewModel;
        }
        #endregion

        #region CurrentTrack commands and attributes
        public ICommand TrackControlCommand { get; set; }
        public ICommand NextTrackCommand { get; set; }
        public ICommand PrevTrackCommand { get; set; }
        public ICommand MuteTrackCommand { get; set; }
        public ICommand ShuffleWaitinglistCommand { get; set; }
        public ICommand LoopCommand { get; set; }
        public ICommand MouseUpCommand { get; set; }
        public ICommand MouseDownCommand { get; set; }
        public ICommand ValueChangedCommand { get; set; }

        private double _currentTrackVolume;
        private double _currentTrackTime;
        private double _totalTrackTime;
        private double oldVolume = 0;
        private Uri _playButtonIcon = new Uri("/ImageResources/playicon.ico", UriKind.RelativeOrAbsolute);
        private Uri _muteButtonIcon = new Uri("/ImageResources/unmute.png", UriKind.RelativeOrAbsolute);
        private Uri _repeatModeIcon = new Uri("/ImageResources/loopOff.png", UriKind.RelativeOrAbsolute);
        private Uri _shuffleButtonIcon = new Uri("/ImageResources/shuffle.png", UriKind.RelativeOrAbsolute);
        private Uri _ritmoLogo = new Uri("/ImageResources/RitmoLogo.png", UriKind.RelativeOrAbsolute);

        private DispatcherTimer _timerTick = new DispatcherTimer();
        private bool _mouse_down = false;
        private string _labelCurrentTrackTime;
        private string _labelTotalTrackTime;

        private Uri _albumImage = new Uri("/ImageResources/Album_Cover_1.jpg", UriKind.RelativeOrAbsolute);
        private string _artistName;
        private string _songName;

        public MediaElement CurrentTrackElement { get; set; } = new MediaElement() { LoadedBehavior = MediaState.Manual };
        public double CurrentTrackVolume
        {
            get { return _currentTrackVolume; }
            set
            {
                _currentTrackVolume = value;
                VolumeSlider_ValueChanged(value);
                NotifyOfPropertyChange();
            }
        }

        public double CurrentTrackTime //Current time of the playing track, also known as het current position.
        {
            get
            {
                return _currentTrackTime;
            }
            set
            {
                _currentTrackTime = value;
                NotifyOfPropertyChange("CurrentTrackTime");
            }
        }

        public string LabelCurrentTrackTime //Label for the currentTrackTime
        {
            get
            {
                return _labelCurrentTrackTime;
            }
            set
            {
                _labelCurrentTrackTime = value;
                NotifyOfPropertyChange();
            }
        }

        public string LabelTotalTrackTime //Label for the totalTrackTime
        {
            get
            {
                return _labelTotalTrackTime;
            }
            set
            {
                _labelTotalTrackTime = value;
                NotifyOfPropertyChange();
            }
        }

        public double TotalTrackTime //Total time of the playing track, also known as het total position.
        {
            get
            {
                return _totalTrackTime;
            }
            set
            {
                _totalTrackTime = value;
                NotifyOfPropertyChange("TotalTrackTime");
            }
        }

        public string ArtistName { get { return _artistName; } set { _artistName = value; NotifyOfPropertyChange(); } }

        public string SongName { get { return _songName; } set { _songName = value; NotifyOfPropertyChange(); } }

        public Uri PlayButtonIcon { get { return _playButtonIcon; } set { _playButtonIcon = value; NotifyOfPropertyChange(); } }

        public Uri MuteButtonIcon { get { return _muteButtonIcon; } set { _muteButtonIcon = value; NotifyOfPropertyChange(); } }


        public Uri RepeatModeIcon { get { return _repeatModeIcon; } set { _repeatModeIcon = value; NotifyOfPropertyChange("RepeatModeIcon"); } }

        public Uri ShuffleButtonIcon { get { return _shuffleButtonIcon; } set { _shuffleButtonIcon = value; NotifyOfPropertyChange(); } }

        public Uri RitmoLogo { get { return _ritmoLogo; } set { _ritmoLogo = value; NotifyOfPropertyChange(); } }

        public Uri AlbumImage { get { return _albumImage; } set { _albumImage = value; NotifyOfPropertyChange(); } } // album image property

        #endregion

        public MainWindowViewModel(Login loggedInUser)
        {
            User = loggedInUser;

            PlayQueue = PlayQueueController.PQ;
            AllPlaylistsController = new AllPlaylistsController(User.User.ConsumerID);

            InitializeCommands();
            InitializeViewModels();

            Navigation.InitializeViewModelNavigation();
            Navigation.ViewModelChanged += ChangeViewModel;

            InitializeCurrentTrackElement();

            TestTrackMethod();
            PlaylistViewModel = new PlaylistViewModel(this, PlaylistController.Playlist);
        }

        //The methods that control or interact with the CurrentTrackElement
        #region CurrentTrackElement Methods
        public void PlayTrack()
        {
            if (CurrentTrackElement.IsLoaded && CurrentTrackElement.Source != null)
            {
                CurrentTrackElement.Play();
                PlayButtonIcon = new Uri(@"\ImageResources\pauseicon.ico", UriKind.Relative);
                PlayQueueController.UnpauseTrack(); //Sets pause bool to true

                SongName = PlayQueue.CurrentTrack.Name; // set the name of the current track
                ArtistName = PlayQueue.CurrentTrack.Artist; // set the artist of the current track 
                AlbumImage = new Uri(@"" + PlayQueue.CurrentTrack.GetAlbumCover(PlayQueue.CurrentTrack.TrackId), UriKind.Relative); // set the album image by calling the "getAlbumCover" function 
            }
        }

        //Pauses track and updates play/pausebutton
        public void PauseTrack()
        {
            CurrentTrackElement.Pause();
            PlayButtonIcon = new Uri(@"\ImageResources\playicon.ico", UriKind.Relative);
            PlayQueueController.PauseTrack(); //Sets pause bool to true
        }

        //Alternates between pausing and playing track
        public void TrackControl()
        {
            if (!PlayQueueController.PQ.IsPaused)
            {
                PauseTrack(); //Pauses media element 
            }
            else
            {
                PlayTrack(); //Starts media element
            }
        }

        //Changes to next track, set CurrentTrackElement and plays track.
        public void NextTrack()
        {
            if (CurrentTrackElement.IsLoaded && CurrentTrackElement.Source != null)
            {
                PlayQueueController.NextTrack();
                MyQueueScreenToViewModel.LoadElements();
                CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;

                //Check if there are repeatmodes selected
                if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackRepeat) //If the mode is TrackRepeat, set mode to TrackListRepeat and set the next track as currentTrack
                {
                    RepeatModeIcon = new Uri("/ImageResources/LoopTrackWaitingList.png", UriKind.RelativeOrAbsolute);
                    PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                    PlayQueue.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                    NextTrack();
                }
                //else if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.Off) //If repeatmode is off, after playlist is played, pause the track
                //{
                //    PauseTrackOnWaitingListEnd(); //Overbodig gemaakt
                //} 
                else if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackListRepeat) //If the repeatmode is TrackListRepeat, set the trackWaitingListEnded to false
                {
                    PlayQueueController.PQ.TrackWaitingListEnded = false;
                }

                if (!PlayQueueController.PQ.TrackWaitingListEnded)
                    PlayTrack();
                else
                    PauseTrack();
            }
        }

        //Changes to the previous track and set CurrentTrackElement
        public void PrevTrack()
        {
            if (CurrentTrackElement.IsLoaded && CurrentTrackElement.Source != null)
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
                            RepeatModeIcon = new Uri("/ImageResources/LoopTrackWaitingList.png", UriKind.RelativeOrAbsolute); //Set icon
                            PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat; //Set repeatmode trackListRepeat
                            PlayQueue.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                        }
                        PlayQueueController.PreviousTrack();
                        MyQueueScreenToViewModel.LoadElements();
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
        }

        public void ShuffleWaitinglist()
        {
            if (PlayQueue.IsShuffle == true)
            {
                PlayQueueController.ShuffleTrackWaitingList();
                ShuffleButtonIcon = new Uri("/ImageResources/unshuffle.png", UriKind.RelativeOrAbsolute);
            }
            else
            {
                PlayQueueController.UnShuffleTrackWaitingList();
                ShuffleButtonIcon = new Uri("/ImageResources/shuffle.png", UriKind.RelativeOrAbsolute);
            }
            MyQueueScreenToViewModel.LoadElements();
        }

        //Runs when the track has ended. The next track will be loaded and played.
        //It's assigned to CurrentTrackElement.MediaEnded in the MainWindowViewModel constructor
        //If the playQueue has played all tracks, CurrentTrack will be set to the first Track in TrackWaitingList and the audio will be paused.
        public void Track_Ended(Object sender, EventArgs e)
        {
            if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackRepeat) //If the repeatMode is TrackRepeat, track will start from the beginning when track is ended
            {
                TimeSpan timerNull = new TimeSpan(0, 0, 0); //Set timer to 0 sec.
                CurrentTrackElement.Position = timerNull;
                CurrentTrackElement.Play();
            }
            else
            {
                NextTrack();
            }
        }

        //When the track is opened the total track time will be set of the current track
        public void Track_Opened(Object sender, EventArgs e)
        {
            if (CurrentTrackElement.NaturalDuration.HasTimeSpan)
            {
                TotalTrackTime = CurrentTrackElement.NaturalDuration.TimeSpan.TotalSeconds; //Set the totalTrackTime
                LabelTotalTrackTime = PrintTimeSpan(TotalTrackTime); //Set the label for the totalTrackTime
            }
        }

        //When the track has a 'timeSpan' the current tracktime will be set
        void TimerTick(object sender, EventArgs e)
        {
            if (!_mouse_down && CurrentTrackElement.NaturalDuration.HasTimeSpan)
            {
                CurrentTrackTime = CurrentTrackElement.Position.Seconds + CurrentTrackElement.Position.Minutes * 60; //Current track time in seconds and minutes
                LabelCurrentTrackTime = PrintTimeSpan(CurrentTrackTime); //Set the label for the currentTrackTime(will be updated every second)
            }
            else
            {
                LabelCurrentTrackTime = PrintTimeSpan(0); //Set the label to 00:00, if there is no CurrentTrack
                LabelTotalTrackTime = PrintTimeSpan(0); //Set the label to 00:00, if there is no CurrentTrack
            }
        }

        //Repeatmodes -> if the button is clicked another repeatMode is set and the icon changes, default value is off.
        public void LoopWaitinglist()
        {
            if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackListRepeat) //RepeatMode was: TrackListRepeat, changes to: TrackRepeat.
            {
                RepeatModeIcon = new Uri("/ImageResources/loopTrack.png", UriKind.RelativeOrAbsolute); //Set icon
                PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.TrackRepeat; //Set repeatmode trackRepeat
                PlayQueue.RepeatMode = PlayQueue.RepeatModes.TrackRepeat; //Set repeatmode trackRepeat
            }
            else if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.TrackRepeat) //RepeatMode was: TrackRepeat, changes to: Off.
            {
                RepeatModeIcon = new Uri("/ImageResources/loopOff.png", UriKind.RelativeOrAbsolute);
                PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.Off;
                PlayQueue.RepeatMode = PlayQueue.RepeatModes.Off;
            }
            else if (PlayQueue.RepeatMode == PlayQueue.RepeatModes.Off) //RepeatMode was: Off, changes to: TrackListRepeat.
            {
                RepeatModeIcon = new Uri("/ImageResources/LoopTrackWaitingList.png", UriKind.RelativeOrAbsolute);
                PlayQueueController.PQ.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
                PlayQueue.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
            }
        }

        //When the left button of the mouse is up and if the track has a timeSpan, the position of the currentTrack will be set to the currentTrackTime.
        //The mouse is not down
        public void MouseUp(MouseButtonEventArgs e)
        {
            _mouse_down = false;
            if (CurrentTrackElement.NaturalDuration.HasTimeSpan)
            {
                CurrentTrackElement.Position = TimeSpan.FromSeconds(CurrentTrackTime); //Value of the slider will be set to the audio file
            }
        }

        //When the left button of the mouse is down, the mouse down is true.
        public void MouseDown(MouseButtonEventArgs e)
        {
            _mouse_down = true;
        }

        //When the user drags the slider and lets it go and if the track has a timeSpan, the position of the currentTrack will be set to the currentTrackTime.
        public void TrackTimeSlider_ValueChanged(RoutedPropertyChangedEventArgs<double> e)
        {
            if (_mouse_down)
            {
                CurrentTrackElement.Position = TimeSpan.FromSeconds(CurrentTrackTime); //Value of the slider will be set to the audio file
            }
            _mouse_down = false;
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

        public string PrintTimeSpan(double secs) //method to print seconds to string in the correct format
        {
            TimeSpan timeSpanFromSeconds = TimeSpan.FromSeconds(secs);
            string answer;
            if (timeSpanFromSeconds.TotalMinutes < 1.0)
            {
                if (timeSpanFromSeconds.TotalSeconds < 10)
                {
                    answer = string.Format("00:0{0}", timeSpanFromSeconds.Seconds);
                }
                else
                {
                    answer = string.Format("00:{0}", timeSpanFromSeconds.Seconds);
                }
            }
            else
            {
                if (timeSpanFromSeconds.TotalSeconds < 10)
                {
                    answer = string.Format("{0}:0{1:D2}", timeSpanFromSeconds.Minutes, timeSpanFromSeconds.Seconds);
                }
                else if (timeSpanFromSeconds.TotalSeconds < 10 && timeSpanFromSeconds.TotalMinutes < 10)
                {
                    answer = string.Format("0{0}:0{1:D2}", timeSpanFromSeconds.Minutes, timeSpanFromSeconds.Seconds);
                }
                else if (timeSpanFromSeconds.TotalMinutes < 10)
                {
                    answer = string.Format("0{0}:{1:D2}", timeSpanFromSeconds.Minutes, timeSpanFromSeconds.Seconds);
                }
                else
                {
                    answer = string.Format("{0}:{1:D2}", timeSpanFromSeconds.Minutes, timeSpanFromSeconds.Seconds);
                }
            }
            return answer; //returns string in format
        }

        #endregion

        //Initialize-methods that are used in the constructor of MainWindowViewModel
        #region Initializer methods
        public void InitializeCommands()
        {
            //Sets the command to the corresponding method
            ToClickedViewModelCommand = new RelayCommand<Screen>(Navigation.ToClickedViewModel); 
            ToPreviousViewModelCommand = new RelayCommand(Navigation.ToPreviousViewModel);
            ToNextViewModelCommand = new RelayCommand(Navigation.ToNextViewModel);

            TrackControlCommand = new RelayCommand(TrackControl);
            NextTrackCommand = new RelayCommand(NextTrack);
            PrevTrackCommand = new RelayCommand(PrevTrack);
            MuteTrackCommand = new RelayCommand(MuteVolume);
            LoopCommand = new RelayCommand(LoopWaitinglist);
            ShuffleWaitinglistCommand = new RelayCommand(ShuffleWaitinglist);
            MouseUpCommand = new RelayCommand<MouseButtonEventArgs>(MouseUp);
            MouseDownCommand = new RelayCommand<MouseButtonEventArgs>(MouseDown);
            ValueChangedCommand = new RelayCommand<RoutedPropertyChangedEventArgs<double>>(TrackTimeSlider_ValueChanged);
        }
        public void InitializeViewModels()
        {
            HomeViewModel = new HomeViewModel(this);
            CurrentViewModel = HomeViewModel;
            AllPlaylistsViewModel = new AllPlaylistsViewModel(this);
            MyQueueViewModel = new MyQueueViewModel(this);
            MyQueueScreenToViewModel = (MyQueueViewModel)MyQueueViewModel;

            Navigation.CurrentViewModel = CurrentViewModel; //Sets the CurrentViewModel to the Navigation class
        }
        public void InitializeCurrentTrackElement()
        {
            //CurrentTrackElement.Source = CurrentTrackSource;
            CurrentTrackElement.MediaOpened += Track_Opened;
            CurrentTrackElement.MediaEnded += Track_Ended;

            //The timer to refresh the time fo the element will start
            _timerTick.Interval = TimeSpan.FromSeconds(1);
            _timerTick.Tick += new EventHandler(TimerTick);
            _timerTick.Start();

            CurrentTrackVolume = PlayQueue.CurrentVolume;
            CurrentTrackElement.Volume = CurrentTrackVolume;
            SongName = ""; //initialize SongName
            ArtistName = ""; //initialize artist name
            AlbumImage = new Uri("", UriKind.RelativeOrAbsolute); //initialize album image field 
        }
        #endregion

        public void TestTrackMethod()
        {
            int count = 0;

            //test with tracks in database (only tracks from database wil show album images)
            string sqlQuery = "SELECT idTrack, title, path, duration FROM Track";
            List<Dictionary<string, object>> trackNames = DatabaseConnector.SelectQueryDB(sqlQuery);
            int idTrack = 0;
            string title = "";
            string path = "";
            int duration = 0;

            foreach (var dictionary in trackNames) // loop trough results to get the values
            {
                foreach (var key in dictionary)
                {
                    if (key.Key.Equals("idTrack")) // check if the key contains the id
                    {
                        idTrack = Convert.ToInt32(key.Value); // convert to the variable above
                        count++; // add one to the counter to calculate later 
                    }
                    else if (key.Key.Equals("title"))
                    {
                        title = key.Value.ToString();
                        count++;
                    }
                    else if (key.Key.Equals("path"))
                    {
                        path = key.Value.ToString();
                        count++;
                    }
                    else if (key.Key.Equals("duration"))
                    {
                        duration = Convert.ToInt32(key.Value);
                        count++;
                    }
                }
                if (count % 4 == 0) // calculate each row of results (the results contain 4 rows)
                {
                    Track databaseTrack = new Track()
                    { // create new track with info from the database
                        TrackId = idTrack,
                        Name = title,
                        Artist = "unknown",
                        Album = "Unknown",
                        Duration = duration,
                        AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"" + path),
                    };
                    PlaylistController.AddTrack(databaseTrack); // add track to the queue 
                }
            }

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
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneUnatco.mp3"),
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
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\Powerup1.wav"),
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


            PlayQueueController.AddTrack(testTrack6);
            PlayQueueController.AddTrack(testTrack7);

            //PlayQueueController.AddTrack(testTrack3);
            //PlayQueueController.AddTrack(testTrack4);


            //Speelt track en zet playlist in wachtrij
            PlayQueueController.PlayTrack(PlaylistController.Playlist.Tracks.First.Value, PlaylistController.Playlist);

            //Zet de CurrentTrack als audio die afgespeeld wordt
            CurrentTrackElement.Source = PlayQueueController.PQ.CurrentTrack.AudioFile;

            MyQueueScreenToViewModel.LoadElements();
        }
    }
}

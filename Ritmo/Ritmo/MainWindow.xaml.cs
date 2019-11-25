using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Ritmo.ViewModels;

namespace Ritmo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayQueueController playQueueController = new PlayQueueController();
        PlaylistController playlistController = new PlaylistController("TestPlaylist");

        public MainWindow()
        {

            InitializeComponent();
            CurrentTrackElement.LoadedBehavior = MediaState.Manual;
            CurrentTrackElement.MediaEnded += Track_Ended;
            PlayButton.Click += OnClickPlay;

            TestTrackMethod();
        }

        //Methode om de logica te testen. Dit zijn uiteindelijk de stappen die de gebruiker zelf moet zetten in de GUI
        public void TestTrackMethod()
        {
            //Een test playlist
            Track testTrack1 = new Track() { AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\AudioTestFiles\Powerup1.wav") };
            Track testTrack2 = new Track() { AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\AudioTestFiles\Powerup2.wav") };
            playlistController.AddTrack(testTrack1);
            playlistController.AddTrack(testTrack2);

            //Speelt track en zet playlist in wachtrij
            playQueueController.PlayTrack(playlistController.Playlist.Tracks.First.Value, playlistController.Playlist);

            //Zet de CurrentTrack als audio die afgespeeld wordt
            CurrentTrackElement.Source = playQueueController.playQueue.CurrentTrack.AudioFile;

        }


      

        //Runs when the track has ended. The next track will be loaded and played.
        //If the playQueue has played all tracks, CurrentTrack will be set to the first Track in TrackWaitingList and the audio will be paused.
        public void Track_Ended(Object sender, EventArgs e)
        {
            playQueueController.NextTrack();
            CurrentTrackElement.Source = playQueueController.playQueue.CurrentTrack.AudioFile;

            if (playQueueController.playQueue.TrackWaitingListEnded)
                CurrentTrackElement.Pause();
        }

        public void OnClickPlay(object sender, EventArgs e)
        {
            CurrentTrackElement.Play();
        }

        private void Home_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new HomeViewModel();
        }

        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new SearchViewModel();
        }

        private void Categories_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new CategoriesViewModel();
        }

        private void Following_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new FollowingViewModel();
        }

        private void AllPlaylists_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AllPlaylistsViewModel();

        }
        private void MyQueue_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new MyQueueViewModel();
        }
        
    }
}

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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        //PlayQueueController playQueueController = new PlayQueueController();
        //PlaylistController playlistController = new PlaylistController("TestPlaylist");

        //public MainWindowView()
        //{
        //    InitializeComponent();
        //    CurrentTrackElement.LoadedBehavior = MediaState.Manual;
        //    CurrentTrackElement.UnloadedBehavior = MediaState.Pause;
            //CurrentTrackElement.MediaEnded += Track_Ended;

        //    //TestTrackMethod();
        //}

        ////Methode om de logica te testen. Dit zijn uiteindelijk de stappen die de gebruiker zelf moet zetten in de GUI
        //public void TestTrackMethod()
        //{
            
        //}

        ////Plays track and updates play/pausebutton
        //public void PlayTrack()
        //{
        //    if (CurrentTrackElement.IsLoaded)
        //    {
        //        CurrentTrackElement.Play();
        //        PlayButtonIcon.Source = new BitmapImage(new Uri(@"\ImageResources\pausebutton.png", UriKind.Relative));
        //    }

        //}

        ////Changes to next track, set CurrentTrackElement and plays track.
        //public void PlayNextTrack()
        //{
        //    playQueueController.NextTrack();
        //    CurrentTrackElement.Source = playQueueController.PQ.CurrentTrack.AudioFile;
        //    if (!playQueueController.PQ.TrackWaitingListEnded)
        //    {
        //        PlayTrack();
        //    }
        //}

        ////Changes to the previous track and set CurrentTrackElement
        //public void PlayPreviousTrack()
        //{
        //    playQueueController.PreviousTrack();
        //    CurrentTrackElement.Source = playQueueController.PQ.CurrentTrack.AudioFile;
        //    PlayTrack();
        //}

        ////Pauses track and updates play/pausebutton
        //public void PauseTrack()
        //{
        //    if (playQueueController.PQ.TrackWaitingListEnded)
        //    {
        //        CurrentTrackElement.Pause();
        //        PlayButtonIcon.Source = new BitmapImage(new Uri(@"\ImageResources\playbutton.png", UriKind.Relative));
        //    }
        //}

        //#region Events

        ////Runs when the track has ended. The next track will be loaded and played.
        ////It's assigned to CurrentTrackElement.MediaEnded in the MainWindow constructor
        ////If the playQueue has played all tracks, CurrentTrack will be set to the first Track in TrackWaitingList and the audio will be paused.
        //public void Track_Ended(Object sender, EventArgs e)
        //{
        //    PlayNextTrack();
        //    PauseTrack();
        //}

        //private void Play_Clicked(object sender, RoutedEventArgs e)
        //{
        //    PlayTrack();
        //}

        //private void Next_Clicked(object sender, RoutedEventArgs e)
        //{
        //    PlayNextTrack();
        //    PauseTrack();
        //}

        //private void Prev_Clicked(object sender, RoutedEventArgs e)
        //{
        //    PlayPreviousTrack();
        //}

        //#endregion


        //// test tristan volume

        //// changes volume based on slidebar, 0 is muted and 1 is the highest volume.
        //private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        //{
        //    //CurrentTrackElement.Volume = (double)volumeSlider.Value; // gets the slider value and puts it as volume

        //}



    }
}

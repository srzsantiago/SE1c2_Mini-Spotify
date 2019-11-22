using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class PlayQueueControllerTest
    {
        //Method: PlayTrack(Track track) - Assign given track as current track playing, success scenario, returns true.
        [TestMethod]
        public void PlayTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            //Act
            playQueueController.PlayTrack(track);
            var result = playQueueController.PQ.CurrentTrack;
            //Assert
            Assert.AreEqual(result, track);
        }

        //Method: PlayTrack(Track track, TrackList trackList) - 
        //set the currentTrack with a track from a tracklist(playlist/album) chosen by the user and at the whole playlist will be added to the waitinglist
        //Success scenario.
        [TestMethod]
        public void PlayTrack_SuccessScenario_AddsPlaylistToWaitingList()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            //Act
            playQueueController.PlayTrack(track, new Playlist("New"));
            var result = playQueueController.PQ.CurrentTrack;
            //Assert
            Assert.AreEqual(result, track);
        }

        //ResumeTrack() -> If track is paused, resume track -> Success scenario.
        [TestMethod]
        public void ResumeTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            playQueueController.PQ.IsPaused = true;
            //Act
            playQueueController.ResumeTrack();
            bool result = playQueueController.PQ.IsPaused;
            //Assert
            Assert.AreEqual(result, false);
        }

        //PauseTrack() -> If track is playing, pause the track -> Success scenario.
        [TestMethod]
        public void PauseTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            playQueueController.PQ.IsPaused = false;
            //Act
            playQueueController.PauseTrack();
            bool result = playQueueController.PQ.IsPaused;
            //Assert
            Assert.AreEqual(result, true);
        }

        //NextTrack() -> Skip the currently palying track and set the next track as current track, success scenario, returns void.
        [TestMethod]
        public void NextTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            Track track1 = new Track();
            Playlist playlistnew = new Playlist("New");
            //Act
            playlistnew.Tracks.AddLast(track);
            playlistnew.Tracks.AddLast(track1);
            playQueueController.SetTrackWatingList(playlistnew);
            playQueueController.PlayTrack(track);
            var result = playQueueController.PQ.TrackWaitingList.Find(playQueueController.PQ.CurrentTrack).Next.Value;
            playQueueController.NextTrack();
            //Assert
            Assert.AreEqual(result, track1);
        }

        //PreviousTrack() ->  Set the PreviousTrack as the CurrentTrack, success scenario, returns void.
        [TestMethod]
        public void PreviousTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            Track track1 = new Track();
            Playlist playlistnew = new Playlist("New");
            //Act
            playlistnew.Tracks.AddLast(track);
            playlistnew.Tracks.AddLast(track1);
            playQueueController.SetTrackWatingList(playlistnew);
            playQueueController.PlayTrack(track);
            playQueueController.PlayTrack(track1);
            var result = playQueueController.PQ.TrackWaitingList.Find(playQueueController.PQ.CurrentTrack).Previous.Value;
            playQueueController.PreviousTrack();
            //Assert
            Assert.AreEqual(result, track);
        }

        //Method: AddTrack(Track track) -> Add given track to queue, success scenario, returns void.
        [TestMethod]
        public void AddTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            //Act
            playQueueController.AddTrack(track);
            var result = playQueueController.PQ.TrackQueue.Contains(track);
            //Assert
            Assert.AreEqual(result, true);
        }

        //Method: RemoveTrackFromQueue(Track track, int index) -> Remove given track at given index from the queue, success scenario, returns void.
        [TestMethod]
        public void RemoveTrackFromQueue_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            bool result;
            //Act
            playQueueController.PQ.TrackQueue.Enqueue(track);
            if (playQueueController.PQ.TrackQueue.Count > 0)
            {
                playQueueController.RemoveTrackFromQueue(track, 0);
                result = playQueueController.PQ.TrackQueue.Contains(track);
            } else
            {
                result = true;
            }
            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void RemoveTRackFromWaitingList_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            bool result;
            //Act
            playQueueController.PQ.TrackWaitingList.AddLast(track);
            playQueueController.PQ.TrackWaitingList.Remove(track);
            result = playQueueController.PQ.TrackWaitingList.Contains(track) ;
            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void RepeatTrackWaitingList_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            //Act
            playQueueController.RepeatTrackWaitingList();
            var result = PlayQueue.RepeatModes.TrackListRepeat;
            //Assert
            Assert.AreEqual(playQueueController.PQ.RepeatMode, result);
        }

        [TestMethod]
        public void RepeatTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            //Act
            playQueueController.RepeatTrack();
            var result = PlayQueue.RepeatModes.TrackRepeat;
            //Assert
            Assert.AreEqual(playQueueController.PQ.RepeatMode, result);
        }

        [TestMethod]
        public void SetVolume_SuccessScenario_Returns30()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            double result;
            //Act
            playQueueController.PQ.CurrentVolume = 20;
            playQueueController.SetVolume(30);
            result = playQueueController.PQ.CurrentVolume;
            //Assert
            Assert.AreEqual(result, 30);
        }

        [TestMethod]
        public void SetMute_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            bool result;
            //Act
            playQueueController.SetMute();
            result = playQueueController.PQ.IsMute;
            //Assert
            Assert.AreEqual(result, true);
        }
    }
}

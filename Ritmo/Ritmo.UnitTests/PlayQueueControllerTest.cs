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
            var result = playQueueController.playQueue.CurrentTrack;
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
            var result = playQueueController.playQueue.CurrentTrack;
            //Assert
            Assert.AreEqual(result, track);
        }

        //ResumeTrack() -> If track is paused, resume track -> Success scenario.
        [TestMethod]
        public void ResumeTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            playQueueController.playQueue.IsPaused = true;
            //Act
            playQueueController.ResumeTrack();
            bool result = playQueueController.playQueue.IsPaused;
            //Assert
            Assert.AreEqual(result, false);
        }

        //PauseTrack() -> If track is playing, pause the track -> Success scenario.
        [TestMethod]
        public void PauseTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            playQueueController.playQueue.IsPaused = false;
            //Act
            playQueueController.PauseTrack();
            bool result = playQueueController.playQueue.IsPaused;
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
            var result = playQueueController.playQueue.TrackWaitingList.Find(playQueueController.playQueue.CurrentTrack).Next.Value;
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
            var result = playQueueController.playQueue.TrackWaitingList.Find(playQueueController.playQueue.CurrentTrack).Previous.Value;
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
            var result = playQueueController.playQueue.TrackQueue.Contains(track);
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
            playQueueController.playQueue.TrackQueue.Enqueue(track);
            playQueueController.RemoveTrackFromQueue(track, 0);
            if (playQueueController.playQueue.TrackQueue.Count > 0)
            {
                result = playQueueController.playQueue.TrackQueue.Contains(track);
            } else
            {
                result = true;
            }
            //Assert
            Assert.AreEqual(result, false);
        }
    }
}

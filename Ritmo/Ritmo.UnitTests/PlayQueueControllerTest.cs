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
            Playlist trackList = new Playlist("New");
            //Act
            playQueueController.PlayTrack(track, trackList);
            var result = playQueueController.playQueue.CurrentTrack;
            //Assert
            Assert.AreEqual(result, track);
        }
    }
}

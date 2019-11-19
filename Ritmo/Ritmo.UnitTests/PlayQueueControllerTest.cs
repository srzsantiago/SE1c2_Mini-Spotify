﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        //Method: PlayTrack(Track track, TrackList trackList) - set the currentTrack with a track from a tracklist(playlist/album) chosen by the user and at the whole list to the WaitingList
        //
        [TestMethod]
        public void PlayTrack_SuccessScenario_
    }
}

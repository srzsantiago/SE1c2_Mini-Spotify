using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class PlayQueueTest
    {
        //Method: TrackQueueHasSongs() - Track queue has tracks, adds track, success scenario and returns true.
        [TestMethod]
        public void TrackQueueHasSongs_Yes_ReturnsTrue()
        {
            //Arrange
            PlayQueue playQueue = new PlayQueue();
            bool result;
            //Act
            playQueue.TrackQueue.Enqueue(new Track()); //Enqueue track to the queue
            result = playQueue.TrackQueueHasSongs();
            //Assert
            Assert.AreEqual(result, true); //Check if the track is added to the queue -> returns true
        }

        //Method: TrackQueueHasSongs() - Track queue has tracks, no tracks, returns false.
        [TestMethod]
        public void TrackQueueHasSongs_No_ReturnsFalse()
        {
            //Arrange
            PlayQueue playQueue = new PlayQueue();
            bool result;
            //Act
            result = playQueue.TrackQueueHasSongs();
            //Assert
            Assert.AreEqual(result, false); //Check if the queue has tracks, when no tracks are added -> returns false
        }

        //Method: TrackEnded() - 
        [TestMethod]
        public void TrackEnded_()
        {

        }
    }
}
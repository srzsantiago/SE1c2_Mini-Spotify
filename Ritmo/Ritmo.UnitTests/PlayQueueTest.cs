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
            bool result = false;
            //Act
            playQueue.TrackQueue.Enqueue(new Track());
            result = playQueue.TrackQueueHasSongs();
            //Assert
            Assert.AreEqual(result, true);
        }

        //Method: TrackQueueHasSongs() - Track queue has tracks, no tracks, returns false.
        [TestMethod]
        public void TrackQueueHasSongs_No_ReturnsFalse()
        {
            //Arrange
            PlayQueue playQueue = new PlayQueue();
            bool result = true;
            //Act
            result = playQueue.TrackQueueHasSongs();
            //Assert
            Assert.AreEqual(result, false);
        }

        //Method: TrackEnded() - 
        [TestMethod]
        public void TrackEnded_()
        {

        }
    }
}


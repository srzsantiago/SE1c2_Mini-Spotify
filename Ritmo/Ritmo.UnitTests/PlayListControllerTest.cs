using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class PlayListControllerTest
    {
        //Method: AddTrack(Track track) - 
        [TestMethod]
        public void Add_SuccessScenario_ReturnsTrue()
        {
            //Arrange
            bool result = false;
            Track track = new Track();
            PlaylistController playlistController = new PlaylistController("Playlist");
            //Act
            playlistController.AddTrack(track);
            result = playlistController.Playlist.Tracks.Contains(track);
            //Assert
            Assert.AreEqual(result, true);
        }

        //Method: AddTrack(Track track)
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Add_TrackALreadyExists_ReturnsException()
        {
            //Arrange
            Track track = new Track();
            PlaylistController playlistController = new PlaylistController("Playlist");
            //Act / Assert
            playlistController.AddTrack(track);
            playlistController.AddTrack(track);
        }

        //Method: RemoveTrack(Track track)
        [TestMethod]
        public void Remove_SuccessScenario_ReturnsFalse()
        {
            //Arrange
            bool result = false;
            Track track = new Track();
            PlaylistController playlistController = new PlaylistController("Playlist");
            //Act
            playlistController.AddTrack(track);
            playlistController.RemoveTrack(track);
            result = playlistController.Playlist.Tracks.Contains(track);
            //Assert
            Assert.AreEqual(result, false);
        }

        //Method: RemoveTrack(Track track)
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Remove_TrackDoesNotExists_ReturnsException()
        {
            //Arrange
            Track track = new Track();
            PlaylistController playlistController = new PlaylistController("Playlist");
            //Act / Assert
            playlistController.RemoveTrack(track);
            playlistController.Playlist.Tracks.Contains(track);
        }

        //Method: SetName(string name)
        [TestMethod]
        public void SetName_ChangeName_ReturnsName()
        {
            //Arrange
            PlaylistController playlistController = new PlaylistController("Before");
            string result;
            //Act
            playlistController.SetName("After");
            result = playlistController.Playlist.Name;
            //Assert
            Assert.AreEqual(result, "After");
        }
    }
}

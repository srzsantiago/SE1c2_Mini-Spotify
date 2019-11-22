using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class PlayListControllerTest
    {
        //Method: AddTrack(Track track) - Add track to playlist, success scenario, returns true.
        [TestMethod]
        public void AddTrack_SuccessScenario_ReturnsTrue()
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

        //Method: AddTrack(Track track) - Add track to playlist twice, returns exception.
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddTrack_TrackALreadyExists_ReturnsException()
        {
            //Arrange
            Track track = new Track();
            PlaylistController playlistController = new PlaylistController("Playlist");
            //Act / Assert
            playlistController.AddTrack(track);
            playlistController.AddTrack(track);
        }

        //Method: RemoveTrack(Track track) -  Remove track from playlist, success scenario, returns false.
        [TestMethod]
        public void RemoveTrack_SuccessScenario_ReturnsFalse()
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

        //Method: RemoveTrack(Track track) - Remove track from playlist, track does not exists, returns exception.
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveTrack_TrackDoesNotExists_ReturnsException()
        {
            //Arrange
            Track track = new Track();
            PlaylistController playlistController = new PlaylistController("Playlist");
            //Act / Assert
            playlistController.RemoveTrack(track);
            playlistController.Playlist.Tracks.Contains(track);
        }

        //Method: SetName(string name) - Sets playlist name, returns name
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

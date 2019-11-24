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
            bool result;
            Track track = new Track();
            PlaylistController playlistController = new PlaylistController("Playlist");
            //Act
            playlistController.AddTrack(track); //Add track to playlist
            result = playlistController.Playlist.Tracks.Contains(track); //Check if playlist contains the track
            //Assert
            Assert.AreEqual(result, true); //Returns true
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
            playlistController.AddTrack(track); //Add track to playlist
            playlistController.AddTrack(track); //Add track to playlist second time -> returns exception
        }

        //Method: RemoveTrack(Track track) -  Remove track from playlist, success scenario, returns false.
        [TestMethod]
        public void RemoveTrack_SuccessScenario_ReturnsFalse()
        {
            //Arrange
            bool result;
            Track track = new Track();
            PlaylistController playlistController = new PlaylistController("Playlist");
            //Act
            playlistController.AddTrack(track);  //Add track to playlist
            playlistController.RemoveTrack(track); //Remove track from playlist
            result = playlistController.Playlist.Tracks.Contains(track); //Check is playlist contains the track
            //Assert
            Assert.AreEqual(result, false); //Returns false
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
            playlistController.RemoveTrack(track); //Remove track from playlist -> playlist does not contain the track -> returns exception
        }

        //Method: SetName(string name) - Sets playlist name, returns name
        [TestMethod]
        public void SetName_ChangeName_ReturnsName()
        {
            //Arrange
            PlaylistController playlistController = new PlaylistController("Before");
            string result;
            //Act
            playlistController.SetName("After"); //Set the name for the playlist
            result = playlistController.Playlist.Name;
            //Assert
            Assert.AreEqual(result, "After"); //Check if the name of the playlist is changed correctly
        }
    }
}

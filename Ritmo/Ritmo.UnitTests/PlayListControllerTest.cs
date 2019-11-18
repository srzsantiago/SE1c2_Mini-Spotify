using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class PlayListControllerTest
    {
        //Method: AddTrack(Track track)
        [TestMethod]
        public void AddTrackToPlaylist_ReturnTrue()
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

        //Method: RemoveTrack(Track track)
        [TestMethod]
        public void RemoveTrackFromPlaylist_ReturnFalse()
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
    }
}

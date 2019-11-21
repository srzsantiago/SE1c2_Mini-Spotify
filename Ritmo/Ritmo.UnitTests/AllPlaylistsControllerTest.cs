using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class AllPlaylistsControllerTest
    {
        //Method: AddTrackList(Playlist playlist) - Add playlist to list of all playlists, success scenario, returns true.
        [TestMethod]
        public void AddTrackList_SuccessScenario_ReturnsTrue()
        {
            //Arrange
            bool result;
            Playlist playlist = new Playlist("Name");
            AllPlaylistsController allPlaylistsController = new AllPlaylistsController();
            //Act
            allPlaylistsController.AddTrackList(playlist);
            result = allPlaylistsController.allplaylists.playlists.Contains(playlist);
            //Assert
            Assert.AreEqual(result, true);
        }

        //Method: AddTrackList(Playlist playlist) - Add playlist to list of all playlists, twice with same name, success scenario, returns true.
        [TestMethod]
        public void AddTrackListT_Twice_SuccessScenario_ReturnsTrue()
        {
            //Arrange
            bool result;
            Playlist playlist = new Playlist("Name");
            Playlist playlist1 = new Playlist("Name");
            AllPlaylistsController allPlaylistsController = new AllPlaylistsController();
            //Act
            allPlaylistsController.AddTrackList(playlist);
            allPlaylistsController.AddTrackList(playlist1);
            result = allPlaylistsController.allplaylists.playlists.Contains(playlist) && allPlaylistsController.allplaylists.playlists.Contains(playlist1);
            //Assert
            Assert.AreEqual(result, true);
        }

        //Method: RemovePlaylist(Playlist playlist) - Remove playlist from list of all playlists, success scenario, returns true.
        [TestMethod]
        public void RemovePlaylist_SuccessScenario_ReturnsTrue()
        {
            //Arrange
            bool result;
            Playlist playlist = new Playlist("Name");
            AllPlaylistsController allPlaylistsController = new AllPlaylistsController();
            //Act
            allPlaylistsController.AddTrackList(playlist); //Add playlist
            allPlaylistsController.RemovePlaylist(playlist); // Remove playlist
            result = allPlaylistsController.allplaylists.playlists.Contains(playlist);
            //Assert
            Assert.AreEqual(result, false);
        }

        //Method: RemovePlaylist(Playlist playlist) - Remove playlist from list of all playlists, playlist does not exist, returns exception.
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemovePlaylist_PlaylistDoesNotExists_ReturnsException()
        {
            //Arrange
            Playlist playlist = new Playlist("Name");
            AllPlaylistsController allPlaylistsController = new AllPlaylistsController();
            //Act / Assert
            allPlaylistsController.RemovePlaylist(playlist);
            allPlaylistsController.allplaylists.playlists.Contains(playlist);
        }
    }
}

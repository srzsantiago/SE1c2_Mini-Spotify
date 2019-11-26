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
            Playlist playlist = new Playlist("Name"); //Create new playlist with name: 'Name'
            AllPlaylistsController allPlaylistsController = new AllPlaylistsController();
            //Act
            allPlaylistsController.AddTrackList(playlist); //Add a playlist called 'Name'
            result = allPlaylistsController.allplaylists.playlists.Contains(playlist);
            //Assert 
            Assert.AreEqual(result, true); //Checks if the playlist 'Name' is added -> should be true
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
            allPlaylistsController.AddTrackList(playlist); //Add a playlist called 'Name'
            allPlaylistsController.AddTrackList(playlist1); //Add a second playlist called 'Name'
            result = allPlaylistsController.allplaylists.playlists.Contains(playlist) && allPlaylistsController.allplaylists.playlists.Contains(playlist1);
            //Assert
            Assert.AreEqual(result, true); //Checks if the list of all playlists contains the playlists called 'Name' twice -> should be true.
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
            allPlaylistsController.AddTrackList(playlist); //Add playlist called 'Name'
            allPlaylistsController.RemovePlaylist(playlist); // Remove playlist called 'Name'
            result = allPlaylistsController.allplaylists.playlists.Contains(playlist);
            //Assert
            Assert.AreEqual(result, false); //Checks if the playlist called 'Name' exists -> returns false
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
            allPlaylistsController.RemovePlaylist(playlist); //Remove not excisisting playlist -> returns exception
        }
    }
}
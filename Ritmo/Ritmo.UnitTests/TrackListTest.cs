using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class TrackListTest
    {
        [TestMethod]
        public void SortPlaylistOnTrackName_AtoZ_ReturnTrue()
        {
            //Arrange
            Track track1 = new Track(1, "Z", "Artist1", 100);
            Track track2 = new Track(1, "X", "Artist2", 120);
            Track track3 = new Track(1, "A", "Artist3", 1000);
            Track track4 = new Track(1, "D", "Artist4", 20);
            PlaylistController tracklist = new PlaylistController("tracklist1");
            //Act
            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.Playlist.SortTrackList(tracklist.Playlist.Tracks, "Name", true); // sorts the list from A to Z
            //Assert
            Assert.AreEqual(tracklist.Playlist.Tracks.First.Value, track3);
        }

        [TestMethod]
        public void SortPlaylistOnTrackName_ZToA_ReturnTrue()
        {
            //Arrange
            Track track1 = new Track(1, "Z", "Artist1", 100);
            Track track2 = new Track(1, "X", "Artist2", 120);
            Track track3 = new Track(1, "A", "Artist3", 1000);
            Track track4 = new Track(1, "D", "Artist4", 20);
            PlaylistController tracklist = new PlaylistController("tracklist1");
            //Act
            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.Playlist.SortTrackList(tracklist.Playlist.Tracks, "Name", false); // sorts the list from Z to A
            //Assert
            Assert.AreEqual(tracklist.Playlist.Tracks.First.Value, track1);
        }

        [TestMethod]
        public void SortPlaylistOnDuration_HightoLow_ReturnTrue()
        {
            //Arrange
            Track track1 = new Track(1, "A", "Artist1", 100);
            Track track2 = new Track(1, "B", "Artist2", 120);
            Track track3 = new Track(1, "C", "Artist3", 1000);
            Track track4 = new Track(1, "D", "Artist4", 20);
            PlaylistController tracklist = new PlaylistController("tracklist1");
            //Act
            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.Playlist.SortTrackList(tracklist.Playlist.Tracks, "Duration", false); // sorts the list from high to low
            //Assert
            Assert.AreEqual(tracklist.Playlist.Tracks.First.Value, track3);
        }

        [TestMethod]
        public void SortPlaylistOnDuration_LowToHigh_ReturnTrue()
        {
            //Arrange
            Track track1 = new Track(1, "A", "Artist1", 100);
            Track track2 = new Track(1, "B", "Artist2", 120);
            Track track3 = new Track(1, "C", "Artist3", 1000);
            Track track4 = new Track(1, "D", "Artist4", 20);
            PlaylistController tracklist = new PlaylistController("tracklist1");
            //Act
            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.Playlist.SortTrackList(tracklist.Playlist.Tracks, "Duration", true); // sorts the list from low to high
            //Assert
            Assert.AreEqual(tracklist.Playlist.Tracks.First.Value, track4);
        }

        [TestMethod]
        public void SortPlaylistOnArtistName_AtoZ_ReturnTrue()
        {
            //Arrange
            Track track1 = new Track(1, "Z", "ArtistA", 100);
            Track track2 = new Track(1, "X", "ArtistD", 120);
            Track track3 = new Track(1, "A", "ArtistX", 1000);
            Track track4 = new Track(1, "D", "ArtistC", 20);
            PlaylistController tracklist = new PlaylistController("tracklist1");
            //Act
            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.Playlist.SortTrackList(tracklist.Playlist.Tracks, "Artist", true); // sorts the list from A to Z
            //Assert
            Assert.AreEqual(tracklist.Playlist.Tracks.First.Value, track1);
        }

        [TestMethod]
        public void SortPlaylistOnArtistName_ZToA_ReturnTrue()
        {
            //Arrange
            Track track1 = new Track(1, "Z", "ArtistA", 100);
            Track track2 = new Track(1, "X", "ArtistD", 120);
            Track track3 = new Track(1, "A", "ArtistX", 1000);
            Track track4 = new Track(1, "D", "ArtistC", 20);
            PlaylistController tracklist = new PlaylistController("tracklist1");
            //Act
            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.Playlist.SortTrackList(tracklist.Playlist.Tracks, "Artist", false); // sorts the list from Z - A
            //Assert
            Assert.AreEqual(tracklist.Playlist.Tracks.First.Value, track3);
        }

        [TestMethod]
        public void FailScenarioSortPlaylistOnArtistName_ZToA_ReturnTrue()
        {
            //Arrange
            Track track1 = new Track(1, "Z", "ArtistA", 100);
            Track track2 = new Track(1, "X", "ArtistD", 120);
            Track track3 = new Track(1, "A", "ArtistX", 1000);
            Track track4 = new Track(1, "D", "ArtistC", 20);
            PlaylistController tracklist = new PlaylistController("tracklist1");
            //Act
            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.Playlist.SortTrackList(tracklist.Playlist.Tracks, "Artist", false); // sorts the list from Z - A
            //Assert
            Assert.AreNotEqual(tracklist.Playlist.Tracks.First.Value, track2); // track 3 is the correct first value
        }
    }
}
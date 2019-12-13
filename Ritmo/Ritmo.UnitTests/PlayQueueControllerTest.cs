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
            playQueueController.PlayTrack(track); //Play assigned track
            var result = playQueueController.PQ.CurrentTrack;
            //Assert
            Assert.AreEqual(result, track); //Check if the current track is the assigned track -> returns true
        }

        //Method: PlayTrack(Track track, TrackList trackList) - 
        //set the currentTrack with a track from a tracklist(playlist/album) chosen by the user and at the whole playlist will be added to the waitinglist
        //Success scenario.
        [TestMethod]
        public void PlayTrack_SuccessScenario_AddsPlaylistToWaitingList()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            //Act
            playQueueController.PlayTrack(track, new Playlist("New")); //Play assgined track and add the playlist to the waitingList
            var result = playQueueController.PQ.CurrentTrack;
            //Assert
            Assert.AreEqual(result, track); //Check if the current track is the assigned track -> returns true
        }

        //ResumeTrack() -> If track is paused, resume track -> Success scenario.
        [TestMethod]
        public void ResumeTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            playQueueController.PQ.IsPaused = true;
            //Act
            playQueueController.UnpauseTrack(); //Resumes currently playing track, when it was paused
            bool result = playQueueController.PQ.IsPaused;
            //Assert
            Assert.AreEqual(result, false); //Checks if the currently playing track is paused or playing -> is not paused -> returns false
        }

        //PauseTrack() -> If track is playing, pause the track -> Success scenario.
        [TestMethod]
        public void PauseTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            playQueueController.PQ.IsPaused = false;
            //Act
            playQueueController.PauseTrack();
            bool result = playQueueController.PQ.IsPaused; //Pauses currently playing track, when it was playing
            //Assert
            Assert.AreEqual(result, true); //Checks if the currently playing ttack is paused or playing -> is paused -> returns true
        }

        //NextTrack() -> Skip the currently palying track and set the next track as current track, success scenario, returns void.
        [TestMethod]
        public void NextTrack_TrackWaitingList_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            Track track1 = new Track();
            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track); //Add track 'track' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track1); //Add track 'track1' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistnew); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track); //Assign 'track' as currently playing track and play 'traçk'
            var result = playQueueController.PQ.TrackWaitingList.Find(playQueueController.PQ.CurrentTrack).Next.Value;
            playQueueController.NextTrack(); //Skip the currently playing track and play the next track
            //Assert
            Assert.AreEqual(result, track1); //Check if the currently playing track was the next track in line: 'track1'
        }

        //NextTrack() -> Check if there are tracks in the queue and deqeueue currentTrack, success scenario, returns void.
        [TestMethod]
        public void NextTrack_TrackQueue_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            Track track1 = new Track();
            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track); //Add track 'track' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track1); //Add track 'track1' to playlist 'playlistnew'
            playQueueController.PlayTrack(playlistnew.Tracks.First.Value, playlistnew); //Assign 'track' as currently playing track and play 'traçk'
            playQueueController.AddTrack(track1);
            playQueueController.NextTrack(); //Skip the currently playing track and play the next track
            var result = playQueueController.PQ.CurrentTrack.Equals(track1);
            //Assert
            Assert.AreEqual(result, true); //Check if the currently playing track was the next track in line: 'track1'
        }

        //NextTrack() ->  Set the NextTrack as the CurrentTrack, no previous track, returns Exception
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NextTrack_NoNextTrack_ReturnsException()
        {
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track); // Add track 'track' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistnew); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track); //Assign 'track' as currently playing track and play 'traçk'
            //Act / Assert
            var result = playQueueController.PQ.TrackWaitingList.Find(playQueueController.PQ.WaitingListToQueueTrack).Next.Value;
            playQueueController.NextTrack(); //Return to the previous track and play that track
        }

        //PreviousTrack() ->  Set the PreviousTrack as the CurrentTrack, success scenario, returns void.
        [TestMethod]
        public void PreviousTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            Track track1 = new Track();
            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track); // Add track 'track' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track1); //Add track 'track1' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistnew); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track); //Assign 'track' as currently playing track and play 'traçk'
            playQueueController.PlayTrack(track1); //Assign 'track1' as currently playing track and play 'traçk1'
            var result = playQueueController.PQ.TrackWaitingList.Find(playQueueController.PQ.CurrentTrack).Previous.Value;
            playQueueController.PreviousTrack(); //Return to the previous track and play that track
            //Assert
            Assert.AreEqual(result, track); //Check if the currently playing track was the previous track: 'track'
        }

        //PreviousTrack() -> The firstTrack of the list is the currentTrack, previousTrack() is called, the last number of the list is now the currentTrack, success scenario, returns void.
        [TestMethod]
        public void PreviousTrack_SuccessScenario_TrackIsFirstTrack()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            Track track1 = new Track();
            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track); // Add track 'track' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track1); //Add track 'track1' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistnew); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track); //Assign 'track' as currently playing track and play 'traçk'
            playQueueController.PlayTrack(track1); //Assign 'track1' as currently playing track and play 'traçk1'
            playQueueController.PQ.CurrentTrack = playQueueController.PQ.TrackWaitingList.First.Value;
            var result = track;
            playQueueController.PreviousTrack(); //Return to the previous track and play that track
            //Assert
            Assert.AreEqual(result, playQueueController.PQ.CurrentTrack); //Check if the currently playing track was the previous track: 'track'
        }

        //PreviousTrack() ->  Set the PreviousTrack as the CurrentTrack, no previous track, returns Exception.
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PreviousTrack_NoPreviousTrack_ReturnsException()
        {
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track); // Add track 'track' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistnew); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track); //Assign 'track' as currently playing track and play 'traçk'
            //Act / Assert
            var result = playQueueController.PQ.TrackWaitingList.Find(playQueueController.PQ.CurrentTrack).Previous.Value;
            playQueueController.PreviousTrack(); //Return to the previous track and play that track
        }

        //Method: AddTrack(Track track) -> Add given track to queue, success scenario, returns void.
        [TestMethod]
        public void AddTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            //Act
            playQueueController.AddTrack(track); //Add track to the queue
            var result = playQueueController.PQ.TrackQueue.Contains(track);
            //Assert
            Assert.AreEqual(result, true); //Check if the queue contains the track
        }

        //Method: RemoveTrackFromQueue(Track track, int index) -> Remove given track at given index from the queue, success scenario, returns void.
        [TestMethod]
        public void RemoveTrackFromQueue_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track trackTest = new Track(0, "H", "A", 10);
            bool result;
            //Act
            playQueueController.PQ.TrackQueue.Enqueue(trackTest); //Add track to the queue
            if (playQueueController.PQ.TrackQueue.Count > 0) //You can only remove a track from the queue if the queue contains 1 or more tracks
            {
                playQueueController.RemoveTrackFromQueue(trackTest, playQueueController.PQ.TrackQueue.Count - 1) ; //Remove track from the queue at given index
                result = playQueueController.PQ.TrackQueue.Contains(trackTest);
            }
            else
            {
                result = true;
            }
            //Assert
            Assert.AreEqual(result, false); //Check if the queue contains the track -> should be false -> returns false
        }

        //Method to remove track from the waitinglist, success scenario, returns false.
        [TestMethod]
        public void RemoveTRackFromWaitingList_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            Track track = new Track();
            bool result;
            //Act
            playQueueController.PQ.TrackWaitingList.AddLast(track); //Add track to the trackWaitingList
            playQueueController.PQ.TrackWaitingList.Remove(track); //Remove track from the trackWaitingList
            result = playQueueController.PQ.TrackWaitingList.Contains(track);
            //Assert
            Assert.AreEqual(result, false); //Check if the trackWaitingList contains the track -> should be false -> returns false
        }


        //Method to repeat track from the waitinglist, success scenario.
        [TestMethod]
        public void RepeatTrackWaitingList_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            //Act
            playQueueController.RepeatTrackWaitingList(); //Repeat a track from the trackWaitingList
            var result = PlayQueue.RepeatModes.TrackListRepeat;
            //Assert
            Assert.AreEqual(playQueueController.PQ.RepeatMode, result); //Checks if the repeatMode is TrackListRepeat
        }

        //Method to repeat track, success scenario.
        [TestMethod]
        public void RepeatTrack_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            //Act
            playQueueController.RepeatTrack(); //Repeat a track
            var result = PlayQueue.RepeatModes.TrackRepeat;
            //Assert
            Assert.AreEqual(playQueueController.PQ.RepeatMode, result); //Checks if the repeatMode is TrackRepeat
        }

        //Method to shuffle the WaitingList when playing the first track of the list, returns true if tracks are shuffled. 
        [TestMethod]
        public void ShuffleListFromFirstTrack_SuccessScenario_TrackIsFirstTrack()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            
            Track track1 = new Track();
            Track track2 = new Track();
            Track track3 = new Track();
            Track track4 = new Track();
            Track track5 = new Track();

            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track1); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track2); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track3); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track4); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track5); //Add track 'track2' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistnew); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track1); //Assign 'track1' as currently playing track and play 'traçk'
            // List with the WaitingList order before shuffle 
            List<Track> before = new List<Track>();
            foreach(Track track in playQueueController.PQ.TrackWaitingList)
            {
                before.Add(track);
            }
            // shuffle the list
            playQueueController.ShuffleTrackWaitingList();
            // List with the WaitingList order after shuffle
            List<Track> after = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                after.Add(track);
            }
            // boolean which checks if order is different
            bool orderHasChanged = false;

            for(int i = 0; i < playQueueController.PQ.TrackWaitingList.Count; i++)
            {
                var trackBefore = before[i];
                var trackAfter = after  [i];

                if(trackBefore != trackAfter)
                {
                    orderHasChanged = true;
                }
            }
            
            //Assert
            Assert.AreEqual(orderHasChanged, true); //Check if the currently playing track was the previous track: 'track'
        }

        //Method to unshuffle the WaitingList when playing the first track of the list, returns true if the list has gone back to it's original state.
        [TestMethod]
        public void UnshuffleListFromFirstTrack_SuccessScenario_TrackIsFirstTrack()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();

            Track track1 = new Track();
            Track track2 = new Track();
            Track track3 = new Track();
            Track track4 = new Track();
            Track track5 = new Track();

            Playlist playlistUnshuffle = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistUnshuffle.Tracks.AddLast(track1); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track2); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track3); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track4); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track5); //Add track 'track2' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistUnshuffle); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track1); //Assign 'track1' as currently playing track and play 'traçk'
            // List with the WaitingList order before shuffle 
            List<Track> before = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                before.Add(track);
            }
            // shuffle the list
            playQueueController.ShuffleTrackWaitingList();
            // unshuffle the list
            playQueueController.UnShuffleTrackWaitingList();
            // List with the WaitingList order after unshuffle
            List<Track> unshuffled = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                unshuffled.Add(track);
            }
            // boolean which checks if order is the same as the original
            bool orderIsTheSame = false;

            for (int i = 0; i < playQueueController.PQ.TrackWaitingList.Count; i++)
            {
                var trackBefore = before[i];
                var trackAfter = unshuffled[i];

                if (trackBefore == trackAfter)
                {
                    orderIsTheSame = true;
                }
            }

            //Assert
            Assert.AreEqual(orderIsTheSame, true); //Check if the currently playing track was the previous track: 'track'
        }
        //Method to shuffle the WaitingList when playing the 3th track of the list, returns true if tracks are shuffled. 
        [TestMethod]
        public void ShuffleListFrom3thTrack_SuccessScenario_TrackIsFirstTrack()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();

            Track track1 = new Track();
            Track track2 = new Track();
            Track track3 = new Track();
            Track track4 = new Track();
            Track track5 = new Track();

            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track1); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track2); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track3); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track4); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track5); //Add track 'track2' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistnew); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track3); //Assign 'track1' as currently playing track and play 'traçk'
            // List with the WaitingList order before shuffle 
            List<Track> before = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                before.Add(track);
            }
            // shuffle the list
            playQueueController.ShuffleTrackWaitingList();
            // List with the WaitingList order after shuffle
            List<Track> after = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                after.Add(track);
            }
            // boolean which checks if order is different
            bool orderHasChanged = false;

            // check for each item in the lists if they are equal or not
            for (int i = 0; i < playQueueController.PQ.TrackWaitingList.Count; i++)
            {
                var trackBefore = before[i];
                var trackAfter = after[i];
                // check if they are not equal, if not equal orderhaschanged=true
                if (trackBefore != trackAfter)
                {
                    orderHasChanged = true;
                }
            }

            //Assert
            Assert.AreEqual(orderHasChanged, true); //Check if the currently playing track was the previous track: 'track'
        }

        //Method to unshuffle the WaitingList when playing the 3th track of the list, returns true if the list has gone back to it's original state. 
        [TestMethod]
        public void UnshuffleListFrom3thTrack_SuccessScenario_TrackIsFirstTrack()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();

            Track track1 = new Track();
            Track track2 = new Track();
            Track track3 = new Track();
            Track track4 = new Track();
            Track track5 = new Track();

            Playlist playlistUnshuffle = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistUnshuffle.Tracks.AddLast(track1); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track2); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track3); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track4); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track5); //Add track 'track2' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistUnshuffle); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track3); //Assign 'track1' as currently playing track and play 'traçk'
            // List with the WaitingList order before shuffle 
            List<Track> before = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                before.Add(track);
            }
            // shuffle the list
            playQueueController.ShuffleTrackWaitingList();
            // unshuffle the list
            playQueueController.UnShuffleTrackWaitingList();
            // List with the WaitingList order after unshuffle
            List<Track> unshuffled = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                unshuffled.Add(track);
            }
            // boolean which checks if order is the same as the original
            bool orderIsTheSame = false;
            // check for each item in the lists if they are equal or not
            for (int i = 0; i < playQueueController.PQ.TrackWaitingList.Count; i++)
            {
                var trackBefore = before[i];
                var trackAfter = unshuffled[i];
                // check if they are not equal, if not equal orderhaschanged=true
                if (trackBefore == trackAfter)
                {
                    orderIsTheSame = true;
                }
            }

            //Assert
            Assert.AreEqual(orderIsTheSame, true); //Check if the currently playing track was the previous track: 'track'
        }

        //Method to shuffle the WaitingList when playing the 3th track of the list, returns true if tracks are shuffled. 
        [TestMethod]
        public void ShuffleListFromNextTrack_SuccessScenario_TrackIsFirstTrack()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();

            Track track1 = new Track();
            Track track2 = new Track();
            Track track3 = new Track();
            Track track4 = new Track();
            Track track5 = new Track();

            Playlist playlistnew = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistnew.Tracks.AddLast(track1); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track2); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track3); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track4); //Add track 'track2' to playlist 'playlistnew'
            playlistnew.Tracks.AddLast(track5); //Add track 'track2' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistnew); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track1); //Assign 'track1' as currently playing track and play 'traçk'
            playQueueController.NextTrack(); //Execute nextTrack command to check if the shuffle still works. 
            // List with the WaitingList order before shuffle 
            List<Track> before = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                before.Add(track);
            }
            // shuffle the list
            playQueueController.ShuffleTrackWaitingList();
            // List with the WaitingList order after shuffle
            List<Track> after = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                after.Add(track);
            }
            // boolean which checks if order is different
            bool orderHasChanged = false;

            // check for each item in the lists if they are equal or not
            for (int i = 0; i < playQueueController.PQ.TrackWaitingList.Count; i++)
            {
                var trackBefore = before[i];
                var trackAfter = after[i];
                // check if they are not equal, if not equal orderhaschanged=true
                if (trackBefore != trackAfter)
                {
                    orderHasChanged = true;
                }
            }

            //Assert
            Assert.AreEqual(orderHasChanged, true); //Check if the currently playing track was the previous track: 'track'
        }

        //Method to unshuffle the WaitingList when playing the 3th track of the list, returns true if the list has gone back to it's original state. 
        [TestMethod]
        public void UnshuffleListFromNextTrack_SuccessScenario_TrackIsFirstTrack()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();

            Track track1 = new Track();
            Track track2 = new Track();
            Track track3 = new Track();
            Track track4 = new Track();
            Track track5 = new Track(); 

            Playlist playlistUnshuffle = new Playlist("New"); //Create new playlist called "New"
            //Act
            playlistUnshuffle.Tracks.AddLast(track1); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track2); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track3); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track4); //Add track 'track2' to playlist 'playlistnew'
            playlistUnshuffle.Tracks.AddLast(track5); //Add track 'track2' to playlist 'playlistnew'
            playQueueController.SetTrackWatingList(playlistUnshuffle); //Add 'playlistnew' to the trackWaitingList
            playQueueController.PlayTrack(track1); //Assign 'track1' as currently playing track and play 'traçk'
            playQueueController.NextTrack(); //Execute nextTrack command to check if the shuffle still works.
            // List with the WaitingList order before shuffle 
            List<Track> before = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                before.Add(track);
            }
            // shuffle the list
            playQueueController.ShuffleTrackWaitingList();
            // unshuffle the list
            playQueueController.UnShuffleTrackWaitingList();
            // List with the WaitingList order after unshuffle
            List<Track> unshuffled = new List<Track>();
            foreach (Track track in playQueueController.PQ.TrackWaitingList)
            {
                unshuffled.Add(track);
            }
            // boolean which checks if order is the same as the original
            bool orderIsTheSame = false;
            // check for each item in the lists if they are equal or not
            for (int i = 0; i < playQueueController.PQ.TrackWaitingList.Count; i++)
            {
                var trackBefore = before[i];
                var trackAfter = unshuffled[i];
                // check if they are not equal, if not equal orderhaschanged=true
                if (trackBefore == trackAfter)
                {
                    orderIsTheSame = true;
                }
            }

            //Assert
            Assert.AreEqual(orderIsTheSame, true); //Check if the currently playing track was the previous track: 'track'
        }

        //Method to set the volume, success scenario, returns current volume.
        [TestMethod]
        public void SetVolume_SuccessScenario_Returns30()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            double result;
            //Act
            playQueueController.PQ.CurrentVolume = 20; //Set current volume to 20
            playQueueController.SetVolume(30); //Set current volume to 30
            result = playQueueController.PQ.CurrentVolume;
            //Assert
            Assert.AreEqual(result, 30); //Checks if the current volume is 30
        }

        //Method to set the volume to mute, success scenario, returns true.
        [TestMethod]
        public void SetMute_SuccessScenario()
        {
            //Arrange
            PlayQueueController playQueueController = new PlayQueueController();
            bool result;
            //Act
            playQueueController.SetMute(); //Set the volume to mute
            result = playQueueController.PQ.IsMute;
            //Assert
            Assert.AreEqual(result, true); //Check if the volume is mute, should be true.
        }

        //Method to clear the queue
        [TestMethod]
        public void ClearQueue_QueueCleared_ReturnsTrue()
        {
            //Assert
            PlayQueueController playQueueController = new PlayQueueController();
            playQueueController.AddTrack(new Track("TestTrack"));

            //Act
            playQueueController.ClearQueue();

            //Assert
            Assert.IsTrue(playQueueController.PQ.TrackQueue.Count == 0);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class PlayQueueController
    {
        public PlayQueue PQ { get; set; }
        
        public PlayQueueController()//Constructor that initializate a new playQueue and link this to the playqueuecontroller
        {
            PQ = new PlayQueue();
        }

        public void PlayTrack(Track track)//Set the currentTrack with a single track chosen by the user
        {
            PQ.CurrentTrack = track;
            if (PQ.TrackWaitingList.Contains(PQ.CurrentTrack)) //check if the tracklist contains the track
            {
                PQ.WaitingListToQueueTrack = PQ.CurrentTrack;//remember the track so it can be use for the method next
            }
            
        }

        public void PlayTrack(Track track, TrackList trackList)//set the currentTrack with a track from a tracklist(playlist/album) chosen by the user and at the whole playlist will be added to the waitinglist
        {
            SetTrackWatingList(trackList);
            PlayTrack(track);
        }

        public void UnpauseTrack()
        {
            PQ.IsPaused= false;

            //???????????????????????????//EVENT
        }

        public void PauseTrack()
        { 
            PQ.IsPaused = true; 
            /////////////////////////////////EVENT
        }

        public void NextTrack(){    //Set the NextTrack as the CurrentTrack
            if (PQ.TrackQueueHasSongs())//check if there are tracks in the queue(queue has priority)
                PQ.CurrentTrack = PQ.TrackQueue.Dequeue();
            else
            {
                //play next song in the tracklist if the Repeatmode is OFF or TrackListRepeat
                if (PQ.RepeatMode == PlayQueue.RepeatModes.Off || PQ.RepeatMode == PlayQueue.RepeatModes.TrackListRepeat)
                {
                    try//Try to play the next track
                    {
                        PQ.CurrentTrack = PQ.TrackWaitingList.Find(PQ.WaitingListToQueueTrack).Next.Value;
                        PQ.WaitingListToQueueTrack = PQ.CurrentTrack;
                        PQ.TrackWaitingListEnded = false;
                    }
                    catch//If there is no next track
                    {
                        PQ.TrackWaitingListEnded = true;
                        PQ.IsPaused = true;
                        PQ.CurrentTrack = PQ.TrackWaitingList.First.Value;
                        PQ.WaitingListToQueueTrack = PQ.CurrentTrack;
                    }
                }
                //play the same track again while mode is TrackRepeat
                else if (PQ.RepeatMode == PlayQueue.RepeatModes.TrackRepeat)
                {
                    PQ.CurrentTrack = PQ.CurrentTrack;
                }
            }
        }

        public void PreviousTrack() //Set the PreviousTrack as the CurrentTrack
        {
            //check if tracklist contains the CurrentTrack
            //(this must be checked because you can not use previous for the tracks in the queue)
            if(PQ.TrackWaitingList.Contains(PQ.CurrentTrack))
            {
                PQ.CurrentTrack = PQ.TrackWaitingList.Find(PQ.WaitingListToQueueTrack).Previous.Value;
                PQ.WaitingListToQueueTrack = PQ.CurrentTrack;
            }
            //if the currentTrack is a song from the queue, currentTrack words the last played track from the tracklist.
            else
            {
                PQ.CurrentTrack = PQ.WaitingListToQueueTrack;
            }
        }

        public void AddTrack(Track track) //Add track to the queue
        {
            PQ.TrackQueue.Enqueue(track);
        }

        public void RemoveTrackFromQueue(Track track, int index) //remove track from the queue
        {
            int count = 0;//count is used to find the track at the given index
            Queue<Track> helpStack = new Queue<Track>();//queue is used as helpQueue to put the tracks in the queue temporaly.
            
            
            while(PQ.TrackQueue.Count > 0)
            {
                //all tracks that does not match the given index are temporally removed
                if (count != index)
                    helpStack.Enqueue(PQ.TrackQueue.Dequeue());
                //given index
                else
                {
                    //check if the track match with the track at this index
                    if (track.Equals(PQ.TrackQueue.Peek()))
                        //track is permanently deleted.
                        PQ.TrackQueue.Dequeue();
                    else
                        throw new Exception("The track doesn't macht with the given index.");
                }
                count++;
            }
            
            //restore all tracks in the queue.
            while(helpStack.Count > 0)
            {
                PQ.TrackQueue.Enqueue(helpStack.Dequeue());
            }
        }

        public void ClearQueue() //Clear the entire queue of track
        {
            PQ.TrackQueue.Clear();
        }


        public void RemoveTrackFromWaitingList(Track track)//Remove track from the waitinglist(Copy of a tracklist)
        {
            PQ.TrackWaitingList.Remove(track);     
        }

        public void SetTrackWatingList(TrackList trackList) //Set the waitingList with a tracklist(playlist/album)
        {
            PQ.TrackWaitingList = trackList.Tracks;
            PQ.OriginalTrackWaitingList = trackList.Tracks;
        }
        
        public void RepeatTrackWaitingList()//Repeat the whole waitingList (the queue can't be repeated)
        {
            PQ.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
        } 

        public void RepeatTrack()//Repeat the currenttrack while its active. 
        {
            PQ.RepeatMode = PlayQueue.RepeatModes.TrackRepeat;
        } 

        public void ShuffleTrackWaitingList() {
            
            Random rand = new Random();
            LinkedList<Track> currentlist = new LinkedList<Track>();//A copy of TrackWaiting list.
            LinkedList<Track> randomtracks = new LinkedList<Track>();//This is going to be the shuffledList

            //We place all items from TrackWaitinglist in currentlist without making a reference to the trackwaitingList.
            foreach (var item in PQ.TrackWaitingList)
            {
                currentlist.AddLast(item);
            }

            // Checks if the current-playing track belongs to the TrackWachtingList
            if (currentlist.Contains(PQ.WaitingListToQueueTrack))
            {
                // Removes the current track from the list of tracks that will be shuffled.
                //The first track is the only track that is not involved in the shuffling
                currentlist.Remove(PQ.WaitingListToQueueTrack);

                // Adds the currently playing track as first track of the random list before the shuffled tracks
                randomtracks.AddFirst(PQ.WaitingListToQueueTrack);
            }
                
            // count the number of tracks that will be shuffled
            int size = currentlist.Count;
            int cijfer;

            // list with random ints to create an random order of tracks
            List<int> randomIntegers = new List<int>();

            // Generate as much random integers as the number of tracks that will be shuffled, no integer can be repeated
            for (int i = 1; i <= size; i++)
            {
                cijfer = rand.Next(0, size);
                // check if random number is not already chosen 
                if (!randomIntegers.Contains(cijfer))
                {
                // add unique random number to List
                randomIntegers.Add(cijfer);
                }
                else
                {
                    // generate new random numbers as long as the number already exists
                    while (randomIntegers.Contains(cijfer))
                    {
                        cijfer = rand.Next(0, size);
                    }
                randomIntegers.Add(cijfer);
                }
            }
            // Add tracks from current list with random ElementAt to the randomtracks list. This wil create an random order
            foreach (int i in randomIntegers)
            {
                Track track = currentlist.ElementAt(i);
                randomtracks.AddLast(track);
            }

            // Fill the PlayQueue, TrackWaitingList with the tracks of the new random list
            PQ.TrackWaitingList = randomtracks;

            //set the bool IsShuffle to false (this is used to check whenever unshuffle or shuffle must be called and to display the correct icon of shuffle)
            PQ.IsShuffle = false; 
                           
        }
        public void UnShuffleTrackWaitingList()
        {
            // Set the very first TrackWaitingList (OriginalTrackWaitingList) as current TrackWaitingList
            PQ.TrackWaitingList = PQ.OriginalTrackWaitingList;

            //set the bool IsShuffle to true (this is used to check whenever unshuffle or shuffle must be called and to display the correct icon of shuffle)
            PQ.IsShuffle = true;
        }

            public void SetVolume(double volume) {//Set the volume to a given value
            PQ.CurrentVolume = volume;
        }

        public void SetMute()  //Turn the Mutemode on en off.
        {
            PQ.IsMute = !PQ.IsMute;
        }
    }
}

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
            //this.TestMethode();
        }

        private void TestMethode()
        {
            PQ.CurrentTrack = new Track("Ritmo", "Jbalvin", 80);
            this.AddTrack(new Track("Blanco", "Jbalvin", 60));
            this.AddTrack(new Track("Contra la pared", "Jbalvin", 120));
            TrackList testlist = new Playlist("TestPlaylist");
            testlist.Tracks.AddLast(new Track("Con Calma", "Daddy Yankee", 55));
            testlist.Tracks.AddLast(new Track("Despacito", "Luis foncin", 40));
            this.SetTrackWatingList(testlist);
        }

        public void PlayTrack(Track track)//Set the currentTrack with a single track chosen by the user
        {
            if (PQ.TrackWaitingList.Contains(PQ.CurrentTrack)) //check if the tracklist contains the track
            {
                PQ.WaitingListToQueueTrack = PQ.CurrentTrack;//remember the track so it can be use for the method next
            }
            PQ.CurrentTrack = track;
            PQ.WaitingListToQueueTrack = null;
        }

        public void PlayTrack(Track track, TrackList trackList)//set the currentTrack with a track from a tracklist(playlist/album) chosen by the user and at the whole playlist will be added to the waitinglist
        {
            PlayTrack(track);
            SetTrackWatingList(trackList);
        }

        public void ResumeTrack()
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
                try
                {
                    if (PQ.WaitingListToQueueTrack == null)//check if the last played track has a value
                    {
                        //play next song in the tracklist if the mode is off or trackrepeat
                        if (PQ.RepeatMode.Equals(PlayQueue.RepeatModes.Off) || PQ.RepeatMode.Equals(PlayQueue.RepeatModes.TrackListRepeat)) 
                        {
                            PQ.CurrentTrack = PQ.TrackWaitingList.Find(PQ.CurrentTrack).Next.Value;
                            PQ.TrackWaitingListEnded = false;
                        }
                        //play the same track again while mode is trackrepeat
                        if (PQ.RepeatMode.Equals(PlayQueue.RepeatModes.TrackRepeat))
                            PQ.CurrentTrack = PQ.CurrentTrack;
                        
                    }
                    else//resume the tracklist at the last played track
                        PQ.CurrentTrack = PQ.TrackWaitingList.Find(PQ.WaitingListToQueueTrack).Next.Value;
                }
                catch
                {
                    //throw new Exception("There is no next track available");
                    if (PQ.RepeatMode.Equals(PlayQueue.RepeatModes.TrackListRepeat))
                    {
                        PQ.CurrentTrack = PQ.TrackWaitingList.First.Value;
                    }
                    if (PQ.RepeatMode.Equals(PlayQueue.RepeatModes.Off))
                    {
                        PQ.TrackWaitingListEnded = true;
                        PQ.IsPaused = true;
                        PQ.CurrentTrack = PQ.TrackWaitingList.First.Value;
                    }
                }

            }
            
        }

        public void PreviousTrack() //Set the PreviousTrack as the CurrentTrack
        {
            //check if tracklist contains the CurrentTrack
            //(this must be checked because you can not use previous for the tracks in the queue)
            //and check if the track is not the first track
            if (PQ.TrackWaitingList.Contains(PQ.CurrentTrack) && !PQ.CurrentTrack.Equals(PQ.TrackWaitingList.First.Value))
                PQ.CurrentTrack = PQ.TrackWaitingList.Find(PQ.CurrentTrack).Previous.Value;
            else if (PQ.CurrentTrack.Equals(PQ.TrackWaitingList.First()))
                PQ.CurrentTrack = PQ.TrackWaitingList.Last();
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


        public void RemoveTrackFromWaitingList(Track track)//Remove track from the waitinglist(Copy of a tracklist)
        {
            PQ.TrackWaitingList.Remove(track);     
        }

        public void SetTrackWatingList(TrackList trackList) //Set the waitingList with a tracklist(playlist/album)
        {
            PQ.TrackWaitingList = trackList.Tracks;
        }
        
        public void RepeatTrackWaitingList()//Repeat the whole waitingList (the queue can't be repeated)
        {
            PQ.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
        } 

        public void RepeatTrack()//Repeat the currenttrack while its active. 
        {
            PQ.RepeatMode = PlayQueue.RepeatModes.TrackRepeat;
        } 

        public void ShuffleTrackWaitingList(TrackList trackList) {
            // werkt niet helemaal en moet volgende sprint verder
            Random rand = new Random();
            int size = trackList.Tracks.Count;
            int cijfer;
            List<int> randomnummers = new List<int>();

            for (int i = 1; i <= size; i++)
            {
                cijfer = rand.Next(0, size);
                if (!randomnummers.Contains(cijfer))
                {
                    randomnummers.Add(cijfer);
                }
                else
                {
                    while (randomnummers.Contains(cijfer))
                    {
                        cijfer = rand.Next(0, size);
                    }
                    randomnummers.Add(cijfer);
                }
            }

            LinkedList<Track> randomtracks = new LinkedList<Track>();
            foreach (int i in randomnummers)
            {
                Track track = trackList.Tracks.ElementAt(i);
                randomtracks.AddLast(track);
            }
            trackList.Tracks = randomtracks;
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

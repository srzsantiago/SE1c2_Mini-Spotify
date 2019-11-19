using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class PlayQueueController
    {
        public PlayQueue playQueue { get; set; }

        public PlayQueueController()//Constructor that initializate a new playQueue and link this to the playqueuecontroller
        {
            this.playQueue = new PlayQueue(); 
        }

        public void PlayTrack(Track track)//Set the currentTrack with a single track chosen by the user
        {
            if (playQueue.TrackWaitingList.Contains(playQueue.CurrentTrack))
            {
                playQueue.WaitingListToQueueTrack = playQueue.CurrentTrack;
            }
            playQueue.CurrentTrack = track;

            
            //remember you have to do something to remember track if it was a track from waitinglist
        }
        public void PlayTrack(Track track, TrackList trackList)//set the currentTrack with a track from a tracklist(playlist/album) chosen by the user and at the whole playlist will be added to the waitinglist
        {
            PlayTrack(track);
            SetTrackWatingList(trackList);
        }
        public void ResumeTrack()
        {
            playQueue.IsPaused= false;

            //???????????????????????????//EVENT
        }
        public void PauseTrack()
        { 
            playQueue.IsPaused = true; 
            /////////////////////////////////EVENT
        }
        public void NextTrack() {//Set the CurrentTrack as the next track
            if (playQueue.TrackQueueHasSongs())
                playQueue.CurrentTrack = playQueue.TrackQueue.Dequeue();
            else
            {
                try
                {

                    if (playQueue.WaitingListToQueueTrack == null && !playQueue.RepeatMode.Equals(PlayQueue.RepeatModes.TrackRepeat))
                        playQueue.CurrentTrack = playQueue.TrackWaitingList.Find(playQueue.CurrentTrack).Next.Value;
                    else if (playQueue.RepeatMode.Equals(PlayQueue.RepeatModes.TrackRepeat))
                        playQueue.CurrentTrack = playQueue.CurrentTrack;
                    else
                        playQueue.CurrentTrack = playQueue.TrackWaitingList.Find(playQueue.WaitingListToQueueTrack).Next.Value;
                }
                catch
                {
                    if (playQueue.RepeatMode.Equals(PlayQueue.RepeatModes.TrackListRepeat)){
                        playQueue.CurrentTrack = playQueue.TrackWaitingList.First.Value;
                    }
                    if (playQueue.RepeatMode.Equals(PlayQueue.RepeatModes.Off))
                    {
                        playQueue.TrackWaitingListEnded = true;
                        playQueue.IsPaused = true;
                        playQueue.CurrentTrack = playQueue.TrackWaitingList.First.Value;
                    }
                }

            }
            
        }
        public void PreviousTrack() //Set the CurrentTrack as the previous track
        {
                if (playQueue.TrackWaitingList.Contains(playQueue.CurrentTrack) && !playQueue.CurrentTrack.Equals(playQueue.TrackWaitingList.Last.Value))
                    playQueue.CurrentTrack = playQueue.TrackWaitingList.Find(playQueue.CurrentTrack).Previous.Value;
        }
        public void AddTrack(Track track) //Add track to the queue
        {
            playQueue.TrackQueue.Enqueue(track);
        }
        public void RemoveTrackFromQueue(Track track, int index) //remove track from the queue
        {
            int count = 0;
            Queue<Track> helpStack = new Queue<Track>();
            
            while(playQueue.TrackQueue.Count > 0)
            {
                if (count != index)
                    helpStack.Enqueue(playQueue.TrackQueue.Dequeue());
                else
                {
                    if (track.Equals(playQueue.TrackQueue.Peek()))
                        playQueue.TrackQueue.Dequeue();
                    else
                        throw new Exception("The track doesn't macht with the given index.");
                }
                count++;
            }
            
            while(helpStack.Count > 0)
            {
                playQueue.TrackQueue.Enqueue(helpStack.Dequeue());
            }
        }


        public void RemoveTrackFromWaitingList(Track track)//Remove track from the waitinglist(Copy of a tracklist)
        {
            playQueue.TrackWaitingList.Remove(track);     
        }
        public void SetTrackWatingList(TrackList trackList) //Set the waitingList with a tracklist(playlist/album)
        {
            playQueue.TrackWaitingList = trackList.Tracks;
        }
        
        public void RepeatTrackWaitingList()//Repeat the whole waitingList (the queue can't be repeated)
        {
            playQueue.RepeatMode = PlayQueue.RepeatModes.TrackListRepeat;
        } 
        public void RepeatTrack()//Repeat the currenttrack while its active. 
        {
            playQueue.RepeatMode = PlayQueue.RepeatModes.TrackRepeat;
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
            playQueue.CurrentVolume = volume;
        }
        public void SetMute()  //Turn the Mutemode on en off.
        {
            playQueue.IsMute = !playQueue.IsMute;
        }
    }
}

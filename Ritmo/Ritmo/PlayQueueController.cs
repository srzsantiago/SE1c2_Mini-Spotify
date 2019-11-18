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

        public void PlayTrack(Track track)//Set the currentTrack with a single track choiced by the user
        {
            if (playQueue.TrackWaitingList.Contains(playQueue.CurrentTrack))
            {
                playQueue.WaitingListToQueueTrack = playQueue.CurrentTrack;
            }
            playQueue.CurrentTrack = track;

            
            //remember you have to do something to remember track if it was a track from waitinglist
        }
        public void PlayTrack(Track track, TrackList trackList)//set the currentTrack with a track from a tracklist(playlist/album) choised by the user and at the whole list to the WaitingList
        {
            PlayTrack(track);
            SetTrackWatingList(trackList);
        }
        public void ResumeTrack() { }
        public void PauseTrack() { }
        public void NextTrack() {//Set the CurrentTrack as the next track
            if (playQueue.TrackQueueHasSongs())
                playQueue.CurrentTrack = playQueue.TrackQueue.Dequeue();
            else
            {
                try
                {
                    if (playQueue.WaitingListToQueueTrack == null)
                        playQueue.CurrentTrack = playQueue.TrackWaitingList.Find(playQueue.CurrentTrack).Next.Value;
                    else
                        playQueue.CurrentTrack = playQueue.TrackWaitingList.Find(playQueue.WaitingListToQueueTrack).Next.Value;
                }
                catch
                {
                    //what are we going to do if there is no TrackWaitingList(playlist/album) linked to the queue
                }

            }
            
        }
        public void PreviousTrack() {//Set the CurrentTrack as the previous track
            try
            {
                if (playQueue.TrackWaitingList.Contains(playQueue.CurrentTrack))
                    playQueue.CurrentTrack = playQueue.TrackWaitingList.Find(playQueue.CurrentTrack).Previous.Value;
            }
            catch
            {
                //what are we going to do if there is no previous track
            }
           

        }
        public void AddTrack(Track track) {//Add track to the queue
            playQueue.TrackQueue.Enqueue(track);
        }
        public void RemoveTrackFromQueue(Track track, int index) {//remove track from the queue
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
        public void SetTrackWatingList(TrackList trackList) {//Set the waitingList with a tracklist(playlist/album)
            playQueue.TrackWaitingList = trackList.Tracks;
        }
        public void ShuffleTrackWaitingList() {}
        public void RepeatTrackWaitingList() { } //Repeat the whole waitingList (the queue can't be repeated)
        public void RepeatTrack() { } //Repeat the currenttrack while its active.
        public void SetVolume(double volume) {//Set the volume to a given value
            playQueue.CurrentVolume = volume;
        }
        public void SetMute() { //Turn the Mutemode on en off.
            playQueue.IsMute = !playQueue.IsMute;
        }
    }
}

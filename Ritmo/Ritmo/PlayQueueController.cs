using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class PlayQueueController
    {
        public PlayQueue PlayQueue { get; set; }

        public PlayQueueController()//Constructor that initializate a new playQueue and link this to the playqueuecontroller
        {
            this.PlayQueue = new PlayQueue(); 
        }

        public void PlayTrack(Track track)//Set the currentTrack with a single track choiced by the user
        {
            PlayQueue.CurrentTrack = track;
        }
        public void PlayTrack(Track track, TrackList trackList)//set the currentTrack with a track from a tracklist(playlist/album) choised by the user and at the whole list to the WaitingList
        {
            PlayTrack(track);
            SetTrackWatingList(trackList);
        }
        public void ResumeTrack() { }
        public void PauseTrack() { }
        public void NextTrack() {//Set the CurrentTrack as the next track
            if (PlayQueue.TrackQueueHasSongs())
                PlayQueue.CurrentTrack = PlayQueue.TrackQueue.Dequeue();
            else
                PlayQueue.CurrentTrack = PlayQueue.TrackWaitingList.Find(PlayQueue.CurrentTrack).Next.Value;

            //what will happen if there is no playlist linked to the WaitingList
        }
        public void PreviousTrack() {//Set the CurrentTrack as the previous track
            if(PlayQueue.TrackWaitingList.Contains(PlayQueue.CurrentTrack))
                PlayQueue.CurrentTrack = PlayQueue.TrackWaitingList.Find(PlayQueue.CurrentTrack).Previous.Value;
            //there is some logica missing here
        }
        public void AddTrack(Track track) {//Add track to the queue
            PlayQueue.TrackQueue.Enqueue(track);
        }
        public void RemoveTrackFromQueue(Track track, int index) {//remove track from the queue
            int count = 0;
            Queue<Track> helpStack = new Queue<Track>();
            
            while(PlayQueue.TrackQueue.Count > 0)
            {
                if (count != index)
                    helpStack.Enqueue(PlayQueue.TrackQueue.Dequeue());
                else
                {
                    if (track.Equals(PlayQueue.TrackQueue.Peek()))
                        PlayQueue.TrackQueue.Dequeue();
                    else
                        throw new Exception("The track doesn't macht with the given index.");
                }
                count++;
            }
            
            while(helpStack.Count > 0)
            {
                PlayQueue.TrackQueue.Enqueue(helpStack.Dequeue());
            }
        }


        public void RemoveTrackFromWaitingList(Track track)//Remove track from the waitinglist(Copy of a tracklist)
        {
            PlayQueue.TrackWaitingList.Remove(track);     
        }
        public void SetTrackWatingList(TrackList trackList) {//Set the waitingList with a tracklist(playlist/album)
            PlayQueue.TrackWaitingList = trackList.Tracks;
        }
        public void ShuffleTrackWaitingList() {}
        public void RepeatTrackWaitingList() { } //Repeat the whole waitingList (the queue can't be repeated)
        public void RepeatTrack() { } //Repeat the currenttrack while its active.
        public void SetVolume(double volume) {//Set the volume to a given value
            PlayQueue.CurrentVolume = volume;
        }
        public void SetMute() { //Turn the Mutemode on en off.
            PlayQueue.IsMute = !PlayQueue.IsMute;
        }
    }
}

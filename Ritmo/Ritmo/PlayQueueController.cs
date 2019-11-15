using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class PlayQueueController
    {
        PlayQueue playqueue;

        public PlayQueueController()
        {
            this.playqueue = new PlayQueue(); 
        }

        public void PlayTrack(Track track) {
            playqueue.CurrentTrack = track;
        }
        public void PlayTrack(Track track, TrackList trackList)
        {
            PlayTrack(track);
            SetTrackWatingList(trackList);
        }
        public void ResumeTrack() { }
        public void PauseTrack() { }
        public void NextTrack() {
            if (playqueue.TrackQueueHasSongs())
                playqueue.CurrentTrack = playqueue.TrackQueue.Dequeue();
            else
                playqueue.CurrentTrack = playqueue.TrackWaitingList.Find(playqueue.CurrentTrack).Next.Value;

            //what will happen if there is no playlist linked to the WaitingList
        }
        public void PreviousTrack() {
            if(playqueue.TrackWaitingList.Contains(playqueue.CurrentTrack))
                playqueue.CurrentTrack = playqueue.TrackWaitingList.Find(playqueue.CurrentTrack).Previous.Value;
            //there is some logica missing here
        }
        public void AddTrack(Track track) {
            playqueue.TrackQueue.Enqueue(track);
        }
        public void RemoveTrackFromQueue(Track track, int index) {
            int count = 0;
            Queue<Track> helpStack = new Queue<Track>();
            
            while(playqueue.TrackQueue.Count > 0)
            {
                if (count != index)
                    helpStack.Enqueue(playqueue.TrackQueue.Dequeue());
                else
                {
                    if (track.Equals(playqueue.TrackQueue.Peek()))
                        playqueue.TrackQueue.Dequeue();
                    else
                        throw new Exception("The track doesn't macht with the given index.");
                }
                count++;
            }
            
            while(helpStack.Count > 0)
            {
                playqueue.TrackQueue.Enqueue(helpStack.Dequeue());
            }
        }


        public void RemoveTrackFromWaitingList(Track track)
        {
            playqueue.TrackWaitingList.Remove(track);     
        }
        public void SetTrackWatingList(TrackList trackList) {
            playqueue.TrackWaitingList = trackList.Tracks;
        }
        public void ShuffleTrackWaitingList() {}
        public void RepeatTrackWaitingList() { } //kan je een queue repeaten?
        public void RepeatTrack() { } //kan je een queue repeaten?
        public void SetVolume(double volume) {
            playqueue.CurrentVolume = volume;
        }
        public void SetMute() {
            playqueue.isMute = !playqueue.isMute;
        }
    }
}

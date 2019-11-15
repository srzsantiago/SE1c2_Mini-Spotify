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

        public PlayQueueController()
        {
            this.PlayQueue = new PlayQueue(); 
        }

        public void PlayTrack(Track track) {
            PlayQueue.CurrentTrack = track;
        }
        public void PlayTrack(Track track, TrackList trackList)
        {
            PlayTrack(track);
            SetTrackWatingList(trackList);
        }
        public void ResumeTrack() { }
        public void PauseTrack() { }
        public void NextTrack() {
            if (PlayQueue.TrackQueueHasSongs())
                PlayQueue.CurrentTrack = PlayQueue.TrackQueue.Dequeue();
            else
                PlayQueue.CurrentTrack = PlayQueue.TrackWaitingList.Find(PlayQueue.CurrentTrack).Next.Value;

            //what will happen if there is no playlist linked to the WaitingList
        }
        public void PreviousTrack() {
            if(PlayQueue.TrackWaitingList.Contains(PlayQueue.CurrentTrack))
                PlayQueue.CurrentTrack = PlayQueue.TrackWaitingList.Find(PlayQueue.CurrentTrack).Previous.Value;
            //there is some logica missing here
        }
        public void AddTrack(Track track) {
            PlayQueue.TrackQueue.Enqueue(track);
        }
        public void RemoveTrackFromQueue(Track track, int index) {
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


        public void RemoveTrackFromWaitingList(Track track)
        {
            PlayQueue.TrackWaitingList.Remove(track);     
        }
        public void SetTrackWatingList(TrackList trackList) {
            PlayQueue.TrackWaitingList = trackList.Tracks;
        }
        public void ShuffleTrackWaitingList() {}
        public void RepeatTrackWaitingList() { } //kan je een queue repeaten?
        public void RepeatTrack() { } //kan je een queue repeaten?
        public void SetVolume(double volume) {
            PlayQueue.CurrentVolume = volume;
        }
        public void SetMute() {
            PlayQueue.IsMute = !PlayQueue.IsMute;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class PlayQueueController
    {
        public PlayQueue playQueue { get; set; }

        public PlayQueueController()
        {
            this.playQueue = new PlayQueue(); 
        }

        public void PlayTrack(Track track) {
            playQueue.CurrentTrack = track;
        }
        public void PlayTrack(Track track, TrackList trackList)
        {
            PlayTrack(track);
            SetTrackWatingList(trackList);
        }
        public void ResumeTrack() { }
        public void PauseTrack() { }
        public void NextTrack() {
            if (playQueue.TrackQueueHasSongs())
                playQueue.CurrentTrack = playQueue.TrackQueue.Dequeue();
            else
                playQueue.CurrentTrack = playQueue.TrackWaitingList.Find(playQueue.CurrentTrack).Next.Value;

            //what will happen if there is no playlist linked to the WaitingList
        }
        public void PreviousTrack() {
            if(playQueue.TrackWaitingList.Contains(playQueue.CurrentTrack))
                playQueue.CurrentTrack = playQueue.TrackWaitingList.Find(playQueue.CurrentTrack).Previous.Value;
            //there is some logica missing here
        }
        public void AddTrack(Track track) {
            playQueue.TrackQueue.Enqueue(track);
        }
        public void RemoveTrackFromQueue(Track track, int index) {
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


        public void RemoveTrackFromWaitingList(Track track)
        {
            playQueue.TrackWaitingList.Remove(track);     
        }
        public void SetTrackWatingList(TrackList trackList) {
            playQueue.TrackWaitingList = trackList.Tracks;
        }
        public void ShuffleTrackWaitingList() {}
        public void RepeatTrackWaitingList() { } //kan je een queue repeaten?
        public void RepeatTrack() { } //kan je een queue repeaten?
        public void SetVolume(double volume) {
            playQueue.CurrentVolume = volume;
        }
        public void SetMute() {
            playQueue.isMute = !playQueue.isMute;
        }
    }
}

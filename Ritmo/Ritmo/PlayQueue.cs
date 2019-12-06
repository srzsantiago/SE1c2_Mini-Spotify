using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class PlayQueue
    {

        public enum RepeatModes { Off, TrackRepeat, TrackListRepeat }

        public LinkedList<Track> TrackWaitingList{ get; set; }
        public LinkedList<Track> OriginalTrackWaitingList { get; set; }
        public Queue<Track> TrackQueue { get; set; }
        public Track CurrentTrack { get; set; }
        public Track WaitingListToQueueTrack { get; set; }
        public double CurrentVolume { get; set; } = 0.5;
        public bool IsMute { get; set; }
        public bool IsShuffle { get; set; }
        public bool TrackWaitingListEnded { get; set; }
        public bool IsPaused { get; set; }
        public RepeatModes RepeatMode { get; set; }
               

        public PlayQueue()
        {
            TrackWaitingList = new LinkedList<Track>();
            OriginalTrackWaitingList = new LinkedList<Track>();
            TrackQueue = new Queue<Track>();
        }

        //Method to check if the queue is empty
        public bool TrackQueueHasSongs()
        {
            return TrackQueue.Count > 0;
        }

        
        public void TrackEnded()
        {
            if (TrackQueueHasSongs())
            {
                //Set CurrentTrack to TrackQueue dequeue
            }
            else if (!CurrentTrack.Equals(TrackWaitingList.Last.Value))
            {
                CurrentTrack = TrackWaitingList.Find(CurrentTrack).Next.Value;
                TrackWaitingListEnded = false;
            }
            else
            {
                CurrentTrack = TrackWaitingList.First.Value;
                TrackWaitingListEnded = true;
            }
        }
    }
}

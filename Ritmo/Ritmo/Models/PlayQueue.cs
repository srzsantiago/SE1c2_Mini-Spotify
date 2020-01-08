using System;
using System.Collections.Generic;

namespace Ritmo
{
    public class PlayQueue
    {

        public enum RepeatModes { Off, TrackRepeat, TrackListRepeat }

        public LinkedList<Track> TrackWaitingList { get; set; }
        public LinkedList<Track> OriginalTrackWaitingList { get; set; }
        public Queue<Track> TrackQueue { get; set; }
        public Track CurrentTrack { get; set; }
        public Track WaitingListToQueueTrack { get; set; }
        public double CurrentVolume { get; set; } = 0.5;
        public bool IsMute { get; set; }
        public bool IsShuffle { get; set; } = true;
        public bool TrackWaitingListEnded { get; set; }
        public bool IsPaused { get; set; } = true;
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


    }
}

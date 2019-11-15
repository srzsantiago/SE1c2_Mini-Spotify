using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class PlayQueue
    {
        //test
        public LinkedList<Track> TrackWaitingList{ get; set; }
        public Queue<Track> TrackQueue { get; set; }
        public Track CurrentTrack { get; set; }
        public Track WaitingListToQueueTrack { get; set; }
        public double CurrentVolume { get; set; }

        public PlayQueue()
        {
            TrackWaitingList = new LinkedList<Track>();
            TrackQueue = new Queue<Track>();
        }

        //Method to check if the queue is empty
        public bool TrackQueueHasSongs()
        {
            return TrackQueue.Count > 0;
        }
    }
}

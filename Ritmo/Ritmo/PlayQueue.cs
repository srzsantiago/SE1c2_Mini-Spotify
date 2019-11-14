using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class PlayQueue
    {
        LinkedList<Track> PlaylistQueue;
        Queue<Track> TrackQueue;
        public Track CurrentTrack { get; set; }
        public double CurrentVolume { get; set; }

        public PlayQueue()
        {
            PlaylistQueue = new LinkedList<Track>();
            TrackQueue = new Queue<Track>();
        }

        //Method to check if the queue is empty
        public bool TrackQueueHasSongs()
        {
            return TrackQueue.Count > 0;
        }
    }
}

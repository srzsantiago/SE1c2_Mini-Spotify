using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class PlayQueueController
    {
        public void PlayTrack() { }
        public void PauseTrack() { }
        public void NextTrack() { }
        public void PreviousTrack() { }
        public void AddTrack(Track track) { }
        public void RemoveTrack(Track track) { }
        public void AddPlayList(Playlist playlist) { } //deze kan eingelijk vervangen worden door playlist een property te maken.
        public void LoopQueue() { } //kan je een queue loopen?
        public void RepeatQueue() { } //kan je een queue repeaten?
        public void SetVolume() { } //volume can een property zijn
    }
}

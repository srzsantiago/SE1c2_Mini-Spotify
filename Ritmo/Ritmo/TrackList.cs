using System.Collections.Generic;
using System.Linq;

namespace Ritmo
{
    public abstract class TrackList
    {
        public string Name { get; set; }
        public LinkedList<Track> Tracks { get; set; }
        

        public TrackList(string name)
        {
            Name = name;
            Tracks = new LinkedList<Track>();
        }

        public void SortTrackList(LinkedList<Track> tracks, string sortOption, bool isAscending)
        {
            var propertyInfo = typeof(Track).GetProperty(sortOption);
            //var orderValues = tracks.OrderBy(x => propertyInfo.GetValue(x, null));

            var ordered = isAscending ? tracks.OrderBy(x => propertyInfo.GetValue(x, null)) : tracks.OrderByDescending(x => propertyInfo.GetValue(x, null));
            this.Tracks = new LinkedList<Track>(ordered.ToList());
        }

        // testing the SortTrackList

        public void TestSortTrackList()
        {
            Track track1 = new Track("track1", "santiago", 100);
            Track track2 = new Track("track2", "Tristan", 120);
            Track track3 = new Track("track3", "A", 1000);
            Track track4 = new Track("ABC", "B", 20000);
            Track track5 = new Track("Z", "F", 1);

            PlaylistController tracklist = new PlaylistController("tracklist1");
            

            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.AddTrack(track5);

            SortTrackList(tracklist.Playlist.Tracks, "Name", true);

            foreach (var item in tracklist.Playlist.Tracks)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
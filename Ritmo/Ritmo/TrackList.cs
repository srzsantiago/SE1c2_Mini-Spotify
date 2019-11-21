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
            var ordered = isAscending ? tracks.OrderBy(x => propertyInfo.GetValue(x, null)) : tracks.OrderByDescending(x => propertyInfo.GetValue(x, null));
            LinkedList<Track> transition = new LinkedList<Track>(ordered.ToList());
            this.Tracks = transition;
            
        }

        // testing the SortTrackList

        public void TestSortTrackList()
        {
            Track track1 = new Track("B", "santiago", 100);
            Track track2 = new Track("X", "Tristan", 120);
            Track track3 = new Track("F", "A", 1000);
            Track track4 = new Track("A", "B", 20000);
            Track track5 = new Track("Z", "F", 1);

            PlaylistController tracklist = new PlaylistController("tracklist1");

            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.AddTrack(track5);

            System.Console.WriteLine("------------- de playlist voor het sorteren -------------");

            foreach (var item in tracklist.Playlist.Tracks)
            {
                System.Console.WriteLine(item.Name);
            }

            SortTrackList(tracklist.Playlist.Tracks, "Name", true);

            System.Console.WriteLine("------------- de playlist na het sorteren -------------");
            foreach (var item in tracklist.Playlist.Tracks)
            {
                System.Console.WriteLine(item.Name);
            }
        }
        //static void Main(string[] args)
        //{
        //    TrackList t1 = new Playlist("hallo");
        //    Playlist p1 = new Playlist("ddd");
        //    t1.TestSortTrackList();
        //    p1.TestSortTrackList();
            
        //}
    }
    
    }
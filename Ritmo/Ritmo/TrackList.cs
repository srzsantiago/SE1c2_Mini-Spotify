using System;
using System.Collections.Generic;
using System.Linq;

namespace Ritmo
{
    public abstract class TrackList
    {
        public string Name { get; set; }

        public DateTime CreationDate { get; set; } // get from database

        public int TrackListDuration { get; set; } // get from database, dont know if we change to double or keep it a string
        public LinkedList<Track> Tracks { get; set; }

        public int TrackListID { get; set; }

        public TrackList(string name)
        {
            Name = name;
            Tracks = new LinkedList<Track>();
            
        }

        public TrackList(int tracklistid, string name, int tracklistduration, DateTime datetime)
        {
            TrackListID = tracklistid;
            Name = name;
            Tracks = new LinkedList<Track>();
            CreationDate = datetime;
            TrackListDuration = tracklistduration;
        }

        public void SortTrackList(LinkedList<Track> tracks, string sortOption, bool isAscending)
        {
            // Track has 3 properties: Name, Artist and Duration. you can give one of to the method as a string and it will sort based on that input.
            var propertyInfo = typeof(Track).GetProperty(sortOption);
            var ordered = isAscending ? tracks.OrderBy(x => propertyInfo.GetValue(x, null)) : tracks.OrderByDescending(x => propertyInfo.GetValue(x, null));
            
            // goes through the ordered linkedlist, removes the items that were in the old one and adds the items from the ordered list named ordered
            foreach (var item in ordered)
            {
                tracks.AddLast(item);
                tracks.RemoveFirst();
            }          
        }

        // testing the SortTrackList
        public void TestSortTrackList()
        {

            // making tracks with a name, artist and duration in seconds
            Track track1 = new Track(1, "B", "santiago", 100);
            Track track2 = new Track(1, "X", "Tristan", 120);
            Track track3 = new Track(1, "F", "A", 1000);
            Track track4 = new Track(1, "A", "B", 20000);
            Track track5 = new Track(1, "Z", "F", 1);

            // making the tracklist (goes from playlistcontroller to playlist to tracklist)
            PlaylistController tracklist = new PlaylistController("tracklist1");

            // adds the tracks to the tracklist 
            tracklist.AddTrack(track1);
            tracklist.AddTrack(track2);
            tracklist.AddTrack(track3);
            tracklist.AddTrack(track4);
            tracklist.AddTrack(track5);

            System.Console.WriteLine("------------- de playlist voor het sorteren -------------");

            // goes through the playlist with tracks before its sorted
            foreach (var item in tracklist.Playlist.Tracks)
            {
                System.Console.WriteLine(item.Name);
                
            }

            // calls the method to sort the tracklist
            SortTrackList(tracklist.Playlist.Tracks, "Name", true);

            System.Console.WriteLine("------------- de playlist na het sorteren -------------");

            // goes through the playlist with tracks after its sorted
            foreach (var item in tracklist.Playlist.Tracks)
            {
                System.Console.WriteLine(item.Name);
            }

            System.Console.WriteLine("------------- de playlist na het sorteren van duration -------------");

            SortTrackList(tracklist.Playlist.Tracks, "Duration", true);

            // goes through the playlist with tracks after it sorted the duration. to test if it also works with another property
            foreach (var item in tracklist.Playlist.Tracks)
            {
                System.Console.WriteLine(item.Duration);
            }


        }

        // test main
        //static void Main(string[] args)
        //{
        //    TrackList t1 = new Playlist("hallo");

        //    t1.TestSortTrackList();


        //}
    }
    
    }
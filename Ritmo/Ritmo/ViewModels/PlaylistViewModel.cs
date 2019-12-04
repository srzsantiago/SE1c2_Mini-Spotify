using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo.ViewModels
{
    public class PlaylistViewModel : Screen
    {
        private ObservableCollection<Track> _playListTracksOC;

        public ObservableCollection<Track> PlayListTracksOC
        {
            get
            {
                if (_playListTracksOC == null)
                    _playListTracksOC = new ObservableCollection<Track>();
                return _playListTracksOC;
            }
            set { _playListTracksOC = value;
                NotifyOfPropertyChange("PlayListTracksOC");
            }
        }

        public PlaylistViewModel()
        {
            TestMethode();
        }

        public void TestMethode()
        {
            PlayListTracksOC.Add(new Track
            {
                TrackId = 1,
                Album = "testAlbum",
                Artist = "Santi",
                Duration = 10,
                Name = "Track1",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });
            PlayListTracksOC.Add(new Track
            {
                TrackId = 2,
                Album = "testAlbum",
                Artist = "Tristan",
                Duration = 10,
                Name = "Track2",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });
            PlayListTracksOC.Add(new Track
            {
                TrackId = 3,
                Album = "testAlbum",
                Artist = "Stefan",
                Duration = 10,
                Name = "Track3",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });
            PlayListTracksOC.Add(new Track
            {
                TrackId = 4,
                Album = "testAlbum",
                Artist = "Susan",
                Duration = 10,
                Name = "Track4",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });
            PlayListTracksOC.Add(new Track
            {
                TrackId = 5,
                Album = "testAlbum",
                Artist = "Marloes",
                Duration = 10,
                Name = "Track5",
                AudioFile = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\TestFiles\RingtoneRoundabout.mp3"),
            });

        }

    }
}

using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class MyQueueViewModel : Screen
    {
        
        MainWindowViewModel mwvm;
        int count;

        public MyQueueViewModel(MainWindowViewModel mwvm)
        {
            this.mwvm = mwvm;
            TestCurrentTrack();
            _outerClickCommand = new RelayCommand<object>(this.OuterClick);
            _innerClickCommand = new RelayCommand<object>(this.InnerClick);
        }

        #region Observable collections
        private ObservableCollection<MyQueueItem> _playingNowItems;
        private ObservableCollection<MyQueueItem> _nextInQueueItems;
        private ObservableCollection<MyQueueItem> _nextUpItems;

        public ObservableCollection<MyQueueItem> PlayingNowItems
        {
            get { return _playingNowItems; }
            set { _playingNowItems = value; }
        }

        public ObservableCollection<MyQueueItem> NextInQueueItems
        {
            get { return _nextInQueueItems; }
            set { _nextInQueueItems = value; }
        }

        public ObservableCollection<MyQueueItem> NextUpItems
        {
            get { return _nextUpItems; }
            set { _nextUpItems = value; }
        }

        #endregion


        private ICommand _outerClickCommand;

        public ICommand OuterClickCommand
        {
            get
            {
                return _outerClickCommand;
            }
            set
            {
                _outerClickCommand = value;
            }
        }

        private ICommand _innerClickCommand;

        public ICommand InnerClickCommand
        {
            get
            {
                return _innerClickCommand;
            }
            set
            {
                _innerClickCommand = value;
            }
        }

        public void TestCurrentTrack()
        {
            //commands
            //initializate item

            if (mwvm.PlayQueueController.PQ.CurrentTrack != null)
            {
                PlayingNowItems = new ObservableCollection<MyQueueItem>()
                {
                new MyQueueItem()
                {
                    Name= mwvm.PlayQueueController.PQ.CurrentTrack.Name,
                    Artist=mwvm.PlayQueueController.PQ.CurrentTrack.Artist,
                    Album= "Album",
                    Duration= mwvm.PlayQueueController.PQ.CurrentTrack.Duration
                }
                };
            }

            if (mwvm.PlayQueueController.PQ.TrackQueue.Count > 0)
            {
                NextInQueueItems = new ObservableCollection<MyQueueItem>();
                count = 0;
                foreach (var item in mwvm.PlayQueueController.PQ.TrackQueue)
                {
                    NextInQueueItems.Add(new MyQueueItem()
                    {
                        playButtonID = count,
                        Name = item.Name,
                        Artist = item.Artist,
                        Album = "Album",
                        Duration = item.Duration
                    }); ;
                }
            }

            if (mwvm.PlayQueueController.PQ.TrackWaitingList.Count > 0)
            {
                NextUpItems = new ObservableCollection<MyQueueItem>();
                count = 0;
                foreach (var item in mwvm.PlayQueueController.PQ.TrackWaitingList)
                {
                    NextUpItems.Add(new MyQueueItem()
                    {
                        playButtonID = count,
                        Name = item.Name,
                        Artist = item.Artist,
                        Album = "Album",
                        Duration = item.Duration
                    });
                }
            }

        }

        private void OuterClick(Object sender)
        {
            String type = (string)sender;
            System.Windows.MessageBox.Show($"OuterButton is double clicked at {(string)sender}");

            if (type.Equals("PlayingNow"))
                //invoke event to play the song
                Console.WriteLine();
            if (type.Equals("NextInQueue"))
                //invoke event to play the song
                Console.WriteLine();
            if (type.Equals("NextUp"))
                //invoke event to play the song
                Console.WriteLine();
        }

        private void InnerClick(object sender)
        {
            String type = (string)sender;
            System.Windows.MessageBox.Show($"InnerButton is clicked at {(string)sender}");

            if (type.Equals("PlayingNow"))
                //volgende sprinter
            if (type.Equals("NextInQueue"))
                //volgende sprinter
            if (type.Equals("NextUp"))
                //invoke event to play the song
                Console.WriteLine();

        }

    }

    public class MyQueueItem
    {
        public int playButtonID { get; set; }
        public String Name { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public int Duration { get; set; }
    }
}

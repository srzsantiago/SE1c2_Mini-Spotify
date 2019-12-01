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
            _outerClickCommand = new RelayCommand<object>(this.OuterClick);
            _innerClickCommand = new RelayCommand<object>(this.InnerClick);
            ShowElements();
        }


        #region Observable collections
        private ObservableCollection<MyQueueItem> _playingNowItems;
        private ObservableCollection<MyQueueItem> _nextInQueueItems;
        private ObservableCollection<MyQueueItem> _nextUpItems;

        public ObservableCollection<MyQueueItem> PlayingNowItems
        {
            get { return _playingNowItems; }
            set { _playingNowItems = value;
                NotifyOfPropertyChange("PlayingNowItems");
            }
        }

        public ObservableCollection<MyQueueItem> NextInQueueItems
        {
            get { return _nextInQueueItems; }
            set { _nextInQueueItems = value;
                NotifyOfPropertyChange("NextInQueueItems");
            }
        }

        public ObservableCollection<MyQueueItem> NextUpItems
        {
            get { return _nextUpItems; }
            set { _nextUpItems = value;
                NotifyOfPropertyChange("NextUpItems");
            }
        }

        #endregion

        #region commands
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
        #endregion

        public void ShowElements()
        {
            //Create ObservableCollections of MyQueueItem(Class) type (The class is at the bottom of this file).
            //This OC are used in the XAML of MyQueueView in a ListBox to draw the elements.

            if (mwvm.PlayQueueController.PQ.CurrentTrack != null)
            {
                //makes an OB for CurrentTrack
                PlayingNowItems = new ObservableCollection<MyQueueItem>()
                {
                    //create instance of MyQueueItem with the correspondent properties
                new MyQueueItem()
                {
                    playButtonID = "",
                    Name= mwvm.PlayQueueController.PQ.CurrentTrack.Name,
                    Artist=mwvm.PlayQueueController.PQ.CurrentTrack.Artist,
                    Album= "Album",
                    Duration= mwvm.PlayQueueController.PQ.CurrentTrack.Duration
                }
                };
            }
            else
            {
                PlayingNowItems = new ObservableCollection<MyQueueItem>();
            }

            if (mwvm.PlayQueueController.PQ.TrackQueue.Count > 0)
            {
                //Makes an OB for All tracks in TrackQueue
                NextInQueueItems = new ObservableCollection<MyQueueItem>();
                count = 0; //this count is used to give every item a ID
                foreach (var item in mwvm.PlayQueueController.PQ.TrackQueue)
                {
                    //Create a instance for each item in TrackQueue
                    NextInQueueItems.Add(new MyQueueItem()
                    {
                        playButtonID = $"NextInQueue {count}",
                        Name = item.Name,
                        Artist = item.Artist,
                        Album = "Album",
                        Duration = item.Duration
                    }); ;
                    count++;
                }
            }
            else
            {
                NextInQueueItems = new ObservableCollection<MyQueueItem>();
            }

            if (mwvm.PlayQueueController.PQ.TrackWaitingList.Count > 0)
            {
                //Makes an OB for All tracks in TrackWaitingList
                NextUpItems = new ObservableCollection<MyQueueItem>();
                count = 0;//this count is used to give every item a ID
                foreach (var item in mwvm.PlayQueueController.PQ.TrackWaitingList)
                {

                    int indexOfWaitingListToQueueTrack = mwvm.PlayQueueController.PQ.TrackWaitingList.TakeWhile(n => n != mwvm.PlayQueueController.PQ.WaitingListToQueueTrack).Count();

                    //Create a instance for each item in TrackWaitingList
                    if (count > indexOfWaitingListToQueueTrack)
                    {
                        NextUpItems.Add(new MyQueueItem()
                        {
                            playButtonID = $"NextUp {count}",
                            Name = item.Name,
                            Artist = item.Artist,
                            Album = "Album",
                            Duration = item.Duration
                        });
                    }
                    count++;
                }
            }
            else
            {
                NextInQueueItems = new ObservableCollection<MyQueueItem>();
            }

        }

        private void OuterClick(Object sender)
        {
            String item = (string)sender; //get playButtonID (this will be splited in two. Type and index.
            string type;
            int index= -1; //if at the end of the if statement it did not change. It means the type is "PlayingNow" and that a index is not needed

            if (!item.Equals(""))//if item is blank it means it does not have a ID, so it is a playingNow type.
            {
                string[] buttonId = item.Split(null); //split playbuttonID
                type = buttonId[0];
                index = Int32.Parse(buttonId[1]);
            }
            else
            {
                type = "PlayingNow";
            }

            System.Windows.MessageBox.Show($"OuterButton is double clicked at {type} at {index}");

            if (type.Equals("PlayingNow"))
                //volgende sprint
                Console.WriteLine();
            if (type.Equals("NextInQueue"))
                //vongende sprint
                Console.WriteLine();
            if (type.Equals("NextUp"))
            {
                Track playTrack = mwvm.PlayQueueController.PQ.TrackWaitingList.ElementAt(index);
                mwvm.PlayQueueController.PlayTrack(playTrack);
                mwvm.CurrentTrackElement.Source = playTrack.AudioFile;
                
            }


            this.ShowElements();
        }

        private void InnerClick(object sender)
        {
            String item = (string)sender; //get playButtonID (this will be splited in two. Type and index.)
            string type;
            int index = -1; //if at the end of the if statement it did not change. It means the type is "PlayingNow" and that a index is not needed

            if (!item.Equals(""))//if item is blank it means it does not have a ID, so it is a playingNow type.
            {
                string[] buttonId = item.Split(null);//split playbuttonID
                type = buttonId[0];
                index = Int32.Parse(buttonId[1]);
            }
            else
            {
                type = "PlayingNow";
            }

            System.Windows.MessageBox.Show($"InnerButton clicked at {type} at {index}");

            if (type.Equals("PlayingNow"))
                //volgende sprint
                Console.WriteLine();
            if (type.Equals("NextInQueue"))
                //volgende sprint
                Console.WriteLine();
            if (type.Equals("NextUp"))
            {
                Track playTrack = mwvm.PlayQueueController.PQ.TrackWaitingList.ElementAt(index);
                mwvm.PlayQueueController.PlayTrack(playTrack);
                mwvm.CurrentTrackElement.Source = playTrack.AudioFile;
            }

            this.ShowElements();
        }

    }

    public class MyQueueItem
    {
        public String playButtonID { get; set; } //is composed of a type and an Index
        public String Name { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public int Duration { get; set; }
    }
}

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
        
        private MainWindowViewModel _mainWindowVM;
        private int _count; //Used for general counting purposes

        #region commands
        public ICommand OuterClickCommand { get; set; }
        public ICommand InnerClickCommand { get; set; }
        public ICommand AddToQueueClickCommand { get; set; }
        public ICommand ClearQueueCommand { get; set; }
        #endregion

        //Observable collections used in a listbox in the view to display the tracks
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

        //private List<string> _itemsList;

        //public List<string> ItemsList
        //{
        //    get {
        //        if (_itemsList == null)
        //            _itemsList = new List<string>() { "Test1", "Test2" };
        //        return _itemsList;
        //    }
        //    set { _itemsList = value; }
        //}

        public MyQueueViewModel(MainWindowViewModel mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
            OuterClickCommand = new RelayCommand<object>(OuterClick);
            InnerClickCommand = new RelayCommand<object>(InnerClick);
            AddToQueueClickCommand = new RelayCommand<object>(AddToQueueClick);
            ClearQueueCommand = new RelayCommand(ClearQueue);
            LoadElements();
        }
        public void LoadElements()
        {
            LoadCurrentTrackItems();
            LoadNextInQueueItems();
            LoadNextUpItems();
        }

        //Methods that are used in LoadElements to update the view and display the tracks
        #region LoadElements
        public void LoadCurrentTrackItems()
        {
            //Create ObservableCollections(OC) of class MyQueueItem (The class is at the bottom of this file).
            //This OC are used in the XAML of MyQueueView in a ListBox to draw the elements.
            if (_mainWindowVM.PlayQueueController.PQ.CurrentTrack != null)
            {
                //makes an OC for CurrentTrack
                PlayingNowItems = new ObservableCollection<MyQueueItem>()
                {
                    //create instance of MyQueueItem with the correspondent properties
                new MyQueueItem()
                {
                    ButtonID = "",
                    Name= _mainWindowVM.PlayQueueController.PQ.CurrentTrack.Name,
                    Artist=_mainWindowVM.PlayQueueController.PQ.CurrentTrack.Artist,
                    Album= "Album",
                    Duration= _mainWindowVM.PlayQueueController.PQ.CurrentTrack.Duration
                }
                };
            }
            else // if CurrentTrack is empty, the OC is cleared.
            {
                PlayingNowItems = new ObservableCollection<MyQueueItem>();
            }
        }


        public void LoadNextInQueueItems()
        {
            if (_mainWindowVM.PlayQueueController.PQ.TrackQueue.Count > 0)
            {
                //Makes an OB for All tracks in TrackQueue
                NextInQueueItems = new ObservableCollection<MyQueueItem>();
                _count = 0; //this count is used to give every item a ID
                foreach (var item in _mainWindowVM.PlayQueueController.PQ.TrackQueue)
                {
                    //Create a instance for each item in TrackQueue
                    NextInQueueItems.Add(new MyQueueItem()
                    {
                        ButtonID = $"NextInQueue {_count}",
                        Name = item.Name,
                        Artist = item.Artist,
                        Album = "Album",
                        Duration = item.Duration
                    }); ;
                    _count++;
                }
            }
            else // if CurrentTrack is empty, the OC is cleared.
            {
                NextInQueueItems = new ObservableCollection<MyQueueItem>();
            }
        }

        public void LoadNextUpItems()
        {
            if (_mainWindowVM.PlayQueueController.PQ.TrackWaitingList.Count > 0)
            {
                //Makes an OB for All tracks in TrackWaitingList
                NextUpItems = new ObservableCollection<MyQueueItem>();
                _count = 0;//this count is used to give every item a ID
                foreach (var item in _mainWindowVM.PlayQueueController.PQ.TrackWaitingList)
                {
                    //This LINQ expression returns the index of WaitingListToQueueTrack
                    int indexOfWaitingListToQueueTrack = _mainWindowVM.PlayQueueController.PQ.TrackWaitingList.TakeWhile(n => n != _mainWindowVM.PlayQueueController.PQ.WaitingListToQueueTrack).Count();

                    //Create a instance for each item after the WaitingListToQueueTrack in TrackWaitingList
                    if (_count > indexOfWaitingListToQueueTrack)
                    {
                        NextUpItems.Add(new MyQueueItem()
                        {
                            ButtonID = $"NextUp {_count}",
                            Name = item.Name,
                            Artist = item.Artist,
                            Album = "Album",
                            Duration = item.Duration
                        });
                    }
                    _count++;
                }
            }
            else// if CurrentTrack is empty, the OC is cleared.
            {
                NextInQueueItems = new ObservableCollection<MyQueueItem>();
            }
        }
        #endregion

        //Handles the double click on a track outside of the play button in the queue
        private void OuterClick(Object sender)
        {
            String item = (string)sender; //get ButtonID (this will be splited in two portions, Type and index)
            string type;
            int index= -1; //if at the end of the if statement it did not change. It means the type is "PlayingNow" and that an index is not needed

            if (!item.Equals(""))//If item ButtonID is not empty.
            {
                string[] buttonId = item.Split(null); //split ButtonID
                type = buttonId[0];
                index = int.Parse(buttonId[1]);
            }
            else//if item is blank it means it does not have a ID, so it is a playingNow type.
            {
                type = "PlayingNow";
            }


            //Now that we have the type and the index we can define what needs to be done with the pressed element.
            if (type.Equals("PlayingNow"))
                //volgende sprint
                Console.WriteLine();
            if (type.Equals("NextInQueue"))
                //vongende sprint
                Console.WriteLine();
            if (type.Equals("NextUp"))
            {
                Track playTrack = _mainWindowVM.PlayQueueController.PQ.TrackWaitingList.ElementAt(index);//gets the element at the given index
                _mainWindowVM.PlayQueueController.PlayTrack(playTrack);//set this element as CurrentTrack
                _mainWindowVM.CurrentTrackElement.Source = playTrack.AudioFile; // play the track
            }


            LoadElements();
        }

        //Handles the click on the play button in the queue
        private void InnerClick(object sender)
        {
            String item = (string)sender; //get ButtonID (this will be splited in two portions, Type and index)
            string type;
            int index = -1; //if at the end of the if statement it did not change. It means the type is "PlayingNow" and that a index is not needed

            if (!item.Equals(""))//If item ButtonID is not empty.
            {
                string[] buttonId = item.Split(null);//split ButtonID
                type = buttonId[0];
                index = Int32.Parse(buttonId[1]);
            }
            else//if item is blank it means it does not have a ID, so it is a playingNow type.
            {
                type = "PlayingNow";
            }


            //Now that we have the type and the index we can define what needs to be done with the pressed element.
            if (type.Equals("PlayingNow"))
                //volgende sprint
                Console.WriteLine();
            if (type.Equals("NextInQueue"))
                //volgende sprint
                Console.WriteLine();
            if (type.Equals("NextUp"))
            {
                Track playTrack = _mainWindowVM.PlayQueueController.PQ.TrackWaitingList.ElementAt(index);//gets the element at the given index
                _mainWindowVM.PlayQueueController.PlayTrack(playTrack);//set this element as CurrentTrack
                _mainWindowVM.CurrentTrackElement.Source = playTrack.AudioFile; // play the track
            }

            LoadElements();
        }

        private void AddToQueueClick(object sender)
        {
            String item = (string)sender; //get ButtonID (this will be splited in two portions, Type and index)
            string type;
            int index = -1; //if at the end of the if statement it did not change. It means the type is "PlayingNow" and that a index is not needed

            if (!item.Equals(""))//If item ButtonID is not empty.
            {
                string[] buttonId = item.Split(null);//split ButtonID
                type = buttonId[0];
                index = Int32.Parse(buttonId[1]);
            }
            else//if item is blank it means it does not have a ID, so it is a playingNow type.
            {
                type = "PlayingNow";
            }

            Track playTrack = _mainWindowVM.PlayQueueController.PQ.TrackWaitingList.ElementAt(index);//gets the element at the given index
            _mainWindowVM.PlayQueueController.AddTrack(playTrack);
            
            LoadElements();
        }

        //Clears all the track from TrackQueue in PlayQeueu model
        //Refreshes queue view
        private void ClearQueue()
        {
            _mainWindowVM.PlayQueueController.ClearQueue();
            LoadNextInQueueItems();
        }
    }

    public class MyQueueItem//Helping Class for the Observable Collection
    {
        public String ButtonID { get; set; } //composition of a type and an Index
        public String Name { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public int Duration { get; set; }
    }
}

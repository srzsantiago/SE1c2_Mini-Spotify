using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Ritmo.ViewModels;

namespace Ritmo
{
    public class Navigation
    {
        //Event that has ChangeViewModel in MainWindowViewModel bound to it
        public delegate void ChangeViewModelEventHandler(Screen viewModel);
        public static event ChangeViewModelEventHandler ViewModelChanged;

        private static Stack<Screen> _previousViewModelStack = new Stack<Screen>();
        private static Stack<Screen> _nextViewModelStack = new Stack<Screen>();
        private static Screen _nextViewModel { get; set; }
        private static Screen _previousViewModel { get; set; }
        public static Screen CurrentViewModel { get; set; }

        //Sets CurrentViewModel in this class
        //Calls ChangeViewModel method in MainWindowViewModel to actually change mainwindow
        public static void ChangeViewModel(Screen viewModel)
        {
            CurrentViewModel = viewModel;
            ViewModelChanged(CurrentViewModel); //Calls event to change view in MainWindow
        }

        //Adds currentviewmodel to previousviewmodelstack and changes viewmodel
        public static void ToViewModel(Screen viewModel)
        {
            _previousViewModelStack.Push(CurrentViewModel); //Adds current viewmodel to previous viewmodel stack
            ChangeViewModel(viewModel);
        }

        //Clears next viewmodel stack when another page is opened when not using previous or next
        public static void ToClickedViewModel(Screen viewModel)
        {
            _nextViewModelStack.Clear();
            ToViewModel(viewModel);
        }

        public static void ToPreviousViewModel()
        {
            if (_previousViewModelStack.Count != 0)
            {
                _nextViewModelStack.Push(CurrentViewModel); //Places previous viewmodel in next viewmodel stack
                _previousViewModel = _previousViewModelStack.Pop(); //Gets previous viewmodel from previous stack

                ChangeViewModel(_previousViewModel); //Changes viewmodel to previous viewmodel
            }
        }

        public static void ToNextViewModel()
        {
            if (_nextViewModelStack.Count != 0)
            {
                _nextViewModel = _nextViewModelStack.Pop(); //Gets next viewmodel from stack

                ToViewModel(_nextViewModel); //Changes viewmodel to next view model
            }
        }

        //Searches for PlaylistViewModel in stack and returns it
        private static void RemovePlaylistViewModelFromStack(Stack<Screen> viewModelStack, int playlistID)
        {
            foreach (Screen item in viewModelStack)
            {
                if(item is PlaylistViewModel)
                {
                    if(((PlaylistViewModel)item).PlaylistController.Playlist.TrackListID == playlistID)
                    {
                        RemoveViewModel(item);
                        return;
                    }
                }
            }
        }

        //Calls Removeplaylistviewmodelfromstack with both stacks
        public static void RemovePlaylistViewModel(int playlistID)
        {
            RemovePlaylistViewModelFromStack(_previousViewModelStack, playlistID);
            RemovePlaylistViewModelFromStack(_nextViewModelStack, playlistID);
        }

        public static void RemoveViewModel(Screen viewModel)
        {
            if (_nextViewModelStack.Contains(viewModel))
                RemoveViewModelFromStack(_nextViewModelStack, viewModel);

            if (_previousViewModelStack.Contains(viewModel))
                RemoveViewModelFromStack(_previousViewModelStack, viewModel);
        }

        private static void RemoveViewModelFromStack(Stack<Screen> viewModelStack, Screen viewModel)
        {
            //Creates list from stack and removes viewmodel
            List<Screen> removalList = viewModelStack.ToList(); 
            removalList.Remove(viewModel); 

            viewModelStack.Clear(); //Clears stack

            removalList.ForEach(vm => viewModelStack.Push(vm)); //Rebuilds stack with list

        }

        public static void InitializeViewModelNavigation()
        {
            _previousViewModelStack = new Stack<Screen>();
            _nextViewModelStack = new Stack<Screen>();
        }
    }
}

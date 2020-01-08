using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                ViewModelChanged(CurrentViewModel); //Calls event to change view in MainWindow
            }
            catch (NullReferenceException)
            {
                //MainWindowView is not instantiated
                //Under normal circumstances impossible. This Try Catch is for testing purposes.
            }

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

        //Searches for PlaylistViewModel in stack based on playlistID and calls RemoveViewModel if there's a match
        private static void RemovePlaylistViewModelFromStack(Stack<Screen> viewModelStack, int playlistID)
        {
            List<Screen> removalList = StackToList(viewModelStack);
            int initialCount = removalList.Count;

            for (int i = 0; i < removalList.Count; i++)
            {
                if (removalList[i] is PlaylistViewModel)
                {
                    if (((PlaylistViewModel)removalList[i]).PlaylistController.Playlist.TrackListID == playlistID)
                    {
                        removalList.RemoveAll(x => x.Equals(removalList[i]));
                        i--;
                    }
                }
            }

            if (removalList.Count < initialCount)
                RebuildListToStack(removalList, viewModelStack);
        }

        //Calls RemovePlaylistViewModelFromStack with both stacks and playlistID
        public static void RemovePlaylistViewModel(int playlistID)
        {
            RemovePlaylistViewModelFromStack(_previousViewModelStack, playlistID);
            RemovePlaylistViewModelFromStack(_nextViewModelStack, playlistID);

            if (CurrentViewModel is PlaylistViewModel)
                ChangeViewModel(_previousViewModelStack.Pop());
        }


        //Checks if viewmodel is in a stack and call RemoveViewModelFromStack to remove it
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
            List<Screen> removalList = StackToList(viewModelStack);
            removalList.Remove(viewModel);

            RebuildListToStack(removalList, viewModelStack);
        }

        //Changes a stack to a list
        private static List<Screen> StackToList(Stack<Screen> viewModelStack)
        {
            return viewModelStack.ToList();
        }

        private static void RebuildListToStack(List<Screen> removalList, Stack<Screen> viewModelStack)
        {
            viewModelStack.Clear(); //Clears stack

            //Rebuilds stack with list
            removalList.Reverse();
            removalList.ForEach(vm => viewModelStack.Push(vm));
        }

        //Used in MainWindow to initialize navigation
        public static void InitializeViewModelNavigation()
        {
            _previousViewModelStack = new Stack<Screen>();
            _nextViewModelStack = new Stack<Screen>();
        }


        ///<summary>
        ///Not used in application! For testing purposes only!
        ///</summary>
        public static void ClearStacks()
        {
            _previousViewModelStack.Clear();
            _nextViewModelStack.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace Ritmo
{
    public class Navigation
    {
        //Event that has ChangeViewModel in MainWindowViewModel bound to it
        public delegate void ChangeViewModelEventHandler(Screen viewModel);
        public static event ChangeViewModelEventHandler ViewModelChanged;

        private static Stack<Screen> _previousViewModelStack = new Stack<Screen>();
        private static Stack<Screen> _nextViewModelStack = new Stack<Screen>();
        private static Screen NextViewModel { get; set; }
        private static Screen PreviousViewModel { get; set; }
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
                PreviousViewModel = _previousViewModelStack.Pop(); //Gets previous viewmodel from previous stack

                ChangeViewModel(PreviousViewModel); //Changes viewmodel to previous viewmodel
            }
        }

        public static void ToNextViewModel()
        {
            if (_nextViewModelStack.Count != 0)
            {
                NextViewModel = _nextViewModelStack.Pop(); //Gets next viewmodel from stack

                ToViewModel(NextViewModel); //Changes viewmodel to next view model
            }
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

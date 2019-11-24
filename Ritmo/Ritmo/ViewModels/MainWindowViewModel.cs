using Caliburn.Micro;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        #region Commands
        public ICommand ChangeViewModelCommand { get; set; }
        #endregion

        #region ViewModel attributes
        private Screen _currentViewModel = new HomeViewModel();
        public Screen HomeViewModel { get; set; } = new HomeViewModel();
        public Screen SearchViewModel { get; set; } = new SearchViewModel();
        public Screen CategoriesViewModel { get; set; } = new CategoriesViewModel();
        public Screen FollowingViewModel { get; set; } = new FollowingViewModel();
        public Screen AllPlaylistsViewModel { get; set; } 
        public Screen MyQueueViewModel { get; set; } = new MyQueueViewModel();
        #endregion

        public MainWindowViewModel()
        {
            ChangeViewModelCommand = new RelayCommand<object>(ChangeViewModel); //Sets the command to the corresponding method
            AllPlaylistsViewModel = new AllPlaylistsViewModel(this);
        }

        //Changes CurrentViewModel and sets the frame
        public void ChangeViewModel(object ViewModel)
        {
            this.CurrentViewModel = (Screen)ViewModel;
        }

        public Screen CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                NotifyOfPropertyChange(() => CurrentViewModel);
            }
        }
    }
}

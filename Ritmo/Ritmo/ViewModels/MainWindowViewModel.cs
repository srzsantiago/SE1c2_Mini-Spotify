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
        public ICommand ChangeViewModelCommand { get; set; }
        private Screen _currentViewModel = new HomeViewModel();

        public MainWindowViewModel()
        {
            ChangeViewModelCommand = new RelayCommand<object>(ChangeViewModel);
        }

        public Screen HomeViewModel { get; set; } = new HomeViewModel();
        public Screen SearchViewModel { get; set; } = new SearchViewModel();
        public Screen CategoriesViewModel { get; set; } = new CategoriesViewModel();
        public Screen FollowingViewModel { get; set; } = new FollowingViewModel();
        public Screen AllPlaylistsViewModel { get; set; } = new AllPlaylistsViewModel();
        public Screen MyQueueViewModel { get; set; } = new MyQueueViewModel();
        

        public Screen CurrentViewModel
        {
            get { return _currentViewModel; }
            set {
                _currentViewModel = value;
                NotifyOfPropertyChange(() => CurrentViewModel);
            }
        }

        public void ChangeViewModel(object ViewModel)
        {
            this.CurrentViewModel = (Screen)ViewModel;
        }

        
    }
}

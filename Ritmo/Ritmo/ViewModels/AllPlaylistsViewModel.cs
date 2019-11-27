using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class AllPlaylistsViewModel : Screen
    {
        

        public MainWindowViewModel MainWindow { get; set; }
        public PlaylistViewModel PlaylistViewModel { get; set; } = new PlaylistViewModel();
        public ICommand OpenPlaylistViewModelCommand { get; set; }

        private static AllPlaylistsController _allPlaylistsController;

        public static AllPlaylistsController AllPlaylistsController
        {

            get { return _allPlaylistsController; }
            set { _allPlaylistsController = value; }
        }

        public AllPlaylistsViewModel(MainWindowViewModel MainWindow)
        {
            AllPlaylistsController = new AllPlaylistsController();
            OpenPlaylistViewModelCommand = new RelayCommand<Screen>(OpenPlaylistViewModel);
            this.MainWindow = MainWindow;
        }

        //Sets CurrentViewModel in the MainWindow to a PlaylistViewModel
        public void OpenPlaylistViewModel(Screen PlaylistViewModel)
        {
            MainWindow.ChangeViewModel(PlaylistViewModel);
        }
    }
}

using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class PopUpWindowViewModel : Screen
    {
        IWindowManager WindowManager = new WindowManager();

        #region Command
        public ICommand OnOkayClickCommand { get; set; }
        #endregion

        #region Popupwindow Attributes
        private AllPlaylistsViewModel _viewModel;
        private string _transferString;
        private string _message;
        private string _textBoxMessage;
        private string _buttonContent;

        public string ButtonContent
        {
            get { return _buttonContent; }
            set { _buttonContent = value; }
        }

        public string TextBoxMessage
        {
            get { return _textBoxMessage; }
            set { _textBoxMessage = value; }
        }

        public string TransferString
        {
            get { return _transferString; }
            set { _transferString = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        #endregion

        public AllPlaylistsViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        static int PlaylistID;

        //Constructor for AllPlaylistsViewModel
        public PopUpWindowViewModel(AllPlaylistsViewModel viewModel, int playlistID)
        {
            ViewModel = viewModel;
            OnOkayClickCommand = new RelayCommand<object>(OnOkayClick);
            PlaylistID = playlistID;
        }

        public void OnOkayClick(object param)
        {
            Playlist p1 = AllPlaylistsViewModel.AllPlaylistsController.GetPlaylist(PlaylistID); //Removes playlist with playlistID
            AllPlaylistsViewModel.AllPlaylistsController.RemovePlaylist(p1); //Removes playlist with playlistID
            this.TryClose();
        }

    }
}

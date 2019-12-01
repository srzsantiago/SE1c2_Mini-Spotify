using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Ritmo.ViewModels
{
    class RegisterViewModel : Screen
    {

        #region ViewModel attributes
        private Screen _currentViewModel;
        private Screen LoginViewModel { get; set; } = new LoginViewModel();
        private Screen ArtistViewModel { get; set; } = new ArtistRegisterViewModel();
        public Screen CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                NotifyOfPropertyChange();
            }
        }
        public void ChangeViewModel(Screen ViewModel)
        {
            this.CurrentViewModel = ViewModel;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;

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

        #region labels
        private string _name;
        private string _email;
        private string _errorMessage;

        public string Name
        {
            get { if (_name == null)
                    _name = "";
                return _name; }
            set { _name = value;
                NotifyOfPropertyChange("Name");
            }
        }

        public string Email
        {
            get { if (_email == null)
                    _email = "";
                return _email; }
            set { _email = value;
                NotifyOfPropertyChange("Email");
            }
        }

        public string ErrorMessage
        {
            get { if (_errorMessage == null)
                    _errorMessage = "";
                return _errorMessage; }
            set { _errorMessage = value;
                NotifyOfPropertyChange("ErrorMessage");
            }
        }
        #endregion

        #region Command
        public ICommand CreateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ArtistCommand { get; set; }
        #endregion

        public RegisterViewModel()
        {
            CreateCommand = new RelayCommand<object>(this.CreateClick);
            CancelCommand = new RelayCommand(this.CancelClick);
            ArtistCommand = new RelayCommand(this.ArtistClick);
        }

        private void ArtistClick()
        {
            this.TryClose();
        }

        private void CancelClick()
        {
            this.TryClose();
        }

        private void CreateClick(object parameters)
        {
            var values = parameters as List<object>;
            var passwordBox = values[0] as PasswordBox;
            var confirmPasswordBox = values[1] as PasswordBox;

            if(!Name.Equals("") && IsValidEmail(Email) && IsPasswordMatch(passwordBox, confirmPasswordBox))
            {
                //Register register = new Register(Name, Email, passwordBox.Password, confirmPasswordBox.Password);
                this.TryClose();
            }
            else
            {
                ErrorMessage = "One or more fields are incorrect.";
            }
        }

        private bool IsPasswordMatch(PasswordBox passwordBox, PasswordBox confirmPasswordBox)
        {
            return passwordBox.Password == confirmPasswordBox.Password;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                Regex rx = new Regex
                    (@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                return rx.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

    }
}

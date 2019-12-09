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
            CurrentViewModel = ViewModel;
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
            CreateCommand = new RelayCommand<object>(CreateClick);
            CancelCommand = new RelayCommand(CancelClick);
            ArtistCommand = new RelayCommand(ArtistClick);
        }

        private void ArtistClick()
        {
            TryClose();
        }

        private void CancelClick()
        {
            TryClose();
        }

        private void CreateClick(object parameters)
        {
            var values = parameters as List<object>; //set the multiple parameters in one array
            var passwordBox = values[0] as PasswordBox; //set the first value in the array as passwordbox
            var confirmPasswordBox = values[1] as PasswordBox;//set the second value in the array as confirmpasswordbox

            if (!Name.Equals("") && IsValidEmail(Email) && IsPasswordMatch(passwordBox, confirmPasswordBox)) //information validation
            {
                //Register register = new Register(Name, Email, passwordBox.Password, confirmPasswordBox.Password);
                TryClose();
            }
            else
            {
                ErrorMessage = "One or more fields are incorrect.";
            }
        }

        private bool IsPasswordMatch(PasswordBox passwordBox, PasswordBox confirmPasswordBox)//check if the passwords match
        {
            return passwordBox.Password == confirmPasswordBox.Password;
        }

        bool IsValidEmail(string email)//check the email format
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

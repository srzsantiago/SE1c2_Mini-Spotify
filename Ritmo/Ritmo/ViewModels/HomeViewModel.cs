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
    public class HomeViewModel : Screen
    {
        public ICommand ChangeTestStringCommand { get; set; }
        private string _TestString = "Dit is een test!";

        public string TestString
        {
            get { return _TestString; }
            set {
                _TestString = value;
                NotifyOfPropertyChange();
            }
        }


        public HomeViewModel()
        {
            ChangeTestStringCommand = new RelayCommand(ChangeTestString);
        }

        private void ChangeTestString()
        {
            TestString = "Tekst is veranderd";
        }
    }
}

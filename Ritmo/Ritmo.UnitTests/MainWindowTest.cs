using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ritmo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class MainWindowTest
    {
        //Method: ChangeViewModel(Screen ViewModel) - Change the view inside the MainWindowView
        [TestMethod]
        public void ChangeViewModel_ViewModelChanged_ReturnsTrue()
        {
            //Arrange 
            MainWindowViewModel TestMWVM = new MainWindowViewModel();
            SearchViewModel TestHVM = new SearchViewModel();

            //Act
            TestMWVM.ChangeViewModel(TestHVM); //Change viewmodel

            //Assert
            Assert.IsTrue(TestMWVM.CurrentViewModel.Equals(TestHVM)); //Checks if the ViewModel has changed
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ritmo.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class MainWindowTest
    {
        [TestMethod]
        public void PreviousViewModel_ViewModel_PreviousViewModelIsCurrentViewModel()
        {
            //Arrange
            MainWindowViewModel MWVM = new MainWindowViewModel();
            AllPlaylistsViewModel APVM = new AllPlaylistsViewModel(MWVM);
            
            //Act
            //MWVM.ToViewModel(APVM);

            //Assert
            Assert.AreEqual(MWVM.CurrentViewModel, APVM);
        }


        // doesnt work yet?

    }
}

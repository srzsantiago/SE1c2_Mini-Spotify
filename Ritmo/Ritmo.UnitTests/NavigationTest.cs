using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ritmo.ViewModels;
using System;
using System.Collections.Generic;

namespace Ritmo.UnitTests
{
    [TestClass]
    public class NavigationTest
    {
        HomeViewModel _homeViewModel = new HomeViewModel();
        AllPlaylistsViewModel _allPlaylistViewModel = new AllPlaylistsViewModel();
        MyQueueViewModel _myQueueViewModel = new MyQueueViewModel();
        FollowingViewModel _followingViewModel = new FollowingViewModel();
        SearchViewModel _searchViewModel = new SearchViewModel();

        [TestInitialize]
        public void TestInitialize()
        {
            //Sets up HomeViewModel as CurrentViewModel to simulate application starting up
            Navigation.ChangeViewModel(_homeViewModel);

        }

        [TestCleanup]
        public void TestCleanUp()
        {
            //Guarantees clean working environment for every test.
            Navigation.ClearStacks();
        }

        [TestMethod]
        public void ToViewModel_ChangeViewModel_ViewModelChanged()
        {
            //Act
            Navigation.ToViewModel(_allPlaylistViewModel);
            //Assert
            Assert.IsTrue(Navigation.CurrentViewModel is AllPlaylistsViewModel);
        }

        [TestMethod]
        public void ToPreviousViewModel_GoToPreviousViewModelWithFilledStack_ViewModelChanged()
        {
            //Arrange
            //Adding a bunch of viewmodels to fill up previous stack
            //CurrentViewModel will be _followingViewModel
            Navigation.ToViewModel(_myQueueViewModel);
            Navigation.ToViewModel(_followingViewModel);

            //Act
            //Go back one viewmodel
            Navigation.ToPreviousViewModel();

            //Assert
            //CurrentViewModel should be _myQueueViewModel
            Assert.IsTrue(Navigation.CurrentViewModel is MyQueueViewModel);
        }

        [TestMethod]
        public void ToNextViewModel_GoToNextViewModelWithFilledStack_ViewModelChanged()
        {
            //Arrange
            //Adding a viewmodel to fill up previous stack
            //Going back one viewmodel to fill up next stack
            //CurrentViewModel will be _followingViewModel
            Navigation.ToViewModel(_followingViewModel);
            Navigation.ToPreviousViewModel();

            //Act
            //Return to viewmodel
            Navigation.ToNextViewModel();

            //Assert
            //CurrentViewModel should be _followingViewModel
            Assert.IsTrue(Navigation.CurrentViewModel is FollowingViewModel);
        }

        [TestMethod]
        public void ToNextViewModel_GoToNextViewModelWithEmptyNextStack_NoViewModelChange()
        {
            //Act
            Navigation.ToNextViewModel();

            //Assert
            //CurrentViewModel should be initialized CurrentViewModel
            Assert.IsTrue(Navigation.CurrentViewModel is HomeViewModel);

        }

        [TestMethod]
        public void ToPreviousViewModel_GoToPreviousViewModelWithEmptyPreviousStack_NoViewModelChange()
        {
            //Act
            Navigation.ToPreviousViewModel();

            //Assert
            //CurrentViewModel should be initialized CurrentViewModel
            Assert.IsTrue(Navigation.CurrentViewModel is HomeViewModel);
        }

        [TestMethod]
        public void RemoveViewModel_RemoveViewModelFromNextStack_ViewModelRemovedAndOtherViewModelIsShown()
        {
            //Arrange
            //Go to two different viewmodels and then go to previous viewmodel
            Navigation.ToViewModel(_followingViewModel);
            Navigation.ToViewModel(_myQueueViewModel);
            Navigation.ToPreviousViewModel();
            Navigation.ToPreviousViewModel();

            //Act
            //Remove viewmodel and then go to next viewmodel (_myQueueViewModel)
            Navigation.RemoveViewModel(_followingViewModel);
            Navigation.ToNextViewModel();

            //Assert
            //Check if CurrentViewModel is not FollowingViewModel
            //Check if CurrentViewModel is MyQueueViewModel
            Assert.IsFalse(Navigation.CurrentViewModel is FollowingViewModel);
            Assert.IsTrue(Navigation.CurrentViewModel is MyQueueViewModel);
        }

        [TestMethod]
        public void RemoveViewModel_RemoveViewModelFromPreviousStack_ViewModelRemovedAndOtherViewModelIsOpened()
        {
            //Arrange
            //Go to viewmodel
            Navigation.ToViewModel(_followingViewModel);
            Navigation.ToViewModel(_myQueueViewModel);

            //Act
            //Remove viewmodel and then go to previous viewmodel
            Navigation.RemoveViewModel(_followingViewModel);
            Navigation.ToPreviousViewModel();

            //Assert
            //Check if CurrentViewModel is not FollowingViewModel
            //Check if CurrentViewModel is HomeViewModel
            Assert.IsFalse(Navigation.CurrentViewModel is FollowingViewModel);
            Assert.IsTrue(Navigation.CurrentViewModel is HomeViewModel);
        }

    }
}
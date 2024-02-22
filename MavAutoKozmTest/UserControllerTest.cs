using MavAutoKozm.Controllers;
using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest_MavAutoKozm
{
    public class UserControllerTest
    {
        private Mock<IMavAutoKozmRepository> _mockRepository;
        private UsersController _userController;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IMavAutoKozmRepository>();
            _userController = new UsersController(_mockRepository.Object, null);
        }

        [Test]
        public void IndexTest()
        {
            //Arrange

            //Action
            var result = _userController.Index();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

        [Test]
        public void UserExistsValidTest()
        {
            //Arrange
            List<AppUser> mockAppUsers = new List<AppUser>{
                new AppUser
                {
                    AspNetUserId = "1de",
                    Email = "www.w",
                    FirstMidName = "Laci",
                    LastName = "Varga",
                    ID = 3,
                    PhoneNumber = "1234567890",
                    Vehicles = new List<Vehicle>()
                }};
            _mockRepository.Setup(e => e.AppUsers).Returns(mockAppUsers);

            //Action

            var result = _userController.UserExists(3);

            //Assert
            Assert.IsInstanceOf<bool>(result);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void UserExistsInvalidTest()
        {
            //Arrange
            
            //Action

            var result = _userController.UserExists(3);

            //Assert
            Assert.IsInstanceOf<bool>(result);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CreateTestGet()
        {
            //Arrange

            //Action
            var result = _userController.Create();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void CreateTestPost()
        {
            //Arrange
            List<AppUser> mockAppUsers = new List<AppUser>{
                new AppUser
                {
                    AspNetUserId = "1de",
                    Email = "www.w",
                    FirstMidName = "Laci",
                    LastName = "Varga",
                    ID = 3,
                    PhoneNumber = "1234567890",
                    Vehicles = new List<Vehicle>()
                }};
            _mockRepository.Setup(e => e.AppUsers).Returns(mockAppUsers);
            //Action
            var result = _userController.Create();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task DeleteConfirmedTest_NullVehicle()
        {
            // Arrange
            int mockAppUsersId = 1;

            List<AppUser> mockAppUsers = null;

            _mockRepository.Setup(x => x.AppUsers).Returns(mockAppUsers); // Simulate null list

            // Action
            var result = await _userController.DeleteConfirmed(mockAppUsersId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);
        }

        [Test]
        public async Task DeleteConfirmedTest_SuccessfulDeletion()
        {
            // Arrange
            int mockAppUsersId = 1; 
            List<AppUser> mockAppUsers = new List<AppUser>{
                new AppUser
                {
                    AspNetUserId = "1de",
                    Email = "www.w",
                    FirstMidName = "Laci",
                    LastName = "Varga",
                    ID = 3,
                    PhoneNumber = "1234567890",
                    Vehicles = new List<Vehicle>()
                }};

            _mockRepository.Setup(x => x.AppUsersDelete(It.IsAny<AppUser>())).Callback<AppUser>(appuser =>
            {
                // Simulate the deletion in the mock repository
                _mockRepository.Object.AppUsers.Remove(appuser);
            });

            _mockRepository.Setup(x => x.AppUsers).Returns(mockAppUsers);
            // Action
            var result = await _userController.DeleteConfirmed(mockAppUsersId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
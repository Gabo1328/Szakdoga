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

        [Test]
        public async Task DetailsTestNullId()
        {
            // Arrange
            List<Orders> mockOrders = new List<Orders>
            {
                new Orders
                {
                    AppUserId = 1,
                    Category = 0,
                    Ceramic = true,
                    CompletedTime = DateTime.Now,
                    Id = 1,
                    Inner = true,
                    OrderTime = DateTime.Now,
                    Outer = false,
                    Polish = true,
                    Ppf = true,
                    Price = 100000,
                    Quality = 2,
                    VehicleId = 2,
                    Wax = false,
                }
            };

            _mockRepository.Setup(e => e.Orders).Returns(mockOrders);

            // Action
            var result = await _userController.Details(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DetailsTestNullOrder()
        {
            // Arrange
            List<Orders> mockOrders = null;

            _mockRepository.Setup(e => e.Orders).Returns(mockOrders);

            // Action
            var result = await _userController.Details(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void EditTestget()
        {
            //Arrange

            //Action
            var result = _userController.Edit(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

        [Test]
        public async Task EditTestPostNotEqualId()
        {
            // Arrange
            AppUser mockAppUsers = new AppUser
            {
                AspNetUserId = "1ererde",
                Email = "www.wddfdf",
                FirstMidName = "László",
                LastName = "Kiss",
                ID = 3,
                PhoneNumber = "1234567890",
                Vehicles = new List<Vehicle>()
            };

            _mockRepository.Setup(x => x.VehiclesUpdate(It.IsAny<Vehicle>()));

            // Action
            var result = await _userController.Edit(2, mockAppUsers);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task EditTestPost_VerifyMailChange()
        {
            // Arrange
            AppUser mockAppUsers = new AppUser
            {
                AspNetUserId = "1ererde",
                Email = "www.wddfdf",
                FirstMidName = "László",
                LastName = "Kiss",
                ID = 3,
                PhoneNumber = "1234567890",
                Vehicles = new List<Vehicle>()
            };

            _mockRepository.Setup(x => x.AppUsersUpdate(It.IsAny<AppUser>())).Callback<AppUser>(appuser =>
            {
                // Simulate the update in the mock repository
                mockAppUsers.Email = appuser.Email;
            });

            // Clear ModelState to ensure a valid ModelState
            _userController.ModelState.Clear();

            // Action
            var result = await _userController.Edit(3, mockAppUsers);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotSame("www.wd", mockAppUsers.Email);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
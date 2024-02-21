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
    }
}
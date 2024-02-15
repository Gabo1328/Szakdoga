using MavAutoKozm.Controllers;
using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest_MavAutoKozm
{
    public class HomeControllerTest
    {
        private Mock<IMavAutoKozmRepository> _mockRepository;
        private HomeController _homeController;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IMavAutoKozmRepository>();
            _homeController = new HomeController(_mockRepository.Object);
        }

        [Test]
        public void IndexTest()
        {
            //Arrange

            //Action
            var result = _homeController.Index();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void PrivacyTest()
        {
            //Arrange

            //Action
            var result = _homeController.Privacy();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}
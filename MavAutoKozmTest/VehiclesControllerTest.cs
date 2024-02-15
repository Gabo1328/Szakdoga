using MavAutoKozm.Controllers;
using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest_MavAutoKozm
{
    public class VehiclesControllerTest
    {
        private Mock<IMavAutoKozmRepository> _mockRepository;
        private VehiclesController _vehiclesController;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IMavAutoKozmRepository>();
            _vehiclesController = new VehiclesController(_mockRepository.Object);
        }

        [Test]
        public void CreateTestGet()
        {
            //Arrange

            //Action
            var result = _vehiclesController.Create();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void CreateTestPost()
        {
            //Arrange
            Vehicle mockJarmu = new Vehicle {
                Brand = "Opel",
                Model = "Vectra",
                AppUserId = 1,
                Color = "Piros",
                NumberPlate = "KYW-675",
                Type = "szedán"
            };
            //Action
            var result = _vehiclesController.Create(mockJarmu);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

    }


}
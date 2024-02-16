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

        [Test]
        public void DeleteTestGet()
        {
            //Arrange

            //Action
            var result = _vehiclesController.Delete(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

        [Test]
        public void DeleteTestPost()
        {
            //Arrange
            Vehicle mockJarmu = new Vehicle
            {
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

        [Test]
        public void DetailsTest()
        {
            //Arrange
            List<Orders> mockOrders = new List<Orders>{
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
                }};

            _mockRepository.Setup(e => e.Orders).Returns(mockOrders);

            //Action
            var result = _vehiclesController.Details(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

    }


}
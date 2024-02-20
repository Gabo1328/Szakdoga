using MavAutoKozm.Controllers;
using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Moq;
using NUnit.Framework.Interfaces;

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
            var result = _vehiclesController.Delete(1);

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
            var result = await _vehiclesController.Details(null);

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
            var result = await _vehiclesController.Details(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void EditTestget()
        {
            //Arrange

            //Action
            var result = _vehiclesController.Edit(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

        [Test]
        public void EditTestPost()
        {
            //Arrange
            Vehicle mockJarmu = new Vehicle
            {
                Brand = "Opel",
                Model = "Astra",
                AppUserId = 1,
                Color = "Fekete",
                NumberPlate = "LCG-017",
                Type = "ferdehátú"
            };
            //Action
            var result = _vehiclesController.Edit(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

        [Test]
        public async Task EditTestPostNotEqualId()
        {
            // Arrange
            Vehicle mockVehicle = new Vehicle
            {
                Id = 1,
                Brand = "Opel",
                Model = "Astra",
                AppUserId = 1,
                Color = "Fekete",
                NumberPlate = "LCG-017",
                Type = "ferdehátú"
            };

            _mockRepository.Setup(x => x.VehiclesUpdate(It.IsAny<Vehicle>()));

            // Action
            var result = await _vehiclesController.Edit(2, mockVehicle);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void IndexTest()
        {
            //Arrange

            //Action
            var result = _vehiclesController.Index();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }


    }


}
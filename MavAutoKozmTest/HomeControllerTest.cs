using MavAutoKozm.Controllers;
using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Http;
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

        [Test]
        public void MegrendelesekTest()
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
            var result = _homeController.Megrendelesek();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void DeleteOrderTest()
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
            mockOrders.Add(new Orders
            {
                AppUserId = 2,
                Category = 1,
                Ceramic = true,
                CompletedTime = DateTime.Now,
                Id = 2,
                Inner = false,
                OrderTime = DateTime.Now,
                Outer = false,
                Polish = true,
                Ppf = true,
                Price = 200000,
                Quality = 2,
                VehicleId = 2,
                Wax = false,
            });
            _mockRepository.Setup(e => e.Orders).Returns(mockOrders);
            _mockRepository.Setup(e => e.OrdersDelete(It.IsAny<Orders>()));
            int EredetiErtek = mockOrders.Count;
            
            //Action
            var result = _homeController.DeleteOrder(1);

            int UjErtek = mockOrders.Count;


            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            // Eredetileg 2 értéket tettünk bele. A törlés tesztjével 1-et töröltünk.
            // Tehát a teszt akkor sikeres ha 1 érték marad
            //Assert.IsTrue(EredetiErtek - UjErtek == 1);
        }

        [Test]
        public void CategorySelectTest()
        {
            //Arrange

            //Action
            var result = _homeController.CategorySelect();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }
        [Test]
        public void DetailsOrderTest()
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
            var result = _homeController.DetailsOrder(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

        [Test]
        public async Task DetailsOrder_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Arrange
            List<Orders> mockOrders = null;          

            _mockRepository.Setup(e => e.Orders).Returns(mockOrders);

            // Action
            var result = await _homeController.DetailsOrder(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result); // Figyelj a Result property-re
        }

        [Test]
        public void CategorySelect_ReturnsViewResult()
        {
            // Arrange
            var contextMock = new Mock<IMavAutoKozmRepository>();
            var controller = new HomeController(contextMock.Object);

            // Act
            var result = controller.CategorySelect();

            // Assert
            Assert.IsNotNull(result);
            //Assert.IsInstanceOf<Task<ViewResult>>(result);
        }

        [Test]
        public void GetPrice_CalculatePriceCorrectly()
        {
            // Arrange
            var serviceSelectViewModel = new ServiceSelectViewModel
            {
                Outer = true,
                Inner = true,
                Polish = false,
                Wax = true,
                Ceramic = false,
                Ppf = true,
                Quality = 2
            };

            // Act
            var result = _homeController.GetPrice(serviceSelectViewModel);

            // Assert
            // Expected price calculation: (3 selected services) * (10000) * (2 quality + 1) = 60000
            Assert.AreEqual(120000, result);
        }

        [Test]
        public void GetPrice_NoSelectedServices_ReturnZeroPrice()
        {
            // Arrange
            var serviceSelectViewModel = new ServiceSelectViewModel();

            // Act
            var result = _homeController.GetPrice(serviceSelectViewModel);

            // Assert
            // No selected services, so the price should be zero
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetPrice_MaxQuality_ReturnsCorrectPrice()
        {
            // Arrange
            var serviceSelectViewModel = new ServiceSelectViewModel
            {
                Outer = true,
                Inner = true,
                Polish = true,
                Wax = true,
                Ceramic = true,
                Ppf = true,
                Quality = 5
            };

            // Act

            //Action
            var result = _homeController.GetPrice(serviceSelectViewModel);

            // Assert
            // Expected price calculation: (6 selected services) * (10000) * (5 quality + 1) = 360000
            Assert.AreEqual(360000, result);
        }

        [Test]
        public async Task DeleteTest_NullOrder()
        {
            // Arrange
            int mockOrderId = 1;

            List<Orders> mockOrders = null;

            _mockRepository.Setup(x => x.Orders).Returns(mockOrders); // Simulate null list

            // Action
            var result = await _homeController.DeleteOrder(mockOrderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);
        }
        [Test]
        public async Task DeleteTest_OrderSuccess()
        {
            // Arrange
            int mockOrderId = 1;

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

            _mockRepository.Setup(x => x.Orders).Returns(mockOrders); // Simulate null list

            // Action
            var result = await _homeController.DeleteOrder(mockOrderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
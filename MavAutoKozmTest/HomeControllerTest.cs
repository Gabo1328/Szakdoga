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
    }
}
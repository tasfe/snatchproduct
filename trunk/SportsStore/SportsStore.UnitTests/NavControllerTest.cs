using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
namespace SportsStore.UnitTests
{


    /// <summary>
    ///这是 NavControllerTest 的测试类，旨在
    ///包含所有 NavControllerTest 单元测试
    ///</summary>
    [TestClass()]
    public class NavControllerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Menu 的测试
        ///</summary>
        // TODO: 确保 UrlToTest 特性指定一个指向 ASP.NET 页的 URL(例如，
        // http://.../Default.aspx)。这对于在 Web 服务器上执行单元测试是必需的，
        //无论要测试页、Web 服务还是 WCF 服务都是如此。
        [TestMethod()]
        public void Can_Creat_Categories_Test()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
                        new Product(){ProductId=1, Name="P1", Category="Watersports"},
                        new Product(){ProductId=2, Name="P2", Category="Watersports"},
                        new Product(){ProductId=3, Name="P3", Category="Soccer"},
                        new Product(){ProductId=4, Name="P4", Category="Soccer"},
                        new Product(){ProductId=5, Name="P5", Category="Soccer"},
                        new Product(){ProductId=6, Name="P6", Category="Chess"},
                        new Product(){ProductId=7, Name="P7", Category="Chess"},
                        new Product(){ProductId=8, Name="P8", Category="Chess"}
                    }.AsQueryable()
                );

            NavController nvaController = new NavController(mock.Object);

            //action
            IEnumerable<string> result = nvaController.Menu(null).Model as IEnumerable<string>;
            string[] categories = result.ToArray();

            //Assert
            Assert.AreEqual(categories.Length, 3);
            Assert.AreEqual(categories[0], "Chess");
            Assert.AreEqual(categories[1], "Soccer");
            Assert.AreEqual(categories[2], "Watersports");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
                        new Product(){ProductId=1, Name="P1", Category="Watersports"},
                        new Product(){ProductId=2, Name="P2", Category="Watersports"},
                        new Product(){ProductId=3, Name="P3", Category="Soccer"},
                        new Product(){ProductId=4, Name="P4", Category="Soccer"},
                        new Product(){ProductId=5, Name="P5", Category="Soccer"},
                        new Product(){ProductId=6, Name="P6", Category="Chess"},
                        new Product(){ProductId=7, Name="P7", Category="Chess"},
                        new Product(){ProductId=8, Name="P8", Category="Chess"}
                    }.AsQueryable()
                );

            NavController nvaController = new NavController(mock.Object);

            //action
           string result= nvaController.Menu("Chess").ViewBag.SelectedCategory;

            //Assert

           Assert.AreEqual(result, "Chess");

        }
    }
}

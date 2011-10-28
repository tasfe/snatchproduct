using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Entities;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SportsStore.UnitTests
{


    /// <summary>
    ///这是 AdminControllerTest 的测试类，旨在
    ///包含所有 AdminControllerTest 单元测试
    ///</summary>
    [TestClass()]
    public class AdminControllerTest
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

        [TestMethod]
        public void Index_Contains_All_Products()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
            new Product(){ ProductId=1,Name="P1"},
            new Product(){ ProductId=2,Name="P2"},
            new Product(){ ProductId=3,Name="P3"},
            new Product(){ ProductId=4,Name="P4"}, 
            new Product(){ ProductId=5,Name="P5"}
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);
            //Act
            Product[] result = ((IEnumerable<Product>)controller.Index().ViewData.Model).ToArray();

            //Assert
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual(result[0].Name, "P1");
            Assert.AreEqual(result[1].Name, "P2");
            Assert.AreEqual(result[2].Name, "P3");
            Assert.AreEqual(result[3].Name, "P4");
            Assert.AreEqual(result[4].Name, "P5");
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[] { 
            new Product(){ ProductId=1,Name="P1"}
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            //Act
            Product result = controller.Edit(1).Model as Product;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ProductId, 1);
            Assert.AreEqual(result.Name, "P1");
                 
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[] { 
            new Product(){ ProductId=1,Name="P1"}
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            //Act
            Product result = controller.Edit(2).Model as Product;

            //Assert
            Assert.IsNull(result);
        }
    }
}

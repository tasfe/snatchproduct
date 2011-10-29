using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Entities;
using System.Linq;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{


    /// <summary>
    ///这是 ProductControllerTest 的测试类，旨在
    ///包含所有 ProductControllerTest 单元测试
    ///</summary>
    [TestClass()]
    public class ProductControllerTest
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
        ///List 的测试
        ///</summary>
        // TODO: 确保 UrlToTest 特性指定一个指向 ASP.NET 页的 URL(例如，
        // http://.../Default.aspx)。这对于在 Web 服务器上执行单元测试是必需的，
        //无论要测试页、Web 服务还是 WCF 服务都是如此。
        [TestMethod()]
        public void ListTest()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product(){ ProductId=1,Name="P1"},
                new Product(){ ProductId=2,Name="P2"},
                new Product(){ ProductId=3,Name="P3"},
                new Product(){ ProductId=4,Name="P4"},
                new Product(){ ProductId=5,Name="P5"},
                new Product(){ ProductId=6,Name="P6"},  
                new Product(){ ProductId=7,Name="P7"}
            }.AsQueryable<Product>());

            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 2;

            //action
            ProductsListViewModel result = productController.List(null, 2).Model as ProductsListViewModel;

            //assert
            Product[] products = result.Products.ToArray();

            Assert.AreEqual(2, products.Length);
            Assert.AreEqual("P3", products[0].Name);
            Assert.AreEqual("P4", products[1].Name);
        }

        [TestMethod()]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]{
                new Product{ProductId=1,Name="P1"},
                new Product{ProductId=2,Name="P2"},
                new Product{ProductId=3,Name="P3"},
                new Product{ProductId=4,Name="P4"},
                new Product{ProductId=5,Name="P5"},
                new Product{ProductId=6,Name="P6"},
                new Product{ProductId=7,Name="P7"}
                }.AsQueryable<Product>()
                );

            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;

            //action
            ProductsListViewModel viewModel = productController.List(null, 2).Model as ProductsListViewModel;

            //assert
            PagingInfo pagingInfo = viewModel.PagingInfo;
            Assert.AreEqual(3, pagingInfo.ItemsPerPage);//
            Assert.AreEqual(2, pagingInfo.CurrentPage);
            Assert.AreEqual(7, pagingInfo.TotalItems);
            Assert.AreEqual(3, pagingInfo.TotalPages);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
                new Product(){ ProductId=1,Name="P1", Category="Cat1"},
                new Product(){ ProductId=2,Name="P2", Category="Cat1"},
                new Product(){ ProductId=3,Name="P3", Category="Cat2"},
                new Product(){ ProductId=4,Name="P4", Category="Cat2"},
                new Product(){ ProductId=5,Name="P5", Category="Cat2"},
                new Product(){ ProductId=6,Name="P6", Category="Cat3"},
                new Product(){ ProductId=7,Name="P7", Category="Cat3"}
            }.AsQueryable()
                );

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 2;

            //Action
            var result = controller.List("Cat2", 2).Model as ProductsListViewModel;

            //Assert
            Product[] products = result.Products.ToArray();
            Assert.AreEqual(products.Length, 1);
            Assert.AreEqual("Cat2", result.CurrentCategory);
            Assert.IsTrue(products[0].Name == "P5" && products[0].Category == "Cat2");

        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
                new Product(){ ProductId=1,Name="P1", Category="Cat1"},
                new Product(){ ProductId=2,Name="P2", Category="Cat1"},
                new Product(){ ProductId=3,Name="P3", Category="Cat2"},
                new Product(){ ProductId=4,Name="P4", Category="Cat2"},
                new Product(){ ProductId=5,Name="P5", Category="Cat2"},
                new Product(){ ProductId=6,Name="P6", Category="Cat3"},
                new Product(){ ProductId=7,Name="P7", Category="Cat3"},
                new Product(){ ProductId=8,Name="P8", Category="Cat3"},
                new Product(){ ProductId=9,Name="P9", Category="Cat3"},
            }.AsQueryable()
                );

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 2;

            //Action
            int cat1Count = ((ProductsListViewModel)controller.List("Cat1", 1).Model).PagingInfo.TotalItems;
            int cat2Count = ((ProductsListViewModel)controller.List("Cat2", 1).Model).PagingInfo.TotalItems;
            int cat3Count = ((ProductsListViewModel)controller.List("Cat3", 1).Model).PagingInfo.TotalItems;

            int allCount = ((ProductsListViewModel)controller.List(null, 1).Model).PagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(cat1Count, 2);
            Assert.AreEqual(cat2Count, 3);
            Assert.AreEqual(cat3Count, 4);
            Assert.AreEqual(allCount, 9);
        }

        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            Product prop = new Product() { ProductId = 2, Name = "P2", ImageData = new byte[] { }, ImageMimeType = "image/png" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
            new Product{ ProductId=1,Name="P1"},
            prop,
            new Product{ProductId=1,Name="P3"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object);

            //act
            ActionResult result = target.GetImage(2);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prop.ImageMimeType, ((FileResult)result).ContentType);
        }
        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            Product prop = new Product() { ProductId = 2, Name = "P2", ImageData = new byte[] { }, ImageMimeType = "image/png" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
            new Product{ ProductId=1,Name="P1"},
            prop,
            new Product{ProductId=1,Name="P3"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object);

            //act
            ActionResult result = target.GetImage(1000);

            //Assert
            Assert.IsNull(result);
        }
    }
}

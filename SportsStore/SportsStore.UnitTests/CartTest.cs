using SportsStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
namespace SportsStore.UnitTests
{


    /// <summary>
    ///这是 CartTest 的测试类，旨在
    ///包含所有 CartTest 单元测试
    ///</summary>
    [TestClass()]
    public class CartTest
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
        ///AddLine 的测试
        ///</summary>
        [TestMethod()]
        public void Can_Add_New_Lines()
        {
            //Arrange
            Product p1 = new Product() { ProductId = 1, Name = "P1" };
            Product p2 = new Product() { ProductId = 2, Name = "P2" };

            Cart cart = new Cart();

            //Act
            cart.AddLine(p1, 1);
            cart.AddLine(p2, 2);

            CartLine[] result = cart.Lines.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[0].Quantity, 1);

            Assert.AreEqual(result[1].Product, p2);
            Assert.AreEqual(result[1].Quantity, 2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Arrange
            Product p1 = new Product() { ProductId = 1, Name = "P1" };
            Product p2 = new Product() { ProductId = 2, Name = "P2" };

            Cart cart = new Cart();

            //Act
            cart.AddLine(p1, 1);
            cart.AddLine(p2, 2);
            cart.AddLine(p1, 10);

            CartLine[] result = cart.Lines.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[0].Quantity, 11);

            Assert.AreEqual(result[1].Product, p2);
            Assert.AreEqual(result[1].Quantity, 2);
        }
        [TestMethod]
        public void Can_Remove_Lines()
        {
            //Arrange
            Product p1 = new Product() { ProductId = 1, Name = "P1" };
            Product p2 = new Product() { ProductId = 2, Name = "P2" };
            Product p3 = new Product() { ProductId = 3, Name = "P3" };

            Cart cart = new Cart();
            cart.AddLine(p1, 1);
            cart.AddLine(p2, 2);
            cart.AddLine(p2, 2);
            cart.AddLine(p3, 2);
            cart.AddLine(p1, 10);
            
            //act
            cart.RemoveLine(p2);

            CartLine[] result = cart.Lines.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[0].Quantity, 11);

            Assert.AreEqual(result[1].Product, p3);
            Assert.AreEqual(result[1].Quantity, 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //Arrange
            Product p1 = new Product() { ProductId = 1, Name = "P1",Price=2 };
            Product p2 = new Product() { ProductId = 2, Name = "P2",Price=3 };
            Product p3 = new Product() { ProductId = 3, Name = "P3",Price=4 };

            Cart cart = new Cart();
            cart.AddLine(p1, 1);
            cart.AddLine(p2, 2);
            cart.AddLine(p2, 2);
            cart.AddLine(p3, 2);
            cart.AddLine(p1, 10);

            //act
            decimal result= cart.ComputeTotalValue();

            //Assert
            Assert.AreEqual(result, 11 * 2 + 4 * 3 + 2 * 4);
        }

        [TestMethod]
        public void Can_Clear()
        {
            //Arrange
            Product p1 = new Product() { ProductId = 1, Name = "P1", Price = 2 };
            Product p2 = new Product() { ProductId = 2, Name = "P2", Price = 3 };
            Product p3 = new Product() { ProductId = 3, Name = "P3", Price = 4 };

            Cart cart = new Cart();
            cart.AddLine(p1, 1);
            cart.AddLine(p2, 2);
            cart.AddLine(p2, 2);
            cart.AddLine(p3, 2);
            cart.AddLine(p1, 10);

            //Act
            cart.Clear();
            CartLine[] products = cart.Lines.ToArray();
            //Assert
            Assert.AreEqual(products.Length, 0);
        }
    }
}

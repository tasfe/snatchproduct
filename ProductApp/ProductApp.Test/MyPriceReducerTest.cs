using ProductApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductApp.Test
{


    /// <summary>
    ///这是 MyPriceReducerTest 的测试类，旨在
    ///包含所有 MyPriceReducerTest 单元测试
    ///</summary>
    [TestClass()]
    public class MyPriceReducerTest
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


        ///// <summary>
        /////ReducePrices 的测试
        /////</summary>
        //[TestMethod()]
        //public void ReducePricesTest()
        //{
        //    IProductRepository repo = null; // TODO: 初始化为适当的值
        //    MyPriceReducer target = new MyPriceReducer(repo); // TODO: 初始化为适当的值
        //    Decimal priceReduction = new Decimal(); // TODO: 初始化为适当的值
        //    target.ReducePrices(priceReduction);
        //    Assert.Inconclusive("无法验证不返回值的方法。");
        //}

        [TestMethod]
        public void All_Prices_Are_Changed()
        {
            // Arrange
            FakeRepository repo = new FakeRepository();
            decimal reductionAmount = 10;
            IEnumerable<decimal> prices = repo.GetProducts().Select(e => e.Price);
            decimal[] initialPrices = prices.ToArray();
            MyPriceReducer target = new MyPriceReducer(repo);
            // Act
            target.ReducePrices(reductionAmount);
            prices.Zip(initialPrices, (p1, p2) =>
            {
                if (p1 == p2)
                {
                    Assert.Fail();
                }
                return p1;
            });
        }

        [TestMethod]
        public void Correct_Total_Reduction_Amount()
        {
            // Arrange
            FakeRepository repo = new FakeRepository();
            decimal reductionAmount = 10;
            decimal initialTotal = repo.GetTotalValue();
            MyPriceReducer target = new MyPriceReducer(repo);
            // Act
            target.ReducePrices(reductionAmount);
            // Assert
            Assert.AreEqual(repo.GetTotalValue(),
            (initialTotal - (repo.GetProducts().Count() * reductionAmount)));
        }
        [TestMethod]
        public void No_Price_Less_Than_One_Dollar()
        {
            // Arrange
            FakeRepository repo = new FakeRepository();
            decimal reductionAmount = decimal.MaxValue;
            MyPriceReducer target = new MyPriceReducer(repo);
            // Act
            target.ReducePrices(reductionAmount);
            // Assert
            foreach (Product prod in repo.GetProducts())
            {
                Assert.IsTrue(prod.Price >= 1);
            }
        }
    }
}

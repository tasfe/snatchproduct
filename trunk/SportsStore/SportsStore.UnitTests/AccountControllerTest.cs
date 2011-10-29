using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportsStore.WebUI.Infrastructure;
using System.Web.Mvc;
using Moq;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{


    /// <summary>
    ///这是 AccountControllerTest 的测试类，旨在
    ///包含所有 AccountControllerTest 单元测试
    ///</summary>
    [TestClass()]
    public class AccountControllerTest
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
        public void Can_Login_With_Valid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(a => a.Authenticate("admin", "secrect")).Returns(true);

            AccountController target = new AccountController(mock.Object);
            LogOnViewModel model = new LogOnViewModel { UserName = "admin", Password = "secrect" };
            //acc
            ActionResult result = target.LogOn(model, "/myUrl");

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/myUrl", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(a => a.Authenticate("badUser", "badPwd")).Returns(false);

            AccountController target = new AccountController(mock.Object);
            LogOnViewModel model = new LogOnViewModel { UserName = "badUser", Password = "badPwd" };
            //acc
            ActionResult result = target.LogOn(model, "/myUrl");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}

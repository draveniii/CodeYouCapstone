using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCWebBanking.Controllers;
using Xunit;
using WebBankingApp.Models;
using WebBankingApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;


namespace MVCWebBanking.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task LoginGetValid()
        {
            // Arrange
            AccountsController controller = new AccountsController(null);

            // Act
            IActionResult result = controller.Login();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
        }

        //Sets up data for other tests
        public AccountsController DataSetup(bool passValidation)
        {

            // Begin Mocking Data
            Account account1 = new Account(1);

            Share account1Share1 = new Share(1, "Savings", 25, .005M, account1, 100);
            Share account1Share2 = new Share(2, "Checking", 0, 0, account1, 100);

            account1.Shares.Add(account1Share1);
            account1.Shares.Add(account1Share2);


            Account account2 = new Account(2);

            Share account2Share1 = new Share(3, "Savings", 25, .005M, account1, 100);

            account2.Shares.Add(account2Share1);

            var shareData = new List<Share>
            {
                account1Share1,
                account1Share2
            }.AsQueryable();

            var accountData = new List<Account>
            {
                account1,
                account2
            }.AsQueryable();

            var mockSetAccount = new Mock<DbSet<Account>>();
            mockSetAccount.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accountData.Provider);
            mockSetAccount.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accountData.Expression);
            mockSetAccount.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accountData.ElementType);
            mockSetAccount.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(() => accountData.GetEnumerator());

            var mockSetShare = new Mock<DbSet<Share>>();
            mockSetShare.As<IQueryable<Share>>().Setup(m => m.Provider).Returns(shareData.Provider);
            mockSetShare.As<IQueryable<Share>>().Setup(m => m.Expression).Returns(shareData.Expression);
            mockSetShare.As<IQueryable<Share>>().Setup(m => m.ElementType).Returns(shareData.ElementType);
            mockSetShare.As<IQueryable<Share>>().Setup(m => m.GetEnumerator()).Returns(() => shareData.GetEnumerator());

            var mockContext = new Mock<BankingContext>();
            mockContext.Setup(a => a.Accounts).Returns(mockSetAccount.Object);
            mockContext.Setup(a => a.Shares).Returns(mockSetShare.Object);
            // End mocking data


            // Begin mocking validator
            var objectValidator = new Mock<IObjectModelValidator>();

            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                It.IsAny<ValidationStateDictionary>(),
                                It.IsAny<string>(),
                                It.IsAny<Object>()));

            AccountsController controller = new AccountsController(mockContext.Object);

            controller.ObjectValidator = objectValidator.Object;

            if (!passValidation)
            {
                controller.ModelState.AddModelError("Test Force Invalid", "Error Message");
            }
            // End mocking validator

            return controller;
        }

        [Fact]
        public async Task LoginPostInvalid()
        {
            // Arrange
            AccountsController controller = DataSetup(true);
            Account account = new Account(10000); // Account number 10000 will probably not exist

            // Act
            IActionResult result = await controller.Login(account);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task LoginPostValid()
        {
            // Arrange
            AccountsController controller = DataSetup(true);
            Account account = new Account(1);

            // Act
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            IActionResult result = await controller.Login(account);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            //Assert.Equal("Accounts", redirectResult.ControllerName);
            Assert.Equal("Details", redirectResult.ActionName);

            List<KeyValuePair<string, object>> routeValues = redirectResult.RouteValues.ToList();

            Assert.Equal(1, routeValues.Count);
            Assert.Equal("id", routeValues[0].Key);
            Assert.Equal(1, routeValues[0].Value);
        }

        [Fact]
        public async Task DetailsGetNotFound()
        {
            // Arrange
            AccountsController controller = DataSetup(true);
            MockHttpContext(controller, "accountId", "10000"); // Account number 10000 will probably not exist

            // Act            
            IActionResult result = await controller.Details(); 

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DetailsGetFound()
        {
            // Arrange
            AccountsController controller = DataSetup(true);
            MockHttpContext(controller, "accountId", "1");

            // Act            
            IActionResult result = await controller.Details();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task MembersGetValid()
        {
            // Arrange
            AccountsController controller = DataSetup(true);

            // Act
            IActionResult result = await controller.Members(1); // Account number 10000 will probably not exist

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task MembersGetInvalid()
        {
            // Arrange
            AccountsController controller = DataSetup(true);

            // Act
            IActionResult result = await controller.Members(null); // Account number 10000 will probably not exist

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Sets up HttpContext for tests that need access to mock cookies
        private void MockHttpContext(AccountsController controller, string cookieKey, string cookieValue)
        {
            string CookieKey = cookieKey;
            var requestCookiesMock = new Mock<IRequestCookieCollection>();
            requestCookiesMock.SetupGet(c => c[CookieKey]).Returns(cookieValue);

            var responseCookiesMock = new Mock<IResponseCookies>();
            responseCookiesMock.Setup(c => c.Delete(CookieKey)).Verifiable();

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(ctx => ctx.Request.Cookies)
                            .Returns(requestCookiesMock.Object);
            httpContextMock.Setup(ctx => ctx.Response.Cookies)
                            .Returns(responseCookiesMock.Object);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContextMock.Object
            };
        }
    }
}


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


namespace MVCWebBanking.Tests.Controllers
{
    public class TransactionControllerTests
    {
        [Fact]
        public async Task DepositGet()
        {
            // Arrange
            TransactionsController controller = new TransactionsController(null);

            // Act
            IActionResult result = controller.Deposit(1);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);       
            
            int shareId = Assert.IsType<int>(viewResult.ViewData["shareId"]);
            Assert.Equal(1, shareId);                         
        }

        // Sets up data for other tests
        public TransactionsController DataSetup(bool passValidation)
        {

            // Begin Mocking Data
            Account account = new Account();
            account.Id = 1;

            var accountData = new List<Account>
            {
                account
            }.AsQueryable();

            var shareData = new List<Share>
            {
                new Share { Id = 1, Account = account, CurrentBalance = 100, InterestRate = 0, MinimumBalance = 0, Type = "Checking", Transactions = null  },
                new Share { Id = 2, Account = account, CurrentBalance = 100, InterestRate = .005M, MinimumBalance = 25, Type = "Savings", Transactions = null  }
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

            TransactionsController controller = new TransactionsController(mockContext.Object);

            controller.ObjectValidator = objectValidator.Object;

            if (!passValidation)
            {
                controller.ModelState.AddModelError("Test Force Invalid", "Error Message");
            }
            // End mocking validator

            return controller;
        }

        [Fact]
        public async Task DepositPostValidated()
        {
            TransactionsController controller = DataSetup(true);
                                              
            WebBankingApp.Models.Transaction transaction = new WebBankingApp.Models.Transaction();
            transaction.Amount = 10;

            // Act
            IActionResult result = await controller.Deposit(1, transaction);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Shares", redirectResult.ControllerName);
            Assert.Equal("Details", redirectResult.ActionName);

            List<KeyValuePair<string, object>> routeValues = redirectResult.RouteValues.ToList();

            Assert.Equal(1, routeValues.Count);
            Assert.Equal("id", routeValues[0].Key);
            Assert.Equal(1, routeValues[0].Value);
        }

        [Fact]
        public async Task DepositPostNonValidated()
        {
            TransactionsController controller = DataSetup(false);

            WebBankingApp.Models.Transaction transaction = new WebBankingApp.Models.Transaction();
            transaction.Amount = 10;

            // Act
            IActionResult result = await controller.Deposit(1, transaction);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            int shareId = Assert.IsType<int>(viewResult.ViewData["shareId"]);
            Assert.Equal(1, shareId);        
        }

        [Fact]
        public async Task DepositPostInvalidAmount()
        {
            // Arrange
            TransactionsController controller = DataSetup(true);

            WebBankingApp.Models.Transaction transaction = new WebBankingApp.Models.Transaction();
            transaction.Amount = -10;

            // Act
            IActionResult result = await controller.Deposit(1, transaction);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            int shareId = Assert.IsType<int>(viewResult.ViewData["shareId"]);
            Assert.Equal(1, shareId);
        }

        [Fact]
        public async Task TransferGetInvalid()
        {
            // Arrange
            TransactionsController controller = DataSetup(true);

            // Act
            IActionResult result = controller.Transfer(10000); // 10,000 should be an invalid share number

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Shares", redirectResult.ControllerName);
            Assert.Equal("Details", redirectResult.ActionName);

            List<KeyValuePair<string, object>> routeValues = redirectResult.RouteValues.ToList();

            Assert.Equal(1, routeValues.Count);
            Assert.Equal("id", routeValues[0].Key);
            Assert.Equal(10000, routeValues[0].Value);
        }
    }
}

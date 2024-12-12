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
        public async Task TransferGetInvalidShareId()
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

        [Fact]
        public async Task TransferGetNotEnoughSharesOnAccount()
        {
            // Arrange
            TransactionsController controller = DataSetup(true);

            // Act
            IActionResult result = controller.Transfer(3); 

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Shares", redirectResult.ControllerName);
            Assert.Equal("Details", redirectResult.ActionName);

            List<KeyValuePair<string, object>> routeValues = redirectResult.RouteValues.ToList();

            Assert.Equal(1, routeValues.Count);
            Assert.Equal("id", routeValues[0].Key);
            Assert.Equal(3, routeValues[0].Value);
        }

        [Fact]
        public async Task TransferGetValid()
        {
            // Arrange
            TransactionsController controller = DataSetup(true);

            // Act
            IActionResult result = controller.Transfer(1);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            int fromShareId = Assert.IsType<int>(viewResult.ViewData["fromShareId"]);
            Assert.Equal(1, fromShareId);
                        
            List<Share> shares = (List<Share>) viewResult.ViewData["shares"];
            Assert.Equal(1, shares.Count());
            Assert.Equal(2, shares[0].Id);
        }

        [Fact]
        public async Task TransferPostNonValidated()
        {
            TransactionsController controller = DataSetup(false);

            WebBankingApp.Models.Transaction transaction = new WebBankingApp.Models.Transaction();
            transaction.Amount = 10;

            // Act
            IActionResult result = await controller.Transfer(1, 2, transaction);

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
        public async Task TransferPostNegativeAmount()
        {
            // Arrange
            TransactionsController controller = DataSetup(true);

            WebBankingApp.Models.Transaction transaction = new WebBankingApp.Models.Transaction();
            transaction.Amount = -10;

            // Act
            IActionResult result = await controller.Transfer(1, 2, transaction);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            int shareId = Assert.IsType<int>(viewResult.ViewData["fromShareId"]);
            Assert.Equal(1, shareId);

            List<Share> shares = (List<Share>)viewResult.ViewData["shares"];
            Assert.Equal(1, shares.Count());
            Assert.Equal(2, shares[0].Id);
        }

        //[Fact]
        //public async Task TransferPostAmountOutOfRange()
        //{
        //    // Arrange
        //    TransactionsController controller = DataSetup(true);

        //    WebBankingApp.Models.Transaction transaction = new WebBankingApp.Models.Transaction();
        //    transaction.Amount = Decimal.MaxValue;
        //    transaction.Amount += 1M;

        //    // Act
        //    IActionResult result = await controller.Transfer(1, 2, transaction);

        //    // Assert
        //    ViewResult viewResult = Assert.IsType<ViewResult>(result);

        //    int shareId = Assert.IsType<int>(viewResult.ViewData["shareId"]);
        //    Assert.Equal(1, shareId);

        //    List<Share> shares = (List<Share>)viewResult.ViewData["shares"];
        //    Assert.Equal(1, shares.Count());
        //    Assert.Equal(2, shares[0].Id);
        //}

        [Fact]
        public async Task TransferPostAmountGreaterThanAvailableBalance()
        {
            // Arrange
            TransactionsController controller = DataSetup(true);

            WebBankingApp.Models.Transaction transaction = new WebBankingApp.Models.Transaction();
            transaction.Amount = 110;

            // Act
            IActionResult result = await controller.Transfer(1, 2, transaction);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            int shareId = Assert.IsType<int>(viewResult.ViewData["fromShareId"]);
            Assert.Equal(1, shareId);

            List<Share> shares = (List<Share>)viewResult.ViewData["shares"];
            Assert.Equal(1, shares.Count());
            Assert.Equal(2, shares[0].Id);
        }

        [Fact]
        public async Task TransferPostValid()
        {
            TransactionsController controller = DataSetup(true);

            WebBankingApp.Models.Transaction transaction = new WebBankingApp.Models.Transaction();
            transaction.Amount = 10;

            // Act
            IActionResult result = await controller.Transfer(1, 2, transaction);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Shares", redirectResult.ControllerName);
            Assert.Equal("Details", redirectResult.ActionName);

            List<KeyValuePair<string, object>> routeValues = redirectResult.RouteValues.ToList();

            Assert.Equal(1, routeValues.Count);
            Assert.Equal("id", routeValues[0].Key);
            Assert.Equal(1, routeValues[0].Value);
        }
    }
}

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
    public class ShareControllerTests
    {
        //Sets up data for other tests
        public SharesController DataSetup(bool passValidation)
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

            SharesController controller = new SharesController(mockContext.Object);

            controller.ObjectValidator = objectValidator.Object;

            if (!passValidation)
            {
                controller.ModelState.AddModelError("Test Force Invalid", "Error Message");
            }
            // End mocking validator

            return controller;
        }

        [Fact]
        public async Task DetailsGetNotFound()
        {
            // Arrange
            SharesController controller = DataSetup(true);

            // Act
            IActionResult result = await controller.Details(10000); // Account number 10000 will probably not exist

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DetailsGetFound()
        {
            // Arrange
            SharesController controller = DataSetup(true);

            // Act
            IActionResult result = await controller.Details(1); 

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}


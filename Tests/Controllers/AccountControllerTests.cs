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
    public class AccountControllerTests
    {
        [Fact]
        public async Task LoginGet()
        {
            // Arrange
            AccountsController controller = new AccountsController(null);

            // Act
            IActionResult result = controller.Login();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);                             
        }

        
    }
}

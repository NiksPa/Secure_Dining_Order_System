using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DiningOrderingSystem.Controllers;
using DiningOrderingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using DiningOrderingSystem.Models.Data;
using DiningOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using NuGet.Protocol;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace DinigOrderingSystem.Tests
{
    public class HomeControllerTests
    {

        [Fact]
        public async void HomeController_TestIndexReturnsNotice()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<NoticeDbContext>();
            optionsBuilder.UseInMemoryDatabase("UMCP");
            var context = new NoticeDbContext(optionsBuilder.Options);
            var homeController = new HomeController(context);

            // Act
            var testResult = await homeController.Index();
            var viewResult = Assert.IsType<ViewResult>(testResult);
            var model = Assert.IsType<List<NoticeItem>>(viewResult.ViewData.Model);

           // Assert
           Assert.IsType<List<NoticeItem>>(model);

        }

        [Fact]
        public async void HomeController_TestAddNotice()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<NoticeDbContext>();
            optionsBuilder.UseInMemoryDatabase("UMCP");
            var context = new NoticeDbContext(optionsBuilder.Options);
            var homeController = new HomeController(context);

            // Act
            var testResult = (homeController.AddNotice()) as ViewResult;

            // Assert
            Assert.IsType<ViewResult>(testResult);
        }

        [Fact]
        public async void HomeController_TestAddNoticeWithInvalidValues()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<NoticeDbContext>();
            optionsBuilder.UseInMemoryDatabase("UMCP");
            var context = new NoticeDbContext(optionsBuilder.Options);
            var homeController = new HomeController(context);
            var model = new NoticeViewModel()
            {
                NoticeTitle = "",
                NoticeContent = "Checking empty titled notice",
                Date = DateTime.Now.AddDays(5)
            };
            var mockTempData = new Mock<ITempDataDictionary>();
            homeController.TempData = mockTempData.Object;



            // Act
            var testResult = homeController.AddNotice(model);

            // Assert
            Assert.Null(testResult.Exception);
            
        }
    }
}

using AngleSharp.Html.Dom;
using DiningOrderingSystem.Areas.Identity.Data;
using DiningOrderingSystem.Controllers;
using DiningOrderingSystem.Data;
using DiningOrderingSystem.Models;
using DiningOrderingSystem.Models.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DinigOrderingSystem.Tests
{
    public class FoodOrderControllerTests
    {
        [Fact]
        public async void FoodOrderController_TestIndexReturnsFoodItems()
        {
            // Arrange
            var optionsBuilderFoodOrder = new DbContextOptionsBuilder<FoodOrderDbContext>();
            optionsBuilderFoodOrder.UseInMemoryDatabase("UMCP");
            var foodOrderDbContext = new FoodOrderDbContext(optionsBuilderFoodOrder.Options);

            var userManager = new Mock<Microsoft.AspNetCore.Identity.UserManager<AppUser>>(Mock.Of<Microsoft.AspNetCore.Identity.IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
            var appUser = new AppUser()
            {
                UserName = "TestUser",
                Id = Guid.NewGuid().ToString()
            };
            await userManager.Object.AddToRoleAsync(appUser,"Admin");
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim("name", appUser.UserName),
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext()
            {
                User = claimsPrincipal
            };
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            var foodOrderController = new FoodOrderController(foodOrderDbContext, userManager.Object)
            {
                ControllerContext = controllerContext
            };

            // Act
            var testResult = await foodOrderController.Index();
            var viewResult = Assert.IsType<ViewResult>(testResult);
            var model = Assert.IsType<List<FoodOrder>>(viewResult.ViewData.Model);

            // Assert
            Assert.IsType<List<FoodOrder>>(model);

        }
        
    }
}

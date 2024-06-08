using DiningOrderingSystem.Areas.Identity.Data;
using DiningOrderingSystem.Controllers;
using DiningOrderingSystem.Data;
using DiningOrderingSystem.Models;
using DiningOrderingSystem.Models.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinigOrderingSystem.Tests
{
    public class FoodItemControllerTests
    {
        [Fact]
        public async void FoodItemController_TestIndexReturnsFoodItems()
        {
            // Arrange
            var optionsBuilderFoodItem = new DbContextOptionsBuilder<FoodItemDbContext>();
            optionsBuilderFoodItem.UseInMemoryDatabase("UMCP");
            var foodItemcontext = new FoodItemDbContext(optionsBuilderFoodItem.Options);

            var optionsBuilderFoodOrder = new DbContextOptionsBuilder<FoodOrderDbContext>();
            optionsBuilderFoodOrder.UseInMemoryDatabase("UMCP");
            var foodOrderDbContext = new FoodOrderDbContext(optionsBuilderFoodOrder.Options);

            var userManager = new Mock<Microsoft.AspNetCore.Identity.UserManager<AppUser>>(Mock.Of<Microsoft.AspNetCore.Identity.IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
            var foodItemController = new FoodItemController(foodItemcontext,userManager.Object,foodOrderDbContext);

            // Act
            var testResult = await foodItemController.Index();
            var viewResult = Assert.IsType<ViewResult>(testResult);
            var model = Assert.IsType<List<FoodItem>>(viewResult.ViewData.Model);

            // Assert
            Assert.IsType<List<FoodItem>>(model);

        }

        [Fact]
        public async void FoodItemController_TestAddFoodItemWithInvalidValues()
        {
            // Arrange
            var optionsBuilderFoodItem = new DbContextOptionsBuilder<FoodItemDbContext>();
            optionsBuilderFoodItem.UseInMemoryDatabase("UMCP");
            var foodItemcontext = new FoodItemDbContext(optionsBuilderFoodItem.Options);

            var optionsBuilderFoodOrder = new DbContextOptionsBuilder<FoodOrderDbContext>();
            optionsBuilderFoodOrder.UseInMemoryDatabase("UMCP");
            var foodOrderDbContext = new FoodOrderDbContext(optionsBuilderFoodOrder.Options);

            var userManager = new Mock<Microsoft.AspNetCore.Identity.UserManager<AppUser>>(Mock.Of<Microsoft.AspNetCore.Identity.IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
            var foodItemController = new FoodItemController(foodItemcontext, userManager.Object, foodOrderDbContext);
            var model = new AddFoodItemViewModel()
            {
                Name = "",
                Calorie = 12,
                Contents = "Checking with empty name"
            };
            var mockTempData = new Mock<ITempDataDictionary>();
            foodItemController.TempData = mockTempData.Object;


            // Act
            var testResult = foodItemController.AddFoodItem(model);

            // Assert
            Assert.Null(testResult.Exception);

        }
    }
}

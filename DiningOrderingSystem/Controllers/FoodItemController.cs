using DiningOrderingSystem.Areas.Identity.Data;
using DiningOrderingSystem.Data;
using DiningOrderingSystem.Models;
using DiningOrderingSystem.Models.Data;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DiningOrderingSystem.Controllers
{
    [Authorize]
    public class FoodItemController : Controller
    {
        private readonly FoodItemDbContext foodItemDbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly FoodOrderDbContext foodOrderDbContext;

        public FoodItemController(FoodItemDbContext foodItemDbContext, UserManager<AppUser> userManager,FoodOrderDbContext foodOrderDbContext)
        {
            this.foodItemDbContext = foodItemDbContext;
            this.userManager = userManager;
            this.foodOrderDbContext = foodOrderDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var foodItemList = await foodItemDbContext.FoodItemList.ToListAsync();
            return View(foodItemList);
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult AddFoodItem()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFoodItem(AddFoodItemViewModel addFoodItem)
        {
            var htmlSanitizer = new HtmlSanitizer();
            htmlSanitizer.AllowedTags.Clear();
            var Name = htmlSanitizer.Sanitize(addFoodItem.Name);
            var Contents= htmlSanitizer.Sanitize(addFoodItem.Contents);

            if(Name != "" & Contents != "" & addFoodItem.Calorie.GetType() == typeof(int))
            {
                var foodItem = new FoodItem()
                {
                    Name = addFoodItem.Name,
                    Calorie = addFoodItem.Calorie,
                    Contents = addFoodItem.Contents
                };

                

                try
                {
                    await foodItemDbContext.FoodItemList.AddAsync(foodItem);
                    await foodItemDbContext.SaveChangesAsync();
                    TempData["Message"] = "Food item added !!!";
                    TempData["Status"] = "200";

                    return RedirectToAction("Index");
                }
                catch (Exception sqlErr)
                {
                    if (sqlErr.InnerException.Message.Contains("UNIQUE"))
                    {
                        TempData["Message"] = "Please use unique Name !!!";
                        TempData["Status"] = "400";
                        return View();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                
            }
            else
            {
                TempData["Message"] = "Enter valid input !!!";
                TempData["Status"] = "400";

                return View();
            }

            

        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public async new Task<IActionResult> View(String Name)
        {
            var htmlSanitizer = new HtmlSanitizer();
            htmlSanitizer.AllowedTags.Clear();
            Name = htmlSanitizer.Sanitize(Name);
            var foodItem = await foodItemDbContext.FoodItemList.FirstOrDefaultAsync(x => x.Name == Name);
            if(foodItem != null)
            {
                var viewModel = new UpdateFoodItemViewModel()
                {
                    Name = foodItem.Name,
                    Calorie = foodItem.Calorie,
                    Contents = foodItem.Contents
                };
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateFoodItem(UpdateFoodItemViewModel updateItem)
        {
            var htmlSanitizer = new HtmlSanitizer();
            htmlSanitizer.AllowedTags.Clear();
            var Name = htmlSanitizer.Sanitize(updateItem.Name);
            var Contents = htmlSanitizer.Sanitize(updateItem.Contents);

            var foodItem = await foodItemDbContext.FoodItemList.FindAsync(Name); 
            
            if(foodItem != null & updateItem.Calorie.GetType() == typeof(int) & Contents != "")
            {
                foodItem.Calorie = updateItem.Calorie;
                foodItem.Contents = Contents;

                await foodItemDbContext.SaveChangesAsync();

                TempData["Message"] = "Food item updated !!!";
                TempData["Status"] = "200";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Invalid details entered !!!";
                TempData["Status"] = "400";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteFoodItem(UpdateFoodItemViewModel deleteItem)
        {
            var htmlSanitizer = new HtmlSanitizer();
            htmlSanitizer.AllowedTags.Clear();
            var Name = htmlSanitizer.Sanitize(deleteItem.Name);

            var foodItem = await foodItemDbContext.FoodItemList.FindAsync(deleteItem.Name);

            if(foodItem != null)
            {
                foodItemDbContext.FoodItemList.Remove(foodItem);
                await foodItemDbContext.SaveChangesAsync();

                TempData["Message"] = "Food item Deleted !!!";
                TempData["Status"] = "200";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Food item Not Found !!!";
                TempData["Status"] = "400";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> PlaceOrder(AddFoodItemViewModel order)
        {
            if (order.orderCheckBox == null)
            {
                TempData["Message"] = "Please Select Food Items !!!";
                TempData["Status"] = "400";
                return RedirectToAction("Index");
            }
            if(order.orderCheckBox.Count > 5)
            {
                TempData["Message"] = "You can select atmost 5 items !!!";
                TempData["Status"] = "400";
                return RedirectToAction("Index");
            }
            if((TimeSpan.Compare(TimeSpan.FromDays(2), order.Date - DateTime.Now) == 1) | (TimeSpan.Compare(TimeSpan.FromDays(33), order.Date - DateTime.Now) == -1))
            {
                TempData["Message"] = "Please select valid Delivery Date !!!";
                TempData["Status"] = "400";
                return RedirectToAction("Index");
            }
            var orderedItems = "";
            foreach (var checkedItem in order.orderCheckBox)
            {
                if (orderedItems == "")
                {
                    orderedItems = orderedItems + checkedItem;
                }
                else
                {
                    orderedItems = orderedItems + "," + checkedItem;
                }
            }

            var currentUser = await userManager.GetUserAsync(User);
            

            var foodOrder = new DiningOrderingSystem.Models.Data.FoodOrder
            {
                OrderId = Guid.NewGuid().ToString(),
                StudentName = currentUser.Name,
                StudentId = currentUser.Id,
                DeliveryDate = order.Date,
                OrderedItems = orderedItems
            };

            await foodOrderDbContext.FoodOrderList.AddAsync(foodOrder);
            await foodOrderDbContext.SaveChangesAsync();

            TempData["Message"] = "Order Added !!!";
            TempData["Status"] = "200";

            return RedirectToAction("Index");
        }
    }
}

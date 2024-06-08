using DiningOrderingSystem.Areas.Identity.Data;
using DiningOrderingSystem.Data;
using DiningOrderingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiningOrderingSystem.Controllers
{
    [Authorize]
    public class FoodOrderController : Controller
    {
        private readonly FoodOrderDbContext foodOrderDbContext;
        private readonly UserManager<AppUser> userManager;

        public FoodOrderController(FoodOrderDbContext foodOrderDbContext, UserManager<AppUser> userManager)
        {
            this.foodOrderDbContext = foodOrderDbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = userManager.GetUserId(User);
            if (User.IsInRole("Admin"))
            {
                var foodOrders = await foodOrderDbContext.FoodOrderList.ToListAsync();
                return View(foodOrders);
            }
            else
            {
                var foodOrders = await foodOrderDbContext.FoodOrderList.Where(x => x.StudentId == currentUserId).ToListAsync();
                return View(foodOrders);
            }
        }
        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> DeleteFoodOrder(FoodOrderViewModel model)
        {
            if (model.deleteOrderCheckBox == null)
            {
                TempData["Message"] = "No order seletected !!!";
                TempData["Status"] = "400";
                return RedirectToAction("Index");
            }
            foreach (var checkedItem in model.deleteOrderCheckBox)
            {
                var currentUserId = userManager.GetUserId(User);
                var foodOrder = await foodOrderDbContext.FoodOrderList.FindAsync(checkedItem);

                if (foodOrder != null)
                {
                    if (foodOrder.StudentId == currentUserId)
                    {
                        if(TimeSpan.Compare(TimeSpan.FromDays(2), foodOrder.DeliveryDate - DateTime.Now) == -1)
                        {
                            foodOrderDbContext.Remove(foodOrder);
                            await foodOrderDbContext.SaveChangesAsync();
                        }
                        else
                        {
                            TempData["Message"] = "Enter valid order details !!!";
                            TempData["Status"] = "400";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Enter valid order details !!!";
                        TempData["Status"] = "400";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Message"] = "Enter valid order details !!!";
                    TempData["Status"] = "400";
                    return RedirectToAction("Index");
                }
            }
            TempData["Message"] = "Food orders deleted !!!";
            TempData["Status"] = "200";
            return RedirectToAction("Index");

        }
    }
}

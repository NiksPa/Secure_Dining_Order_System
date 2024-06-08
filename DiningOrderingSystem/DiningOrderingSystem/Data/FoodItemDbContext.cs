using DiningOrderingSystem.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace DiningOrderingSystem.Data
{
    public class FoodItemDbContext: DbContext
    {
        public FoodItemDbContext(DbContextOptions<FoodItemDbContext> options) : base(options)
        {
        }

        public DbSet<FoodItem> FoodItemList { get; set; }
    }
}

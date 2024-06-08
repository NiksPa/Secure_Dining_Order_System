using DiningOrderingSystem.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace DiningOrderingSystem.Data
{
    public class FoodOrderDbContext: DbContext
    {
        public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options) : base(options)
        {

        }
        public DbSet<FoodOrder> FoodOrderList { get; set; }
    }
}

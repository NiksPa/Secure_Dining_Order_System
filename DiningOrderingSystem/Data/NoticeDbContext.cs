using DiningOrderingSystem.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace DiningOrderingSystem.Data
{
    public class NoticeDbContext: DbContext
    {
        public NoticeDbContext(DbContextOptions<NoticeDbContext> options) : base(options)
        {

        }

        public DbSet<NoticeItem> NoticeItemList { get; set; }
    }
}

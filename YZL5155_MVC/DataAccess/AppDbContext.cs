
using System.Data.Entity;
using YZL5155_MVC.Models.Entities;

namespace YZL5155_MVC.DataAccess
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
            Database.Connection.ConnectionString = @"Server=SHADOWBLOOD\ETA;Database=MVC_Db;Integrated Security=true;";
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
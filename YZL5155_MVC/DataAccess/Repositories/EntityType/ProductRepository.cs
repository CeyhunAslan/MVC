using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using YZL5155_MVC.Models.Entities;

namespace YZL5155_MVC.DataAccess.Repositories.EntityType
{
    public class ProductRepository : IBaseRepository<Product>
    {
        AppDbContext db = new AppDbContext();

        public void Create(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
        }

        public void Delete(Product entity)
        {
            db.SaveChanges();
        }

        public Product Get(Expression<Func<Product, bool>> exp)
        {
            return db.Products.FirstOrDefault(exp);
        }

        public List<Product> Gets(Expression<Func<Product, bool>> exp)
        {
            return db.Products.Where(exp).ToList();
        }

        public void Update(Product entity)
        {
            db.SaveChanges();
        }
    }
}
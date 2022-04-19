
using System.Collections.Generic;

namespace YZL5155_MVC.Models.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
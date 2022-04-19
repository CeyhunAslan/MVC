using System;
using YZL5155_MVC.Models.Entities;

namespace YZL5155_MVC.Models.VMs
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public short Stock { get; set; }
        public Status Status { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
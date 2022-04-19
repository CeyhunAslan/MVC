using System;
using YZL5155_MVC.Models.Entities;

namespace YZL5155_MVC.Models.VMs
{
    public class CategoryDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
    }
}
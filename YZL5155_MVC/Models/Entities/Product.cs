
namespace YZL5155_MVC.Models.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public short Stock { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
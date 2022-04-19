
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YZL5155_MVC.Models.VMs;

namespace YZL5155_MVC.Models.DTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please type into name")]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please type into description")]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please type into price")]
        [DataType(DataType.Currency, ErrorMessage = "Please type into valid value")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please type into stock")]
        public short Stock { get; set; }

        [Required(ErrorMessage = "Please choose category")]
        public int CategoryId { get; set; }


        //İlk adımda DTO, Controller'dan view giderken category'leri aşağıdaki listeye dolduracağız
        public IEnumerable<CategoryVM> Categories { get; set; }
    }
}
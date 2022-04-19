
using System.ComponentModel.DataAnnotations;

namespace YZL5155_MVC.Models.DTO
{
    public class CategoryCreateDTO
    {
        //DTOs yani Data Transfer Objects, bu sınıfların amacı yapılan işe göre Controller'dan View'a, View'dan Controller'a veri taşımaktır.

        //Aşağıdaki data annotations ile kullanıcıdan alınacak Name bilgisine kurallar koyduk.
        [Required(ErrorMessage ="Please type into category name")]//Kullanıcının bu alanı boş geçmemesi temin edilmiştir.
        [MinLength(3, ErrorMessage ="Minimum length is 3")]//Burada kullanıcı en az 3 karakterli bir kategory girebilmesi temin edilmiştir. aksi duurmlarda ErrorMessage çalışacaktır.
        [RegularExpression(@"[a-zA-Z ]+$", ErrorMessage ="Only letters ars allowed")] //Reqular Expression kullanıcıdanın gireceği name bilgisinde sadece harf ve boşluk olmasını temin etti. Yani "/, ?, !" gibi sembolleri kullanıcı girerse ErrorMessage alacaktır.
        [Display(Name ="Category Name")]
        public string Name { get; set; }
    }
}
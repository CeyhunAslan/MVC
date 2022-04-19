
namespace YZL5155_MVC.Models.VMs
{
    public class CategoryVM
    {
        //View Model'in amacı kullanıcıya göstermek istediğimiz alanların oluşturulmasıdır. Vm'ler DTO'lar gibi veri transferinde kullanılmazlar. Sadece kullanıcıya veri göstermek için kullandığımız bir yapıdır.
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
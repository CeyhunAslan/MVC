
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YZL5155_MVC.DataAccess.Repositories.EntityType;
using YZL5155_MVC.Models.DTO;
using YZL5155_MVC.Models.Entities;
using YZL5155_MVC.Models.VMs;

namespace YZL5155_MVC.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Add(CategoryCreateDTO model)
        //{
        //    if (ModelState.IsValid) //Model'de koyduğumuz kurallara uyulmuşsa true döner.
        //    {
        //        //Aşağıdaki 3 satırlık kod bloğunda abstraction 2 örneğimizden beri yaptığımız işi yapıyoruz. Burada farklı olarak devreye Model kavramı girmiştir. Aşağıda CategoryCreateDTO tipinde ki model objesi ile Category tipinde ki category objesi match edilmektedir.
        //        Category category = new Category();
        //        category.Name = model.Name;
        //        categoryRepository.Create(category); //Unutmayın Create() methodumuz Category tipinde entity bekler. Çünkü BU methodun amacı Categories tablosunun içerisine Category eklemektir. Categories tablosu sadece Category tipinde entity ekliyebilirim yani CategoryCreateDTO yada başka bir generic tipte bir şey ekleyemem. Bu sebepten ötürü yukarıda Model ile Category nesnemizi eşledik.
        //        ViewBag.Condition = 1;
        //        return View();
        //        //return RedirectToAction("List"); // Buradaki RedirectToAction() methodu "List" isimli action method'a git demektir.
        //    }
        //    else
        //    {
        //        ViewBag.Condition = 2;
        //        return View(model);
        //    }
        //}

        [HttpPost]
        public JsonResult Add(CategoryCreateDTO model)
        {
            if (ModelState.IsValid) //Model'de koyduğumuz kurallara uyulmuşsa true döner.
            {
                //Aşağıdaki 3 satırlık kod bloğunda abstraction 2 örneğimizden beri yaptığımız işi yapıyoruz. Burada farklı olarak devreye Model kavramı girmiştir. Aşağıda CategoryCreateDTO tipinde ki model objesi ile Category tipinde ki category objesi match edilmektedir.
                Category category = new Category();
                category.Name = model.Name;
                categoryRepository.Create(category); //Unutmayın Create() methodumuz Category tipinde entity bekler. Çünkü BU methodun amacı Categories tablosunun içerisine Category eklemektir. Categories tablosu sadece Category tipinde entity ekliyebilirim yani CategoryCreateDTO yada başka bir generic tipte bir şey ekleyemem. Bu sebepten ötürü yukarıda Model ile Category nesnemizi eşledik.
                ViewBag.Condition = 1;
                return Json(model, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("List"); // Buradaki RedirectToAction() methodu "List" isimli action method'a git demektir.
            }
            else
            {
                ViewBag.Condition = 2;
                return Json(model);
            }
        }


        public ActionResult List()
        {
            IEnumerable<CategoryVM> categories = categoryRepository.Gets(x => x.Status != Status.Passive)
                                                                    .Select(x=> new CategoryVM { 
                                                                        Id = x.Id,
                                                                        Name=  x.Name
                                                                    });

            return View(categories);
        }

        public ActionResult Details(int id)
        {
            //Category detayına gitmek istediğimizde yada her hangi bir ürünün detayı gitmek istediğimizde ilk önce ilgli entity'i Id'sinden yakalıyoruz. Aşağıda ki satırda detayı görülmek istenilen category yakalıyoruz ve Category tipindeki objemize atıyoruz.
            Category category = categoryRepository.Get(x => x.Id == id);
            //Burada VM (View Model) mantığından bahsetmekte fayda var. VM'ler kullanıcıya göstermek istediğimiz veriler uygulama tarafında karşılamak için oluşturulurlar. Yani tam olarak business ne ise ona göre dizayn edilirler. Burada category detayında kullanıcıya ne göstermek istiyorsam ona göre CategoryDetailsVM sınıfında propery'ler tanımladım. Aşağıda ise ilgi VM'e yukarıda doldurduğum category objesinde karşılık gelen alanları ekledim ve view'a onu gönderdir. 
            CategoryDetailsVM detailsVM = new CategoryDetailsVM();
            detailsVM.Id = category.Id;//yakalanan nesnenin alanların isimleri ile VM'de ki alan isimleri aynı olmak zorunda değildir. Burada rahat anlaşılması için aynı sisimleri kullanıyoruz. Lakin ilerleyen zamanda bu eşleme işlemlerini AutoMapper aracılığıyla yaptığımızda isimlerin aynı olması bizim için çok önemli olacak.
            detailsVM.Name = category.Name;
            detailsVM.Status = category.Status;
            detailsVM.CreateDate = category.CreateDate;
            return View(detailsVM);//az önce doldurduğumuz objeyi view gönderdik.
        }

        //Update ve Create işlemlerinde dikkat ettiğiniz gibi 2 tane action method kullanıyoruz. BU işlemlerde kullanıcının önüne sayfa getirmek ve sayfa yani view aracılığıyla kullanıcıdan data alıp bunu veri tabanına göndermek için 2 tane action method kullanıyouruz. 
        [HttpGet] //Create işlemindeki get methodu boş view dönerken burada ki get methodu güncellenecek entity'i view taşıdı. Bu tarz işlemlerde senaryoya göre düşünmek bizim işimizdir. Neye ihtiyacımız varsa ona göre hareket etmeliyiz.
        public ActionResult Update(int id)
        {
            //Bu method bana güncellenecek category getirecek ve güncellenmek istenilen category'nin bilgilerini view'a basacak
            Category category = categoryRepository.Get(x => x.Id == id); //güncellemek istediğim category yakaladım.

            //DTO'lar data transferi için varlar. Yukarıda yakaladığım nesnenin view taşıayacağım alanlarını DTO aracılığıla view götürüyoruz. BUrada DTO kullanmak şart değildir. ViewBag ile de data taşınabilinir. Lakin MVC deseni gereği DTO kullanmak burada daha doğrudur.
            CategoryUpdateDTO model = new CategoryUpdateDTO();
            model.Id = category.Id;
            model.Name = category.Name;

            return View(model);//DTO aracılığıyla sadace ihtiyaçlarımızı view götürdük.
        }

        //Post methodu ise kullanıcıdan alınan bilgileri veri tabanına göndermek için var. Burada bir diğer önemli husus DTO (Data Transfer Object). DTO aracılığla UpdateView page controller'dan veri götürme ve gene view'dan controller'a veri transfer etmek için kullanılmaktadır.
        [HttpPost]
        public ActionResult Update(CategoryUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                Category category = categoryRepository.Get(x => x.Id == model.Id);
                category.Name = model.Name;
                category.UpdateDate = model.UpdateDate;
                category.Status = model.Status;
                ViewBag.Condition = 1;
                categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Condition = 2;
                return View(model);
            }   
        }


        //public ActionResult Delete(int id)
        //{
        //    Category category = categoryRepository.Get(x => x.Id == id);
        //    category.Status = Status.Passive;
        //    category.DeleteDate = DateTime.Now;
        //    ViewBag.Condition = 1;
        //    categoryRepository.Delete(category);

        //    return RedirectToAction("List");
        //}

        //JavaScript kütüphanesi olan Ajax ile delete işleminin yapılması. Ajax ile sayfamız post back olmadan. Daha performanslı bir şekilde delete işlemini gerçekleştireceğiz. Js kütüphaneleri olmasaydı, günümüzde kullanılan sosyal medya uygulamaları bu denli gelişemezdi. Örneğin bir tweet atıldığında bütün bir sayfanın post back olduğunu düşünelim, client devamlı  loading ekranını görürdü çünkü 1 saniye çok fazla requrest alacaktı uygulamamız. Başka bir örnek vermek gerekirse  comment yada like gibi process'lerde bu durum söz konusudur. BUrada Ajax devreye girmektedir.

        public JsonResult Delete(int id)
        {
            //Json (Java Script Object Natation)web teknolojileri arasında veri transferi için kullanılan bir veri tipidir diyebiliriz. Burada işlemlerimizi yaptıktan sonra view dönmek yerine json döneceğiz.
            //Delete işleminin BackEnd olduğu gibi ilk etapta delete edilecek entity yakalanır.
            Category category = categoryRepository.Get(x => x.Id == id);
            category.Status = Status.Passive;
            category.DeleteDate = DateTime.Now;
            categoryRepository.Delete(category);
            //yukarıdaki adımlarda gördüğünüz gibi işlemlerimizde bir farklılık bulunmamaktadır. Tek fark view yada farklı bir actionresult tetiklemek yerine yerine Json dönüyoruz.
            return Json("");
        }
    }
}
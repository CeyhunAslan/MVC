using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZL5155_MVC.DataAccess.Repositories.EntityType;
using YZL5155_MVC.Models.DTO;
using YZL5155_MVC.Models.Entities;
using YZL5155_MVC.Models.VMs;

namespace YZL5155_MVC.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        CategoryRepository categoryRepository = new CategoryRepository();

        [HttpGet]
        public ActionResult Add()
        {
            //ViewBag.Categories = new SelectList(catRepo.Gets(x => x.Status != Status.Passive)
            //    .Select(x => new { x.Id, x.Name }), "Id", "Name");

            CreateProductDTO model = new CreateProductDTO();
            model.Categories = categoryRepository.Gets(x => x.Status != Status.Passive)
                                                 .Select(x => new CategoryVM
                                                 {
                                                     Id = x.Id,
                                                     Name = x.Name
                                                 });

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(CreateProductDTO model)
        {
            
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Stock = model.Stock;
                product.CategoryId = model.CategoryId;

                ViewBag.Condition = 1;
                productRepository.Create(product);
                return RedirectToAction("Add");
            }
            else
            {
                ViewBag.Condition = 2;
                return RedirectToAction("Add");
            }
        }

        public ActionResult List()
        {
            //IEnumerable<ProductVM> products = productRepository.Gets(x => x.Status != Status.Passive)
            //    .Select(x => new ProductVM 
            //    { 
            //        Id = x.Id,
            //        Name = x.Name,
            //        Price = x.Price,
            //        Stock = x.Stock,
            //        Description = x.Description
            //        CategoryName = x.Category.Name
            //    });

            List<Product> products = productRepository.Gets(x => x.Status != Status.Passive);

            return View(products);
        }

        public ActionResult Details(int id)
        {
            Product product = productRepository.Get(x => x.Id == id);
            ProductDetailsVM productDetailsVM = new ProductDetailsVM();
            productDetailsVM.Id = product.Id;
            productDetailsVM.Name = product.Name;
            productDetailsVM.Description = product.Description;
            productDetailsVM.Price = product.Price;
            productDetailsVM.Stock = product.Stock;
            productDetailsVM.CategoryName = product.Category.Name;
            productDetailsVM.Status = product.Status;

            return View(productDetailsVM);
        }


        public ActionResult Update(int id)
        {
            Product product = productRepository.Get(x => x.Id == id);

            UpdateProductDTO model = new UpdateProductDTO();
            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Stock = product.Stock;
            
            model.Categories = categoryRepository.Gets(x => x.Status != Status.Passive)
                                                 .Select(x => new CategoryVM
                                                 {
                                                     Id = x.Id,
                                                     Name = x.Name
                                                 });

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(UpdateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                Product product = productRepository.Get(x => x.Id == model.Id);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Stock = model.Stock;
                product.CategoryId = model.CategoryId;
                product.UpdateDate = DateTime.Now;
                product.Status = Status.Modified;
                productRepository.Update(product);
                ViewBag.Condition = 1;
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Condition = 2;
                return RedirectToAction("Update");
            }
        }

        public ActionResult Delete(int id)
        {
            Product product = productRepository.Get(x => x.Id == id);

            product.Status = Status.Passive;
            product.DeleteDate = DateTime.Now;

            productRepository.Delete(product);
            return RedirectToAction("List");
        }
    }
}
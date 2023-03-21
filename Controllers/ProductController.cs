using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestOneDrive.Data;
using TestOneDrive.Models.Product;

namespace TestOneDrive.Controllers
{



    public class ProductController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        [Obsolete]
        private readonly IHostingEnvironment environment;

        [Obsolete]
        public ProductController(ApplicationDbContext applicationDbContext, IHostingEnvironment environment)
        {
            this.applicationDbContext = applicationDbContext;
            this.environment = environment;
        }

        //file upload

        public IActionResult UploadImage()
        {
            return View();
        }

        public ProductRegister UploadImage(ProductRegister model)
        {


            var path = environment.WebRootPath;
            var filePath = "Content/Image/" + model.ImagePath.FileName;
            var fullPath = Path.Combine(path, filePath);
            UploadFile(model.ImagePath, fullPath);
            //var data = new Image()
            //{
            //    ImageName = model.ImageName,
            //    ImagePath = filePath

            //};
            //applicationDbContext.ProductRegisters.Add(data);
            //applicationDbContext.SaveChanges();

            //return RedirectToAction("Index");
            model.FilePath = filePath;
            return model;


        }
        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
        public IActionResult Index()
        {
            List<ProductRegister> objlist = new List<ProductRegister>();
            objlist = GetProductList();
            return View(objlist);
        }

        [HttpPost]
        public IActionResult Index(string SearchText = "")
        {
            List<ProductRegister> products;
            if (SearchText != "" && SearchText != null)
            {

                products = applicationDbContext.ProductRegisters
                    .Where(e => e.ProductName.Contains(SearchText)|| e.ProductCode.Contains(SearchText) || e.ProductCategory.Contains(SearchText)||
                    e.Country.Contains(SearchText)|| e.State.Contains(SearchText) || e.City.Contains(SearchText) 
                    || e.CustomerPhone.Contains(SearchText)
                    || e.CustomerName.Contains(SearchText)
                    || e.Addess1.Contains(SearchText)
                    || e.Addess2.Contains(SearchText)
                    || e.Landmark.Contains(SearchText)
                    
                    )
                    .ToList();
            }
            
            else
            {
                products = GetProductList();
            }


            return View(products);
        }
        //Apply Search functiionality by Dates
       

        public List<ProductRegister> GetProductList()
        {
            var products = applicationDbContext.ProductRegisters.ToList();
            foreach (var item in products)
            {
                int countryi = Convert.ToInt32(item.Country);
                item.Country = applicationDbContext.Countries.Where(s => s.Id == countryi).Select(s => s.Name).FirstOrDefault();
                int statei = Convert.ToInt32(item.State);
                item.State = applicationDbContext.States.Where(s => s.Id == statei).Select(s => s.Name).FirstOrDefault();
                int cityi = Convert.ToInt32(item.City);
                item.City = applicationDbContext.Cities.Where(s => s.Id == cityi).Select(s => s.Name).FirstOrDefault();
            }

            return products;
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductRegister product)
        {
            if (ModelState.IsValid)
            {
                ProductRegister img = UploadImage(product);

                var Pr = new ProductRegister()
                {
                    ProductCode = product.ProductCode,
                    CustomerName=product.CustomerName,
                    CustomerEmail=product.CustomerEmail,
                    CustomerPhone=product.CustomerPhone,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductCategory = product.ProductCategory,
                    Price = product.Price,
                    ManufucturingDate = product.ManufucturingDate,
                    ExpireDate = product.ExpireDate,
                    Country = product.Country,
                    State = product.State,
                    City = product.City,
                    Addess1 = product.Addess1,
                    Addess2 = product.Addess2,
                    Landmark = product.Landmark,
                    ImageName = img.ImageName,
                    FilePath = img.FilePath



                };
                applicationDbContext.ProductRegisters.Add(Pr);
                applicationDbContext.SaveChanges();
                TempData["succsess"] = "Product registeration successfully completed ";
                //return View();
                return RedirectToAction("Index");
            }

            else
            {
                TempData["error"] = "Product alredy exist";
                return View(product);
            }



        }
        // Casecading dropdown Country/State/City
        public JsonResult Country()
        {
            var ctn = applicationDbContext.Countries.ToList();
            return new JsonResult(ctn);
        }
        public JsonResult State(int id)
        {
            var str = applicationDbContext.States.Where(e => e.Country.Id == id).ToList();
            return new JsonResult(str);
        }
        public JsonResult City(int id)
        {
            var ct = applicationDbContext.Cities.Where(e => e.State.Id == id).ToList();
            return new JsonResult(ct);
        }
        public IActionResult Edit(int Id)
        {
            var Result = applicationDbContext.ProductRegisters.Where(model => model.ProductId == Id).FirstOrDefault();
            var emp = new ProductRegister()
            {
                ProductCode = Result.ProductCode,
                CustomerName = Result.CustomerName,
                CustomerEmail = Result.CustomerEmail,
                CustomerPhone = Result.CustomerPhone,
                ProductName = Result.ProductName,
                ProductDescription = Result.ProductDescription,
                ProductCategory = Result.ProductCategory,
                Price = Result.Price,
                ManufucturingDate = Result.ManufucturingDate,
                ExpireDate = Result.ExpireDate,
                Addess1 = Result.Addess1,
                Addess2 = Result.Addess2,
                Landmark = Result.Landmark



            };

            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(ProductRegister products)
        {
            
            var edi = new ProductRegister()
            {
                ProductCode = products.ProductCode,
                CustomerName = products.CustomerName,
                CustomerEmail = products.CustomerEmail,
                CustomerPhone = products.CustomerPhone,
                ProductName = products.ProductName,
                ProductDescription = products.ProductDescription,
                ProductCategory = products.ProductCategory,
                Price = products.Price,
                ManufucturingDate = products.ManufucturingDate,
                ExpireDate = products.ExpireDate,
                Addess1 = products.Addess1,
                Addess2 = products.Addess2,
                Landmark = products.Landmark,

                



            };
            applicationDbContext.ProductRegisters.Update(edi);
            applicationDbContext.SaveChanges();
            TempData["succsess"] = "Product registration updated successfully ";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var res = applicationDbContext.ProductRegisters.SingleOrDefault(e => e.ProductId == id);
            applicationDbContext.ProductRegisters.Remove(res);
            applicationDbContext.SaveChanges();
            TempData["succsess"] = "Product Deleted successfully ";
            return RedirectToAction("Index");

        }




    }
}

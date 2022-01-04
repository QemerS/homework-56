using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shopping.Data;
using Shopping.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Areas.adminka.Controllers
{
    [Area("adminka")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _web;
        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _web = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_context.Products.OrderByDescending(e=>e.CreatedDate).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png")
                {
                    string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                    string filePath = Path.Combine(_web.WebRootPath, "Uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(stream);
                    }
                    model.Image = fileName;
                    model.CreatedDate = DateTime.Now;

                    _context.Products.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", "Sekilin olcusu 3 mb-dan boyukdur");
                    return View(model);
                }
                
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_context.Products.Find(id));
        }

        [HttpPost]
        public IActionResult Update(Product model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (model.ImageFile != null)
                    {
                        if (model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpg")
                        {
                            //update ucun elave
                            string oldImagePath = Path.Combine(_web.WebRootPath, "Uploads", model.Image);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                            //kopiya kimi qalan
                            string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_web.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }
                            model.Image = fileName;
                            /*model.CreatedDate = DateTime.Now;*/

                            _context.Products.Update(model); //
                            _context.SaveChanges();
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ModelState.AddModelError("", "Sekilin olcusu 3 mb-dan boyukdur");
                            return View(model);
                        }
                    }
                    else {
                        _context.Products.Update(model);
                        _context.SaveChanges();
                        return RedirectToAction("index");
                    }

                }
                else
                {
                    return View(model);
                }
                
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product name = _context.Products.Find(id);
            _context.Products.Remove(name);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

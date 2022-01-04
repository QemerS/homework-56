using Microsoft.AspNetCore.Mvc;
using Shopping.Data;
using Shopping.Models;
using Shopping.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            VmProduct name = new VmProduct()
            {
                Products = _context.Products.ToList()
            };
            string cart = Request.Cookies["in-cart"];
            if(!string.IsNullOrEmpty(cart))
            {
                name.Cart = cart.Split("-").ToList();
            }
            return View(name);
        }
        
        
    }
    
}

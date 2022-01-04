using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult addToCart(int Id)
        {
            CookieOptions optionlar = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1)
            };

            string oldData = Request.Cookies["in-cart"];
            string newData = null;
            if(string.IsNullOrEmpty(oldData))
            {
                newData = Id.ToString();
            }
            else
            {
                List<string> dataList = oldData.Split("-").ToList();
                if(dataList.Any(d=>d==Id.ToString()))
                {
                    dataList.Remove(Id.ToString());
                    newData = string.Join("-", dataList);
                }
                else
                {
                    newData = oldData + "-" + Id;
                }
            }

            Response.Cookies.Append("in-cart", newData, optionlar);
            return RedirectToAction("Index", "Product");
        }
    }
}

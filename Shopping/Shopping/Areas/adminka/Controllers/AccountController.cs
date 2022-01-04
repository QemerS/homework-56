using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Areas.adminka.Controllers
{
    [Area("adminka")]
    public class AccountController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
    }
}

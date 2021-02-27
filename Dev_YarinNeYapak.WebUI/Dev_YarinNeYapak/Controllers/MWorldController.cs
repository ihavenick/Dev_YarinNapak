using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dev_YarinNeYapak.Controllers
{
    public class MWorldController : Controller
    {
        public IActionResult Index(string enlem,string boylam)
        {
            ViewBag.Enlem = enlem;
            ViewBag.Boylam = boylam;
            return View();
        }
    }
}
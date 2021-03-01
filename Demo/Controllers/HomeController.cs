using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.User = User.Identity.Name;
            return View();
        }

       [Authorize(Policy= "blockCheck")]
        public IActionResult Privacy()
        {
           var data= User.Claims.ToList();
            return View();
        }

        [Authorize(Policy = "TehranCity")]
        public string Tehran() => "Theran";

        [Authorize(Policy = "HasCity")]
        public string City() => "City";

    }
}

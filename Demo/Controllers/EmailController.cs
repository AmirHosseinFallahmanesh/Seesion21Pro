using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class EmailController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public EmailController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Confirmed(string token,string email)
        {
        var user=  await  userManager.FindByEmailAsync(email);
            if (user!=null)
            {
                await userManager.ConfirmEmailAsync(user, token);
            }
            return Redirect("/");
        }
    }
}
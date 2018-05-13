using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VtpGameApi.Models;

namespace VtpGameApi.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            User user = new User();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (this.ModelState.IsValid && FormsAuthentication.Authenticate(user.Login, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Login, false);
                return RedirectToAction("Index", "Promo");
            }

            // If we got this far, something failed, redisplay form 
            ModelState.AddModelError("", "Incorrect username or password.");
            return View(user);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}
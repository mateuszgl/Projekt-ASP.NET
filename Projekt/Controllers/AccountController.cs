using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Projekt.Models;
using Projekt.DAL;

namespace Projekt.Controllers
{
    public class AccountController : Controller
    {
        private static BlogContext db = new BlogContext();
        private CustomAuthenticationService service = new CustomAuthenticationService(db);

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (service.isValid(model.Email, model.Password)){

                Session["LoggedAs"] = model.Email;
                Session["logged"] = true;
                Session["loggedId"] = service.GetPersonByEmail(model.Email).ID;
                return RedirectToAction("Index","Home");
            }
            else ViewBag.Error = "Nieprawidłowe dane logowania";


            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Error = "";
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!service.NicknameValid(model.Nickname))
                {
                    ViewBag.Error = "Podany Nick jest już zajęty";
                    return View(model);
                }

                if (!service.PersonExists(model.Email))
                {
                    var user = new Person { Nickname = model.Nickname, Email = model.Email, Password = model.Password };
                    user.Role = Person.Roles.Reader;
                    db.People.Add(user);
                    db.SaveChanges();
                    ViewBag.Error  = "";
                    return RedirectToAction("Index", "Home");

                }
                else ViewBag.Error = "Podany adres Email jest już zajęty";


            }

            return View(model);
        }



        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }




        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session["logged"] = false;

            //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Index", "Home");
        }
    }
    
    
}
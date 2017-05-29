using System;
using System.Linq;
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
    [IsLogged]
    public class ManageController : Controller
    {
        private static BlogContext db = new BlogContext();
        private CustomAuthenticationService service = new CustomAuthenticationService(db);
        //
        // GET: /Manage/Index
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index(string message)
        {
            if(message!=null)
            ViewBag.Message = message;

            //var person = service.GetPersonByEmail((string)Session["LoggedAs"]);
            var id = (int)Session["loggedId"];
            Person person = db.People.Find(id);

            //var person = db.People.Single(p => p.Email == login);
            //ViewBag.Person = person;

            return View(person);
        }

     
   
        //
        // GET: /Manage/ChangePassword
        [IsLogged]
        public ActionResult ChangePassword()
        {
            ViewBag.Error = "";
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [IsLogged]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var person = service.GetPersonByEmail((string)Session["LoggedAs"]);
                if (service.isValid(person.Email, model.OldPassword)){
                    person.Password = model.NewPassword;
                    service.UpdatePerson(person);
                    return RedirectToAction("Index", new { Message = "Pomślnie zmieniono hasło" });
                }
                else ViewBag.Error = "Hasło nieprawidłowe";
            }
            return View(model);
        }

    }
}
using Projekt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Projekt.Models.Person;

namespace Projekt.Controllers
{
    public class IsLogged: ActionFilterAttribute
    {
        public Roles Role { get; set; }
        private BlogContext db = new BlogContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((bool)filterContext.HttpContext.Session["logged"] == false)
            {
                    filterContext.Result = new RedirectResult("~/Account/Login");
                    return;
            }
                
            var pId = (int)filterContext.HttpContext.Session["loggedId"];
            var pRole = db.People.Single(p => p.ID == pId).Role;

            if ((Role==Models.Person.Roles.Administrator)&&(pRole!=Models.Person.Roles.Administrator)) 
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            if ((Role == Models.Person.Roles.Writer) && (pRole == Models.Person.Roles.Reader))
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Projekt.Models.Person;

namespace Projekt.DAL
{
    public abstract class CustomHelpers: WebViewPage
    {

        public static bool isInRole(Roles role)
        {
            BlogContext db = new BlogContext();
            Roles CurrentRole;

            if (HttpContext.Current.Session["loggedId"] != null)
            {
                var pId = (int)HttpContext.Current.Session["loggedId"];
                CurrentRole = db.People.Single(p => p.ID == pId).Role;
            } else
                CurrentRole = Roles.Reader;

            if ((role == Roles.Administrator) && (CurrentRole == role))
                return true;
            else
                if ((role == Roles.Writer) && (CurrentRole != Roles.Reader))
                return true;

            return false;
        }
    }
}
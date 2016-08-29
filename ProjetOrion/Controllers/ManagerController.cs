using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetOrion.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult IndexManager()
        {
            return View();
        }

        public ActionResult ProfilUtilisateur()
        {
            return View();
        }
    }
}
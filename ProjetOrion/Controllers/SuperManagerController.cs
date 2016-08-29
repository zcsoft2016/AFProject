using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetOrion.Controllers
{
    public class SuperManagerController : Controller
    {
        // GET: SuperManager
        public ActionResult IndexSuperManager()
        {
            return View();
        }

        public ActionResult ProfilUtilisateur()
        {
            return View();
        }

        public ActionResult AjouterUtilisateur()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnregistrerUtilisateur(int? id, string pseudo, string email, string motDePasse, string confirmMotDePasse)
        {
            return View();
        }
    }
}
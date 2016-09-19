using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetOrion.Models;

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
        public ActionResult AjouterUtilisateur(Utilisateur utilisateur)
        {
            using (IDal dal = new Dal())
            {
                if (dal.PseudoExiste(utilisateur.Pseudo))
                {
                    ModelState.AddModelError("Pseudo", "Ce pseudo existe déjà");
                    return View(utilisateur);
                }
                if (dal.EmailExiste(utilisateur.Email))
                {
                    ModelState.AddModelError("Email", "Cet email existe déjà");
                    return View(utilisateur);
                }
                if (!ModelState.IsValid)
                    return View(utilisateur);
                //utilisateur.Photo = Path.Combine(Server.MapPath("~/Content/img/profiles"), "default.jpg");
                //todo to refactor
                utilisateur.Photo = "~/Content/img/pictures/profiles/" + "default.jpg";
                dal.AjouterUtilisateur(utilisateur);
                return RedirectToAction("TousLesUtilisateurs");
            }
        }

        public ActionResult TousLesUtilisateurs()
        {
            return View();
        }
    }
}
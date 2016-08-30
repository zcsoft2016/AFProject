using System;
using System.Collections.Generic;
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
            if (!ModelState.IsValid)
            {
                //todo remplacer par de l'ajax
                ViewBag.MessageErreur = "Erreur lors de l'enregistrement";
                return View(utilisateur);
            }
            using (IDal dal = new Dal())
            {
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
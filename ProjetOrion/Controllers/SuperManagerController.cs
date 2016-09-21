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

        public ActionResult ProfilUtilisateur(int id)
        {
            using (var dal = new Dal())
            {
                var utilisateur = dal.ObtenirUtilisateur(id);
                if (utilisateur != null)
                    return View(utilisateur);
            }
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
                utilisateur.Photo = "/Content/img/pictures/profiles/" + "default.jpg";
                dal.AjouterUtilisateur(utilisateur);
                return RedirectToAction("TousLesUtilisateurs");
            }
        }

        public ActionResult TousLesUtilisateurs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModifierUtilisateur(Utilisateur utilisateur, HttpPostedFileBase myPhoto)
        {
            using (var dal = new Dal())
            {
                string motDePasse = null;
                string photo = null;
                var user = dal.ObtenirUtilisateur(utilisateur.Pseudo);
                if (!string.IsNullOrEmpty(utilisateur.MotDePasse))
                    motDePasse = utilisateur.MotDePasse;

                if (myPhoto != null && myPhoto.ContentLength>0)
                {
                        try
                        {
                            string path = Path.Combine(Server.MapPath("~/Content/img/pictures/profiles"),
                                GetFileName(myPhoto.FileName, user.Id));
                            myPhoto.SaveAs(path);

                            photo = "/Content/img/pictures/profiles/" + GetFileName(myPhoto.FileName, user.Id);
                            ViewBag.Message = "File uploaded successfully";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        }
                }
                dal.ModifierUtilisateur(user, motDePasse, photo);
                return View("ProfilUtilisateur", utilisateur);
            }
        }

        private string GetFileName(string fileName, int userId)
        {
            var index = fileName.IndexOf('.');
            if (index != -1)
                return string.Format("{0}{1}", userId, fileName.Substring(index));
            throw new ArgumentException("Error dans l'extension de l'image");
        }
    }
}
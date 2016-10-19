using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjetOrion.Models;

namespace ProjetOrion.Controllers
{
    public class SuperManagerController : Controller
    {
        const string ContentImgPicturesProfiles = "/Content/img/pictures/profiles/";
        // GET: SuperManager
        [Authorize]
        public ActionResult IndexSuperManager()
        {
            using (var dal = new Dal())
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var idString = HttpContext.User.Identity.Name;
                    var id = int.Parse(idString);
                    var utilisateur = dal.ObtenirUtilisateur(id);
                    return View(utilisateur);
                }
                return View("Error");
            }
        }

        [Authorize]
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

        //[Authorize]
        //public ActionResult ProfilUtilisateur(Utilisateur utilisateur)
        //{
        //    return View(utilisateur);
        //}

        [Authorize]
        public ActionResult AjouterUtilisateur()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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
                utilisateur.Photo = ContentImgPicturesProfiles + "default.jpg";
                dal.AjouterUtilisateur(utilisateur);
                return RedirectToAction("TousLesUtilisateurs");
            }
        }

        [Authorize]
        public ActionResult TousLesUtilisateurs()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

                            photo = ContentImgPicturesProfiles + GetFileName(myPhoto.FileName, user.Id);
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

        [Authorize]
        public ActionResult AjouterCommercial()
        {
            return View();
        }

        [Authorize]
        public ActionResult TousLesCommerciaux()
        {
            return View();
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
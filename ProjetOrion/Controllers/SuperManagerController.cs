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
            var userViewModel = ObtenirUtilisateurViewModelConnecte();
            if (userViewModel != null)
                return View(userViewModel);
            return View("Error");
        }

        public UtilisateurViewModel ObtenirUtilisateurViewModelConnecte()
        {
            using (var dal = new Dal())
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var idString = HttpContext.User.Identity.Name;
                    var id = int.Parse(idString);
                    var utilisateur = dal.ObtenirUtilisateur(id);
                    return new UtilisateurViewModel(utilisateur);
                }
                return null;
            }
        }

        [Authorize]
        public ActionResult ProfilUtilisateur(int id)
        {
            using (var dal = new Dal())
            {
                var utilisateur = dal.ObtenirUtilisateur(id);
                if (utilisateur != null)
                    return View(new UtilisateurViewModel(utilisateur));
            }
            return View("Error");
        }

        //[Authorize]
        //public ActionResult ProfilUtilisateur(Utilisateur utilisateur)
        //{
        //    return View(utilisateur);
        //}

        [Authorize]
        public ActionResult AjouterUtilisateur()
        {
            var viewModelConnecte = ObtenirUtilisateurViewModelConnecte();
            var newUser = new Utilisateur();
            if (viewModelConnecte != null)
                return View(new UtilisateurViewModel(newUser, viewModelConnecte.Id));
            return View("Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AjouterUtilisateur(Utilisateur utilisateur)
        {
            var viewModelConnecte = ObtenirUtilisateurViewModelConnecte();
            var viewModel = new UtilisateurViewModel(utilisateur, viewModelConnecte.Id);
            using (IDal dal = new Dal())
            {
                if (dal.PseudoExiste(utilisateur.Pseudo))
                {
                    //ModelState.AddModelError("Pseudo", "Ce pseudo existe déjà");
                    ViewBag.ErrorPseudo = "Ce pseudo existe déjà";
                    return View(viewModel);
                }
                if (dal.EmailExiste(utilisateur.Email))
                {
                    //ModelState.AddModelError("Email", "Cet email existe déjà");
                    ViewBag.ErrorEmail = "Cet email existe déjà";
                    return View(viewModel);
                }
                if (!string.IsNullOrEmpty(utilisateur.MotDePasse) && !string.IsNullOrEmpty(utilisateur.ConfirmerMotDePasse))
                {
                    if (!utilisateur.MotDePasse.Equals(utilisateur.ConfirmerMotDePasse))
                    {
                        ViewBag.ErrorPassword = "Les mots de passe ne concordent pas";
                        return View(viewModel);
                    }
                }
                if (!ModelState.IsValid)
                    return View(viewModel);
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
            var viewModelConnecte = ObtenirUtilisateurViewModelConnecte();
            return View(viewModelConnecte);
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
                return View("ProfilUtilisateur", new UtilisateurViewModel(utilisateur));
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjetOrion.Models;

namespace ProjetOrion.Controllers
{
    /*public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
    }*/

    public class LoginController : Controller
    {
        private IDal dal;

        public LoginController() : this(new Dal())
        {

        }

        private LoginController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        public ActionResult Index()
        {
            UtilisateurViewModel viewModel = new UtilisateurViewModel(HttpContext.User.Identity.IsAuthenticated);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
                return RedirectToAction("IndexSuperManager", "SuperManagerController");
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(UtilisateurViewModel viewModel, string returnUrl)
        {
            if (viewModel.Utilisateur != null)
            {
                Utilisateur utilisateur = dal.Authentifier(viewModel.Utilisateur.Pseudo, viewModel.Utilisateur.MotDePasse);
                if (utilisateur != null)
                {
                    ViewData["user"] = utilisateur;
                    FormsAuthentication.SetAuthCookie(utilisateur.Id.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("IndexSuperManager", "SuperManager");
                }
                ModelState.AddModelError("Utilisateur.Pseudo", "Pseudo et/ou mot de passe incorrect(s)");
            }
            return View(viewModel);
        }

       

        /*public ActionResult CreerCompte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerCompte(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                int id = dal.AjouterUtilisateur(utilisateur.Pseudo, utilisateur.MotDePasse);
                FormsAuthentication.SetAuthCookie(id.ToString(), false);
                return Redirect("/");
            }
            return View(utilisateur);
        }
        */

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }

}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetOrion.Controllers
{
    [Authorize]
    public class CommercialController : Controller
    {
        // GET: Commercial
        public ActionResult IndexCommercial()
        {
            return View();
        }

        public ActionResult ProfilUtilisateur()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BO;
using WebMatrix.WebData;
using WUI.Models;

namespace WUI.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Connection/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PersonneModel personne)
        {

            if (personne != null)
            {
                Personne pers = Extensions.Extensions.ToBo(personne);
                MgtPersonne mgt = new MgtPersonne();
                pers = mgt.Addpersonne(pers);
                personne = Extensions.Extensions.ToModel(pers);
                if (pers != null)
                {
                    return View("Connect", personne);
                }

            }
            else
            {
                ModelState.AddModelError("", "Le nom d'utilisateur ou mot de passe fourni est incorrect.");
                return View(personne);
            }
            return null;
        }

    }
}

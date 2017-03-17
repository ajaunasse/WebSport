using System;
using System.Collections.Generic;
using System.Configuration;
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
            int outPhone = 0;
            if (!String.IsNullOrEmpty(personne.Phone) && personne.Phone.Length != 10 && !int.TryParse(personne.Phone,out outPhone))
            {
                        ViewBag.MessageErreurPhone = "le numéro de téléphone doit comporter 10 chiffres sans espace";
                        return View();
            }
            

            if (personne != null)
            {
                Personne pers = Extensions.Extensions.ToBo(personne);
                MgtPersonne mgt = new MgtPersonne();
                pers = mgt.Addpersonne(pers);
                personne = Extensions.Extensions.ToModel(pers);
                if (pers != null)
                {
                    Session.Add("user",personne);
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

        
        public ActionResult Edit()
        {
            PersonneModel personne = (PersonneModel)HttpContext.Session["user"];
            return View(personne);
        }

    }
}

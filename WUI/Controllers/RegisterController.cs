using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BO;
using WebMatrix.WebData;
using WUI.Filters;
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

        [RoleFilter(idRole = 2)]
        public ActionResult Edit()
        {
            PersonneModel personne = (PersonneModel)HttpContext.Session["user"];
            return View(personne);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(idRole = 2)]
        public ActionResult Edit(PersonneModel model)
        {
            if (model.Nom == null)
            {
                ViewBag.MessageErreurNom = "Un nom doit être renseigné";
                return View("Edit", model);
            }
            if (model.Prenom == null)
            {
                ViewBag.MessageErreurPrenom = "Un prénom doit être renseigné";
                return View("Edit", model);
            }

            if (model.Email == null)
            {
                ViewBag.MessageErreurEmail = "Un email doit être renseigné";
                return View("Edit", model);
            }

            if (model.Password == null)
            {
                ViewBag.MessageErreurPass = "Un mot de passe doit être renseigné";
                return View("Edit", model);
            }

            int outPhone = 0;

            if (!String.IsNullOrEmpty(model.Phone) && model.Phone.Length != 10 && !int.TryParse(model.Phone, out outPhone))
            {
                ViewBag.MessageErreurPhone = "le numéro de téléphone doit comporter 10 chiffres sans espace";
                return View("Edit", model);
            }


            Personne pers = Extensions.Extensions.ToBo(model);
            MgtPersonne mgt = new MgtPersonne();
            pers = mgt.UpdatePersonne(pers);
            model = Extensions.Extensions.ToModel(pers);

            Session.Add("user", model);
            return View("Connect", model);
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Connect(PersonneModel personneModel)
        {
            return View(personneModel);
        }
    }
}

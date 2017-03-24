using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WUI.Extensions;

namespace WUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var races = MgtRace.GetInstance().GetAllItems().ToModels();
            ArrayList villes = new ArrayList();
            foreach (var race in races)
            {
                villes.Add(race.Town);
            }
            ViewBag.villes = villes;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Votre page de description d’application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Votre page de contact.";

            return View();
        }
    }
}

using BLL;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WUI.Extensions;

namespace WUI.Controllers
{
    public class StatistiqueController : Controller
    {
        //
        // GET: /Statistique/

        public ActionResult Index()
        {
            //Stats Races
            var races = MgtRace.GetInstance().GetAllItems(true).ToModels(true);
            ArrayList villes = new ArrayList();
            ArrayList nbParticipants = new ArrayList();

            //Stats âges
            var nbPersonne = MgtPersonne.GetInstance().GetAllItems().Count();
            foreach (var race in races)
            {
                villes.Add(race.Town);
                nbParticipants.Add(race.Competitors.Count());
            }

            ViewBag.villes = villes;
            ViewBag.nbParticipants = nbParticipants;
            return View();
        }



    }

  
}

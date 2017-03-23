using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
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

            var IDNantes = MgtRace.GetInstance().GetRace(1);
            List<Resultat> temps = MgtResultat.GetInstance().GetResultatsByIdRace(IDNantes);

            ViewBag.TempsCourseNantes = temps.Select(x => x.TempsDeCourse).ToList();
            ViewBag.idParticipant = temps.Select(x => x.Personne.Id).ToList();

            ViewBag.villes = villes;
            ViewBag.nbParticipants = nbParticipants;
            return View();
        }



    }

  
}

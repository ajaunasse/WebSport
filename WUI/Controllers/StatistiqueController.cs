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

            //Variables
            ArrayList villes = new ArrayList();
            ArrayList nbParticipants = new ArrayList();
            string[] tranches = new string[4] { "18-29", "30-39", "40-49", "+50" };
            ArrayList totalByAge = statByAge();
  

            var races = MgtRace.GetInstance().GetAllItems(true).ToModels(true);
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
            ViewBag.totalByAge = totalByAge;
            ViewBag.tranches = tranches;

            return View();
        }

        private ArrayList statByAge()
        {
            ArrayList nbByAge= new ArrayList();

            //Total Personne
            var nbPersonne = MgtPersonne.GetInstance().GetAllItems().Count();

            //18-29
            DateTime currentDate = DateTime.Now;
            DateTime date1 = new DateTime(currentDate.Year - 18, 1, 1);
            DateTime date2 = new DateTime(currentDate.Year - 29, 1, 1);
            var totalTranche1 = MgtPersonne.GetInstance().getByTrancheDate(date1, date2).Count();

            // 30 - 39
            date1 = new DateTime(currentDate.Year - 30, 1, 1);
            date2 = new DateTime(currentDate.Year - 39, 1, 1);
            var totalTranche2 = MgtPersonne.GetInstance().getByTrancheDate(date1, date2).Count();

            // 40 - 49
            date1 = new DateTime(currentDate.Year - 40, 1, 1);
            date2 = new DateTime(currentDate.Year - 49, 1, 1);
            var totalTranche3 = MgtPersonne.GetInstance().getByTrancheDate(date1, date2).Count();

            // +50
            date1 = new DateTime(currentDate.Year - 50, 1, 1);
            var totalTranche4 = MgtPersonne.GetInstance().getByTrancheDate(null, date1).Count();

            //Calcul %age
            double pourcent1 = Math.Round((double) totalTranche1 / (double) nbPersonne,2) * 100 ;
            double pourcent2 = Math.Round((double)totalTranche2 / (double)nbPersonne, 2) * 100;
            double pourcent3 = Math.Round((double)totalTranche3 / (double)nbPersonne, 2) * 100;
            double pourcent4 = Math.Round((double)totalTranche4 / (double)nbPersonne, 2) * 100;


            //Ajout dans ArrayList
            nbByAge.Add(pourcent1);
            nbByAge.Add(pourcent2);
            nbByAge.Add(pourcent3);
            nbByAge.Add(pourcent4);

            return nbByAge;
        }


    }

  
}

using BLL;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WUI.Extensions;
using WUI.Filters;
using WUI.Models;

namespace WUI.Controllers
{
    // Pour appeler n'importe quelle méthode, l'utilisateur doit être connecté
    // Sauf si une méthode lève la condition avec : [AllowAnonymous]
   
    public class RaceController : Controller
    {

        /// <summary>
        /// Constructeur
        /// </summary>
        public RaceController()
        {
        }

        //
        // GET: /Race/
        // Tous les utilisateurs peuvent visionnés la liste
        [RoleFilter(idRole = 1)]
        public ActionResult Index()
        {
            var result = MgtRace.GetInstance().GetAllItems().ToModels();
            return View(result);
        }

        //
        // GET: /Race/Details/5
        [RoleFilter(idRole = 1)]
        public ActionResult Details(int id)
        {
            var result = MgtRace.GetInstance().GetRace(id).ToModel();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        //
        // GET: /Race/Create
        [RoleFilter(idRole = 1)]
        public ActionResult Create()
        {
            return View();
        }

        //GET: /Race/AddPoint
        [RoleFilter(idRole = 1)]
        public ActionResult AddPoint()
        {
           // var result = MgtPoint.GetInstance().GetAllItems.ToModels();
            List<Point> points =  MgtPoint.GetInstance().GetAllItems();
            return View(points);
        }

        //
        // POST: /Race/Create
        [HttpPost]
        [RoleFilter(idRole = 1)]
        // Le flag ci-dessus permet de préciser à l'action Create qu'il faut vérifier
        // si le token issu du cookie d'authentification a été bien été défini dans
        // la requête HTTP POST pour l'envoi du formulaire
        public ActionResult Create(RaceModel race)
        {
            try
            {
                if (MgtRace.GetInstance().AddRace(race.ToBo()))
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    return View();
                }

            }
            catch(Exception e)
            {
                return View();
            }
        }

        //
        // GET: /Race/Edit/5
        [RoleFilter(idRole = 1)]
        public ActionResult Edit(int id = 0)
        {
            var result = MgtRace.GetInstance().GetRace(id).ToModel(true);
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        //
        // POST: /Race/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(idRole = 1)]
        public ActionResult Edit(RaceModel race)
        {
            try
            {
                var result = MgtRace.GetInstance().UpdateRace(race.ToBo());
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Race/Delete/5
        [RoleFilter(idRole = 1)]
        public ActionResult Delete(int id)
        {
            var result = MgtRace.GetInstance().GetRace(id).ToModel();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        //
        // POST: /Race/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(idRole = 1)]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var result = MgtRace.GetInstance().RemoveRace(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
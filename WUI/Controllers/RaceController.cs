﻿using BLL;
using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using PagedList;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(idRole = 1)]
        public ActionResult AddPoint(RaceModel race)
        {
            try
            {
                RaceModel newRace = MgtRace.GetInstance().GetRace(race.Id).ToModel();
                newRace.Points = new List<PointModel>();
                newRace.Distance = race.Distance;
                var check = Request.Form["checkbox"];
                if (check == null)
                {
                    race.point.isPoi = false;
                    race.point.Category = null;
                }
                else
                {
                    race.point.isPoi = true;
                }
                newRace.Points.Add(race.point);
                var result = MgtRace.GetInstance().UpdateRace(newRace.ToBo());
                RaceModel reloadRace = MgtRace.GetInstance().GetRace(race.Id).ToModel();
                if (result)
                {
                    return Json(reloadRace);

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        [RoleFilter(idRole = 1)]
        public void UpdateDistance(int distance, int idRace)
        {
            Race newRace = MgtRace.GetInstance().GetRace(idRace);
            newRace.Distance = distance;
            MgtRace.GetInstance().UpdateDistance(newRace);
        }

        //
        // POST: /Race/DeletePoint
        [HttpGet]
        [RoleFilter(idRole = 1)]
        public ActionResult DeletePoint(int idPoint)
        {
            try
            {
                double latitude = MgtPoint.GetInstance().GetPoint(idPoint).Latitude;
                if (MgtPoint.GetInstance().DeletePoint(idPoint))
                {
                    return Json(latitude, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpGet]
        [RoleFilter(idRole = 1)]
        public ActionResult DeleteRace(int idRace)
        {
            try
            {
                if (MgtRace.GetInstance().RemoveRace(idRace))
                {
                    return RedirectToAction("Index");
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
                    PersonneModel user = (PersonneModel)Session.Contents["User"];
                    int lastId = MgtRace.GetInstance().GetAllItems().Max(x => x.Id);
                    MgtRace.GetInstance().SuscribeRace(user.ToBo(), lastId);

                    return RedirectToAction("Edit", new { id = lastId });
                }
                else
                {
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }
        }

        //
        // GET: /Race/Edit/5
        [RoleFilter(idRole = 1)]
        public ActionResult Edit(int id = 0)
        {
            var result = new MgtRace().GetRace(id).ToModel(true);

            List<CategoryModel> categories = new MgtRace().getAllCategory().ToModels();
            List<SelectListItem> cats = new List<SelectListItem>();

            foreach (CategoryModel cat in categories)
            {
                SelectListItem slc = new SelectListItem();

                slc.Text = cat.Title;
                slc.Value = cat.Id.ToString();

                cats.Add(slc);
            }

            ViewBag.Categories = cats;

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
                race.Points = new List<PointModel>();
                race.Points.Add(race.point);
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
            catch (Exception e)
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



        [RoleFilter(idRole = 2)]
        public ActionResult ListRace()
        {
            PersonneModel user = (PersonneModel)Session.Contents["user"];
            //List<Race> races = MgtRace.GetInstance().GetAllItems();
            List<Race> racesDispo = MgtRace.GetInstance().getRacebyUser(user.Id);

            return View(racesDispo.ToModels());

        }


        [HttpGet]
        public ActionResult Suscribe(int idRace)
        {
            PersonneModel user = (PersonneModel)Session.Contents["User"];
            if(user == null)
            {
                return RedirectToAction("Index", "Register", new { idRace = idRace });
            }
            List<Race> races = MgtRace.GetInstance().SuscribeRace(user.ToBo(), idRace);
            return View("MyRaces", races.ToModels());

        }

        public ActionResult MyRaces()
        {
            PersonneModel user = (PersonneModel)Session.Contents["User"];
            List<Race> races = MgtRace.GetInstance().MyRaces(user.Id);
            if (races == null)
            {
                return View();
            }
            return View(races.ToModels());
        }


        public ActionResult Unsubscribe(int idrace)
        {
            PersonneModel user = (PersonneModel)Session.Contents["User"];
            List<Race> races = MgtRace.GetInstance().Unsubscribe(user.Id, idrace);
            return View("MyRaces", races.ToModels());
        }

        public ActionResult ResultatByUser()
        {

            PersonneModel user = (PersonneModel)Session.Contents["User"];

            List<Resultat> results = new List<Resultat>();
            results = MgtResultat.GetInstance().GetResultatsById(user.Id);

            List<ResultatModel> resultatModels = results.Select(x => x.ToModel()).ToList();

            return View(resultatModels);
        }

        public ActionResult Importresult()
        {
            return View();
        }

        public ActionResult Import(int? page = 1)
        {

            PersonneModel personne = (PersonneModel)Session.Contents["user"];
            List<ResultatModel> modelsResult = new List<ResultatModel>();

           
            HttpPostedFileBase fichier = Request.Files["FileResult"];


            if (fichier != null)
            {
                StreamReader stream = new StreamReader(fichier.InputStream);
                string chaine = stream.ReadToEnd();

                String[] lignes = chaine.Split('\r', '\n');

                foreach (var ligne in lignes)
                {
                    if(String.IsNullOrEmpty(ligne)) { continue;}
                    ResultatModel model = new ResultatModel();
                    string[] champs = ligne.Split(';');
                    
                    model.Race = MgtRace.GetInstance().GetRace(int.Parse(champs[1])).ToModel();
                    model.Personne = MgtPersonne.GetInstance().GetPersonneByID(int.Parse(champs[0])).ToModel();
                    model.Classement = int.Parse(champs[2]);
                    model.TempsDeCourse = TimeSpan.Parse(champs[3]);
                    model.HeureDebut = TimeSpan.Parse(champs[4]);
                    model.HeureArrivee = TimeSpan.Parse(champs[5]);
                    modelsResult.Add(model);
                }
               
            }

            
            


            bool res = MgtResultat.GetInstance().Save(modelsResult.Select(x => x.ToBo()).ToList());


            if (res)
            {
                return RedirectToAction("Index", "Admin");
                //return View(valRet);
            }
            else
            {
                return View("Importresult");
            }

            
        }


        public ActionResult Affichage()
        {

            List<int> results = MgtResultat.GetInstance().GetResultats();

            List<RaceModel> races = new List<RaceModel>();
            foreach (int result in results)
            {
                RaceModel race = MgtRace.GetInstance().GetRace(result).ToModel();
                races.Add(race);
            }


            return View(races);
        }

        public ActionResult AfficheResult(int idRace, int? page = 1)
        {

            List<Resultat> results = MgtResultat.GetInstance().GetResultatsByIdRace(idRace);
            Race race = MgtRace.GetInstance().GetRace(idRace);

            int pageSize = 50;
            int pageNumber = (page ?? 1);

            var resultPage = results.Select(x => x.ToModel()).ToPagedList(pageNumber, pageSize);

            ViewBag.race = race.Id;
            ViewBag.ville = race.Title +" - "+ race.Town;
            return View("Import", resultPage);
        }


        public ActionResult ExportResults(int idRace)
        {
            List<Resultat> results = MgtResultat.GetInstance().GetResultatsByIdRace(idRace);
            Race course = MgtRace.GetInstance().GetRace(idRace);

            string lines =  "Id Particpant;IdCourse;Classement;.Temps de course";

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Result" + course.Title+"_" + course.Town +".csv");
            file.WriteLine(lines);

            foreach (Resultat resultat in results)
            {
                string ligne = resultat.Personne.Nom+";"+resultat.Race.Town+";"+resultat.Classement+";"+resultat.TempsDeCourse;
                file.WriteLine(ligne);
            }

            file.Close();
            return RedirectToAction("Index", "Admin");

        }
    }
}
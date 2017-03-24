using BLL;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WUI.Extensions;
using WUI.Models;

namespace WUI.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            // Variables
            string ville = null;
            string strDateDebut = null;
            string strDateFin = null;
            DateTime? dateDebut = null;
            DateTime? dateFin = null;
            List<Race> races = new List<Race>();
            ArrayList listId = new ArrayList();
            if (Request.QueryString["ville"] != "")
            {
                ville = Request.QueryString["ville"];
            }
            if(Request.QueryString["dateDebut"] != "")
            {
                strDateDebut = Request.QueryString["dateDebut"];
                dateDebut= DateTime.Parse(strDateDebut);
            }
            if(Request.QueryString["dateFin"] != "")
            {
                strDateFin = Request.QueryString["dateFin"];
                dateFin = DateTime.Parse(strDateDebut);
            }
            if(Session.Contents["User"] != null)
            {
                PersonneModel user = (PersonneModel)Session.Contents["User"];
                races = MgtRace.GetInstance().MyRaces(user.Id);
                foreach(var race in races)
                {
                    listId.Add(race.Id);
                }
            }

            ViewBag.racesId = listId;

            var result = MgtRace.GetInstance().getRacesBySearch(ville, dateDebut, dateFin);
            return View(result);

  
        }

    }
}

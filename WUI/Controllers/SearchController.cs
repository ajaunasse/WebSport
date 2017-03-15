using BLL;
using System;
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
            string ville = Request.QueryString["ville"];
            string strDateDebut = Request.QueryString["dateDebut"];
            string strDateFin = Request.QueryString["dateFin"];
            DateTime dateDebut = DateTime.Parse(strDateDebut);
            DateTime dateFin = DateTime.Parse(strDateFin);
            var result = MgtRace.GetInstance().GetRaceByTownAndDate(ville, dateDebut, dateFin);
            return View(result);

  
        }

    }
}

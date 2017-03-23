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
            // Variables
            string ville = null;
            string strDateDebut = null;
            string strDateFin = null;
            DateTime? dateDebut = null;
            DateTime? dateFin = null;
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
            

            var result = MgtRace.GetInstance().getRacesBySearch(ville, dateDebut, dateFin);
            return View(result);

  
        }

    }
}

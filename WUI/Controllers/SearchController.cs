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
            string type = Request.QueryString["dateDebut"];
            string date = Request.QueryString["dateFin"];

            var result = MgtRace.GetInstance().GetAllItems();
            return View();

  
        }

    }
}

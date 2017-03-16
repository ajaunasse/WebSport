using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WUI.Filters;
using WUI.Models;

namespace WUI.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [RoleFilter(idRole=1)]
        public ActionResult Index()
        {
            return View();
            
        }

    }
}

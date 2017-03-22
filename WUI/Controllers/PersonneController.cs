using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using WUI.Extensions;
using PagedList;
using WUI.Filters;

namespace WUI.Controllers
{
    public class PersonneController : Controller
    {
        //
        // GET: /Personne/
        [RoleFilter(idRole = 1)]
        public ActionResult Index(int? page = 1)
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);

            var result = MgtPersonne.GetInstance().GetAllItems().ToModels();
            var valRet = result.ToPagedList(pageNumber, pageSize);
            return View(valRet);
        }

    }
}

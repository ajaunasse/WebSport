using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WUI.Controllers
{
    public class StatistiqueController : Controller
    {
        //
        // GET: /Statistique/

        public ActionResult Index()
        {
            return View();
        }

        public void Test()
        {



            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\Public\WriteLines.txt"))
            {
                for(var i=3; i<500; i++)
                {
                    Random rnd = new Random();
                    var line = "INSERT INTO [Participant] VALUES(" + i + ", 2, 1,0);";
                    file.WriteLine(line);
                }
            }
        }

    }
}

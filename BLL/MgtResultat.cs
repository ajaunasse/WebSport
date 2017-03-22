using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL.EntityFramework;
using DAL.Extensions;

namespace BLL
{
    public class MgtResultat
    {

        private static MgtResultat _instance;

        public static MgtResultat GetInstance()
        {
            if (_instance == null)
                _instance = new MgtResultat();
            return _instance;
        }

        private WebSportEntities _context { get; set; }

        public MgtResultat()
        {
            _context = new WebSportEntities();
        }


        public List<Resultat> GetResultatsById(int userId)
        {
            List<Resultat> results = new List<Resultat>();
            List<ResultatEntity> entities = _context.ResultatEntities.Where(r => r.PersonneId == userId).ToList();

            results = entities.Select(x => x.ToBo()).ToList();

            return results;
        } 


    }
}

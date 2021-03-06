﻿using System;
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


        public bool Save(List<Resultat> models)
        {
            try
            {
                _context = new WebSportEntities();

                foreach (Resultat resultat in models)
                {
                    
                    _context.ResultatEntities.Add(resultat.ToEntity());
                }

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
            
            
        }

        public List<Resultat> GetResultatsByIdRace(int idrace)
        {
            _context = new WebSportEntities();
            List < ResultatEntity > liste = _context.ResultatEntities.Where(x => x.CourseId == idrace).OrderBy(x =>x.Classement).ToList();
            return liste.Select(x => x.ToBo()).ToList();
        }

        public List<int> GetResultats()
        {
            _context = new WebSportEntities();
            List<int> idRaces = _context.ResultatEntities.Select(x => x.CourseId).Distinct().ToList();
            return idRaces;
        }
    }
}

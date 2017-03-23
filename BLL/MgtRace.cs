using BO;
using DAL;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EntityFramework;
using DAL.Extensions;


namespace BLL
{
    public class MgtRace
    {

        private static MgtRace _instance;

        public static MgtRace GetInstance()
        {
            _instance = new MgtRace();
            return _instance;
        }

        private UnitOfWork _uow { get; set; }

        public MgtRace()
        {
            _uow = new UnitOfWork();                        
        }

        public bool AddRace(Race race)
        {
            if (race != null)
            {

                int lastId = this._uow.RaceRepo.Add(race);
                if (lastId > 0)
                {
                    race.Id = lastId;
                }
                return true;
            }

            return false;
        }

        public bool AddPoint(Point point)
        {
            if(point != null)
            {
                int lastId = this._uow.PointRepo.Add(point);
                if(lastId > 0)
                {
                    point.Id = lastId;
                }
                return true;
            }

            return false;
        }

        public List<Race> GetAllItems(bool withJoins = false)
        {
            return this._uow.RaceRepo.GetAllItems(withJoins);
        }

        public List<Category> getAllCategory()
        {
            return this._uow.RaceRepo.getAllCategory();
        }


        public Race GetRace(int id)
        {
            return this._uow.RaceRepo.GetById(id);
        }

        public List<Race> getRacesBySearch(string ville, DateTime? dateDebut, DateTime? dateFin)
        {
            List<Race> valRet;
            var repo = this._uow.RaceRepo.GetAll();
            if(ville != null)
            {
                repo = repo.Where(r => r.Town == ville).ToList();
            }
            if(dateDebut != null)
            {
                repo = repo.Where(r => r.DateStart >= dateDebut).ToList();
            }
            if(dateFin != null)
            {
                repo = repo.Where(r => r.DateStart <= dateFin).ToList();
            }
            valRet = repo.ToBos();
            return valRet;
        }



        public bool UpdateRace(Race race)
        {
            if (race == null || race.Id < 1) return false;

            this._uow.RaceRepo.Update(race);
            this._uow.Save();
            return true;
        }

        public bool RemoveRace(int id)
        {
            if (id < 1) return false;

            this._uow.RaceRepo.Remove(id);
            this._uow.Save();

            return true;
        }

        public List<Race> SuscribeRace(Personne user, int idrace)
        {
            
            WebSportEntities context = new WebSportEntities();
            ContributorEntity join = new ContributorEntity();
            join.RaceId = idrace;
            join.PersonId = user.Id;
            if (user.Role == 1)
            {
                join.IsOrganiser = true;
            }
            else
            {
                join.IsCompetitor = true;
            }

            context.ContributorEntities.Add(join);
            context.SaveChanges();

            List<int> idRaces = context.ContributorEntities.Where(c => c.PersonId == user.Id).Select(c => c.RaceId).ToList();
            List<RaceEntity> races = new List<RaceEntity>();
            foreach (int race in idRaces)
            {
                races.Add(context.RaceEntities.Single(r => r.Id == race));
            }

            return races.ToBos();
        }


        public List<Race> MyRaces(int idUser)
        {
            WebSportEntities context = new WebSportEntities();
            List<int> idRaces = context.ContributorEntities.Where(c => c.PersonId == idUser).Select(c => c.RaceId).ToList();
            if (idRaces == null)
            {
                return null;
            }
            List<RaceEntity> races = new List<RaceEntity>();
            foreach (int race in idRaces)
            {
                races.Add(context.RaceEntities.Single(r => r.Id == race));
            }

            return races.ToBos();
        }

        public List<Race> Unsubscribe(int idUser, int idRace)
        {
            WebSportEntities context = new WebSportEntities();
            ContributorEntity join = context.ContributorEntities.Single(j => j.PersonId == idUser && j.RaceId == idRace);
            context.ContributorEntities.Remove(join);
            context.SaveChanges();
            List<int> idRaces = context.ContributorEntities.Where(c => c.PersonId == idUser).Select(c => c.RaceId).ToList();
            List<RaceEntity> races = new List<RaceEntity>();
            foreach (int race in idRaces)
            {
                races.Add(context.RaceEntities.Single(r => r.Id == race));
            }

            return races.ToBos();
        }

        public List<Race> getRacebyUser(int idUser)
        {
            WebSportEntities context = new WebSportEntities();
            List<Race> races = GetAllItems();
            List<int> idRaceUser =
                context.ContributorEntities.Where(c => c.PersonId == idUser).Select(c => c.RaceId).ToList();
            foreach (int id in idRaceUser)
            {
                Race r = races.Find(c => c.Id == id);
                races.Remove(r);
            }

            return races;
        }
    }
}
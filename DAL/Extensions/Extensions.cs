using BO;
using DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Extensions
{
    public static class Extensions
    {
        #region Race

        public static List<Race> ToBos(this List<RaceEntity> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToBo(withJoin)).ToList()
                : null;
        }

        public static Race ToBo(this RaceEntity bo, bool withJoin = false)
        {
            if (bo == null) return null;

            return new Race
            {
                Id = bo.Id,
                Title = bo.Title,
                Description = bo.Description,
                DateStart = bo.DateStart,
                DateEnd = bo.DateEnd,
                Town = bo.Town,
                Points = bo.Points.ToList().ToBos(),

                Organisers = withJoin && bo.Contributors != null ? bo.Contributors.Where(x => x.IsOrganiser).Select(x => x.ToOrganiserBo()).ToList() : null,
                Competitors = withJoin && bo.Contributors != null ? bo.Contributors.Where(x => x.IsCompetitor).Select(x => x.ToCompetitorBo()).ToList() : null
            };
        }

        public static RaceEntity ToDataEntity(this Race model)
        {
            if (model == null) return null;

            return new RaceEntity
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                DateStart = model.DateStart,
                DateEnd = model.DateEnd,
                Town = model.Town,
            };
        }


        public static Race ToBo(this GetRaceById_Result entity)
        {
            if (entity == null) return null;

            return new Race
            {
                Id = entity.CId,
                Title = entity.CTitre,
                Description = entity.CDescription,
                DateStart = entity.CDateStart,
                DateEnd = entity.CDateEnd,
                Town = entity.CVille
            };
        }

        #endregion

        #region Point

        public static List<Point> ToBos(this List<PointEntity> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToBo(withJoin)).ToList()
                : null;
        }

        public static Point ToBo(this PointEntity bo, bool withJoin = false)
        {
            if (bo == null) return null;

            return new Point
            {
                Id = bo.Id,
                Longitude = bo.Longitude,
                Latitude = bo.Latitude,
                Altitude = bo.Altitude

            };
        }

        public static PointEntity ToDataEntity(this Point model)
        {
            if (model == null) return null;

            return new PointEntity
            {
                Id = model.Id,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                Altitude = model.Altitude
            };
        }


        //public static Point ToBo(this GetPointById_Result entity)
        //{
        //    if (entity == null) return null;

        //    return new Point
        //    {
        //        Longitude = entity.Longitude,
        //        Latitude = entity.Latitude,
        //        Altitude = entity.Altitude
        //    };
        //}

        #endregion

        #region Competitor

        public static List<Competitor> ToCompetitorBos(this List<ContributorEntity> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToCompetitorBo()).ToList()
                : null;
        }

        public static Competitor ToCompetitorBo(this ContributorEntity bo)
        {
            if (bo == null) return null;

            return new Competitor
            {
                Id = bo.PersonId,
                Nom = bo.Person.Lastname,
                Prenom = bo.Person.Firstname,
                DateNaissance = bo.Person.BirthDate.HasValue ? bo.Person.BirthDate.Value : DateTime.MinValue,
                Email = bo.Person.Mail,
                Phone = bo.Person.Phone,
                Race = bo.Race.ToBo()
            };
        }

        #endregion

        #region Organizer

        public static List<Organizer> ToOrganiserBos(this List<ContributorEntity> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToOrganiserBo()).ToList()
                : null;
        }

        public static Organizer ToOrganiserBo(this ContributorEntity bo)
        {
            if (bo == null) return null;

            return new Organizer
            {
                Id = bo.PersonId,
                Nom = bo.Person.Lastname,
                Prenom = bo.Person.Firstname,
                DateNaissance = bo.Person.BirthDate.HasValue ? bo.Person.BirthDate.Value : DateTime.MinValue,
                Email = bo.Person.Mail,
                Phone = bo.Person.Phone
            };
        }

        #endregion

        public static Resultat ToBo(this ResultatEntity bo, bool withJoin = false)
        {
            if (bo == null) return null;

            Resultat result = new Resultat();
            result.Race = bo.Course.ToBo();
            result.PersonneID = bo.PersonneId;
            if (bo.HeureArrive != null) result.HeureArrivee = (TimeSpan)bo.HeureArrive;
            if (bo.HeureDepart != null) result.HeureDebut = (TimeSpan)bo.HeureDepart;

            return result;
        }


        #region Personne
        public static List<Personne> ToBos(this List<PersonEntity> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToBo(withJoin)).ToList()
                : null;
        }


        public static Personne ToBo(this PersonEntity bo, bool withJoin = false)
        {
            if (bo == null) return null;

            return new Personne
            {
                Id = bo.Id,
                Prenom = bo.Firstname,
                Nom = bo.Lastname,
                DateNaissance = bo.BirthDate,
                Email = bo.Mail,
                Phone = bo.Phone,
            };
        }


        #endregion
    }

}
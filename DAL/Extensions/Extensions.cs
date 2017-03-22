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
                Pois = bo.POIs.ToList().ToBos(),

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
                Altitude = model.Altitude,
            };
        }

        #endregion

        #region poi

        public static POIEntity ToDataEntity(this Poi model, WebSportEntities context)
        {
            if (model == null) return null;

            POIEntity poiEnt = new POIEntity();
            PointEntity newPoint = new PointEntity();

            newPoint.Latitude = model.Latitude;
            newPoint.Longitude = model.Longitude;

            poiEnt.Id = model.Id;
            poiEnt.Point = newPoint;
            poiEnt.Titre = model.Title;
            try
            {
                poiEnt.Categorie = context.CategorieEntities.Where(x => x.Id == model.idCategory).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }

            return poiEnt;
        }

        public static Poi ToBo(this POIEntity bo, bool withJoin = false)
        {
            if (bo == null) return null;

            return new Poi
            {
                Id = bo.Id,
                Title = bo.Titre,
                Latitude = bo.Point.Latitude,
                Longitude = bo.Point.Longitude,
                idPoint = bo.Point.Id
            };
        }

        public static List<Poi> ToBos(this List<POIEntity> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToBo(withJoin)).ToList()
                : null;
        }

        #endregion

        #region category

        public static List<Category> ToBos(this List<CategorieEntity> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToBo(withJoin)).ToList()
                : null;
        }

        public static Category ToBo(this CategorieEntity bo, bool withJoin = false)
        {
            if (bo == null) return null;

            return new Category
            {
                Id = bo.Id,
                Title = bo.Titre
            };
        }
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
    }
}

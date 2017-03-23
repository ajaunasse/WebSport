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

            Race race = new Race
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
            if(bo.Distance == null)
            {
                race.Distance = 0;
            } else
            {
                race.Distance = bo.Distance;
            }
            return race;
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
                Distance = model.Distance
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
                Town = entity.CVille,
                Distance = (int)entity.CDistance
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
                Altitude = bo.Altitude,
                categorie = bo.Categorie.ToBo(),
                isPoi = (bool) bo.isPoi,
                titre = bo.title,

            };
        }

        public static PointEntity ToDataEntity(this Point model)
        {
            if (model == null) return null;



            PointEntity pts =  new PointEntity
            {
                Id = model.Id,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                Altitude = model.Altitude,
                isPoi = model.isPoi,
                title = model.titre,

            };
            if(model.categorie != null)
            {
                pts.CategorieId = model.categorie.Id;
            }

            return pts;
        }

        #endregion

        #region poi

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

        public static CategorieEntity ToDataEntity(this Category model)
        {
            if (model == null) return null;

            return new CategorieEntity
            {
                Id = model.Id,
                Titre = model.Title,
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

        public static Resultat ToBo(this ResultatEntity bo, bool withJoin = false)
        {
            if (bo == null) return null;

            Resultat result = new Resultat();
            result.Race = bo.Course.ToBo();
            result.PersonneID = bo.PersonneId;
            if (bo.HeureArrive != null) result.HeureArrivee = (TimeSpan)bo.HeureArrive;
            if (bo.HeureDepart != null) result.HeureDebut = (TimeSpan)bo.HeureDepart;
            result.TempsDeCourse = result.HeureArrivee - result.HeureDebut;

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
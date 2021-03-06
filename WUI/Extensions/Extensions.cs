﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WUI.Models;

namespace WUI.Extensions
{
    public static class Extensions
    {
        #region Category

        public static List<CategoryModel> ToModels(this List<Category> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static CategoryModel ToModel(this Category bo)
        {
            if (bo == null) return null;

            return new CategoryModel
            {
                Id = bo.Id,
                Title = bo.Title
            };
        }

        #endregion

        #region resultat

        public static ResultatModel ToModel(this Resultat bo)
        {
            if (bo == null) return null;

            return new ResultatModel
            {
                Personne = bo.Personne.ToModel(),
                Race = bo.Race.ToModel(),
                Classement = bo.Classement,
                TempsDeCourse = bo.TempsDeCourse,
                HeureArrivee = bo.HeureArrivee,
                HeureDebut = bo.HeureDebut,
            };
        }

        public static Resultat ToBo(this ResultatModel model)
        {
            if (model == null) return null;
            Resultat newResultat = new Resultat();
            newResultat.Personne = model.Personne.ToBo();
            newResultat.Race = model.Race.ToBo();
            newResultat.Classement = model.Classement;
            newResultat.TempsDeCourse = model.TempsDeCourse;
            newResultat.HeureArrivee = model.HeureArrivee;
            newResultat.HeureDebut = model.HeureDebut;

            return newResultat;
        }


        #endregion

        #region Competitor

        public static List<CompetitorModel> ToModels(this List<Competitor> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static CompetitorModel ToModel(this Competitor bo)
        {
            if (bo == null) return null;

            return new CompetitorModel
            {
                Id = bo.Id,
                Nom = bo.Nom,
                Prenom = bo.Prenom,
                DateNaissance = bo.DateNaissance,
                Email = bo.Email,
                Phone = bo.Phone,
                Race = bo.Race.ToModel(),
                DisplayConfigurations = bo.DisplayConfigurations.Select(x => x.ToModel()).ToList()
            };
        }

        #endregion

        #region Race

        public static List<RaceModel> ToModels(this List<Race> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel(withJoin)).ToList()
                : null;
        }

        public static RaceModel ToModel(this Race bo, bool withJoin = false)
        {
            if (bo == null) return null;

            RaceModel raceModel = new RaceModel();

            raceModel.Id = bo.Id;
            raceModel.Title = bo.Title;
            raceModel.Description = bo.Description;
            raceModel.DateStart = bo.DateStart;
            raceModel.DateEnd = bo.DateEnd;
            raceModel.Town = bo.Town;
            raceModel.Distance = (int) bo.Distance;
            raceModel.Points = bo.Points.ToModels();

            raceModel.Organisers = withJoin && bo.Organisers != null ? bo.Organisers.Select(x => x.ToModel()).ToList() : null;
            raceModel.Competitors = withJoin && bo.Competitors != null ? bo.Competitors.Select(x => x.ToModel()).ToList() : null;



            return raceModel;
        }

        public static Race ToBo(this RaceModel model)
        {
            if (model == null) return null;

            Race newRace = new Race();

            newRace.Id = model.Id;
            newRace.Title = model.Title;
            newRace.Description = model.Description;
            newRace.DateStart = model.DateStart;
            newRace.DateEnd = model.DateEnd;
            newRace.Town = model.Town;
            if (model.Points != null)

                newRace.Distance = model.Distance;
            if(model.Points != null)

            {
                newRace.Points = model.Points.Select(x => x.ToBo()).ToList();
            }



            return newRace;
        }

        #endregion

        #region Point

        public static List<PointModel> ToModels(this List<Point> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel(withJoin)).ToList()
                : null;
        }

        public static PointModel ToModel(this Point bo, bool withJoin = false)
        {
            if (bo == null) return null;

            return new PointModel
            {
                Id = bo.Id,
                Latitude = bo.Latitude,
                Longitude = bo.Longitude,
                Altitude = bo.Altitude,
                titre = bo.titre,
                isPoi = bo.isPoi,
                Category = bo.categorie,
            };
        }

        public static Point ToBo(this PointModel model)
        {
            if (model == null) return null;

            Point pts = new Point
            {
                Id = model.Id,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Altitude = model.Altitude,
                titre = model.titre,
                isPoi = model.isPoi,
            };

            if (model.Category != null)
            {
                pts.categorie = model.Category;
            }

            return pts;
        }

        #endregion

        #region poi
        #endregion

        #region DisplayConfiguration

        public static List<DisplayConfigurationModel> ToModels(this List<DisplayConfiguration> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static DisplayConfigurationModel ToModel(this DisplayConfiguration bo)
        {
            if (bo == null) return null;

            return new DisplayConfigurationModel
            {
                Id = bo.Id,
                PersonneId = bo.PersonneId,
                DeviceName = bo.DeviceName,
                SpeedAvg = bo.SpeedAvg,
                SpeedMax = bo.SpeedMax,
                UnitDistance = bo.UnitDistance.ToModel(),

                Person = bo.Person.ToModel()
            };
        }

        #endregion

        public static List<PersonneModel> ToModels(this List<Personne> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }
        public static PersonneModel ToModel(this Personne bo)
        {
            if (bo == null) return null;

            return new PersonneModel
            {
                Id = bo.Id,
                Nom = bo.Nom,
                Prenom = bo.Prenom,
                DateNaissance = bo.DateNaissance,
                Email = bo.Email,
                Phone = bo.Phone,
                Role = bo.Role,
                Password = bo.Password,
                //DisplayConfigurations = bo.DisplayConfigurations.Select(x => x.ToModel()).ToList()
            };
        }

        public static Personne ToBo(this PersonneModel bo)
        {
            if (bo == null) return null;

            return new Personne
            {
                Id = bo.Id,
                Nom = bo.Nom,
                Prenom = bo.Prenom,
                DateNaissance = bo.DateNaissance,
                Email = bo.Email,
                Phone = bo.Phone,
                Password = bo.Password,
                Role = bo.Role,

                //DisplayConfigurations = bo.DisplayConfigurations.Select(x => x.ToModel()).ToList()
            };
        }

        public static OrganizerModel ToModel(this Organizer bo)
        {
            if (bo == null) return null;

            return new OrganizerModel
            {
                Id = bo.Id,
                Nom = bo.Nom,
                Prenom = bo.Prenom,
                DateNaissance = bo.DateNaissance,
                Email = bo.Email,
                Phone = bo.Phone,
                DisplayConfigurations = bo.DisplayConfigurations.Select(x => x.ToModel()).ToList()
            };
        }

        public static UnitDistanceModel ToModel(this UnitDistance bo)
        {
            UnitDistanceModel result;

            switch (bo)
            {
                case UnitDistance.Miles:
                    result = UnitDistanceModel.Miles;
                    break;

                default:
                case UnitDistance.Kilometers:
                    result = UnitDistanceModel.Kilometers;
                    break;
            }

            return result;
        }
    }
}
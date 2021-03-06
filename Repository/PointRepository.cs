﻿using BO;
using DAL.EntityFramework;
using DAL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Classe contenant les méthodes spécifiques aux "Point"
    /// </summary>
    public class PointRepository : GenericRepository<PointEntity>
    {
        #region Constructor

        public PointRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        #region Public methods

        public int Add(Point element)
        {
            try
            {
                var result = base.Add(element.ToDataEntity());
                return result.Id;
            }
            catch
            {
                return 0;
            }
        }

        public Point GetById(int id)
        {
            var point = this.GetByIdPrivate(id);
            return point != null ? point.ToBo() : null;
        }

        private PointEntity GetByIdPrivate(int id)
        {
            return base.Where(x => x.Id == id).SingleOrDefault();
        }

        public void Update(Point element)
        {
            var pointToUpdate = this.GetByIdPrivate(element.Id);
            pointToUpdate.Altitude = element.Altitude;
            pointToUpdate.Latitude = element.Latitude;
            pointToUpdate.Longitude = element.Longitude;
            base.Update(pointToUpdate);
        }

        public void Remove(int id)
        {
            var pointToDelete = this.GetByIdPrivate(id);
            base.Remove(pointToDelete);
        }

        public List<Point> GetAllItems()
        {
            return base.GetAll().ToBos();
        }

        public Task<List<Point>> GetAllItemsAsync()
        {
            return Task.Run(() => base.GetAll().AsParallel().ToList().ToBos());
        }

        #endregion
    }
}

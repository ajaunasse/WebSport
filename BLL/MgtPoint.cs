using BO;
using DAL;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Extensions;
using DAL.EntityFramework;

namespace BLL
{
    public class MgtPoint
    {
        private static MgtPoint _instance;

        public static MgtPoint GetInstance()
        {
            if (_instance == null)
                _instance = new MgtPoint();
            return _instance;
        }

        private UnitOfWork _uow { get; set; }

        private MgtPoint()
        {
            _uow = new UnitOfWork();
        }

        public Point AddPoint(Point point)
        {
            try
            {
                PointEntity entity = EntityToBo(point);
                WebSportEntities context = new WebSportEntities();
                context.PointEntities.Add(entity);
                context.SaveChanges();

                return point;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Point> GetAllItems()
        {
            return this._uow.PointRepo.GetAllItems();
        }

        private PointEntity EntityToBo(Point point)
        {
            PointEntity bo = new PointEntity();
            bo.Altitude = point.Altitude;
            bo.Latitude = point.Latitude;
            bo.Longitude = point.Longitude;

            return bo;
        }
    }
}

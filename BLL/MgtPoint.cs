using BO;
using DAL;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Extensions;

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

        public bool AddPoint(Point point)
        {
            if (point != null)
            {
                int lastId = this._uow.PointRepo.Add
            }
        }
    }
}

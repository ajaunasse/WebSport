using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL.EntityFramework;

namespace BLL
{
    public class MgtPersonne
    {

        private static MgtPersonne _instance;

        public static MgtPersonne GetInstance()
        {
            if (_instance == null)
                _instance = new MgtPersonne();
            return _instance;
        }
        public bool ValidateConnection(Personne pers)
        {
            WebSportEntities context = new WebSportEntities();

            PersonEntity entity = context.PersonEntities.Single(p =>p.Mail == pers.Email);

            if (entity.Password == pers.Password)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        // CREATE

        // UPDATE

        // DELETE
    }
}

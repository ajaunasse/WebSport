using BO;
using DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Extensions;


namespace Repository
{
    /// <summary>
    /// Classe contenant les méthodes spécifiques aux "Race"
    /// </summary>
    public class PersonneRepository : GenericRepository<PersonEntity>
    {
        #region Constructor

        public PersonneRepository(WebSportEntities context)
            : base(context)
        {
        }


        public List<Personne> GetAllItems()
        {
            return base.GetAll().ToBos();
        }
        #endregion
    }
}

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

        public List<Personne> GetByTranche(DateTime? date1, DateTime? date2)
        {
            var test = base.GetAll().Where(x => x.BirthDate >= date1 && x.BirthDate <= date2).ToString(); 
            var valRet = base.GetAll().ToList();
            if (date1 != null)
            {
                valRet = valRet
                    .Where(x => x.BirthDate <= date1)
                    .ToList();
            }
            if(date2 != null)
            {
                valRet = valRet.Where(x => x.BirthDate >= date2).ToList();
            }

            return valRet.ToBos();
        }
        #endregion
    }
}

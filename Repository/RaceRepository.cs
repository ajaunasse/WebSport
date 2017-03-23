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
    public class RaceRepository : GenericRepository<RaceEntity>
    {
        #region Constructor

        public RaceRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        #region Public methods

        public int Add(Race element)
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

        public Race GetById(int id)
        {
            var race = this.GetByIdPrivate(id);
            return race != null ? race.ToBo(true) : null;
        }

        private RaceEntity GetByIdPrivate(int id)
        {
            return base.Where(x => x.Id == id).SingleOrDefault();
        }

        public void Update(Race element)
        {
            RaceEntity race = context.RaceEntities.FirstOrDefault(x => x.Id == element.Id);
            var raceToUpdate = this.GetByIdPrivate(element.Id);
            raceToUpdate.Title = element.Title;
            raceToUpdate.Description = element.Description;
            raceToUpdate.DateStart = element.DateStart;
            raceToUpdate.DateEnd = element.DateEnd;
            raceToUpdate.Distance = element.Distance;
            raceToUpdate.Town = element.Town;
            if (element.Points != null)
            {
                raceToUpdate.Points.Add(element.Points.Select(x => x.ToDataEntity()).First());
            }
            base.Update(raceToUpdate);
        }

        public void Remove(int id)
        {
            var raceToDelete = this.GetByIdPrivate(id);
            base.Remove(raceToDelete);
        }

        public List<Race> GetAllItems(bool withJoins = false)
        {
            return base.GetAll().ToBos(withJoins);
        }

        public List<Category> getAllCategory()
        {
            List<Category> cats = context.CategorieEntities.ToList().ToBos();
            return cats;
        }

        public Task<List<Race>> GetAllItemsAsync()
        {
            return Task.Run(() => base.GetAll().AsParallel().ToList().ToBos());
        }

        #endregion
    }
}

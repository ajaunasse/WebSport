using DAL.EntityFramework;
using System;


namespace Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed;
        private WebSportEntities context;

        #region Constructors

        public UnitOfWork()
            : this(new WebSportEntities())
        {
        }

        public UnitOfWork(WebSportEntities context)
        {
            this.context = context;
        }

        #endregion

        #region Repositories

        private RaceRepository _raceRepo;
        public RaceRepository RaceRepo
        {
            get
            {
                if (_raceRepo == null)
                    _raceRepo = new RaceRepository(this.context);
                return _raceRepo;
            }
        }

        private PointRepository _pointRepo;
        public PointRepository PointRepo
        {
            get
            {
                if (_pointRepo == null)
                    _pointRepo = new PointRepository(this.context);
                return _pointRepo;
            }
        }


        private PersonneRepository _personneRepo;
        public PersonneRepository PersonneRepo
        {
            get
            {
                if (_personneRepo == null)
                    _personneRepo = new PersonneRepository(this.context);
                return _personneRepo;
            }
        }

        private GenericRepository<ContributorEntity> _contributorRepo;
        public GenericRepository<ContributorEntity> ContributorRepo
        {
            get
            {
                if (_contributorRepo == null)
                    _contributorRepo = new GenericRepository<ContributorEntity>(this.context);
                return _contributorRepo;
            }
        }

        // Etc... on liste les repositories

        #endregion

        #region Public methods

        public bool Save()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose the instance
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the instance if it's not already
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        #endregion
    }
}

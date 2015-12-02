using System;
using System.Linq;
using System.Data.Entity;
using EYM.EntityFramework.Interfaces;
using EYM.Repositories.Interfaces;
using System.Linq.Expressions;

namespace EYM.EntityFramework
{
    public  class EntityFrameworkRepository<TEntity> : IGenericDataRepository<TEntity>
        where TEntity : class, IEntity
    {
        private IContext _context;
        private bool _disposed = false;

        public IContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

	    public EntityFrameworkRepository(IContext context)
	    {
		    _context = context;
	    }

        public virtual IQueryable<TEntity> GetAll()
        {
            var query = _context.Set<TEntity>();
            return query;
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _context.Set<TEntity>().Where(predicate);
            return query;
        }

        public virtual TEntity Get(int id)
        {
            var query = _context.Set<TEntity>().Find(id);
            return query;
        }

        public virtual bool Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return true;
        }

        public virtual bool Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return true;
        }

        public virtual void Delete(TEntity entity)
        {

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
                if (disposing)
                    _context.Dispose();

            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

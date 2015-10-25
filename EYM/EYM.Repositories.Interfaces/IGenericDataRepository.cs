using System;
using System.Linq;
using System.Linq.Expressions;

namespace EYM.Repositories.Interfaces
{
    public interface IGenericDataRepository<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int key);
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        void Delete(TEntity entity);
    }
}

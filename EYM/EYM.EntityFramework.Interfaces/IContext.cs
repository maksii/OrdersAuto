using System;
using System.Data.Entity;
using EYM.Repositories.Interfaces;

namespace EYM.EntityFramework.Interfaces
{
    public interface IContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;
	    void Update(IEntity entity);
        int SaveChanges();
    }
}

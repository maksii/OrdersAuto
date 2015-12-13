using System.Linq;
using EYM.Repositories.Interfaces;
using EYM.DBServices.Interfaces;
using System;

namespace EYM.DBServices
{
	internal class DBService<TEntity> : IDBService<TEntity>
		where TEntity : class, IEntity
	{
		private readonly IGenericDataRepository<TEntity> _rep;

		public DBService(IGenericDataRepository<TEntity> rep)
		{
			_rep = rep;
		}

		public IQueryable<TEntity> GetAll()
		{
			return _rep.GetAll();
		}

		public bool Add(TEntity entity)
		{
			return _rep.Add(entity);
		}


		public TEntity Get(int key)
		{
			return _rep.Get(key);
		}

		public bool Update(TEntity entity)
		{
			return _rep.Update(entity);
		}

		public void Delete(TEntity entity)
		{
			_rep.Delete(entity);
		}
	}
}
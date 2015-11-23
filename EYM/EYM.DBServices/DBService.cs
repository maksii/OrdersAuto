using System.Linq;
using EYM.Repositories.Interfaces;
using EYM.DBServices.Interfaces;
using System;

namespace EYM.DBServices
{
	internal class DBService<TEntity> : IDBService<TEntity>
		where TEntity : class, IEntity
	{
		private IGenericDataRepository<TEntity> _rep;

		public DBService(IGenericDataRepository<TEntity> rep)
		{
			_rep = rep;
		}

		public IQueryable<TEntity> Serve()
		{
			return _rep.GetAll();
		}
	}
}
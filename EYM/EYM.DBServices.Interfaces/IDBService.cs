using System.Linq;

namespace EYM.DBServices.Interfaces
{
	public interface IDBService <TEntity>
		
	{
		IQueryable<TEntity> GetAll();
		TEntity Get(int key);
		bool Add(TEntity entity);
		bool Update(TEntity entity);
		void Delete(TEntity entity);
	}
}

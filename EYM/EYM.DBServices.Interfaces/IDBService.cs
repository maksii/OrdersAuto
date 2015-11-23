using System.Linq;

namespace EYM.DBServices.Interfaces
{
	public interface IDBService <TEntity>
		
	{
		IQueryable<TEntity> Serve();
	}
}

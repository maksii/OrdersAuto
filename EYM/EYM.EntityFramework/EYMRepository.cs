using EYM.EntityFramework.Interfaces;
using EYM.Repositories.Interfaces;

namespace EYM.EntityFramework
{
    public class EYMRepository<TEntity>: EntityFrameworkRepository<TEntity>
        where TEntity: class, IEntity
    {
	    public EYMRepository(IContext context) : base(context)
	    {
	    }
    }
}

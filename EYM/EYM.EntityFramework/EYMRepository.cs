using EYM.EntityFramework.Interfaces;
using EYM.Repositories.Interfaces;

namespace EYM.EntityFramework
{
    class EYMRepository<TEntity>: EntityFrameworkRepository<IEntity>
        where TEntity: class, IEntity
    {
	    public EYMRepository(IContext context) : base(context)
	    {
	    }
    }
}

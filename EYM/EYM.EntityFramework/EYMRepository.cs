using EYM.Repositories.Interfaces;

namespace EYM.EntityFramework
{
    class EYMRepository<TEntity>: EntityFrameworkRepository<EYMContext, IEntity>
        where TEntity: class, IEntity
    {
    }
}

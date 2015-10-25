using EYM.EntityFramework.Interfaces;
using EYM.Repositories.Interfaces;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EYM.EntityFramework
{
    public abstract class Context : DbContext, IContext
    {
        protected Context() { }

        protected Context(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }
    }
}
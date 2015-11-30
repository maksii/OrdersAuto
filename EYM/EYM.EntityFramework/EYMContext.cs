using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EYM.Entities;

namespace EYM.EntityFramework
{
    public class EYMContext : Context
    {
        public EYMContext() : base()
        {
            
        }

        public EYMContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }
        public DbSet<Role> Rols { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTemplate> ProductTemplates { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Provider> Providers { get; set; } 	

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

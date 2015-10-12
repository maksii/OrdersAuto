using System.Data.Entity;

namespace EYM.Entities
{
	public class DinnerContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserBalance> UserBalances { get; set; }
		public DbSet<Role> Rols { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderLine> OrderLines { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductTemplate> ProductTemplates { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<Provider> Providers { get; set; } 	
	}
}

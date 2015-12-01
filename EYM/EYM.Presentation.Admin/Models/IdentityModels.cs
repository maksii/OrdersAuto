using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using EYM.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EYM.Presentation.Admin.Models
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext()
			: base("DefaultConnection")
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		public DbSet<Provider> Providers
		{
			get; set;
		}

		public new DbSet<User> Users
		{
			get; set;
		}
	}
}
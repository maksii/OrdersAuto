using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using EYM.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EYM.Entities
{
	public class User : IUser<int>, IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }

		public int RoleId { get; set; }

		[ForeignKey("RoleId")]
		public virtual Role Role { get; set; }

		public virtual List<UserBalance> UserBalances { get; set; }
		public virtual List<Order> Orders { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
		{
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			userIdentity.AddClaim(new Claim("UserFullName", FullName));

			return userIdentity;
		}

		[NotMapped]
		public string FullName => $"{FirstName} {LastName}";
	}
}

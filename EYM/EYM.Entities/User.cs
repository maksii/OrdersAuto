using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EYM.Repositories.Interfaces;

namespace EYM.Entities
{
    public class User : IEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }

		public int RoleId { get; set; }

		[ForeignKey("UserId")]
		public virtual Role Role { get; set; }

		public virtual List<UserBalance> UserBalances { get; set; }
		public virtual List<Order> Orders { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EYM.Repositories.Interfaces;
using Microsoft.AspNet.Identity;

namespace EYM.Entities
{
	public class Role : IRole<int>, IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual List<User> Users { get; set; }
	}
}

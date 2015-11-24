using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EYM.Repositories.Interfaces;

namespace EYM.Entities
{
	public class Category : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual List<Product> Products { get; set; }
	}
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EYM.Entities
{
	public class ProductType
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductTypeId { get; set; }
		public string Name { get; set; }

		public virtual List<ProductTemplate> ProductTemplates { get; set; }
	}
}

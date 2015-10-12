using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EYM.Entities
{
	public class ProductTemplate
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductTemplateId { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public string Description { get; set; }
		public string ImagePath { get; set; }

		public int ProviderId { get; set; }
		public int ProductTypeId { get; set; }

		[ForeignKey("ProviderId")]
		public virtual Provider Provider { get; set; }
		[ForeignKey("ProductTypeId")]

		public virtual ProductType ProductType { get; set; }
		public virtual List<Product> Products { get; set; }
	}
}

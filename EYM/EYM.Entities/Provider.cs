using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EYM.Entities
{
	public class Provider
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProviderId { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string ImagePath { get; set; }
		public string Description { get; set; }
		public string AdditionalInfo { get; set; }
		public string Type { get; set; }

		public virtual List<ProductTemplate> ProductTemplates { get; set; }
	}
}

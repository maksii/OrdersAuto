using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EYM.Repositories.Interfaces;

namespace EYM.Entities
{
	public class Product : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public double Price { get; set; }
		public DateTime DateToOrder { get; set; }

		public int ProductTemplateId { get; set; }

		[ForeignKey("ProductTemplateId")]
		public virtual ProductTemplate ProductTemplate { get; set; }
		public virtual List<OrderLine> OrderLines { get; set; }
	}
}

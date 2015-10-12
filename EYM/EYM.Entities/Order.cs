using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EYM.Entities
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderId { get; set; }
		public DateTime Date { get; set; }


		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		public virtual List<OrderLine> OrderLines { get; set; }
	}
}

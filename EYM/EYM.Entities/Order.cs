using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EYM.Repositories.Interfaces;

namespace EYM.Entities
{
	public class Order : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Date { get; set; }


		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		public virtual List<OrderLine> OrderLines { get; set; }
	}
}

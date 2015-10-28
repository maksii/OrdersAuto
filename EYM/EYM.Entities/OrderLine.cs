using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EYM.Repositories.Interfaces;

namespace EYM.Entities
{
	public class OrderLine : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int Quantity { get; set; }
		public string Comment { get; set; }

		public int ProductId { get; set; }
		public int OrderId { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }
	}
}

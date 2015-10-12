using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EYM.Entities
{
	public class OrderLine
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderLineId { get; set; }
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

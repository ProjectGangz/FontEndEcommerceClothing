using System.ComponentModel.DataAnnotations;

namespace WebEcommerceClothing.Models
{
	public class CategoryModel
	{
		[Key]
		public int CategoryID { get; set; }

		[Required]
		[MaxLength(100)]
		public string CategoryName { get; set; }

		public string CategoryDescription { get; set; }

		// Navigation property
		public virtual ICollection<ProductModel> Products { get; set; }
	}
}

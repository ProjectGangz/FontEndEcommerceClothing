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
        [Required]
        public string CategoryDescription { get; set; }

	}
}

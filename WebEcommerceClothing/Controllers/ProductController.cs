using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebEcommerceClothing.Models;

namespace WebEcommerceClothing.Controllers
{
    public class ProductController : Controller
    {
		private readonly IConfiguration _configuration;
		public ProductController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public IActionResult ProductShop()
		{
			string connectionString = _configuration.GetConnectionString("MyConnectionString");
			List<ProductModel> products = new List<ProductModel>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string query = "SELECT * FROM Product";
				SqlCommand command = new SqlCommand(query, connection);
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					ProductModel product = new ProductModel
					{
						ProductID = (int)reader["ProductID"],
						CategoryID = (int)reader["CategoryID"],
						ProductName = reader["ProductName"].ToString(),
						ProductDescription = reader["ProductDescription"].ToString(),
						Price = (decimal)reader["Price"],
						Size = reader["Size"].ToString(),
						Color = reader["Color"].ToString(),
						ProductImage = reader["ProductImage"].ToString(),
						Quantity = (int)reader["Quantity"]
						// Không cần navigation property (Category) trong trường hợp này
					};

					products.Add(product);
				}

				reader.Close();
			}

			return View(products);
		}
		public IActionResult ProductDetails() 
        { 
            return View();
        }
    }
}

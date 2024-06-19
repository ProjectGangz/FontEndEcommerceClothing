using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebEcommerceClothing.Models;

namespace WebEcommerceClothing.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IConfiguration _configuration;

        public CategoriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("MyConnectionString");
            List<CategoryModel> categories = new List<CategoryModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Category";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoryModel category = new CategoryModel
                    {
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader["CategoryName"].ToString(),
                        CategoryDescription = reader["CategoryDescription"].ToString()
                    };

                    categories.Add(category);
                }

                reader.Close();
            }

            return View(categories);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryModel category = GetCategoryById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {

                string connectionString = _configuration.GetConnectionString("MyConnectionString");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Category (CategoryName, CategoryDescription) VALUES (@Name, @Description)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", category.CategoryName);
                    command.Parameters.AddWithValue("@Description", category.CategoryDescription);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryModel category = GetCategoryById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryModel category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("MyConnectionString");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Category SET CategoryName = @Name, CategoryDescription = @Description WHERE CategoryID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", category.CategoryName);
                    command.Parameters.AddWithValue("@Description", category.CategoryDescription);
                    command.Parameters.AddWithValue("@ID", category.CategoryID);
                    command.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryModel category = GetCategoryById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("MyConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Category WHERE CategoryID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }

        private CategoryModel GetCategoryById(int id)
        {
            string connectionString = _configuration.GetConnectionString("MyConnectionString");
            CategoryModel category = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Category WHERE CategoryID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    category = new CategoryModel
                    {
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader["CategoryName"].ToString(),
                        CategoryDescription = reader["CategoryDescription"].ToString()
                    };
                }

                reader.Close();
            }

            return category;
        }
    }
}

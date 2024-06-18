using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebEcommerceClothing.Models;
using Microsoft.AspNetCore.Http;

namespace WebEcommerceClothing.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult LoginAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginAccount(AccountModel accountModel)
        {
            string connectionString = _configuration.GetConnectionString("MyConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Accounts WHERE Email = @Email AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", accountModel.Email);
                    command.Parameters.AddWithValue("@Password", accountModel.Password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Lấy giá trị của trường Role từ dữ liệu đọc được
                            string role = reader["Role"].ToString();
                            string name = reader["UserName"].ToString();

                            // Đăng nhập thành công, chuyển hướng dựa vào vai trò
                            HttpContext.Session.SetString("UserName", name);
                            HttpContext.Session.SetString("Role", role);
                            if (role == "Customer")
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else if(role == "Admin")
                            {
                                return RedirectToAction("CrudAdmin", "Admin");
                            }
                            else if (role == "Seller")
                            {
                                return RedirectToAction("CrudSeller", "Seller");
                            }
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            return View(accountModel);
        }
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the login page
            return RedirectToAction("LoginAccount");
        }

        [HttpGet]
        public IActionResult RegisterAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterAccount(AccountModel accountModel)
        {
            // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            if (ModelState.IsValid)
            {
                // Lấy chuỗi kết nối từ cấu hình
                string connectionString = _configuration.GetConnectionString("MyConnectionString");

                // Tạo câu lệnh SQL để chèn dữ liệu vào cơ sở dữ liệu, không bao gồm cột Role
                string query = "INSERT INTO Accounts (UserName, Email, Password) VALUES (@UserName, @Email, @Password)";

                // Thực hiện kết nối và thực thi câu lệnh SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@UserName", accountModel.UserName);
                        command.Parameters.AddWithValue("@Email", accountModel.Email);
                        command.Parameters.AddWithValue("@Password", accountModel.Password);

                        // Thực thi câu lệnh SQL
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }

                // Chuyển hướng người dùng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("LoginAccount");
            }

            // Trả về view đăng ký với model hiện tại nếu dữ liệu không hợp lệ
            return View();
        }

        public IActionResult MyAccount()
        {
            return View();
        }
        public IActionResult CrudAdmin() 
        {
            return View();
        }
        public IActionResult CrudShopEmployee()
        {
            return View();
        }
    }
}

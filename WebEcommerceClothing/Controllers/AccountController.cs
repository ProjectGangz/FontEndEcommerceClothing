using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebEcommerceClothing.Models;

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
                            //string role = reader["Role"].ToString();

                            // Đăng nhập thành công, chuyển hướng dựa vào vai trò
                            //if (role == "Customer")
                            //{
                            //    return RedirectToAction("Index", "Home");
                            //}
                            //else if (role == "Admin")
                            //{
                            //    return RedirectToAction("CrudAdmin", "Admin");
                            //}
                            //else if (role == "ShopEmployee")
                            //{
                            //    return RedirectToAction("CrudShopEmployee", "ShopEmployee");
                            //}

                            // Đăng nhập thành công, chuyển hướng đến trang Home
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            // Đăng nhập không thành công, hiển thị lại view đăng nhập
            return View(accountModel);
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

                // Tạo câu lệnh SQL để chèn dữ liệu vào cơ sở dữ liệu
                string query = "INSERT INTO Accounts (UserName, Email, Password,Role) VALUES (@UserName, @Email, @Password, @Role )";

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
                        string role = string.IsNullOrEmpty(accountModel.Role) ? "Customer" : accountModel.Role;
                        command.Parameters.AddWithValue("@Role", role);

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
    }
}

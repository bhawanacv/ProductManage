using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index(ProductManagement.Models.Product prod)
        {
           
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Products (Name, Description) VALUES (@Name, @Description)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Name", prod.Name);
                    cmd.Parameters.AddWithValue("@Description", prod.Description ?? (object)DBNull.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index");
         }
            
        

        [HttpPost]
        public IActionResult Create(Product product)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (Name, Description) VALUES (@Name, @Description)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

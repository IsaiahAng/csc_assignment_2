using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using csc_assignment_2.Models;
using Microsoft.AspNetCore.Identity;
using csc_assignment_2.Areas.Identity.Data;
using System.Data.SqlClient;
using System.Data;

namespace csc_assignment_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            string plan = getUserData(_userManager.GetUserName(HttpContext.User));
            ViewBag.Message = plan;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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

        public string getUserData(string email)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "SELECT SubPlan FROM AspNetUsers WHERE Email = '" + email + "'";
            string result = "";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result = dr["SubPlan"].ToString();

            }
            con.Close();
            return result;
        }
    }
}

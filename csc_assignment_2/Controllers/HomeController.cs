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
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Amazon;
using Microsoft.AspNetCore.Authorization;

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
            string plan = getUserPlan(_userManager.GetUserName(HttpContext.User));
            ViewBag.FirstName = getUserFirstName(_userManager.GetUserName(HttpContext.User));
            ViewBag.LastName = getUserLastName(_userManager.GetUserName(HttpContext.User));
            ViewBag.Email = getUserEmail(_userManager.GetUserName(HttpContext.User));
            ViewBag.LastPaid = getUserLastPaid(_userManager.GetUserName(HttpContext.User));
            // Store Current Logged user into DynamoDb
            //storeSession(_userManager.GetUserName(HttpContext.User));
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

        public string getUserPlan(string email)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "SELECT * FROM AspNetUsers WHERE Email = '" + email + "'";
            string result = "";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                System.Diagnostics.Debug.WriteLine("DataReader: " + dr);
                result = dr["SubPlan"].ToString();

            }
            con.Close();
            return result;
        }
        public string getUserFirstName(string email)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "SELECT * FROM AspNetUsers WHERE Email = '" + email + "'";
            string result = "";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                System.Diagnostics.Debug.WriteLine("DataReader: " + dr);
                result = dr["FirstName"].ToString();

            }
            con.Close();
            return result;
        }
        public string getUserLastName(string email)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "SELECT * FROM AspNetUsers WHERE Email = '" + email + "'";
            string result = "";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                System.Diagnostics.Debug.WriteLine("DataReader: " + dr);
                result = dr["LastName"].ToString();

            }
            con.Close();
            return result;
        }
        public string getUserEmail(string email)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "SELECT * FROM AspNetUsers WHERE Email = '" + email + "'";
            string result = "";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                System.Diagnostics.Debug.WriteLine("DataReader: " + dr);
                result = dr["Email"].ToString();

            }
            con.Close();
            return result;
        }
        public string getUserLastPaid(string email)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "SELECT * FROM AspNetUsers WHERE Email = '" + email + "'";
            string result = "";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                System.Diagnostics.Debug.WriteLine("DataReader: " + dr);
                result = dr["LastPaid"].ToString();

            }
            con.Close();
            return result;
        }
        //public void storeSession(string email)
        //{
        //    var credentials = new BasicAWSCredentials("AKIAZ4FDJ5SPKEP54XUT", "/D5JtdPsUWBMbJ108z/jbiHR7S/z4bXnINxP2p61");

        //    var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
        //    SqlConnection con = new SqlConnection(GetConStr.ConString());
        //    string query = "SELECT * FROM Customer WHERE email = '" + email + "'";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        var request1 = new PutItemRequest
        //        {
        //            TableName = "Customer",
        //            Item = new Dictionary<string, AttributeValue>
        //            {
        //                {"customer_id", new AttributeValue {S = dr["customer_id"].ToString()} },
        //                {"email", new AttributeValue {S = dr["email"].ToString()} },
        //                {"subscription_status", new AttributeValue {S = dr["subscription_status"].ToString()} },
        //                {"charge_status", new AttributeValue {S = dr["charge_status"].ToString()} },
        //                {"premium", new AttributeValue {S = dr["premium"].ToString()} },
        //                {"customer_user_id", new AttributeValue {S = dr["customer_user_id"].ToString()} }
        //            }
        //        };
        //        client.PutItemAsync(request1);
        //    }
        //}
    }
}

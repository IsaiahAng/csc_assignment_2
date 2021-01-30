using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace csc_assignment_2.Models
{
    public class CustomerModel
    {


        public string customer_id { get; set; }
        public string email { get; set; }
        public string subscription_status { get; set; }
        public string charge_status { get; set; }
        //public string free { get; set; }
        public string premium { get; set; }
        public string customer_user_id { get; set; }

        public CustomerModel() { }
        public CustomerModel(string customer_id, string email, string subscription_status, string premium, string customer_user_id)
        {
            this.customer_id = customer_id;
            this.email = email;
            this.subscription_status = subscription_status;
            this.premium = premium;
            this.customer_user_id = customer_user_id;

        }
        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "INSERT INTO Customer(customer_id, email, subscription_status, premium, customer_user_id) values ('" + customer_id + "','" + email + "','" + subscription_status + "','" + premium + "','" + customer_user_id + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int SaveIntoUser(string plan, string email)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "UPDATE AspNetUsers SET SubPlan = '" + plan + "' WHERE Email = '" + email + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int RecordChargeStatus(string cs, string customer_id)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "UPDATE Customer set charge_status = '" + cs + "' WHERE customer_id = '" + customer_id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int LastPaid(string date, string customer_id)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "UPDATE Customer set last_paid = '" + date + "' WHERE customer_id = '" + customer_id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
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
        public string getDetailsWithId(string customer_id)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "SELECT * FROM Customer WHERE customer_id = '" + customer_id + "'";
            string result = "";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result = dr["free"].ToString();

            }
            con.Close();
            return result;
        }
        //public int PlanChange(string cond, string customer_id)
        //{
        //    SqlConnection con = new SqlConnection(GetConStr.ConString());
        //    string query = "";
        //    if (cond == "free")
        //    {
        //        query = "UPDATE Customer set subscription_status = 'active', free = 'True', premium = 'False' WHERE customer_id = '" + customer_id + "'";
        //    }
        //    else
        //    {
        //        query = "UPDATE Customer set subscription_status = 'active', free = 'False', premium = 'True' WHERE customer_id = '" + customer_id + "'";
        //    }

        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    int i = cmd.ExecuteNonQuery();
        //    con.Close();
        //    return i;
        //}
        public int PlanRemove(string customer_id)
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "UPDATE Customer set subscription_status = 'inactive' WHERE customer_id = '" + customer_id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}

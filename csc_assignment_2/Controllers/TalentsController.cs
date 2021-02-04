using csc_assignment_2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace csc_assignment_2.Controllers
{
    [Route("api/[controller]")]
    public class TalentsController : Controller
    {

        private static readonly TalentRepository repository = new TalentRepository();

        //[EnableCors]
        [HttpGet]
        public IActionResult GetAllTalents()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetTalent(int id)
        {
            Talent talent = repository.Get(id);
            if (talent == null)
            {
                return BadRequest(new { message = "Talent cannot be found!" });
            }
            return Ok(talent);
        }

        [HttpPost]
        public IActionResult AddTalent([FromBody] Talent talent)
        {
            if (talent == null)
            {
                return BadRequest(new { message = "Talent cannot be empty!" });
            }
            repository.Add(talent);
            //SqlConnection con = new SqlConnection(GetConStr.ConString());
            //string a = talent.Name;
            //string query = "INSERT INTO Talent(Name, ShortName, Reknown, Bio, Img_Url) values ('" + talent.Name + "','" + talent.ShortName + "','" + talent.Reknown + "','" + talent.Bio + "','" + talent.Img_Url + "')";
            //SqlCommand cmd = new SqlCommand(query, con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            return Ok(talent);
        }

        [HttpPut("{id}")]
        public IActionResult EditTalent(int id, [FromBody] Talent talent)
        {
            if (talent == null)
            {
                return BadRequest(new { message = "Talent cannot be empty!" });
            }
            talent.Id = id;
            repository.Update(talent);
            //SqlConnection con = new SqlConnection(GetConStr.ConString());
            //string a = talent.Name;
            //string query = "UPDATE Talent set Name = '" + talent.Name + "', ShortName ='" + talent.ShortName + "', Reknown = '" + talent.Reknown +"', Bio = '"+ talent.Bio+"', Img_Url = '"+ talent.Img_Url+ "' WHERE Id =" + id;
            //SqlCommand cmd = new SqlCommand(query, con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            return Ok(talent);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTalent(int id)
        {
            repository.Remove(id);
            //SqlConnection con = new SqlConnection(GetConStr.ConString());
            //string query = "DELETE FROM Talent WHERE Id='"+id+"';";
            //SqlCommand cmd = new SqlCommand(query, con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            return Ok(new { message = "Deleted " + id });
        }
    }
}

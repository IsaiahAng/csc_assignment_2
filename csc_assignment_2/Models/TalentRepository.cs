using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace csc_assignment_2.Models
{
    public class TalentRepository
    {
        private List<Talent> talents = new List<Talent>();
        public Talent loadTalent = new Talent();
        private int _nextId = 1;
        //public List<Talent> getTalent()
        //{
        //    SqlConnection con = new SqlConnection(GetConStr.ConString());
        //    string query = "SELECT SubPlan FROM Talent";
        //    List<Talent> result = new List<Talent>();
        //    Talent aTalent = new Talent();
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        aTalent.Name = dr["Name"].ToString();
        //        aTalent.ShortName = dr["ShortName"].ToString();
        //        aTalent.Reknown = dr["Reknown"].ToString();
        //        aTalent.Bio = dr["Bio"].ToString();
        //        result.Add(aTalent);
        //    }
        //    con.Close();
        //    return result;
        //}
        public TalentRepository()
        {
            SqlConnection con = new SqlConnection(GetConStr.ConString());
            string query = "SELECT * FROM Talent";
            List<Talent> result = new List<Talent>();
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Talent aTalent = new Talent();
                aTalent.Id = int.Parse(dr["Id"].ToString());
                aTalent.Name = dr["Name"].ToString();
                aTalent.ShortName = dr["ShortName"].ToString();
                aTalent.Reknown = dr["Reknown"].ToString();
                aTalent.Bio = dr["Bio"].ToString();
                aTalent.Img_Url = dr["Img_Url"].ToString();
                result.Add(aTalent);
            }
            con.Close();
            
            for(int i = 0; i < result.Count(); i++)
            {
                Add(new Talent
                {
                    //Fix_Id = _nextId++,
                    Id = result[i].Id,
                    Name = result[i].Name,
                    ShortName = result[i].ShortName,
                    Reknown = result[i].Reknown,
                    Bio = result[i].Bio,
                    Img_Url = result[i].Img_Url

                });
            }
            //Add(new Talent
            //{
            //    Name = "Barot Bellingham",
            //    ShortName = "Barot_Bellingham",
            //    Reknown = "Royal Academy of Painting and Sculpture",
            //    Bio = "Barot edee has just finished his final year at The Royal Academy of Painting and Sculpture, where he excelled in glass etching paintings and portraiture. Hailed as one of the most diverse artists of his generation, Barot is equally as skilled with watercolors as he is with oils, and is just as well-balanced in different subject areas. Barot's collection entitled \"The Un-Collection\" will adorn the walls of Gilbert Hall, depicting his range of skills and sensibilities - all of them, uniquely Barot, yet undeniably different"
            //});
            //Add(new Talent
            //{
            //    Id = 1,
            //    Name = "Jonathan G. Ferrar II",
            //    ShortName = "Jonathan_Ferrar",
            //    Reknown = "Artist to Watch in 2012",
            //    Bio = "The Artist to Watch in 2012 by the London Review, Johnathan has already sold one of the highest priced-commissions paid to an art student, ever on record. The piece, entitled Gratitude Resort, a work in oil and mixed media, was sold for $750,000 and Jonathan donated all the proceeds to Art for Peace, an organization that provides college art scholarships for creative children in developing nations"
            //});
            //Add(new Talent
            //{
            //    Id = 2,
            //    Name = "Hillary Hewitt Goldwynn-Post",
            //    ShortName = "Hillary_Goldwynn",
            //    Reknown = "New York University",
            //    Bio = "Hillary is a sophomore art sculpture student at New York University, and has already won all the major international prizes for new sculptors, including the Divinity Circle, the International Sculptor's Medal, and the Academy of Paris Award. Hillary's CAC exhibit features 25 abstract watercolor paintings that contain only water images including waves, deep sea, and river."
            //});
            //Add(new Talent
            //{
            //    Id = 3,
            //    Name = "Hassum Harrod",
            //    ShortName = "Hassum_Harrod",
            //    Reknown = "Art College in New Dehli",
            //    Bio = "The Art College in New Dehli has sponsored Hassum on scholarship for his entire undergraduate career at the university, seeing great promise in his contemporary paintings of landscapes - that use equal parts muted and vibrant tones, and are almost a contradiction in art. Hassum will be speaking on \"The use and absence of color in modern art\" during Thursday's agenda."
            //});
        }

        public IEnumerable<Talent> GetAll()
        {
            return talents;
        }

        public Talent Get(int id)
        {
            return talents.Find(t => t.Id == id);
        }

        public Talent Add(Talent talent)
        {
            if (talent == null)
            {
                throw new ArgumentNullException("talent");
            }
            talents.Add(talent);
            return talent;
        }

        public void Remove(int id)
        {
            talents.RemoveAll(t => t.Id == id);
        }

        public bool Update(Talent talent)
        {
            if (talent == null)
            {
                throw new ArgumentNullException("talent");
            }
            int index = talents.FindIndex(p => p.Id == talent.Id);
            if (index == -1)
            {
                return false;
            }
            talents.RemoveAt(index);
            talents.Add(talent);
            return true;
        }
    }
}

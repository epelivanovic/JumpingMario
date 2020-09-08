using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Igrica
{
    class DataProvider
    {
        private  string _poruka;
        public string Poruka { get { return _poruka; } }
        public List<Rezultat> UcitajSve()
        {
            List<Rezultat> lista = new List<Rezultat>();
            try
            {
                using (SqlConnection con=new SqlConnection(Konekcija.GetCon()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from Rezultat " +
                        "order by Score desc";
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow red in dt.Rows)
                    {
                        Rezultat r = new Rezultat
                        {
                            Ime = Convert.ToString(red["Ime"]),
                            Score = Convert.ToInt32(red["Score"])
                        };
                        lista.Add(r);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {

                _poruka = ex.Message;
                return null;
            }
        }
        public bool Upisi(int score,string ime="Nepoznat")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Konekcija.GetCon()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "usp_Upisi";
                    cmd.Parameters.AddWithValue("@ime", ime);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {

                _poruka = ex.Message;
                return false;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Programacion6.Models;
using System.Data.SqlClient;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Programacion6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoferesController : ControllerBase
    {

        string connectionString = "Server=DESKTOP-6G4R2RA;Database=ParcialProg6; Trusted_Connection=true;TrustServerCertificate=true;";

        [HttpPost]
        [Route("CrearOrdenes2")]
        public bool crearOrdenes(Choferes C)
        {

            string query = "INSERT INTO Choferes (Nombre, Edad, Apellido, Unidad, DNI) VALUES (@Nombre, @Edad, @Apellido, @Unidad, @DNI)";
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlCommand cm = new SqlCommand(query, sqlConn);

                cm.Parameters.AddWithValue("@IdChofer", C.IdChofer);
                cm.Parameters.AddWithValue("@Nombre", C.Nombre);
                cm.Parameters.AddWithValue("@Edad", C.Edad);
                cm.Parameters.AddWithValue("@Apellido", C.Apellido);
                cm.Parameters.AddWithValue("@Unidad", C.Unidad);
                cm.Parameters.AddWithValue("@DNI", C.DNI);

                cm.ExecuteNonQuery();
            }

            return true;
        }



        [HttpPost]
[Route("CrearOrden")]
        public string crearOrden(Choferes C)
        {
            string query = "INSERT INTO Choferes (Nombre, Edad, Apellido, Unidad, DNI) VALUES (@Nombre, @Edad, @Apellido, @Unidad, @DNI)";

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlCommand CM = new SqlCommand(query, sqlConn);

                CM.Parameters.AddWithValue("@Nombre", C.Nombre);
                CM.Parameters.AddWithValue("@Edad", C.Edad);
                CM.Parameters.AddWithValue("@Apellido", C.Apellido);
                CM.Parameters.AddWithValue("@Unidad", C.Unidad);
                CM.Parameters.AddWithValue("@DNI", C.DNI);

                if (CM.ExecuteNonQuery() == 1)
                {
                    sqlConn.Close();
                    return "{\"resultado\":\"OK\"}";
                }
                else
                {
                    sqlConn.Close();
                    return "{\"resultado\":\"NOK\"}";
                }
            }
        }






        [HttpGet]
        [Route("getChoferes")]
        public List<Choferes> getChoferes()
        {
            List<Choferes> LH = new List<Choferes>();

            string Query="Select IdChofer, Nombre, EDAD, Apellido, Unidad, DNI from Choferes";

            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            SqlCommand sqlCM = new SqlCommand(Query, sqlConn);
            SqlDataReader dr = sqlCM.ExecuteReader();
            while (dr.Read())
            {
                Choferes oh = new Choferes();
                oh.IdChofer = int.Parse(dr[0].ToString());
                oh.Nombre = dr[1].ToString();
                oh.Edad = int.Parse(dr[2].ToString());
                oh.Apellido = dr[3].ToString();
                oh.Unidad = dr[4].ToString();
                oh.DNI = int.Parse(dr[5].ToString());


                LH.Add(oh);
            }
            

            sqlConn.Close();

         



            return LH;
        }

        [HttpGet]
        [Route("getChoferesUnidad")]
        public List<Choferes> getChoferesUnidad(string Unidad)
        {
            List<Choferes> lh = new List<Choferes>();

            // Utiliza parámetros en la consulta SQL para evitar la inyección de SQL
            string Query = "SELECT IdChofer, Nombre, EDAD, Apellido, Unidad, DNI FROM Choferes WHERE Unidad = @Unidad";

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                using (SqlCommand sqlCM = new SqlCommand(Query, sqlConn))
                {
                    sqlCM.Parameters.AddWithValue("@Unidad", Unidad);
                    SqlDataReader dr = sqlCM.ExecuteReader();

                    while (dr.Read())
                    {
                        Choferes oh = new Choferes();
                        oh.IdChofer = int.Parse(dr[0].ToString());
                        oh.Nombre = dr[1].ToString();
                        oh.Edad = int.Parse(dr[2].ToString());
                        oh.Apellido = dr[3].ToString();
                        oh.Unidad = dr[4].ToString();
                        oh.DNI = int.Parse(dr[5].ToString());

                        lh.Add(oh);
                    }
                }
            }

            return lh;
        }
    }
}





// GET: api/<ChoferesController>


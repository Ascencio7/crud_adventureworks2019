using crud_adventureworks2019.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace crud_adventureworks2019.Models
{
    public class datos
    {
        // Cadena de conexion a la base de datos desde el Web.config
        string con = ConfigurationManager.ConnectionStrings["AWConnection"].ConnectionString;

        // YA MUESTRA LOS DATOS

        #region Obtener Datos
        public List<Personas> ObtenerDatos()
        {
            // Lista para guardar los datos
            List<Personas> lista = new List<Personas>();

            // Conexion a la base usando SqlConnection
            using (SqlConnection cn = new SqlConnection(con))
            {
                // Query para obtener los datos de la tabla Person.Person, con un limite de 10 datos (se pueden mas)
                string consulta = @"SELECT TOP 10 
                            BusinessEntityID,
                            ISNULL(FirstName,'') AS FirstName,
                            ISNULL(LastName,'') AS LastName,
                            PersonType
                            FROM Person.Person";

                // Comando para ejecutar la consulta
                SqlCommand cmd = new SqlCommand(consulta, cn);

                // Se abre la conexion
                cn.Open();

                // Se ejecuta la consulta
                SqlDataReader dr = cmd.ExecuteReader();

                // Se recorren los datos y se guardan los datos
                while (dr.Read())
                {
                    // Se crea un objeto y se asignan los datos a las propiedade del objeto
                    Personas p = new Personas();
                    p.BusinessEntityID = Convert.ToInt32(dr["BusinessEntityID"]);
                    p.FirstName = dr["FirstName"].ToString();
                    p.LastName = dr["LastName"].ToString();
                    p.PersonType = dr["PersonType"].ToString();

                    // Se agrega el objeto a la lista creada
                    lista.Add(p);
                }
            }

            return lista;
        }
        #endregion


        // NECESITO REVISAR ESTOS METODOS, NO HAY VISTAS DE ELLOS TODAVIA, por eso habrá errores

        #region Insertar
        // Insertar personas
        public void InsertarDatos(Personas p)
        {
            using (SqlConnection cn = new SqlConnection(con))
            {
                string consulta = @"INSERT INTO Person.Person
                                (PersonType, NameStyle, FirstName, LastName)
                                VALUES (@type, 0, @first, @last)";
                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@type", p.PersonType);
                cmd.Parameters.AddWithValue("@first", p.FirstName);
                cmd.Parameters.AddWithValue("@last", p.LastName);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion


        #region Buscar ID
        // Buscar por el id
        public Personas ObtenerID(int id)
        {
            Personas p = new Personas();

            using (SqlConnection cn = new SqlConnection(con))
            {
                string consulta = @"SELECT BusinessEntityID, FirstName, LastName, PersonType
                            FROM Person.Person
                            WHERE BusinessEntityID = @id";

                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    p.BusinessEntityID = (int)dr["BusinessEntityID"];
                    p.FirstName = dr["FirstName"].ToString();
                    p.LastName = dr["LastName"].ToString();
                    p.PersonType = dr["PersonType"].ToString();
                }
            }
            return p;
        }
        #endregion


        #region Actualizar
        // Actualizar
        public void Actualizar(Personas p)
        {
            using (SqlConnection cn = new SqlConnection(con))
            {
                string consulta = @"UPDATE Person.Person
                            SET FirstName=@first,
                                LastName=@last,
                                PersonType=@type
                            WHERE BusinessEntityID=@id";

                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@id", p.BusinessEntityID);
                cmd.Parameters.AddWithValue("@first", p.FirstName);
                cmd.Parameters.AddWithValue("@last", p.LastName);
                cmd.Parameters.AddWithValue("@type", p.PersonType);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion


        #region Eliminar
        // Eliminar
        public void Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(con))
            {
                string consulta = "DELETE FROM Person.Person WHERE BusinessEntityID=@id";

                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion


    }
}
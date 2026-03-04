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
                string consulta = @"SELECT TOP 20779
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


        public void InsertarDatos(Personas p)
        {
            using (SqlConnection cn = new SqlConnection(con))
            {
                cn.Open();

                // 1. Insertar en BusinessEntity y obtener el ID generado
                SqlCommand cmdID = new SqlCommand("INSERT INTO Person.BusinessEntity DEFAULT VALUES; SELECT SCOPE_IDENTITY();", cn);
                int newId = Convert.ToInt32(cmdID.ExecuteScalar());

                // 2. Insertar en Person.Person usando el ID generado
                string consulta = @"INSERT INTO Person.Person
                            (BusinessEntityID, PersonType, NameStyle, FirstName, LastName)
                            VALUES (@id, @type, 0, @first, @last)";
                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@id", newId);
                cmd.Parameters.AddWithValue("@type", p.PersonType);
                cmd.Parameters.AddWithValue("@first", p.FirstName);
                cmd.Parameters.AddWithValue("@last", p.LastName);

                cmd.ExecuteNonQuery();
            }
        }


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


        // YA SE PUEDE ACTUALIZAR LOS DATOS SIN PROBLEMA
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


        /*
            NO SE PODRAN BORRAR DATOS PORQUE ELLOS DEPENDEN EN OTRAS TABLAS
        */
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace crud_adventureworks2019.Models
{
    public class Personas
    {
        // Los campos de aqui deben coincider con los campos de la tabla en la BD, si no, no se muestran los datos
        public int BusinessEntityID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonType { get; set; }
    }
}
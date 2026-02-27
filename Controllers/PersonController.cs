using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using crud_adventureworks2019.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace crud_adventureworks2019.Controllers
{
    public class PersonController : Controller
    {
        // Se crea un objeto de la clase datos para usar sus metodos
        datos dal = new datos();

        #region Inicio
        public ActionResult Index()
        {
            // Se obtiene la lista de datos usando el metodo de la clase datos.cs
            var lista = dal.ObtenerDatos();
            // Se guarda el total de los datos en un ViewBag
            ViewBag.Total = lista.Count;
            // Se retorna la vista con la lista de los datos
            return View(lista);
        }
        #endregion


        // IGNORAR
        public ActionResult Create()
        {
            return View();
        }
        // IGNORAR


        #region Crear
        // AUN NO HAY VISTA
        [HttpPost]
        public ActionResult Create(Personas p)
        {
            if (ModelState.IsValid)
            {
                dal.InsertarDatos(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }
        #endregion


        #region Editar
        // AUN NO HAY VISTA
        public ActionResult Edit(int id)
        {
            return View(dal.ObtenerID(id));
        }

        [HttpPost]
        public ActionResult Edit(Personas p)
        {
            if (ModelState.IsValid)
            {
                dal.Actualizar(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }
        #endregion


        #region Eliminar
        // AUN NO HAY VISTA
        public ActionResult Delete(int id)
        {
            return View(dal.ObtenerID(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            dal.Eliminar(id);
            return RedirectToAction("Index");
        }
        #endregion


        #region TestConexion

        // Metodo para probar la conexion a la base
        public ActionResult TestConexion()
        {
            // Una variable para guardar la cadena de conexion desde Web.config
            string cadena = ConfigurationManager.ConnectionStrings["AWConnection"].ConnectionString;

            // Manejo de errores
            try
            {
                // Se intenta abrir la conexion
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    cn.Open();
                    // Si se abre la conexion se mostrara el mensaje de exito
                    ViewBag.Msg = "Conexión exitosa";
                }
            }
            catch (Exception ex)
            {
                // S hay error de conexion mostrara los detalles del error
                ViewBag.Msg = ex.Message;
            }

            return View();
        }
        #endregion


    }
}
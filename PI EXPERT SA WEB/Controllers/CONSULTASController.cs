using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PI_EXPERT_SA_WEB.Models;

namespace PI_EXPERT_SA_WEB.Controllers
{
    public class CONSULTASController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: CONSULTAS
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult DesarrolladoresAsignadosDisponibles() {

        //}

        public ActionResult ComparacionDuracionRequerimientos()
        {
            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }

        //public ActionResult TotalHorasRequerimiento() {

        //}

        //public ActionResult HistorialDesarrollador() {

        //}

        //public ActionResult PeriodosDesocupacion() {

        //}

        //public ActionResult ComparacionDuracionRequerimientoComplejidad() {

        //}

        //public ActionResult ConocimientosFrecuentes() {

        //}

        //public ActionResult EstadoResponsablesRequerimientos() {

        //}

        //public ActionResult RequerimientosTerminadosEjecucion() {

        //}







    }
}
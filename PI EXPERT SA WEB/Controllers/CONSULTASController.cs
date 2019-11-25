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

        public ActionResult DesarrolladoresAsignadosDisponibles() {
            return View();
        }


        public ActionResult ComparacionDuracionRequerimientos()
        {

            ViewBag.empleados = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");

            return View();
        }

        public PartialViewResult GetListaProyectos(string cedulaPK) {
            
            var CONSULTAS =
                from req in db.REQUERIMIENTO
                join mod in db.MODULO
                on req.idModuloPK equals mod.idModuloPK
                join proy in db.PROYECTO
                on mod.idProyectoPK equals proy.idProyectoPK
                where proy.fechaFin != null
                where req.cedulaDesarrolladorFK == cedulaPK
                select new CONSULTAS {modeloRequerimiento = req, modeloModulo = mod, modeloProyecto = proy};


            return PartialView(CONSULTAS);
        }

        //public ActionResult TotalHorasRequerimiento() {

        public ActionResult PeriodosDesocupacion() {
            return View();
        }

        public ActionResult ComparacionDuracionRequerimientoComplejidad() {


            ViewBag.complejidad = new SelectList(db.REQUERIMIENTO, "complejidad", "complejidad");
            return View();
        }

        public ActionResult ConocimientosFrecuentes() {
            return View();
        }

        public ActionResult EstadoResponsablesRequerimientos() {
            return View();
        }

        public ActionResult RequerimientosTerminadosEjecucion() {
            return View();
        }







    }
}
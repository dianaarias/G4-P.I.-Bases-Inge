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
            var queryAsig = db.SP_DesarrolladoresAsignadosDisponibles();
            var desarolladores = queryAsig.ToList();
            var queryDisp = db.EMPLEADO.Where(x => x.disponibilidad == true).ToList();
            ViewBag.disponibles = queryDisp;
            return View(desarolladores);
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
            ViewBag.Cliente = new SelectList(db.CLIENTE, "cedulaPK", "nombre");
            return View();
        }

        public PartialViewResult GetListaProyectosCliente(string cedulaPK)
        {
            var query = from proy in db.PROYECTO
                        where proy.cedulaClienteFK == cedulaPK
                        select new { proy.idProyectoPK, proy.nombre };
            ViewBag.Proyecto = query.ToList();
            return PartialView();
        }

        public PartialViewResult GetListaDesarolladoresResp(int idProyecto)
        {
            var query = from req in db.REQUERIMIENTO
                        join emp in db.EMPLEADO
                        on req.cedulaDesarrolladorFK equals emp.cedulaPK
                        where req.idProyectoPK == idProyecto
                        select new { nombreReq = req.nombre, estadoReq = req.estado, apellidoEmp = emp.apellido1, nombreEmp = emp.nombre };
            return PartialView(query);
        }

        public ActionResult RequerimientosTerminadosEjecucion() {
            return View();
        }







    }
}
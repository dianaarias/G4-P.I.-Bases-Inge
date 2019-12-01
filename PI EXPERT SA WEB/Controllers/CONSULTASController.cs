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
            var queryAsig = from req in db.REQUERIMIENTO
                           join emp in db.EMPLEADO
                           on req.cedulaDesarrolladorFK equals emp.cedulaPK
                           join equipo in db.ROL
                           on emp.cedulaPK equals equipo.cedulaPK
                           join proy in db.PROYECTO
                           on equipo.idProyectoPK equals proy.idProyectoPK
                           where proy.fechaFin != null
                           select new { nombreEmpleado = emp.nombre + ' ' + emp.apellido1 + ' ' + emp.apellido2, nombreProyecto = proy.nombre, 
                               fechaInicioEmp = proy.fechaInicio, fechaDesocupEst = DbFunctions.AddDays(proy.fechaInicio, proy.duracionEstimada/8) };
            ViewBag.EmpDesoc = db.EMPLEADO.Where(x => x.disponibilidad == true);
            return View(queryAsig);
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
        public ActionResult HorasEstRealProy()
        {
            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            var horasTot = db.PROYECTO.Where(x => x.fechaFin != null)
                                      .Join(db.MODULO,
                                                proy => proy.idProyectoPK,
                                                modu => modu.idProyectoPK,
                                                (proy, modu) => new { proy,  modu })
                                      .Join(db.REQUERIMIENTO,
                                              modu => new { modu.modu.idModuloPK, modu.modu.idProyectoPK },
                                              req => new { req.idModuloPK, req.idProyectoPK },
                                              (modu , req) => new { modu, req })
                                      .GroupBy(s => new {s.modu.proy.nombre })
                                      .Select(g => new {Nombre = g.Key.nombre, 
                                                        duracionEst = g.Sum(x => x.req.duracionEstimada), 
                                                        duracionTot = g.Sum(x => x.req.duracionReal),
                                                        diffDuracion = g.Sum(x => x.req.duracionEstimada) - g.Sum(x => x.req.duracionReal)});
            return View(horasTot);
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
            var queryEmp =  from req in db.REQUERIMIENTO
                            join emp in db.EMPLEADO
                            on req.cedulaDesarrolladorFK equals emp.cedulaPK
                            join equipo in db.ROL
                            on emp.cedulaPK equals equipo.cedulaPK
                            join proy in db.PROYECTO
                            on equipo.idProyectoPK equals proy.idProyectoPK
                            join cli in db.CLIENTE 
                            on proy.cedulaClienteFK equals cli.cedulaPK
                            where cli.cedulaPK == cedulaPK
                            select new {nombreProy = proy.nombre,  nombreReq = req.nombre, estadoReq = req.estado,
                                nombreEmp = emp.nombre +' '+ emp.apellido1+' '+emp.apellido2};

            ViewBag.Proyecto = query.ToList();
            return PartialView(queryEmp);
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
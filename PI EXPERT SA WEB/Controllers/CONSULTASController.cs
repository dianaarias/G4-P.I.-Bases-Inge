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

        //-------------------------Celeste COMIENZO-------------------------

        //public ActionResult: ComparacionDuracionRequerimiento() 
        //Comparacíón de las duraciones reales vs estimadas de los requerimientos de un desarrollador 
        public ActionResult ComparacionDuracionRequerimientos()
        {

            ViewBag.empleados = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");
            return View();
        }

        public PartialViewResult GetListaProyectos(string cedulaPK) {

            List<ROL> proyecto = db.ROL.Where(x => x.cedulaPK == cedulaPK).ToList();
            ViewBag.proyectos = new SelectList(proyecto, "idProyectoPK", "idProyectoPK");

            var CONSULTAS =
                from req in db.REQUERIMIENTO
                join mod in db.MODULO
                on req.idModuloPK equals mod.idModuloPK
                join proy in db.PROYECTO
                on mod.idProyectoPK equals proy.idProyectoPK
                where proy.fechaFin == null
                where req.cedulaDesarrolladorFK == cedulaPK
                select new CONSULTAS {modeloRequerimiento = req, modeloModulo = mod, modeloProyecto = proy};


            return PartialView(CONSULTAS);
        }

        public PartialViewResult MostrarComparacionDuraciones( string cedulaPk, int? idProyectoPK){
            var CONSULTAS =
               from req in db.REQUERIMIENTO
               join mod in db.MODULO
               on req.idModuloPK equals mod.idModuloPK
               join proy in db.PROYECTO
               on mod.idProyectoPK equals proy.idProyectoPK
               where proy.fechaFin == null
               where req.cedulaDesarrolladorFK == cedulaPk
               where req.idProyectoPK == idProyectoPK
               select new CONSULTAS { modeloRequerimiento = req, modeloModulo = mod, modeloProyecto = proy };

            return PartialView(CONSULTAS);
        }

        //public ActionResult  HistorialDesarrollador() {
        public ActionResult HistorialDesarrollador()
        {
            ViewBag.empleados = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");
            return View();
        }

        public PartialViewResult MostrarHistorial(string cedulaPk)
        {
     var CONSULTAS =

               from proy in db.PROYECTO
               join rol in db.ROL
               on proy.idProyectoPK equals rol.idProyectoPK
               join emp in db.EMPLEADO
               on rol.cedulaPK equals emp.cedulaPK
               join req in db.REQUERIMIENTO 
               on emp.cedulaPK equals req.cedulaDesarrolladorFK
               where req.idProyectoPK == proy.idProyectoPK
               //where req.fechaFin != null
               where rol.cedulaPK == cedulaPk
               select new CONSULTAS
               {
                   modeloProyecto = proy,
                   modeloRol = rol,
                   modeloEmpleado = emp,
                   modeloRequerimiento = req
       
               } into t1
               group t1 by t1.modeloProyecto.nombre  into g
               select new Group<string, CONSULTAS> { Key = g.Key, Values = g, suma = g.Sum(x=>  x.modeloRequerimiento.duracionReal) };

            return PartialView(CONSULTAS.ToList());
        }

        //-------------------------Celeste fin-------------------------

        //public ActionResult TotalHorasRequerimiento() {

        public ActionResult PeriodosDesocupacion() {
            return View();
        }








        //-------------------------JOHN COMIENZO-------------------------


        public ActionResult ComparacionDuracionRequerimientoComplejidad() {

            //ViewBag.complejidad = new SelectList(db.REQUERIMIENTO, "complejidad", "complejidad");
            return View();
        }


        public PartialViewResult GetRequerimientoComplejidad(string complex) {

            //var ss = db.REQUERIMIENTO.GroupBy(s => new { s.complejidad })
            //    .Select(g => new
            //    {
            //        complejidad = g.Key.complejidad,
            //        totalRequerimientos = g.Count(x => x.complejidad == x.complejidad),
            //        minimo = g.Min(x => (x.duracionReal - x.duracionEstimada)),
            //        maximo = g.Max(x => (x.duracionReal - x.duracionEstimada)),
            //        promedio = g.Average(x => x.duracionReal)
            //    });


            var CONSULTAS =

          from req in db.REQUERIMIENTO
          select new CONSULTAS
          {
              modeloRequerimiento = req
          } into t1
          group t1 by t1.modeloProyecto.nombre into g
          select new Group<string, CONSULTAS> { Key = g.Key, Values = g, suma = g.Sum(x => x.modeloRequerimiento.duracionReal) };







            return PartialView();
        }

        public ActionResult RequerimientosTerminadosEjecucion()
        {



            return View();
        }






        public ActionResult HorasEstRealProy()
        {
            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");

            var horasTot = db.PROYECTO.Where(x => x.fechaFin != null)
            .Join(db.MODULO,
                    proy => proy.idProyectoPK,
                    modu => modu.idProyectoPK,
                    (proy, modu) => new { proy, modu })
            .Join(db.REQUERIMIENTO,
                    modu => new {modu.modu.idModuloPK, modu.modu.idProyectoPK },
                    req => new { req.idModuloPK, req.idProyectoPK },
                    (modu, req) => new { modu, req })
            .GroupBy(s => new { s.modu.proy.nombre })
            .Select(g => new {
                Nombre = g.Key.nombre,
                duracionEst = g.Sum(x => x.req.duracionEstimada),
                duracionTot = g.Sum(x => x.req.duracionReal),
                diffDuracion = g.Sum(x => x.req.duracionEstimada) - g.Sum(x => x.req.duracionReal)
            });


            return View(horasTot.ToList());
        }




        //-------------------------JOHN FIN-------------------------




        public ActionResult ConocimientosFrecuentes() {
            return View();
        }

        public ActionResult EstadoResponsablesRequerimientos() {
            return View();
        }


    }
}
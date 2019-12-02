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


            //System.Linq.IQueryable<PI_EXPERT_SA_WEB.Models.Group<string, PI_EXPERT_SA_WEB.Models.CONSULTAS>> consulta;
            IQueryable<Group<string, CONSULTAS>> consulta;

            if (complex == "Todas")
            {
                consulta =
                from req in db.REQUERIMIENTO
                select new CONSULTAS
                {
                    modeloRequerimiento = req
                } into t1
                group t1 by t1.modeloRequerimiento.complejidad into g
                select new Group<string, CONSULTAS>
                {
                    Key = g.Key,
                    Values = g,
                    suma = g.Count(x => x.modeloRequerimiento.complejidad == x.modeloRequerimiento.complejidad),
                    minimo = g.Min(x => (x.modeloRequerimiento.duracionReal - x.modeloRequerimiento.duracionEstimada)),
                    maximo = g.Max(x => (x.modeloRequerimiento.duracionReal - x.modeloRequerimiento.duracionEstimada)),
                    promedio = (int)g.Average(x => x.modeloRequerimiento.duracionReal)
                };
            }
            else
            {
                consulta =
                    from req in db.REQUERIMIENTO
                    where req.complejidad == complex
                    select new CONSULTAS
                    {
                        modeloRequerimiento = req
                    } into t1
                    group t1 by t1.modeloRequerimiento.complejidad into g
                    select new Group<string, CONSULTAS>
                    {
                        Key = g.Key,
                        Values = g,
                        suma = g.Count(x => x.modeloRequerimiento.complejidad == complex),
                        minimo = g.Min(x => (x.modeloRequerimiento.duracionReal - x.modeloRequerimiento.duracionEstimada)),
                        maximo = g.Max(x => (x.modeloRequerimiento.duracionReal - x.modeloRequerimiento.duracionEstimada)),
                        promedio = (int)g.Average(x => x.modeloRequerimiento.duracionReal)
                    };

                var a = !(consulta.Any());
            }

            return PartialView(consulta.ToList());
        }



        public ActionResult RequerimientosTerminadosEjecucion()
        {
            ViewBag.clientes = new SelectList(db.CLIENTE, "cedulaPK", "name");
            return View();
        }

        public PartialViewResult GetProyectoForCliente(string cliente) {

            ViewBag.proyectos = new SelectList(db.PROYECTO.Where(x => x.cedulaClienteFK == cliente));
            return PartialView();
        }


        public PartialViewResult GetRequerimientosForCliente(string cliente, string proyecto) {

            var a = db.REQUERIMIENTO;

            var CONSULTAS =
                from proy in db.PROYECTO
                join mod in db.MODULO
                on proy.idProyectoPK equals mod.idProyectoPK
                join req in db.REQUERIMIENTO
                on mod.idModuloPK equals req.idModuloPK
                where proy.cedulaClienteFK == cliente
                where req.estado == "En Ejecución"
                select new CONSULTAS
                {
                    modeloProyecto = proy,
                    modeloModulo = mod,
                    modeloRequerimiento = req
                } into t1
                group t1 by t1.modeloRequerimiento.estado into g
                select new Group<string, CONSULTAS>
                {
                    Key = g.Key,
                    Values = g                    

                    suma = g.Count(x => x.modeloRequerimiento.estado == "En Ejecución"),
                    fecha = System.Data.Entity.SqlServer.SqlFunctions.DateAdd("DAY", (g.Sum(x => x.modeloRequerimiento.duracionEstimada) - 
                    g.Sum(x => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("DAY",x.modeloRequerimiento.fechaInicio, System.Data.Entity.SqlServer.SqlFunctions.GetDate())), 
                    System.Data.Entity.SqlServer.SqlFunctions, System.Data.Entity.SqlServer.SqlFunctions.GetDate())
                };


            fecha = System.Data.Entity.SqlServer.SqlFunctions.DateAdd("DAY", 5, System.Data.Entity.SqlServer.SqlFunctions.GetDate()),

            return PartialView();
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
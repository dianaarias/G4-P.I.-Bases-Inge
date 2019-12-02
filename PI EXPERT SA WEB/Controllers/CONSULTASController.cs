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

        //Consulta comparación entre duración estimada y real por cada requerimiento para un desarrollador específico.
        //___________________________________________________________________________________________________________
        public ActionResult ComparacionDuracionRequerimientos()
        {

            ViewBag.empleados = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");

            return View();
        }

        //Consulta para seleccionar los proyectos terminados en que ha participado el desarrollador
        public PartialViewResult GetListaProyectos(string cedulaPK) {

            var query =
               (from p in db.PROYECTO
                join r in db.ROL on p.idProyectoPK equals r.idProyectoPK
                where r.cedulaPK == cedulaPK
                where p.fechaFin != null
                where r.tipoRol != "Lider"
                select new {p.idProyectoPK, p.nombre }).Distinct();

            List<SelectListItem> items = new SelectList(query, "idProyectoPK", "nombre").ToList();
            items.Insert(0, (new SelectListItem { Text = "Todos proyectos", Value = "Todos" }));
            ViewData["proyectos"] = items;

            return PartialView();
        }

        //Consulta para dar los resultados de las compraciones de las duraciones de todos los proyectos terminados de un desarrollador
        public PartialViewResult MostrarComparacionDuracionesProyectos(string cedulaPK)
        {
            var CONSULTAS =
                from req in db.REQUERIMIENTO
                join mod in db.MODULO
                on req.idModuloPK equals mod.idModuloPK
                join proy in db.PROYECTO
                on mod.idProyectoPK equals proy.idProyectoPK
                where proy.fechaFin != null
                where req.cedulaDesarrolladorFK == cedulaPK
                select new CONSULTAS { modeloRequerimiento = req, modeloModulo = mod, modeloProyecto = proy };

            return PartialView(CONSULTAS);
        }

        //Consulta para dar los resultados de las compraciones de las duraciones de un proyecto especifico en el que ha participado el desarrollador
        public PartialViewResult MostrarComparacionDuraciones( string cedulaPk, int? idProyectoPK){
            var CONSULTAS =
               from req in db.REQUERIMIENTO
               join mod in db.MODULO
               on req.idModuloPK equals mod.idModuloPK
               join proy in db.PROYECTO
               on mod.idProyectoPK equals proy.idProyectoPK
               where proy.fechaFin != null
               where req.cedulaDesarrolladorFK == cedulaPk
               where req.idProyectoPK == idProyectoPK
               select new CONSULTAS { modeloRequerimiento = req, modeloModulo = mod, modeloProyecto = proy };

            return PartialView(CONSULTAS);
        }

        //Consulta historial sobre la participación de un desarrollador en diferentes proyectos, su rol y total de horas dedicadas
        //________________________________________________________________________________________________________________________
        public ActionResult HistorialDesarrollador()
        {
            ViewBag.empleados = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");
            return View();
        }

        //Consulta para mostrar resultados del historial de un desarrollador
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

        //Consulta cantidad de desarrolladores con conocimientos específicos y promedio de su antigüedad en la empresa.
        //_____________________________________________________________________________________________________________
        public ActionResult HabilidadesFrecuentes()
        {
            var query =
                (from emp in db.EMPLEADO
                join h in db.HABILIDADES on emp.cedulaPK equals h.cedulaEmpleadoPK
                select new { h.habilidadPK }).Distinct();

            List<SelectListItem> items = new SelectList(query, "habilidadPK", "habilidadPK").ToList();
            items.Insert(0, (new SelectListItem { Text = "Todas habilidades", Value = "Todas" }));
            ViewData["habilidades"] = items;

            return View();
        }

        //PartialView para mostrar resultados consultas cantidad de desarrolladores para todas las habilidades. 
        public PartialViewResult MostrarResultadoHabilidades()
        {
            DateTime today = DateTime.Now.Date;
            var CONSULTAH =
               from H in db.HABILIDADES
               join E in db.EMPLEADO
               on H.cedulaEmpleadoPK equals E.cedulaPK
               where E.fechaDespido == null
               select new CONSULTAS
               {
                   modeloHabilidades = H,
                   modeloEmpleado = E
               } into t1
               group t1 by t1.modeloHabilidades.habilidadPK into g
               select new Group<string, CONSULTAS> { Key = g.Key, Values = g, suma = g.Count(), avg = Math.Abs(Math.Round(g.Average(x => x.modeloEmpleado.fechaContratacion.Year - today.Year))) };

            return PartialView(CONSULTAH.ToList());
        }

        //PartialView para mostrar resultados consultas cantidad de desarrolladores para una habilidad especificica. 
        public PartialViewResult MostrarResultadoHabilidad(string habilidadPK)
        {
            DateTime today = DateTime.Now.Date;
            var CONSULTAH =
               from H in db.HABILIDADES
               join E in db.EMPLEADO
               on H.cedulaEmpleadoPK equals E.cedulaPK
               where E.fechaDespido == null
               where H.habilidadPK == habilidadPK
               select new CONSULTAS
               {
                   modeloHabilidades = H,
                   modeloEmpleado = E
               } into t1
               group t1 by t1.modeloHabilidades.habilidadPK into g
               select new Group<string, CONSULTAS> { Key = g.Key, Values = g, suma = g.Count(), avg = Math.Abs(Math.Round(g.Average(x => x.modeloEmpleado.fechaContratacion.Year - today.Year))) };

            return PartialView(CONSULTAH.ToList());

        }

        //-------------------------Celeste fin-------------------------

        //public ActionResult TotalHorasRequerimiento() {

        public ActionResult PeriodosDesocupacion() {
            return View();
        }








        //-------------------------JOHN COMIENZO-------------------------


        public ActionResult ComparacionDuracionRequerimientoComplejidad() {


            ViewBag.complejidad = new SelectList(db.REQUERIMIENTO, "complejidad", "complejidad");
            return View();
        }


        public PartialViewResult GetRequerimientoComplejidad(string complex) {

            var CONSULTAS =
            from req in db.REQUERIMIENTO
            select new CONSULTAS
            {
                modeloRequerimiento = req,
            } into t1
            group t1 by t1.modeloRequerimiento.complejidad into g
            select new Group<string, CONSULTAS> { Key = g.Key, Values = g };

            return PartialView(CONSULTAS.ToList());
        }

        public ActionResult RequerimientosTerminadosEjecucion()
        {
            return View();
        }



        //-------------------------JOHN FIN-------------------------




       

        public ActionResult EstadoResponsablesRequerimientos() {
            return View();
        }


    }
}
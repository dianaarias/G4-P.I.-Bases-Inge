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


        //-------------------------Fitzberth COMIENZO-------------------------


        //Consulta Desarrolladores Asginados y Disponibles
        public ActionResult DesarrolladoresAsignadosDisponibles() {
            var queryAsig = from req in db.REQUERIMIENTO // lista de desarrolladores asignados y su fecha pronta a desocupar
                            join emp in db.EMPLEADO
                            on req.cedulaDesarrolladorFK equals emp.cedulaPK
                            join equipo in db.ROL
                            on emp.cedulaPK equals equipo.cedulaPK
                            join proy in db.PROYECTO
                            on equipo.idProyectoPK equals proy.idProyectoPK 
                            where proy.fechaFin == null //solo proyectos sin terminar
                            select new DesarrolladoresAsigDisp { NombreEmp = emp.nombre + " " + emp.apellido1 + " " + emp.apellido2, NombreProy = proy.nombre, 
                               FechaInicio = proy.fechaInicio, FechaEstDesocup = DbFunctions.AddDays(proy.fechaInicio, proy.duracionEstimada/8) };
            ViewBag.EmpDesoc = db.EMPLEADO.Where(x => x.disponibilidad == true); //Lista de desarrolladores disponibles
            
            return View(queryAsig.Distinct().AsEnumerable());
        }
        //Consulta ehoras estimadas y reales de todos los proyectos
        public ActionResult HorasEstRealProy()
        {
            ViewBag.proyectos = new SelectList(db.PROYECTO.Where(x =>x.fechaFin != null ), "idProyectoPK", "nombre"); //Viewbag que contiene todos los proyectos finalizados
            var horasTot = db.PROYECTO.Where(x => x.fechaFin != null)
                                      .Join(db.MODULO, //Join tabla proyecto con modulo
                                                proy => proy.idProyectoPK,
                                                modu => modu.idProyectoPK,
                                                (proy, modu) => new { proy, modu })
                                      .Join(db.REQUERIMIENTO, //Join tabla modulo con requerimiento
                                              modu => new { modu.modu.idModuloPK, modu.modu.idProyectoPK },
                                              req => new { req.idModuloPK, req.idProyectoPK },
                                              (modu, req) => new { modu, req })
                                      .GroupBy(s => new { s.modu.proy.nombre }) //group by por nombre de proyecto
                                      .Select(g => new HorasEstRealProy {
                                          NombreProy = g.Key.nombre, // selecciona nombre
                                          HorasEst = g.Sum(x => x.req.duracionEstimada), //suma de duracion estimada  de todos los req
                                          HorasReal = g.Sum(x => x.req.duracionReal), //suma de duracion real de todos los req
                                          DiffHoras = g.Sum(x => x.req.duracionEstimada) - g.Sum(x => x.req.duracionReal) //diferencia entre duracion estimada y real
                                      });
            return View(horasTot.Distinct().AsEnumerable());
        }
        //horas estimadas y reales de un proyecto en especifico.
        public PartialViewResult HorasEstReal(int? idProyectoPK)
        {
            var horasTot = db.PROYECTO.Where(x => x.fechaFin != null)// solo proyectos finalizados
                                      .Where(x=> x.idProyectoPK == idProyectoPK) // el proyecto debe ser el seleccionado anteriormente
                                      .Join(db.MODULO, //join de proyecto con modulo
                                                proy => proy.idProyectoPK,
                                                modu => modu.idProyectoPK,
                                                (proy, modu) => new { proy, modu })
                                      .Join(db.REQUERIMIENTO,// join de modulo con requerimiento
                                              modu => new { modu.modu.idModuloPK, modu.modu.idProyectoPK },
                                              req => new { req.idModuloPK, req.idProyectoPK },
                                              (modu, req) => new { modu, req })
                                      .GroupBy(s => new { s.modu.proy.nombre })// group by por el nombre de proyecto
                                      .Select(g => new HorasEstRealProy
                                      {
                                          NombreProy = g.Key.nombre, // nombre del proyecto
                                          HorasEst = g.Sum(x => x.req.duracionEstimada), // suma de duraciones estimadas
                                          HorasReal = g.Sum(x => x.req.duracionReal), //suma de duraciones reales
                                          DiffHoras = g.Sum(x => x.req.duracionEstimada) - g.Sum(x => x.req.duracionReal) //diferencia de las duraciones totales
                                      });
            return PartialView(horasTot.Distinct().AsEnumerable());
        }

        //Consulta Estado y Responsables para cada requerimiento del proyecto de un cliente.
        public ActionResult EstadoResponsablesRequerimientos()
        {
            ViewBag.clientes = new SelectList(db.CLIENTE, "cedulaPK", "name"); //Viewbag con la lista de clientes
            var query = from req in db.REQUERIMIENTO
                        join emp in db.EMPLEADO
                        on req.cedulaDesarrolladorFK equals emp.cedulaPK
                        join equipo in db.ROL
                        on emp.cedulaPK equals equipo.cedulaPK
                        join proy in db.PROYECTO
                        on equipo.idProyectoPK equals proy.idProyectoPK
                        join cli in db.CLIENTE
                        on proy.cedulaClienteFK equals cli.cedulaPK
                        select new ListaDesResp
                        {
                            NombreCli = cli.name + " " + cli.apellido1 + " " + cli.apellido2,
                            NombreProy = proy.nombre,
                            NombreReq = req.nombre,
                            NombreResp = emp.nombre + " " + emp.apellido1 + " " + emp.apellido2,
                            EstadoReq = req.estado
                        };
            return View(query.Distinct().AsEnumerable());
        }
        //Vista parcial para desplegar los proyectos de un cliente en especifico
        public PartialViewResult GetListaProyectosCliente(string cedulaPK)
        {
            ViewBag.proyectos = new SelectList(db.PROYECTO.Where(x => x.cedulaClienteFK == cedulaPK),"idProyectoPK","nombre"); // Viewbag con los proyectos que posee el cliente con propositos de filtrado
            var queryEmp = from req in db.REQUERIMIENTO //Query para la lista de desarrolladores responsables
                           join emp in db.EMPLEADO
                           on req.cedulaDesarrolladorFK equals emp.cedulaPK
                           join equipo in db.ROL
                           on emp.cedulaPK equals equipo.cedulaPK
                           join proy in db.PROYECTO
                           on equipo.idProyectoPK equals proy.idProyectoPK
                           join cli in db.CLIENTE
                           on proy.cedulaClienteFK equals cli.cedulaPK
                           where cli.cedulaPK == cedulaPK
                           select new ListaDesResp
                           {
                               NombreProy = proy.nombre, // Nombre de proyectos
                               NombreReq = req.nombre,  // Nombre de requerimientos
                               EstadoReq = req.estado, // Estado del requerimiento
                               NombreResp = emp.nombre + " " + emp.apellido1 + " " + emp.apellido2 //Responsable del requerimiento
                           };

            return PartialView(queryEmp);
        }
        //Vista parcial con la lista de desarrolladores de un proyecto en especifico despues de haber filtrado el cliente.
        public PartialViewResult GetListaDesarolladoresResp(int idProyecto)
        {
            var query = from req in db.REQUERIMIENTO //query que busca todos los responsables en un equipo en especifico
                        join emp in db.EMPLEADO
                        on req.cedulaDesarrolladorFK equals emp.cedulaPK
                        where req.idProyectoPK == idProyecto
                        select new ListaDesResp 
                        {   NombreReq = req.nombre, //nombre del requerimiento 
                            EstadoReq = req.estado, //estado del requerimiento
                            NombreResp = emp.nombre+" " + emp.apellido1 + " "+ emp.apellido2 //Nombre del responsable del requerimiento
                        };
            return PartialView(query.Distinct().AsEnumerable());
        }

        //-------------------------Fitzberth fin-------------------------


        //-------------------------Celeste COMIENZO-------------------------

        //Consulta comparación entre duración estimada y real por cada requerimiento para un desarrollador específico.
        //___________________________________________________________________________________________________________
        public ActionResult ComparacionDuracionRequerimientos()
        {
            var query =
               from emp in db.EMPLEADO
               select new { nombreEmp = emp.nombre +" " + emp.apellido1, emp.cedulaPK, emp.apellido1};
            
            ViewBag.empleados = new SelectList(query, "cedulaPK", "nombreEmp");
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
            var query =
               from emp in db.EMPLEADO
               select new { nombreEmp = emp.nombre + " " + emp.apellido1, emp.cedulaPK, emp.apellido1 };

            ViewBag.empleados = new SelectList(query, "cedulaPK", "nombreEmp");
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
               where req.fechaFin != null
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

        //-------------------------DIANA COMIENZO-------------------------

        //Esta consulta retorna los periodos de desocupación de los empleados de la empresa en un rango
        //específico de tiempo, junto con la cantidad total de días de desocupación.
        //Es una consulta avanzada de baja frecuencia, cuyo objetivo es el de realizar una comparación
        //entre los periodos de desocupación de los empleados de manera que la empresa pueda determinar si
        //existen empleados que estén generando pérdidas económicas para la empresa.
        
        
        public ActionResult getPeriodosDesocupacion(DateTime fechaInicioR, DateTime fechaFinR) {

            //Lista de empleados sobre la que se iterará 
            List<EMPLEADO> empleados = db.EMPLEADO.ToList();
            List <SP_PeriodosDesocupacion_Result> resultados = new List<SP_PeriodosDesocupacion_Result>();
            //DateTime fechaInicioR = new DateTime();
            //DateTime fechaFinR = new DateTime();
            for (int i = 0; i < empleados.Count(); ++i)
            {
                var result = db.SP_PeriodosDesocupacion(empleados[i].cedulaPK, fechaInicioR, fechaFinR).Single();
                resultados.Add(result);
            }


            return View(resultados.Distinct().AsEnumerable());
        }

        //-------------------------DIANA FIN------------------------------






    }
}
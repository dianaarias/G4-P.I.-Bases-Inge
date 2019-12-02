﻿using System;
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
                            where proy.fechaFin != null //solo proyectos sin terminar
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
            return View();
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

            return PartialView(queryEmp.Distinct().AsEnumerable());
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

        //public ActionResult: ComparacionDuracionRequerimiento() 
        //Comparacíón de las duraciones reales vs estimadas de los requerimientos de un desarrollador 
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




        public ActionResult ConocimientosFrecuentes() {
            return View();
        }



    }
}
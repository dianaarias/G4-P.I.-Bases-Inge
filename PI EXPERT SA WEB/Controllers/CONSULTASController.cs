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

        public ActionResult DesarrolladoresAsignadosDisponibles() {
            return View();
        }

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
            //var CONSULTAS =
            //   from req in db.REQUERIMIENTO
            //   join mod in db.MODULO
            //   on req.idModuloPK equals mod.idModuloPK
            //   join proy in db.PROYECTO
            //   on mod.idProyectoPK equals proy.idProyectoPK
            //   join rol in db.ROL
            //   on proy.idProyectoPK equals rol.idProyectoPK
            //   join empl in db.EMPLEADO
            //   on rol.cedulaPK equals empl.cedulaPK
            //   where req.cedulaDesarrolladorFK == cedulaPk
            //   where req.fechaFin == null
            //   group proy by proy.nombre into g
            //   select new CONSULTAS { modeloRequerimiento = req, modeloModulo = mod, modeloProyecto = proy };
            //   select new Group<string, PROYECTO> { Key = g.Key, Values = g  };

            var CONSULTAS =

               from req in db.REQUERIMIENTO
               join mod in db.MODULO
               on req.idModuloPK equals mod.idModuloPK
               join proy in db.PROYECTO
               on mod.idProyectoPK equals proy.idProyectoPK
               join rol in db.ROL
               on proy.idProyectoPK equals rol.idProyectoPK
               join empl in db.EMPLEADO
               on rol.cedulaPK equals empl.cedulaPK
               where req.cedulaDesarrolladorFK == cedulaPk

               //where req.fechaFin == null
               select new CONSULTAS
               {
                   modeloRequerimiento = req,
                   modeloModulo = mod,
                   modeloProyecto = proy,
                   modeloRol = rol,
                   modeloEmpleado = empl 
               } into t1
               group t1 by t1.modeloProyecto.nombre into g
               select new Group<string, CONSULTAS> { Key = g.Key, Values = g };
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
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

        public ActionResult ComparacionDuracionRequerimientos() {
            return View();
        }

        public ActionResult TotalHorasRequerimiento() {
            return View();
        }

        public ActionResult HistorialDesarrollador() {
            return View();
        }

        public ActionResult PeriodosDesocupacion() {
            return View();
        }

        public ActionResult ComparacionDuracionRequerimientoComplejidad() {


            ViewBag.complejidad = new SelectList(db.REQUERIMIENTO, "complejidad");
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
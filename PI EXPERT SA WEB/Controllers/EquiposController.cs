using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PI_EXPERT_SA_WEB.Models;

namespace PI_EXPERT_SA_WEB.Controllers
{
    public class EquiposController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();
        // GET: Equipos
        public ActionResult Equipo()
        {
            ModeloEquipo modelo = new ModeloEquipo();
            var empleados = db.EMPLEADO.Where(t => t.disponibilidad == true).ToList();
            return View(empleados.ToList());
        }
    }
}
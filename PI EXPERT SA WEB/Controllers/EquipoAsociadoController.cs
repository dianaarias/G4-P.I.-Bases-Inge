using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PI_EXPERT_SA_WEB.Models;

namespace PI_EXPERT_SA_WEB.Controllers
{
    
    public class EquipoAsociadoController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: EquipoAsociado
        public ActionResult Index()
        {
            ModeloIntermedioRolEmpleadoHabilidades modelo = new ModeloIntermedioRolEmpleadoHabilidades();
            modelo.empleados = db.EMPLEADO.ToList();
            modelo.habilidades = db.HABILIDADES.ToList();
            //modelo.rol = db.ROL;
            return View(modelo);
        }
    }
}
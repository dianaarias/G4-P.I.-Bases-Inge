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
    public class RHVersion2Controller : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: RHVersion2
        public ActionResult Index()
        {
            ModeloIntermedio modelo = new ModeloIntermedio();
            modelo.listaEmpleados = db.EMPLEADO.ToList();
            modelo.listaHabilidades = db.HABILIDADES.ToList();
            return View(modelo);
        }
    }
}
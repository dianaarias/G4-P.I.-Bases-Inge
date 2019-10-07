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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModeloIntermedio modelo)
        {
            if (ModelState.IsValid)
            {
                db.EMPLEADO.Add(modelo.modeloEmpleado);
                db.SaveChanges();
                if (modelo.modeloHabilidades1.habilidadPK != null)
                {
                    modelo.modeloHabilidades1.cedulaEmpleadoPK = modelo.modeloEmpleado.cedulaPK;
                    db.HABILIDADES.Add(modelo.modeloHabilidades1);
                }
                if (modelo.modeloHabilidades2.habilidadPK != null)
                {
                    modelo.modeloHabilidades2.cedulaEmpleadoPK = modelo.modeloEmpleado.cedulaPK;
                    db.HABILIDADES.Add(modelo.modeloHabilidades2);
                }
                if (modelo.modeloHabilidades3.habilidadPK != null)
                {
                    modelo.modeloHabilidades3.cedulaEmpleadoPK = modelo.modeloEmpleado.cedulaPK;
                    db.HABILIDADES.Add(modelo.modeloHabilidades3);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Debe completar toda la información necesaria.");
                return View(modelo);
            }
        }

        public ActionResult Details(string id)
        {
            ModeloIntermedio modelo = new ModeloIntermedio();
            modelo.listaEmpleados = db.EMPLEADO.ToList();
            modelo.listaHabilidades = db.HABILIDADES.ToList();
            return View(modelo);
        }

    }
}
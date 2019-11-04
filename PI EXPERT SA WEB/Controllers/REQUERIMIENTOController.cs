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
    public class REQUERIMIENTOController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: REQUERIMIENTO
        public ActionResult Index()
        {
            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }

       
        public PartialViewResult GetListaModulos(int? idProyectoPK)
        {
            List<MODULO> modulo= db.MODULO.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.modulos = new SelectList(modulo, "idModuloPK", "nombre");

            List<REQUERIMIENTO> requerimiento;
            requerimiento = db.REQUERIMIENTO.Where(x => x.idProyectoPK == idProyectoPK).ToList();

            return PartialView(requerimiento);
        }

        public ActionResult IndexDevelopersRequirements()
        {





            //ViewBag.dropDowmEmpleados = new SelectList(Things, "cedulaPK", "nombre");
            //ViewBag.dropDowmEmpleados = Things;

            //ViewBag.dropDowmEmpleados = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");

            //ViewBag.dropDowmEmpleados = new SelectList(db.ROL, "idProyectoPK","cedulaPK");

            //List<EMPLEADO> modulo = db.EMPLEADO.Where(x => x.cedulaPK == ViewBag.dropDowmEmpleado).ToList();
            //ViewBag.modulos = new SelectList(modulo, "cedulaPK", "nombre");

            ViewBag.dropDowmProyecto = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }


        public PartialViewResult GetListaModulos(int? idProyectoPK)
        {
            List<REQUERIMIENTO> requerimiento;
            requerimiento = db.REQUERIMIENTO.Where(x => x.idProyectoPK == idProyectoPK && x.idModuloPK == idModuloPK).ToList();
            return PartialView(requerimiento);
        }



        public PartialViewResult DropDownModulo(int? idProyectoPK) {
            List<MODULO> modulo = db.MODULO.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.modulos = new SelectList(modulo, "idModuloPK", "nombre");
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return PartialView();
        }

        public PartialViewResult GetDevelopers(int? idProyectoPK)
        {
            //var Things = from e in db.EMPLEADO
            //             join r in db.ROL
            //             on e.cedulaPK equals r.cedulaPK
            //             where r.idProyectoPK == idProyectoPK
            //             select new { e, r };

            //ViewBag.empleadospro = new SelectList(Things, "cedulaPK", "nombre");
            //ViewBag.empleadospro = Things.ToList();

            List<ROL> equipo = db.ROL.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.empleadospro = new SelectList(equipo, "idProyectoPK", "cedulaPK");

            return PartialView();
        }


        public PartialViewResult MostrarRequerimientos(int? idProyectoPK, int? idModuloPK)
        {


            return PartialView(requerimiento);
        }

        public PartialViewResult MostrarRequerimientosDesarrollador(string cedulaPk)
        {

            List<REQUERIMIENTO> requerimiento = db.REQUERIMIENTO.Where(x => x.cedulaDesarrolladorFK == cedulaPk).ToList();

            return PartialView(requerimiento);
        }

        // GET: REQUERIMIENTO/Details/5
        public ActionResult Details(int? idProyecto, int? idModulo, int? idRequerimiento)
        {
            if (idProyecto == null || (idModulo == null || idRequerimiento == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REQUERIMIENTO rEQUERIMIENTO = db.REQUERIMIENTO.Find(idRequerimiento, idModulo, idProyecto);
            if (rEQUERIMIENTO == null)
            {
                return HttpNotFound();
            }

            List<PROYECTO> proyecto = db.PROYECTO.Where(x => x.idProyectoPK == idProyecto ).ToList();
            ViewBag.proyecto = new SelectList(proyecto, "idProyectoPK", "nombre");
            //List<MODULO> modulos = db.MODULO.Where(x => (x.idProyectoPK == idProyecto && x.idModuloPK == idModulo)).ToList();
            //ViewBag.modulo = new SelectList(modulos, "idModuloPK", "nombre");
            return View(rEQUERIMIENTO);
        }


        // GET: REQUERIMIENTO/Create
        public ActionResult Create(int? idProyecto, int? idModulo)
        {
            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");


            List<MODULO> modulo = db.MODULO.Where(x => x.idProyectoPK == 0).ToList();
            ViewBag.idModuloPK = new SelectList(modulo, "idModuloPK", "nombre");


            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }


        // POST: REQUERIMIENTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRequerimientoPK,idModuloPK,idProyectoPK,estado,fechaEstado,nombre,complejidad,duracionEstimada,cedulaDesarrolladorFK,fechaInicio,fechaFin")] REQUERIMIENTO rEQUERIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.REQUERIMIENTO.Add(rEQUERIMIENTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rEQUERIMIENTO.cedulaDesarrolladorFK);
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre", rEQUERIMIENTO.idModuloPK);
            return View(rEQUERIMIENTO);
        }

        // GET: REQUERIMIENTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REQUERIMIENTO rEQUERIMIENTO = db.REQUERIMIENTO.Find(id);
            if (rEQUERIMIENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rEQUERIMIENTO.cedulaDesarrolladorFK);
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre", rEQUERIMIENTO.idModuloPK);
            return View(rEQUERIMIENTO);
        }

        // POST: REQUERIMIENTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRequerimientoPK,idModuloPK,idProyectoPK,estado,fecha,nombre,complejidad,duracionEstimada,cedulaDesarrolladorFK")] REQUERIMIENTO rEQUERIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEQUERIMIENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rEQUERIMIENTO.cedulaDesarrolladorFK);
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre", rEQUERIMIENTO.idModuloPK);
            return View(rEQUERIMIENTO);
        }

        // GET: REQUERIMIENTO/Delete/5
        public ActionResult Delete(int? idProyecto, int? idModulo, int? idRequerimiento)
        {
            if (idProyecto == null || (idModulo == null || idRequerimiento == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //REQUERIMIENTO rEQUERIMIENTO = db.REQUERIMIENTO.Find(idProyecto,idModulo,idRequerimiento);
            REQUERIMIENTO rEQUERIMIENTO = db.REQUERIMIENTO.Find(idRequerimiento, idModulo, idProyecto);
            if (rEQUERIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(rEQUERIMIENTO);
        }

        // POST: REQUERIMIENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idProyecto, int idModulo, int idRequerimiento)
        {
            REQUERIMIENTO rEQUERIMIENTO = db.REQUERIMIENTO.Find(idRequerimiento, idModulo, idProyecto);
            db.REQUERIMIENTO.Remove(rEQUERIMIENTO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

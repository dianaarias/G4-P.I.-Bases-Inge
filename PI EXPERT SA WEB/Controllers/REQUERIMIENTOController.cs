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

            ViewBag.Proyectos = new SelectList(getListaProyecto(), "idProyectoPK", "nombre");
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre");
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            var rEQUERIMIENTO = db.REQUERIMIENTO.Include(r => r.EMPLEADO).Include(r => r.MODULO);
            return View(rEQUERIMIENTO.ToList());
        }

        public List<PROYECTO> getListaProyecto()
        {
            List<PROYECTO> proyectos = db.PROYECTO.ToList();
            return proyectos;
        }

        public ActionResult getListaModulos(int idProyectoPK)
        {
            List<MODULO> modulos = db.MODULO.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.modulos = new SelectList(modulos, "idModuloPK", "nombre");
            return PartialView("MostrarModulos");
        }

        // GET: REQUERIMIENTO/Details/5
        public ActionResult Details(int? id)
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
            return View(rEQUERIMIENTO);
        }

        // GET: REQUERIMIENTO/Create
        public ActionResult Create(int? idProyecto, int? idModulo)
        {
            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre");
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
            REQUERIMIENTO rEQUERIMIENTO = db.REQUERIMIENTO.Find(idProyecto,idModulo,idRequerimiento);
            if (rEQUERIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(rEQUERIMIENTO);
        }

        // POST: REQUERIMIENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REQUERIMIENTO rEQUERIMIENTO = db.REQUERIMIENTO.Find(id);
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

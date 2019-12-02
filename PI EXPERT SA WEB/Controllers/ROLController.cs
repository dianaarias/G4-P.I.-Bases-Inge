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
    public class ROLController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: ROL
        public ActionResult Index()
        {
            //Se crea una lista que contenga los proyectos en la tabla de rol con su respectivo nombre en la tabla de proyecto
            //Se filtra para incluir solo los proyectos que tengan un equipo asignado
            var query =
                from pro in db.PROYECTO
                join rol in db.ROL on pro.idProyectoPK equals rol.idProyectoPK
                select new { rol.idProyectoPK, pro.nombre };

            ViewBag.proyectos = new SelectList(query.Distinct(), "idProyectoPK", "nombre");
            return View();
        }

        // GET: ROL/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL rOL = db.ROL.Find(id);
            if (rOL == null)
            {
                return HttpNotFound();
            }
            return View(rOL);
        }

        // GET: ROL/Create
        public ActionResult Create()
        {
            //Lista de empleados que están disponibles, es decir, que no forman parte de ningún equipo 
            List<EMPLEADO> empleadosDisponibles;
            empleadosDisponibles = db.EMPLEADO.Where(x => x.disponibilidad == true).ToList();
            ViewBag.cedulaPK = new SelectList(empleadosDisponibles, "cedulaPK", "nombre");

            //Consulta de proyectos sin equipo, es decir, proyectos cuyo ID no exista en la tabla ROL
            var query = (from proyecto in db.PROYECTO
                           where !db.ROL.Any(m => m.idProyectoPK == proyecto.idProyectoPK)
                           select proyecto);

            ViewBag.proyectosSinEquipo = new SelectList(query, "idProyectoPK", "nombre");
            //ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }

    
        // POST: ROL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string[] miembrosEquipo, int proyectoEquipo)//[Bind(Include = "cedulaPK,idProyectoPK,tipoRol")] ROL rOL)
        {
            if (ModelState.IsValid)
            {
                //db.ROL.Add(rOL);
                foreach (var developer in miembrosEquipo)
                {
                    db.ROL.Add(new ROL
                    {
                        cedulaPK = developer,
                        idProyectoPK = proyectoEquipo,
                        tipoRol = "Desarrollador"
                    });
                }
                db.SaveChanges();
                return Json(new
                {
                    isRedirect = false
                });
                
            }
            return RedirectToAction("Index");

            List<EMPLEADO> empleadosDisponibles;
            empleadosDisponibles = db.EMPLEADO.Where(x => x.disponibilidad == true).ToList();
            ViewBag.cedulaPK = new SelectList(empleadosDisponibles, "cedulaPK", "nombre");

            //Consulta de proyectos sin equipo, es decir, proyectos cuyo ID no exista en la tabla ROL
            var query = (from proyecto in db.PROYECTO
                         where !db.ROL.Any(m => m.idProyectoPK == proyecto.idProyectoPK)
                         select proyecto);

            ViewBag.proyectosSinEquipo = new SelectList(query, "idProyectoPK", "nombre");
            //ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre", rOL.idProyectoPK);
            //return View(rOL);
            return View();
        }

        // GET: ROL/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL rOL = db.ROL.Find(id);
            if (rOL == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rOL.cedulaPK);
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre", rOL.idProyectoPK);
            return View(rOL);
        }

        // POST: ROL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedulaPK,idProyectoPK,tipoRol")] ROL rOL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rOL.cedulaPK);
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre", rOL.idProyectoPK);
            return View(rOL);
        }

        // GET: ROL/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL rOL = db.ROL.Find(id);
            if (rOL == null)
            {
                return HttpNotFound();
            }
            return View(rOL);
        }

        // POST: ROL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ROL rOL = db.ROL.Find(id);
            db.ROL.Remove(rOL);
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

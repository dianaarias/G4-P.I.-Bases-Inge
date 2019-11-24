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



        public ActionResult Index()
        {
            //Se crea una lista que contenga los proyectos en la tabla de rol con su respectivo nombre en la tabla de proyecto
            var query =
                from pro in db.PROYECTO
                join rol in db.ROL on pro.idProyectoPK equals rol.idProyectoPK
                select new { rol.idProyectoPK, pro.nombre };

            ViewBag.proyectos = new SelectList(query.Distinct(), "idProyectoPK", "nombre");


            return View();
        }



        public PartialViewResult Equipo(int? idProyectoPK) {


            //modelo de rol y empleado
            var query = from rol in db.ROL
                        join emp in db.EMPLEADO on rol.cedulaPK equals emp.cedulaPK
                        where rol.idProyectoPK == idProyectoPK
                        select new CONSULTAS { modeloRol = rol, modeloEmpleado = emp };


            //tempdata que almacena el nombre y la cedula del lider del proyecto actual
            TempData.Remove("liderNombre");
            TempData.Remove("liderCedula");
            TempData.Add("liderCedula", db.PROYECTO.Find(idProyectoPK).cedulaLiderFK);
            TempData.Add("liderNombre", db.EMPLEADO.Find(db.PROYECTO.Find(idProyectoPK).cedulaLiderFK).nombre);

            //tempdata que alamcena el nombre y el id del proyecto actual
            TempData.Remove("proyectoID");
            TempData.Remove("proyectoNombre");
            TempData.Add("proyectoID", idProyectoPK);
            TempData.Add("proyectoNombre", db.PROYECTO.Find(idProyectoPK).nombre);


            return PartialView(query);
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
            ViewBag.cedulaPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }

        // POST: ROL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedulaPK,idProyectoPK,tipoRol,numEquipo")] ROL rOL)
        {
            if (ModelState.IsValid)
            {
                db.ROL.Add(rOL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedulaPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rOL.cedulaPK);
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "cedulaClienteFK", rOL.idProyectoPK);
            return View(rOL);
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
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "cedulaClienteFK", rOL.idProyectoPK);
            return View(rOL);
        }

        // POST: ROL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedulaPK,idProyectoPK,tipoRol,numEquipo")] ROL rOL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rOL.cedulaPK);
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "cedulaClienteFK", rOL.idProyectoPK);
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

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
    public class HABILIDADES2Controller : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: HABILIDADES2
        public ActionResult Index(string id)
        {
            HABILIDADES modelo = new HABILIDADES();
            List<HABILIDADES> aList;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aList = new List<HABILIDADES>();
            modelo.listaHabilidades = db.HABILIDADES.ToList();
            for (int j = 0; j < modelo.listaHabilidades.Count; j++)
            {
                if (id.Equals(modelo.listaHabilidades.ElementAt(j).cedulaEmpleadoPK))
                {
                    aList.Add(modelo.listaHabilidades.ElementAt(j));
                }
            }
            return View(aList.ToList());
        }

        // GET: HABILIDADES2/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABILIDADES hABILIDADES = db.HABILIDADES.Find(id);
            if (hABILIDADES == null)
            {
                return HttpNotFound();
            }
            return View(hABILIDADES);
        }

        // GET: HABILIDADES2/Create
        public ActionResult Create()
        {
            ViewBag.cedulaEmpleadoPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");
            return View();
        }

        // POST: HABILIDADES2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedulaEmpleadoPK,habilidadPK")] HABILIDADES hABILIDADES)
        {
            if (ModelState.IsValid)
            {
                db.HABILIDADES.Add(hABILIDADES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedulaEmpleadoPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", hABILIDADES.cedulaEmpleadoPK);
            return View(hABILIDADES);
        }

        // GET: HABILIDADES2/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABILIDADES hABILIDADES = db.HABILIDADES.Find(id);
            if (hABILIDADES == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaEmpleadoPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", hABILIDADES.cedulaEmpleadoPK);
            return View(hABILIDADES);
        }

        // POST: HABILIDADES2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedulaEmpleadoPK,habilidadPK")] HABILIDADES hABILIDADES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hABILIDADES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaEmpleadoPK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", hABILIDADES.cedulaEmpleadoPK);
            return View(hABILIDADES);
        }

        // GET: HABILIDADES2/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABILIDADES hABILIDADES = db.HABILIDADES.Find(id);
            if (hABILIDADES == null)
            {
                return HttpNotFound();
            }
            return View(hABILIDADES);
        }

        // POST: HABILIDADES2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HABILIDADES hABILIDADES = db.HABILIDADES.Find(id);
            db.HABILIDADES.Remove(hABILIDADES);
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

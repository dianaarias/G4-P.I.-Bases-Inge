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
    public class EMPLEADOesController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: EMPLEADOes
        // Genera la vista index con todos los empleados o con el nombre del empleado a buscar
        public ActionResult Index(string busqueda)
        {
            //Se usa el atributo busqueda para filtrar por nombre a los empleados
            return View(db.EMPLEADO.Where(x => x.nombre.Contains(busqueda) || busqueda == null).ToList());

            //return View(db.EMPLEADO);
        }

        public ActionResult prueba() {
            return View();
        }

        // GET: EMPLEADOes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // GET: EMPLEADOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EMPLEADOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedulaPK,nombre,apellido1,apellido2,correo,telefono,fechaContratacion,fechaDespido,tipoUsuario,disponibilidad,provincia,canton,distrito,fechaNacimiento")] EMPLEADO eMPLEADO)
        {
            if (ModelState.IsValid)
            {
                if (!db.EMPLEADO.Any(model => model.cedulaPK == eMPLEADO.cedulaPK))
                {
                    db.EMPLEADO.Add(eMPLEADO);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("cedulaPK", "La cédula ya se encuentra en el sistema");
                }
            }

            return View(eMPLEADO);
        }

        // GET: EMPLEADOes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // POST: EMPLEADOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedulaPK,nombre,apellido1,apellido2,correo,telefono,fechaContratacion,fechaDespido,tipoUsuario,disponibilidad,provincia,canton,distrito,fechaNacimiento")] EMPLEADO eMPLEADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLEADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eMPLEADO);
        }

        // GET: EMPLEADOes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // POST: EMPLEADOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            db.EMPLEADO.Remove(eMPLEADO);
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

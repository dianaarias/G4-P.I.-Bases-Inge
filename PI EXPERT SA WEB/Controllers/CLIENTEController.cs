﻿using System;
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
    public class CLIENTEController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: CLIENTE
        // Genera la vista index con todos los clientes o Con el nombre de los clientes a buscar
        public ActionResult Index(string busqueda)
        {
            //Se usa el atributo busqueda para filtrar por nombre a los clientes, en caso de no filtrar nada se mostraran todos
            return View(db.CLIENTE.Where(x=>x.name.Contains(busqueda) || busqueda == null).ToList());
        }

        // GET: CLIENTE/Details/5
        // Detalles para cada cliente, no se hizo ninguna modificacion en base a la plantilla
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTE);
        }

        // GET: CLIENTE/Create
        // Crear nuevo cliente, no se hizo ninguna modificacion en base a la plantilla
        public ActionResult Create()
        {
            return View();
        }

        // POST: CLIENTE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedulaPK,name,apellido1,apellido2,correo,telefono,provincia,canton,distrito")] CLIENTE cLIENTE)
        {
            if (ModelState.IsValid)
            {
                if (!db.CLIENTE.Any(model => model.cedulaPK == cLIENTE.cedulaPK))
                {
                    db.CLIENTE.Add(cLIENTE);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("cedulaPK","La cédula ya se encuentra en el sistema");
                }
            }

            return View(cLIENTE);
        }

        // GET: CLIENTE/Edit/5
        // Editar un cliente, no se hizo ninguna modificacion en base a la plantilla
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTE);
        }

        // POST: CLIENTE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedulaPK,name,apellido1,apellido2,correo,telefono,provincia,canton,distrito")] CLIENTE cLIENTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cLIENTE);
        }

        // GET: CLIENTE/Delete/5
        // Borrar un cliente, no se hizo ninguna modificacion en base a la plantilla
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTE);
        }

        // POST: CLIENTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            db.CLIENTE.Remove(cLIENTE);
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

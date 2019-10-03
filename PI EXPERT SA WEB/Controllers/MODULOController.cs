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
    public class MODULOController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();

        // GET: MODULO
        public ActionResult Index()
        {
            var mODULO = db.MODULO.Include(m => m.PROYECTO);
            return View(mODULO.ToList());
        }

        // GET: MODULO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODULO mODULO = db.MODULO.Find(id);
            if (mODULO == null)
            {
                return HttpNotFound();
            }
            return View(mODULO);
        }

        // GET: MODULO/Create
        public ActionResult Create()
        {
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "cedulaClienteFK");
            return View();
        }

        // POST: MODULO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idModuloPK,idProyectoPK,nombre,fechaInicio")] MODULO mODULO)
        {
            if (ModelState.IsValid)
            {
                db.MODULO.Add(mODULO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "cedulaClienteFK", mODULO.idProyectoPK);
            return View(mODULO);
        }

        // GET: MODULO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODULO mODULO = db.MODULO.Find(id);
            if (mODULO == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "cedulaClienteFK", mODULO.idProyectoPK);
            return View(mODULO);
        }

        // POST: MODULO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idModuloPK,idProyectoPK,nombre,fechaInicio")] MODULO mODULO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mODULO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "cedulaClienteFK", mODULO.idProyectoPK);
            return View(mODULO);
        }

        // GET: MODULO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODULO mODULO = db.MODULO.Find(id);
            if (mODULO == null)
            {
                return HttpNotFound();
            }
            return View(mODULO);
        }

        // POST: MODULO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MODULO mODULO = db.MODULO.Find(id);
            db.MODULO.Remove(mODULO);
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
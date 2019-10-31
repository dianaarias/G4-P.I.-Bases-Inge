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
    public class MODULOController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();





        ////INDEX ORIGINAL
        ////GET: MODULO
        //public ActionResult Index()
        //{
        //    //ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "cedulaClienteFK");
        //    List<PROYECTO> proyectos = new PROYECTOController().GetProyecto();
        //    List<MODULO> modulos = db.MODULO.ToList();
        //    return View(modulos);
        //}




        //INDEX ORIGINAL
        //GET: MODULO
        public ActionResult Index()
        {
            ViewBag.bolsaDeProyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }


        public ActionResult ModuloPartialView(int? idProyectoPK)
        {
            var mODULO = db.MODULO;
            return View(mODULO.ToList());
        }




        //DETAILS ORIGINAL
        // GET: MODULO/Details/5
        public ActionResult Details(int? idModuloPK, int? idProyectoPK)
        {
            if (idModuloPK == null || idProyectoPK == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODULO mODULO = db.MODULO.Find(idModuloPK, idProyectoPK);
            if (mODULO == null)
            {
                return HttpNotFound();
            }
            return View(mODULO);
        }








        //CREATE ORIGINAL
        // GET: MODULO/Create
        public ActionResult Create()
        {
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }









        //POST CREATE ORIGINAL
        // POST: MODULO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProyectoPK,idModuloPK,nombre,fechaInicio")] MODULO mODULO)
        {
            if (ModelState.IsValid)
            {
                db.MODULO.Add(mODULO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre", mODULO.idProyectoPK);
            return View(mODULO);
        }










        //EDIT ORIGINAL
        // GET: MODULO/Edit/5
        public ActionResult Edit(int? idModuloPK, int? idProyectoPK)
        {
            if (idModuloPK == null || idProyectoPK == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODULO mODULO = db.MODULO.Find(idModuloPK, idProyectoPK);
            //List<MODULO> lmodulo = db.MODULO.Where(x => x.PROYECTO.idProyectoPK == idProyectoPK).ToList();

            //ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "idModuloPK", mODULO.idProyectoPK);
            return View(mODULO);

            //return View(lmodulo);
        }










        //POST EDIT ORIGINAL
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










        //DELETE ORIGINAL
        // GET: MODULO/Delete/5
        public ActionResult Delete(int? idModuloPK, int? idProyectoPK)
        {
            if (idModuloPK == null || idProyectoPK == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODULO mODULO = db.MODULO.Find(idModuloPK, idProyectoPK);
            if (mODULO == null)
            {
                return HttpNotFound();
            }
            return View(mODULO);
        }











        //POST DELETE ORIGINAL
        // POST: MODULO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idModuloPK, int? idProyectoPK)
        {
            MODULO mODULO = db.MODULO.Find(idModuloPK, idProyectoPK);
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

















        //Retorna una lista con los modulos del proyecto actual
        public List<MODULO> GetModulo(int? idProyectoPK) {

            List<MODULO> mlista = db.MODULO.Where(x => x.idProyectoPK == idProyectoPK).ToList();

            return mlista;
        }




        //Vista principal de una lista
        public ActionResult Lista() {
            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK");
            return View();
        }




        public ActionResult ListaParcialModulos()
        {
            List<MODULO> m = db.MODULO.ToList();
            return PartialView(m);
        }


    }
}

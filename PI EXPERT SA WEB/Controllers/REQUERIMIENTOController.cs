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

            @TempData.Remove("proyectoID");
            @TempData.Remove("nombreProyecto");
            @TempData.Remove("moduloID");
            @TempData.Remove("nombreModulo");

            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }

       
        public PartialViewResult GetListaModulos(int? idProyectoPK)
        {
            List<MODULO> modulo= db.MODULO.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.modulos = new SelectList(modulo, "idModuloPK", "nombre");

            List<REQUERIMIENTO> requerimiento;
            requerimiento = db.REQUERIMIENTO.Where(x => x.idProyectoPK == idProyectoPK).ToList();

            List<PROYECTO> proyecto = db.PROYECTO.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.proyecto = new SelectList(proyecto, "idProyectoPK", "nombre");


            TempData.Remove("proyectoID");
            TempData.Add("proyectoID", idProyectoPK);

            if (idProyectoPK != null) {
                TempData.Remove("nombreProyecto");
                TempData.Add("nombreProyecto", db.PROYECTO.Find(idProyectoPK).nombre);
            } 

            var a = TempData.Peek("nombreProyecto");

            return PartialView(requerimiento);
        }


        public PartialViewResult MostrarRequerimientos(int? idProyectoPK, int? idModuloPK) {

            List<REQUERIMIENTO> requerimiento;
            requerimiento = db.REQUERIMIENTO.Where(x => x.idProyectoPK == idProyectoPK && x.idModuloPK == idModuloPK).ToList();

            List<PROYECTO> proyecto = db.PROYECTO.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.proyecto = new SelectList(proyecto, "idProyectoPK", "nombre");

            TempData.Remove("moduloID");
            TempData.Add("moduloID", idModuloPK);

            if (idModuloPK != null) {
                TempData.Remove("nombreModulo");
                TempData.Add("nombreModulo", db.MODULO.Find(idModuloPK, idProyectoPK).nombre);
            }
            return PartialView(requerimiento);
        }



        public ActionResult IndexDevelopersRequirements()
        {

            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }



        public PartialViewResult DropDownModulo(int? idProyectoPK) {
            List<MODULO> modulo = db.MODULO.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.modulos = new SelectList(modulo, "idModuloPK", "nombre");
            return PartialView();
        }





        public PartialViewResult GetDevelopers(int? idProyectoPK)
        {
            //List<EMPLEADO> desarrolladores = things as List<EMPLEADO>;   
            //TempData["empleadospro"] = things.ToList();


            //var things = 
            //             from r in db.ROL
            //             where r.idProyectoPK == idProyectoPK
            //             select r.EMPLEADO.nombre;


            //List<string> empleados = things.ToList();

            /*foreach (var r in db.ROL) {
                foreach (var e in db.EMPLEADO) {
                    if (r.idProyectoPK == idProyectoPK && r.cedulaPK == e.cedulaPK) {
                        em1 += db.EMPLEADO.Where(x => x.cedulaPK == r.cedulaPK).ToList();
                    }
                }
            }*/
            var query =
                from emp in db.EMPLEADO
                join rolEmp in db.ROL on emp.cedulaPK equals rolEmp.cedulaPK
                where rolEmp.idProyectoPK == idProyectoPK
                select new { emp.nombre, rolEmp.cedulaPK };

            //List<ROL> em = db.ROL.Join().Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.empleadospro = new SelectList(query, "cedulaPK", "nombre");

            return PartialView();
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

            //List<PROYECTO> proyecto = db.PROYECTO.Where(x => x.idProyectoPK == idProyecto ).ToList();
            //ViewBag.proyecto = new SelectList(proyecto, "idProyectoPK", "nombre");

            TempData.Remove("proyectoDetalle");
            TempData.Add("proyectoDetalle", db.PROYECTO.Find(idProyecto).nombre);

            //List<MODULO> modulos = db.MODULO.Where(x => (x.idProyectoPK == idProyecto && x.idModuloPK == idModulo)).ToList();
            //ViewBag.modulo = new SelectList(modulos, "idModuloPK", "nombre");
            return View(rEQUERIMIENTO);
        }


        // GET: REQUERIMIENTO/Create
        public ActionResult Create()
        {
            //if (idProyecto == null || idModulo == null )
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var a = TempData.Peek("proyectoID");
            var b = TempData.Peek("moduloID");
            var c = TempData.Peek("nombreProyecto");
            var d = TempData.Peek("nombreModulo");



            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");

            //ViewBag.idModuloPK = new SelectList(db.MODULO.ToList(), "idModuloPK", "nombre");
            //ViewBag.idProyectoPK = new SelectList(db.PROYECTO.ToList(), "idProyectoPK", "nombre");




            if (TempData.Peek("proyectoID") != null && TempData.Peek("moduloID") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("index");
            }
        }


        // POST: REQUERIMIENTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRequerimientoPK,idModuloPK,idProyectoPK,estado,fechaCreacion,nombre,complejidad,duracionEstimada,cedulaDesarrolladorFK,fechaInicio,fechaFin")] REQUERIMIENTO rEQUERIMIENTO)
        {

            var a = rEQUERIMIENTO.idRequerimientoPK;
            var b = rEQUERIMIENTO.idModuloPK;
            var c = rEQUERIMIENTO.idProyectoPK;
            var d = rEQUERIMIENTO.estado;
            var e = rEQUERIMIENTO.fechaCreacion;
            var f = rEQUERIMIENTO.nombre;
            var g = rEQUERIMIENTO.complejidad;
            var h = rEQUERIMIENTO.duracionEstimada;
            var i = rEQUERIMIENTO.cedulaDesarrolladorFK;
            var j = rEQUERIMIENTO.fechaInicio;
            var k = rEQUERIMIENTO.fechaFin;

            if (ModelState.IsValid)
            {
                db.REQUERIMIENTO.Add(rEQUERIMIENTO);
                db.SaveChanges();


                //TempData.Remove("proyectoID");
                //TempData.Add("proyectoID", null);

                //TempData.Remove("nombreProyecto");
                //TempData.Add("nombreProyecto", null);



                //TempData.Remove("moduloID");
                //TempData.Add("moduloID", null);

                //TempData.Remove("nombreModulo");
                //TempData.Add("nombreModulo", null);


                return RedirectToAction("Index");
            }

            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rEQUERIMIENTO.cedulaDesarrolladorFK);
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre", rEQUERIMIENTO.idModuloPK);
            return View(rEQUERIMIENTO);
        }

        // GET: REQUERIMIENTO/Edit/5
        public ActionResult Edit(int? idProyecto, int? idModulo, int? idRequerimiento)
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


            TempData.Remove("proyectoDetalle");
            TempData.Add("proyectoDetalle", db.PROYECTO.Find(idProyecto).nombre);

            TempData.Remove("moduloDetalle");
            TempData.Add("moduloDetalle", db.MODULO.Find(idModulo, idProyecto).nombre);

            //List<PROYECTO> proyecto = db.PROYECTO.Where(x => x.idProyectoPK == idProyecto).ToList();
            //ViewBag.proyecto = new SelectList(proyecto, "idProyectoPK", "nombre");
            //List<MODULO> modulos = db.MODULO.Where(x => (x.idProyectoPK == idProyecto && x.idModuloPK == idModulo)).ToList();
            //ViewBag.modulo = new SelectList(modulos, "idModuloPK", "nombre");
            ViewBag.desarrolladores = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");
            return View(rEQUERIMIENTO);
        }

        // POST: REQUERIMIENTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


            //[Bind(Include = "idRequerimientoPK,idModuloPK,idProyectoPK,estado,fechaEstado,nombre,complejidad,duracionEstimada,cedulaDesarrolladorFK,fechaInicio,fechaFin")] REQUERIMIENTO rEQUERIMIENTO

            //[Bind(Include = "idRequerimientoPK,idModuloPK,idProyectoPK,estado,fecha,nombre,complejidad,duracionEstimada,cedulaDesarrolladorFK")] REQUERIMIENTO rEQUERIMIENTO

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRequerimientoPK,idModuloPK,idProyectoPK,estado,fechaCreacion,nombre,complejidad,duracionEstimada,cedulaDesarrolladorFK,fechaInicio,fechaFin")] REQUERIMIENTO rEQUERIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEQUERIMIENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRequerimientoPK = new SelectList(db.REQUERIMIENTO, "idRequerimientoPK", "idRequerimientoPK", rEQUERIMIENTO.idRequerimientoPK);
            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rEQUERIMIENTO.cedulaDesarrolladorFK);
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre", rEQUERIMIENTO.idModuloPK);
            return View(rEQUERIMIENTO);
        }

        // GET: REQUERIMIENTO/Delete/5
        // Método para acceder a la vista de borrar un requerimiento
        // Parametros: id proyecto, id modulo y id de requerimiento
        public ActionResult Delete(int? idProyecto, int? idModulo, int? idRequerimiento)
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
            //tempData para visualizar el nombre del proyecto en la vista de borrar
            TempData.Remove("proyectoDetalle");
            TempData.Add("proyectoDetalle", db.PROYECTO.Find(idProyecto).nombre);

            return View(rEQUERIMIENTO);
        }

        // POST: REQUERIMIENTO/Delete/5
        //Médodo para borrar requerimiento
        //Sólo se puede borrar un requerimiento si su estado está en suspendido
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idProyecto, int idModulo, int idRequerimiento)
        {
            REQUERIMIENTO rEQUERIMIENTO = db.REQUERIMIENTO.Find(idRequerimiento, idModulo, idProyecto);
            if (rEQUERIMIENTO.estado == "Suspendido")
            {
                db.REQUERIMIENTO.Remove(rEQUERIMIENTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete", new { idProyecto, idModulo, idRequerimiento });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public Boolean DesplegarMensaje() {
            bool mensaje = false;

            var a = TempData.Peek("proyectoID");

            var b = TempData.Peek("moduloID");

            if (a == null || b == null)
            {
                mensaje = true;
            }
            return mensaje;
        }
    }
}

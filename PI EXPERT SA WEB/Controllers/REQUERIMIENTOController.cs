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


        public ActionResult Index()
        {
            //Al regresar al index se remueven los valores temporales para volverlos asignar
            TempData.Remove("proyectoID");
            TempData.Remove("nombreProyecto");
            TempData.Remove("moduloID");
            TempData.Remove("nombreModulo");

            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }

        

        //Vista parcial que muestra la tabla de requerimientos filtrada por proyecto. Tambien muestra la lista de módulos de dicho proyecto
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





        //Vista parcial que muestra la tabla de requerimientos filtrados por proyecto y módulo
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





        //Index de la consulta de requerimientos en proyecto por desarrollador
        public ActionResult IndexDevelopersRequirements()
        {

            ViewBag.proyectos = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            return View();
        }






        //Vista parcial que devuelve una lista de módulos filtrados por proyecto
        public PartialViewResult DropDownModulo(int? idProyectoPK) {
            List<MODULO> modulo = db.MODULO.Where(x => x.idProyectoPK == idProyectoPK).ToList();
            ViewBag.modulos = new SelectList(modulo, "idModuloPK", "nombre");
            return PartialView();
        }




        //Vista parcial con los desarrolladores que pertenecen a un proyecto
        public PartialViewResult GetDevelopers(int? idProyectoPK)
        {
            var query =
                from emp in db.EMPLEADO
                join rolEmp in db.ROL on emp.cedulaPK equals rolEmp.cedulaPK
                where rolEmp.idProyectoPK == idProyectoPK
                select new { emp.nombre, rolEmp.cedulaPK };

            ViewBag.empleadospro = new SelectList(query, "cedulaPK", "nombre");

            return PartialView();
        }





        //Vista parcial que filtra los requerimientos asociados a un desarrollador 
        public PartialViewResult MostrarRequerimientosDesarrollador(string cedulaPk)
        {

            List<REQUERIMIENTO> requerimiento = db.REQUERIMIENTO.Where(x => x.cedulaDesarrolladorFK == cedulaPk).ToList();

            return PartialView(requerimiento);
        }






        // GET: REQUERIMIENTO/Details/
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

            //actualiza el nombre del proyecto correspondiente a la consulta
            TempData.Remove("proyectoDetalle");
            TempData.Add("proyectoDetalle", db.PROYECTO.Find(idProyecto).nombre);

            return View(rEQUERIMIENTO);
        }








        // GET: REQUERIMIENTO/Create
        public ActionResult Create()
        {

            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre");
<<<<<<< HEAD
<<<<<<< HEAD
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre");
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre");
            return View();
=======
=======

>>>>>>> 41870458fafde7fa5ea1de5c46f51ab9b837a178

            //Solo permite que se despliegue la vista de Create si se ha seleccionado un proyecto y un módulo en el index
            if (TempData.Peek("proyectoID") != null && TempData.Peek("moduloID") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("index");
            }
<<<<<<< HEAD
>>>>>>> master
=======

>>>>>>> 41870458fafde7fa5ea1de5c46f51ab9b837a178
        }






        // POST: REQUERIMIENTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRequerimientoPK,estado,nombre,complejidad,duracionEstimada,cedulaDesarrolladorFK,fechaInicio,fechaFin")] REQUERIMIENTO rEQUERIMIENTO)
        {
            //el id de proyecto y módulo se agregan al modelo desde el tempdata
            rEQUERIMIENTO.idProyectoPK = (int)TempData.Peek("proyectoID");
            rEQUERIMIENTO.idModuloPK = (int)TempData.Peek("moduloID");
            var a = rEQUERIMIENTO.cedulaDesarrolladorFK;


            //el atributo fechaCreacion se actualiza automáticamente con el trigger correspondiente


            //el atributo estado no puede ser enviado nulo a la base de datos, es necesario ponerle un valor para que visual permita guardarlo
            rEQUERIMIENTO.estado = "relleno";

            if (ModelState.IsValid)
            {
                db.REQUERIMIENTO.Add(rEQUERIMIENTO);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rEQUERIMIENTO.cedulaDesarrolladorFK);
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre", rEQUERIMIENTO.idProyectoPK);
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

            //actualiza el nombre de proyecto y modulo que serán mostrados en la vista Edit
            TempData.Remove("proyectoDetalle");
            TempData.Add("proyectoDetalle", db.PROYECTO.Find(idProyecto).nombre);
            TempData.Remove("moduloDetalle");
            TempData.Add("moduloDetalle", db.MODULO.Find(idModulo, idProyecto).nombre);


            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rEQUERIMIENTO.cedulaDesarrolladorFK);
<<<<<<< HEAD
<<<<<<< HEAD
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre", rEQUERIMIENTO.idProyectoPK);
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre", rEQUERIMIENTO.idModuloPK);
=======
>>>>>>> master
=======
>>>>>>> 41870458fafde7fa5ea1de5c46f51ab9b837a178
            return View(rEQUERIMIENTO);
        }





        // POST: REQUERIMIENTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRequerimientoPK,idModuloPK,idProyectoPK,estado,fechaCreacion,nombre,complejidad,duracionEstimada,cedulaDesarrolladorFK,fechaInicio,fechaFin")] REQUERIMIENTO rEQUERIMIENTO)
        {


            var a = rEQUERIMIENTO.estado;
            var b = rEQUERIMIENTO.fechaCreacion;
            var c = rEQUERIMIENTO.idProyectoPK;
            var d = rEQUERIMIENTO.idModuloPK;
            var e = rEQUERIMIENTO.idRequerimientoPK;
            var f = rEQUERIMIENTO.fechaInicio;
            var g = rEQUERIMIENTO.fechaFin;
            var h = rEQUERIMIENTO.complejidad;
            var i = rEQUERIMIENTO.duracionEstimada;
            var j = rEQUERIMIENTO.cedulaDesarrolladorFK;
            var l = rEQUERIMIENTO.nombre;

            //solución temporal que busca la cédula del desarrollador
            if (rEQUERIMIENTO.cedulaDesarrolladorFK == null) {
                rEQUERIMIENTO.cedulaDesarrolladorFK = db.REQUERIMIENTO.Find(e, d, c).cedulaDesarrolladorFK;
                var p = rEQUERIMIENTO.cedulaDesarrolladorFK;
            }

            if (ModelState.IsValid)
            {
                db.Entry(rEQUERIMIENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRequerimientoPK = new SelectList(db.REQUERIMIENTO, "idRequerimientoPK", "idRequerimientoPK", rEQUERIMIENTO.idRequerimientoPK);
            ViewBag.cedulaDesarrolladorFK = new SelectList(db.EMPLEADO, "cedulaPK", "nombre", rEQUERIMIENTO.cedulaDesarrolladorFK);
            ViewBag.idProyectoPK = new SelectList(db.PROYECTO, "idProyectoPK", "nombre", rEQUERIMIENTO.idProyectoPK);
            ViewBag.idModuloPK = new SelectList(db.MODULO, "idModuloPK", "nombre", rEQUERIMIENTO.idModuloPK);
            return View(rEQUERIMIENTO);
        }




        // GET: REQUERIMIENTO/Delete/
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

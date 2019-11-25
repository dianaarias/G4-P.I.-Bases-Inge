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
            //Se filtra para incluir solo los proyectos que tengan un equipo asignado
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


        public PartialViewResult TablaFiltradaDesarrolladores(string habilidades) {

            //modelo de rol y empleado
            var query = from hab in db.HABILIDADES
                        join emp in db.EMPLEADO on hab.cedulaEmpleadoPK equals emp.cedulaPK
                        where hab.habilidadPK == habilidades
                        select new CONSULTAS { modeloHabilidades = hab, modeloEmpleado = emp };


            return PartialView(query);
        }




        //metodo que recibe el desarrollador seleccionado en la vista de ROL/Create
        //Si se selecciona un desarrollador, este se almacena temporalmente.
        //Si se vuelve a seleccionar el desarrollador, se elimina del almacenamiento temporal
        public void SeleccionarEliminarDesarrollador(string cedulaPK) {


            //Prueba, borrar luego
            //TempData.Add("hilera", "123456789,");


            //string local que toma el string almacenado en tempdata
            string hilera = TempData.Peek("hilera").ToString();
    
            //array que separa las cédulas del string por ','
            string[] array = hilera.Split(',');

            //booleano para búsqueda de cédula
            bool continua = true;

            //valor para señalar la pocision del elemento en el array
            int temp = 0;

            //nuevaHilera que se guarda, en caso de que ya exista el elemento seleccionado
            string nuevaHilera = "";

            //Si existen elementos en la hilera
            if (array.Length > 1) {
                for (int i = 0; i < array.Length && continua; ++i) {
                    //Si la cédula ya fue seleccionada
                    if (array[i] == cedulaPK) {
                        continua = false;
                        temp = i;
                    }
                }

                //si la cédula aun no ha sido seleccionada
                if (continua)
                {
                    hilera += cedulaPK;
                    hilera += ",";


                    TempData.Remove("hilera");
                    TempData.Add("hilera", hilera);
                }
                //la cédula ya fue seleccionada, por lo que se elimina de la hilera
                else {

                    for (int i = 0; i < array.Length; ++i) {
                        if (i != temp) {

                            nuevaHilera += array[i];

                            if (nuevaHilera != "") {
                                nuevaHilera += ",";
                            }
                        }
                    }
                    TempData.Remove("hilera");
                    TempData.Add("hilera", nuevaHilera);
                }
            }

            //prueba, borrar luego
            //var f = TempData.Peek("hilera");
            //var a = f;
        }



        public PartialViewResult ActualizarDesarrolladoresSeleccionados() {

            //string local que toma el string almacenado en tempdata
            string hilera = TempData.Peek("hilera").ToString();

            //array que separa las cédulas del string por ','
            string[] array = hilera.Split(',');

            List<EMPLEADO> lista = null;
            

            for (int i = 0; i < array.Length; ++i)
            {
                lista.Add(db.EMPLEADO.Find(array[i]));
            }


            return PartialView(lista);
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
            var query =
                from hab in db.HABILIDADES
                select new { hab.habilidadPK };

            ViewBag.habilidades = new SelectList(query.Distinct(), "habilidadPK", "habilidadPK");
            return View(db.EMPLEADO);
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

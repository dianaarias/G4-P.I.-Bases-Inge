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

        //Comentado porque al final no se está utilizando una vista parcial para filtrar los empleados por habilidad
        //public PartialViewResult filtroHabilidad() {

        //    return PartialView();
        //}



        public PartialViewResult Equipo(int idProyectoPK) {
            var query = db.ROL.Where(x => x.idProyectoPK == idProyectoPK);
            return PartialView(query.ToList());
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
        [HttpGet]
        public ActionResult Create(string busqueda)
        {
            //Exiten dos escenarios a la hora de filtrar los empleados:
            //1. No se ha introducido ninguna habilidad en la búsqueda. Se muestran todos los empleados disponibles
            //2. Se ha introducido una habilidad en la búsqueda. Se filtran los empleados
            var eem = (System.Collections.IEnumerable)null;
            if (String.IsNullOrEmpty(busqueda))
            {
                eem =
                  from emp in db.EMPLEADO
                  where emp.disponibilidad == true
                  select new { nombreEmp = emp.nombre + " " + emp.apellido1 + " " + emp.apellido2, emp.cedulaPK, emp.apellido1 };
            }
            else
            {
                eem =
                    from emp in db.EMPLEADO
                    join hab in db.HABILIDADES on emp.cedulaPK equals hab.cedulaEmpleadoPK
                    where emp.disponibilidad == true && hab.habilidadPK.Contains(busqueda)
                    select new
                    {
                        nombreEmp = emp.nombre + " " + emp.apellido1 + " " + emp.apellido2,
                        emp.cedulaPK,
                        emp.apellido1
                    };
            }

            ViewBag.cedulaPK = new SelectList(eem, "cedulaPK", "nombreEmp");

            //Consulta de proyectos sin equipo, es decir, proyectos cuyo ID no exista en la tabla ROL
            //Se necesita esta lsita de proyectos para el dropdown en Crear
            var query = (from proyecto in db.PROYECTO
                           where !db.ROL.Any(m => m.idProyectoPK == proyecto.idProyectoPK)
                           select proyecto);

            ViewBag.proyectosSinEquipo = new SelectList(query, "idProyectoPK", "nombre");

            return View();
        }


        public PartialViewResult PreEquipo(string busqueda) {

            //Exiten dos escenarios a la hora de filtrar los empleados:
            //1. No se ha introducido ninguna habilidad en la búsqueda. Se muestran todos los empleados disponibles
            //2. Se ha introducido una habilidad en la búsqueda. Se filtran los empleados
            var eem = (System.Collections.IEnumerable)null;
            if (String.IsNullOrEmpty(busqueda))
            {
                eem =
                  from emp in db.EMPLEADO
                  where emp.disponibilidad == true
                  select new { nombreEmp = emp.nombre + " " + emp.apellido1 + " " + emp.apellido2, emp.cedulaPK, emp.apellido1 };
            }
            else
            {
                eem =
                    from emp in db.EMPLEADO
                    join hab in db.HABILIDADES on emp.cedulaPK equals hab.cedulaEmpleadoPK
                    where emp.disponibilidad == true && hab.habilidadPK.Contains(busqueda)
                    select new
                    {
                        nombreEmp = emp.nombre + " " + emp.apellido1 + " " + emp.apellido2,
                        emp.cedulaPK,
                        emp.apellido1
                    };
            }

            ViewBag.cedulaPK = new SelectList(eem, "cedulaPK", "nombreEmp");

            //Consulta de proyectos sin equipo, es decir, proyectos cuyo ID no exista en la tabla ROL
            //Se necesita esta lsita de proyectos para el dropdown en Crear
            var query = (from proyecto in db.PROYECTO
                         where !db.ROL.Any(m => m.idProyectoPK == proyecto.idProyectoPK)
                         select proyecto);

            ViewBag.proyectosSinEquipo = new SelectList(query, "idProyectoPK", "nombre");

            return PartialView();
        }
    
        // POST: ROL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string[] miembrosEquipo, string proyectoEquipo)//[Bind(Include = "cedulaPK,idProyectoPK,tipoRol")] ROL rOL)
        {

            //Recibimos el id del proyecto como un string desde la vista, hay que pasarlo a int
            int idProject = Int32.Parse(proyectoEquipo);


            if (ModelState.IsValid)
            {
                //db.ROL.Add(rOL);
                //Por cada elemento devuelto por el script por POST se crea una tupla con la información necesario
                foreach (var developer in miembrosEquipo)
                {
                    db.ROL.Add(new ROL
                    {
                        cedulaPK = developer,
                        idProyectoPK = idProject,
                        tipoRol = "Desarrollador"
                    });
                }

                db.SaveChanges();
                //Retorna json a script de ajax (el de post) 
                return Json(new
                {
                    isRedirect = false,
                    url = @Url.Action("Index","ROL"),
                });

            }

            return RedirectToAction("Index");
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

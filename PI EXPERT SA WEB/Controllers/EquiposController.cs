using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PI_EXPERT_SA_WEB.Models;

namespace PI_EXPERT_SA_WEB.Controllers
{
    public class EquiposController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();
        // GET: Equipos
        public ActionResult Equipo()
        {
            ModeloEquipo modelo = new ModeloEquipo();
            var empleados = db.EMPLEADO.Where(t =>  t.disponibilidad == true && t.tipoUsuario == "Desarrollador").ToList();
            return View(empleados.ToList());
        }

        public ActionResult guardarEquipo(String equipo) {
            // Create and execute raw SQL query.
            //string query = "INSERT INTO ROL() WHERE DepartmentID = @p0";
            //Department department = await db.Departments.SqlQuery(query, id).SingleOrDefaultAsync();


            // Redirect to Home Page or Team Summary
            ModeloEquipo modelo = new ModeloEquipo();
            return View(/*empleados.ToList()*/);
        }
    }
}
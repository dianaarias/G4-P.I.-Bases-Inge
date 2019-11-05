using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PI_EXPERT_SA_WEB.Models;

namespace PI_EXPERT_SA_WEB.Controllers
{
    public class LoginController : Controller
    {
        private Gr02Proy4Entities db = new Gr02Proy4Entities();
        
        // Crear mapas de empleado y de cliente vacias

            public LoginController()
        {
            crearMapas();
        }

        // GET: Login
        public ActionResult Login ()
        {

            TempData.Add("proyectoID", null);
            TempData.Add("nombreProyecto", null);
            TempData.Add("moduloID", null);
            TempData.Add("nombreModulo", null);


            return View();
        }

        /*
         * Metodo que se encarga de llenar los mapas con la cedula como llave
         * El valor asociado es una tupla con el correo y la contrasenna
         */
         public void crearMapas()
        {
            // Llenar los dos mapas
        }
        
        /*
         * Metodo que se encarga de verificar que los parametros recibidos sean los correctos
         */
        public void validacionLogin(string valor)
        {
            // Comparar valor con los mapas
            
            
        }
        
        /*
         * Redirige a Proyecto Index 
         * Solo si la validacion es correcta
         */
         public ActionResult dirigirHome() {
            return View();
         }

        /*
         * Muestra un alert message si la informacion
         * no es la correcta
         */
         public void alertMessage()
        {
             
        }
    }
}
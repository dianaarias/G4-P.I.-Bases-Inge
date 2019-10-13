using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_EXPERT_SA_WEB.Models
{
    public class ModeloIntermedioRolEmpleadoHabilidades
    {
        public List<EMPLEADO> empleados = new List<EMPLEADO>();
        public List<HABILIDADES> habilidades = new List<HABILIDADES>();
        public ROL rol = new ROL();
    }
}
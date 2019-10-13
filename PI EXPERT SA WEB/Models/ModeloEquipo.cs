using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_EXPERT_SA_WEB.Models
{
    public class ModeloEquipo
    {
        public List<EMPLEADO> empleados = new List<EMPLEADO>();
        public PROYECTO proyecto { get; set; }
        public ROL rol { get;   set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PI_EXPERT_SA_WEB.Models
{
    public class ModeloIntermedio
    {
        
        public EMPLEADO modeloEmpleado { get; set; }
        public HABILIDADES modeloHabilidades1 { get; set; }
        public HABILIDADES modeloHabilidades2 { get; set; }
        public HABILIDADES modeloHabilidades3 { get; set; }

        public List<EMPLEADO> listaEmpleados { get; set; }

        public List<HABILIDADES> listaHabilidades { get; set; }
    }
}
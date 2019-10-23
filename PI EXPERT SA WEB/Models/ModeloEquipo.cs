using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_EXPERT_SA_WEB.Models
{
    public class ModeloEquipo
    {
        PROYECTO proyecto { get; set; }
        List<ROL> equipo = new List<ROL>(); 
        List<EMPLEADO> listaEmpleados = new List<EMPLEADO>();
    }
}
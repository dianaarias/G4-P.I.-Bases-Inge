using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_EXPERT_SA_WEB.Models
{
    public class ModeloLogin
    {
        List<EMPLEADO> listaEmpleado = new List<EMPLEADO>();
        List<CLIENTE> listaCliente = new List<CLIENTE>();
        String password { get; set; }
    }
}
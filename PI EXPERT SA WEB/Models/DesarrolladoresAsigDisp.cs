using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace PI_EXPERT_SA_WEB.Models
{
    public class DesarrolladoresAsigDisp
    {
        [DisplayName("Nombre del empleado")]
        public String NombreEmp { get; set; }
        [DisplayName("Nombre del proyecto")]
        public String NombreProy { get; set; }
        [DisplayName("Fecha de inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaInicio { get; set; }
        [DisplayName("Fecha estimada de finalizacion")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaEstDesocup { get; set; }
    }
}
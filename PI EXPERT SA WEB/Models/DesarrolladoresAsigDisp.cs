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
        public String NombreEmp {  get; set; } //Nombre de tabla EMPLEADO
        [DisplayName("Nombre del proyecto")]
        public String NombreProy { get; set; } //Nombre de tabla PROYECTO
        [DisplayName("Fecha de inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaInicio { get; set; } // fechaInicio de tabla PROYECTO
        [DisplayName("Fecha estimada de finalizacion")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaEstDesocup { get; set; } // fechaEst de desocupacion, se crea sumandole 
                                                       // la duracion estimada/8 a la fecha de inicio
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PI_EXPERT_SA_WEB.Models
{
    public class HorasEstRealProy
    {
        [Display(Name ="Nombre del proyecto")]
        public String NombreProy { get; set; } // Nombre de la tabla proyecto
        [Display(Name ="Duracion estimada")]
        public int? HorasEst { get; set; } //Suma de la duracion estimada de todos de los requerimientos del proyecto
        [Display(Name ="Duracion real")]
        public int? HorasReal { get; set; }//Suma de la duracion real de todos los requerimientos del proyecto
        [Display(Name ="Differencia")]
        public int? DiffHoras { get; set; }// Diferencia de las sumas de duracion estimada y duracion real
    }
}
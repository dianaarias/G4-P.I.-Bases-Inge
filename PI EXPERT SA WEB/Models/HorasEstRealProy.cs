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
        public String NombreProy { get; set; }
        [Display(Name ="Duracion estimada")]
        public int? HorasEst { get; set; }
        [Display(Name ="Duracion real")]
        public int? HorasReal { get; set; }
        [Display(Name ="Differencia")]
        public int? DiffHoras { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PI_EXPERT_SA_WEB.Models
{
    public class ListaDesResp
    {
        [Display(Name = "Cliente")]
        public String NombreCli { get; set; }
        [Display(Name = "Proyecto")]
        public String NombreProy { get; set; }
        [Display(Name ="Responsable")]
        public String NombreResp { get; set; }
        [Display(Name ="Requerimiento")]
        public String NombreReq { get; set; }
        [Display(Name = "Estado del requerimiento")]
        public String EstadoReq { get; set; }

    }
}
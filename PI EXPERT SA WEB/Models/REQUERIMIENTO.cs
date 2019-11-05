//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PI_EXPERT_SA_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class REQUERIMIENTO
    {
        //verifica que id req sea unico
        [Key]
        [Display(Name = "Id requermiento")]
        [Required(ErrorMessage = "El campo id requermiento es requerido")]
        public int idRequerimientoPK { get; set; }
        //verifica que id modulo sea unico
        [Key]
        [Display(Name = "Id modulo")]
        [Required(ErrorMessage = "El campo id modulo es requerido")]
        public int idModuloPK { get; set; }
        //verifica que id proyecto sea unico
        [Key]
        [Display(Name = "Id proyecto")]
        [Required(ErrorMessage = "El campo id proyecto es requerido")]
        public int idProyectoPK { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo estado es requerido")]
        public string estado { get; set; }
        [Display(Name = "Fecha estado")]
        public System.DateTime fechaEstado { get; set; }
        [Display(Name = "Nombre requerimiento")]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(64, ErrorMessage = "El campo nombre excede el n�mero de caracteres")]
        public string nombre { get; set; }
        [Display(Name = "Complejidad")]
        public int complejidad { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "La Duraci�n Estimada s�lo puede contener n�meros")]
        [Display(Name = "Duraci�n estimada en horas")]
        public Nullable<int> duracionEstimada { get; set; }
        [Display(Name = "C�dula Desarrollador")]
        public string cedulaDesarrolladorFK { get; set; }
        [Display(Name = "Fecha inicio")]
        public Nullable<System.DateTime> fechaInicio { get; set; }
        [Display(Name = "Fecha fin")]
        public Nullable<System.DateTime> fechaFin { get; set; }

        public virtual EMPLEADO EMPLEADO { get; set; }
        public virtual MODULO MODULO { get; set; }

        public virtual PROYECTO PROYECTO { get; set; }
    }
}

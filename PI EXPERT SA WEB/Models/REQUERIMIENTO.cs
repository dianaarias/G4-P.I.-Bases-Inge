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
<<<<<<< HEAD
<<<<<<< HEAD
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class REQUERIMIENTO
    {
        
        public int idRequerimientoPK { get; set; }
        
        
        public int idModuloPK { get; set; }
        
        
        public int idProyectoPK { get; set; }

        [DisplayName("Estado")]
        public string estado { get; set; }
        
        
        [DisplayName("Fecha de Inicio")]
        [Required(ErrorMessage = "Campo requerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha { get; set; }

        [DisplayName("Nombre de Requerimiento")]
        [StringLength(maximumLength: 64, MinimumLength = 1, ErrorMessage = "No puede introducir nombres de m醩 de 64 caracteres")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Caracteres inv醠idos")]
        [Required(ErrorMessage = "Campo requerido")]
        public string nombre { get; set; }

        [DisplayName("Complejidad")]
        public int complejidad { get; set; }

        [DisplayName("Duraci髇 Estimada")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Caracteres inv醠idos")]
        public Nullable<int> duracionEstimada { get; set; }

        [DisplayName("Desarrollador Asignado")]
        [Required(ErrorMessage = "Campo requerido")]
=======
=======

>>>>>>> 41870458fafde7fa5ea1de5c46f51ab9b837a178
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
        public string estado { get; set; }
        [Display(Name = "Fecha de creaci贸n")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        [Display(Name = "Requerimiento")]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(64, ErrorMessage = "El campo nombre excede el n煤mero de caracteres")]
        public string nombre { get; set; }
        [Display(Name = "Complejidad")]
        [Required(ErrorMessage = "El campo complejidad es requerido")]
        public string complejidad { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "La Duraci贸n Estimada s贸lo puede contener n煤meros")]
        [Display(Name = "Duraci贸n estimada en horas")]
        public Nullable<int> duracionEstimada { get; set; }
        [Display(Name = "Duraci贸n real en horas")]
        public Nullable<int> duracionReal { get; set; }
<<<<<<< HEAD
        [Display(Name = "C閐ula Desarrollador")]
>>>>>>> master
=======
        [Display(Name = "C茅dula Desarrollador")]
>>>>>>> 41870458fafde7fa5ea1de5c46f51ab9b837a178
        public string cedulaDesarrolladorFK { get; set; }
        [Display(Name = "Fecha inicio")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> fechaInicio { get; set; }
        [Display(Name = "Fecha fin")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> fechaFin { get; set; }

        public virtual EMPLEADO EMPLEADO { get; set; }
        public virtual MODULO MODULO { get; set; }

        public virtual PROYECTO PROYECTO { get; set; }
    }
}

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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ROL
    {
        //La llave primaria de Rol est� constituida por dos atributos, se�alados por "[Key]", para verificar que estos sean �nicos
        //Estos atributos son el n�mero de c�dula y el ID del proyecto al que est� asociado ese rol
        [Key]
        [Display(Name = "C�dula")]
        [Required(ErrorMessage = "Campo requerido")]
        public string cedulaPK { get; set; }
        [Key]
        [Display(Name = "ID Proyecto")]
        [Required(ErrorMessage = "Campo requerido")]
        public int idProyectoPK { get; set; }
        [Display(Name = "Tipo de rol")]
        [Required(ErrorMessage = "Campo requerido")]
        public string tipoRol { get; set; }
        [Display(Name = "N�mero de equipo")]
        [Required(ErrorMessage = "Campo requerido")]
        public int numEquipo { get; set; }
    
        public virtual EMPLEADO EMPLEADO { get; set; }
        public virtual PROYECTO PROYECTO { get; set; }
    }
}

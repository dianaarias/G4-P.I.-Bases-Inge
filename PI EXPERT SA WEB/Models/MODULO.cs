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

    public partial class MODULO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MODULO()
        {
            this.REQUERIMIENTO = new HashSet<REQUERIMIENTO>();
        }

        [DisplayName("ID de M�dulo")]
        [Required(ErrorMessage = "Campo requerido")]
        public int idModuloPK { get; set; }

        [DisplayName("ID de Proyecto")]
        [Required(ErrorMessage = "Campo requerido")]
        public int idProyectoPK { get; set; }

        [DisplayName("Nombre de M�dulo")]
        [StringLength(maximumLength: 64, MinimumLength = 1, ErrorMessage = "No puede introducir nombres de m�s de 64 caracteres")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Caracteres inv�lidos")]
        [Required(ErrorMessage = "Campo requerido")]
        public string nombre { get; set; }

        [DisplayName("Fecha de Inicio")]
        [Required(ErrorMessage = "Campo requerido")]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fechaInicio { get; set; }
    
        public virtual PROYECTO PROYECTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REQUERIMIENTO> REQUERIMIENTO { get; set; }
    }
}

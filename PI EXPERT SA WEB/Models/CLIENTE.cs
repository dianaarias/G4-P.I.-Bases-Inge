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
    
    public partial class CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTE()
        {
            this.PROYECTO = new HashSet<PROYECTO>();
        }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La cedula solo contiene numeros")]
        [StringLength(maximumLength: 9,ErrorMessage ="Cedula invalida", MinimumLength = 9)]
        public string cedulaPK { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string correo { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string telefono { get; set; }
        public string provincia { get; set; }
        public string canton { get; set; }
        public string distrito { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROYECTO> PROYECTO { get; set; }
    }
}

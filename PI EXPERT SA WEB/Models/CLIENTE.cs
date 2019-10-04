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
    using System.ComponentModel;
    
    public partial class CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTE()
        {
            this.PROYECTO = new HashSet<PROYECTO>();
        }
        [DisplayName("C�dula de Identidad")]
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La cedula solo contiene numeros")]
        [StringLength(maximumLength: 9,ErrorMessage ="Cedula invalida", MinimumLength = 9)]
        public string cedulaPK { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [StringLength(16, ErrorMessage ="El Nombre excede el numero de caracteres")]
        public string name { get; set; }
        [DisplayName("Primer apellido")]
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [StringLength(16, ErrorMessage = "El Primer apellido excede el numero de caracteres")]
        public string apellido1 { get; set; }
        [DisplayName("Segundo apellido")]
        [StringLength(16, ErrorMessage = "El Segundo apellido excede el numero de caracteres")]
        public string apellido2 { get; set; }
        [DisplayName("Correo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Porfavor introducir un correo valido")]
        public string correo { get; set; }
        [DisplayName("Tel�fono")]
        [DataType(DataType.PhoneNumber)]
        public string telefono { get; set; }
        [DisplayName("Provincia")]
        public string provincia { get; set; }
        [DisplayName("Distrito")]
        public string canton { get; set; }
        [DisplayName("Cant�n")]
        public string distrito { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROYECTO> PROYECTO { get; set; }
    }
}

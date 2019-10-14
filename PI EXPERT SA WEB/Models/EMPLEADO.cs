namespace PI_EXPERT_SA_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EMPLEADO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLEADO()
        {
            this.HABILIDADES = new HashSet<HABILIDADES>();
            this.REQUERIMIENTO = new HashSet<REQUERIMIENTO>();
            this.ROL = new HashSet<ROL>();
            this.PROYECTO = new HashSet<PROYECTO>();
        }

        [Required(ErrorMessage = "El campo c�dula es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La c�dula s�lo puede contener n�meros")]
        [StringLength(maximumLength: 9, ErrorMessage = "C�dula invalida", MinimumLength = 9)]
        [Display(Name = "C�dula")]
        public string cedulaPK { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(16, ErrorMessage = "El campo nombre excede el n�mero de caracteres")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo apellido es requerido")]
        [StringLength(16, ErrorMessage = "El campo apellido excede el n�mero de caracteres")]
        [Display(Name = "Primer apellido")]
        public string apellido1 { get; set; }
        [StringLength(16, ErrorMessage = "El campo apellido excede el n�mero de caracteres")]
        [Display(Name = "Segundo apellido")]
        public string apellido2 { get; set; }
        [Required(ErrorMessage = "El campo correo es requerido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string correo { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Tel�fono")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "El campo fecha de contrataci�n es requerido")]
        [Display(Name = "Fecha de contrataci�n")]
        public System.DateTime fechaContratacion { get; set; }
        [Display(Name = "Fecha finalizaci�n de contrato")]
        public Nullable<System.DateTime> fechaDespido { get; set; }
        [StringLength(16, ErrorMessage = "El campo rol actual excede el n�mero de caracteres")]
        [Display(Name = "Rol actual")]
        public string tipoUsuario { get; set; }
        [Display(Name = "Disponibilidad")]
        public bool disponibilidad { get; set; }
        [StringLength(16, ErrorMessage = "El campo provincia excede el n�mero de caracteres")]
        [Display(Name = "Provincia")]
        public string provincia { get; set; }
        [StringLength(16, ErrorMessage = "El campo cant�n excede el n�mero de caracteres")]
        [Display(Name = "Cant�n")]
        public string canton { get; set; }
        [StringLength(16, ErrorMessage = "El campo distrito excede el n�mero de caracteres")]
        [Display(Name = "Distrito")]
        public string distrito { get; set; }
        [Required(ErrorMessage = "El campo fecha de nacimiento es requerido")]
        [Display(Name = "Fecha de nacimiento")]
        public System.DateTime fechaNacimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HABILIDADES> HABILIDADES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REQUERIMIENTO> REQUERIMIENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROL> ROL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROYECTO> PROYECTO { get; set; }
    }
}

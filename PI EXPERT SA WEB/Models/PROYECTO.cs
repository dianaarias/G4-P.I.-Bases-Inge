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

    public partial class PROYECTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROYECTO()
        {
            this.MODULO = new HashSet<MODULO>();
            this.ROL = new HashSet<ROL>();
        }
        [DisplayName("ID de Proyecto")]
        [Required(ErrorMessage = "Campo requerido")]
        public int idProyectoPK { get; set; }
        [DisplayName("Costo Estimado")]
        [RegularExpression("^$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Caracteres inv�lidos")]
        public decimal costoEstimado { get; set; }
        [DisplayName("Costo Real")]
        [RegularExpression("^$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Caracteres inv�lidos: Introducir n�meros solamente")]
        public Nullable<decimal> costoReal { get; set; }
        [DisplayName("Fecha de Inicio")]
        [Required(ErrorMessage = "Campo requerido")]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fechaInicio { get; set; }
        [DisplayName("Fecha de Finalizaci�n")]
        [Required(ErrorMessage = "Campo requerido")]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fechaFin { get; set; }
        [DisplayName("Duraci�n Estimada")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Caracteres inv�lidos: Introducir n�meros solamente")]
        public Nullable<int> duracionEstimada { get; set; }
        [DisplayName("Cliente")]
        [Required(ErrorMessage = "Campo requerido")]
        public string cedulaClienteFK { get; set; }
        [DisplayName("Nombre de Proyecto")]
        [StringLength(maximumLength: 64, MinimumLength = 1, ErrorMessage = "No puede introducir nombres de m�s de 64 caracteres")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Caracteres inv�lidos")]
        [Required(ErrorMessage = "Campo requerido")]
        public string nombre { get; set; }
        [DisplayName("Objetivo")]
        [Required(ErrorMessage = "Campo requerido")]
        public string objetivo { get; set; }
        [DisplayName("Duraci�n Real")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Caracteres inv�lidos: Introducir n�meros solamente")]
        public Nullable<int> duracionReal { get; set; }
        [DisplayName("Costo Desarrollador")]
        [RegularExpression("^$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Caracteres inv�lidos: Introducir n�meros solamente")]
        public Nullable<decimal> costoDesarrollador { get; set; }
        [DisplayName("L�der")]
        [Required(ErrorMessage = "Campo requerido")]
        public string cedulaLiderFK { get; set; }

        public virtual CLIENTE CLIENTE { get; set; }
        public virtual EMPLEADO EMPLEADO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MODULO> MODULO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROL> ROL { get; set; }
    }
}
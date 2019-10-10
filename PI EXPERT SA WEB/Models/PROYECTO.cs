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
    
    public partial class PROYECTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROYECTO()
        {
            this.MODULO = new HashSet<MODULO>();
            this.ROL = new HashSet<ROL>();
        }
    
        public int idProyectoPK { get; set; }
        public int costoEstimado { get; set; }
        public Nullable<int> costoReal { get; set; }
        public Nullable<System.DateTime> fechaInicio { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
        public Nullable<int> duracionEstimada { get; set; }
        public string cedulaClienteFK { get; set; }
        public string nombre { get; set; }
        public string objetivo { get; set; }
        public Nullable<int> duracionReal { get; set; }
        public Nullable<decimal> costoDesarrollador { get; set; }
        public string cedulaLiderFK { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MODULO> MODULO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROL> ROL { get; set; }
        public virtual EMPLEADO EMPLEADO { get; set; }
    }
}

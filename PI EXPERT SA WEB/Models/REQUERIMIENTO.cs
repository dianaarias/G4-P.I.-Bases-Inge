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
    
    public partial class REQUERIMIENTO
    {
        public int idRequerimientoPK { get; set; }
        public int idModuloPK { get; set; }
        public int idProyectoPK { get; set; }
        public int estado { get; set; }
        public System.DateTime fecha { get; set; }
        public string nombre { get; set; }
        public int complejidad { get; set; }
        public Nullable<int> duracionEstimada { get; set; }
        public string cedulaDesarrolladorFK { get; set; }
    
        public virtual MODULO MODULO { get; set; }
        public virtual EMPLEADO EMPLEADO1 { get; set; }
    }
}

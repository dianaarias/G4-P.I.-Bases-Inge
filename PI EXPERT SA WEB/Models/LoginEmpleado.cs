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
    
    public partial class LoginEmpleado
    {
        public string cedulaEmpleadoPK { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
    
        public virtual EMPLEADO EMPLEADO { get; set; }
    }
}

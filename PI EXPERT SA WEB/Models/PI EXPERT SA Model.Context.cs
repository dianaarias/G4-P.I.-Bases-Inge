﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Gr02Proy4Entities : DbContext
    {
        public Gr02Proy4Entities()
            : base("name=Gr02Proy4Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CLIENTE> CLIENTE { get; set; }
        public virtual DbSet<EMPLEADO> EMPLEADO { get; set; }
        public virtual DbSet<HABILIDADES> HABILIDADES { get; set; }
        public virtual DbSet<MODULO> MODULO { get; set; }
        public virtual DbSet<PROYECTO> PROYECTO { get; set; }
        public virtual DbSet<REQUERIMIENTO> REQUERIMIENTO { get; set; }
        public virtual DbSet<ROL> ROL { get; set; }
    
        public virtual ObjectResult<SP_RecuperarRequerimientos_Result> SP_RecuperarRequerimientos(string nombreProyecto, string nombreModulo)
        {
            var nombreProyectoParameter = nombreProyecto != null ?
                new ObjectParameter("nombreProyecto", nombreProyecto) :
                new ObjectParameter("nombreProyecto", typeof(string));
    
            var nombreModuloParameter = nombreModulo != null ?
                new ObjectParameter("nombreModulo", nombreModulo) :
                new ObjectParameter("nombreModulo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_RecuperarRequerimientos_Result>("SP_RecuperarRequerimientos", nombreProyectoParameter, nombreModuloParameter);
        }
    
        public virtual ObjectResult<SP_PeriodosDesocupacion_Result> SP_PeriodosDesocupacion(Nullable<int> cedula, Nullable<System.DateTime> fechaInicioR, Nullable<System.DateTime> fechaFinR)
        {
            var cedulaParameter = cedula.HasValue ?
                new ObjectParameter("cedula", cedula) :
                new ObjectParameter("cedula", typeof(int));
    
            var fechaInicioRParameter = fechaInicioR.HasValue ?
                new ObjectParameter("fechaInicioR", fechaInicioR) :
                new ObjectParameter("fechaInicioR", typeof(System.DateTime));
    
            var fechaFinRParameter = fechaFinR.HasValue ?
                new ObjectParameter("fechaFinR", fechaFinR) :
                new ObjectParameter("fechaFinR", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_PeriodosDesocupacion_Result>("SP_PeriodosDesocupacion", cedulaParameter, fechaInicioRParameter, fechaFinRParameter);
        }
    }
}

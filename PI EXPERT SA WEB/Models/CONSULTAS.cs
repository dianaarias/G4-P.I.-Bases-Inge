﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_EXPERT_SA_WEB.Models
{
    public class CONSULTAS
    {
        public EMPLEADO modeloEmpleado { get; set; }
        public CLIENTE modeloCliente { get; set; }
        public PROYECTO modeloProyecto { get; set; }
        public MODULO modeloModulo { get; set; }
        public REQUERIMIENTO modeloRequerimiento{ get; set; }
        public ROL modeloRol { get; set; }
        public HABILIDADES modeloHabilidades { get; set; }
    }
}
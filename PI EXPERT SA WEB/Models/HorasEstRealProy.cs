using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_EXPERT_SA_WEB.Models
{
    public class HorasEstRealProy
    {
        public String NombreProy { get; set; }
        public int? HorasEst { get; set; }
        public int? HorasReal { get; set; }
        public int? DiffHoras { get; set; }
    }
}
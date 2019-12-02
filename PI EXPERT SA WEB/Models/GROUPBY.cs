using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_EXPERT_SA_WEB.Models
{
    public class Group<K, T>
    {
        public K Key;
        public IEnumerable<T> Values;
        public int? suma ;
        public double? avg;
        
        public int? minimo;
        public int? maximo;
        public int? promedio;
        public DateTime? fecha;

    }
}
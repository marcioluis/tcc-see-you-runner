//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SeeYouSite.Models
{
    public partial class pontos
    {
        public long id_ponto { get; set; }
        public Nullable<long> id_percurso { get; set; }
        public Nullable<System.DateTime> data_ponto { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Nullable<double> caloria { get; set; }
        public Nullable<double> distancia { get; set; }
        public Nullable<double> Pace { get; set; }
        public Nullable<int> duracao { get; set; }
        public Nullable<double> velocidade { get; set; }
        public string isMetrico { get; set; }
        public Nullable<double> altitude { get; set; }
    
        public virtual percursos percursos { get; set; }
    }
    
}

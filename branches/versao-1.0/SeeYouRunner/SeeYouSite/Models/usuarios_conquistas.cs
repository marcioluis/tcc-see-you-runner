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
    public partial class usuarios_conquistas
    {
        public long id_usuarios_conquistas { get; set; }
        public Nullable<long> id_conquista { get; set; }
        public Nullable<long> id_usuario { get; set; }
    
        public virtual conquistas conquistas { get; set; }
        public virtual usuarios usuarios { get; set; }
    }
    
}

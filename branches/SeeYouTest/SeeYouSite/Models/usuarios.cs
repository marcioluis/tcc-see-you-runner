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
    public partial class usuarios
    {
        public usuarios()
        {
            this.comentarios = new HashSet<comentarios>();
            this.funcoes_usuarios = new HashSet<funcoes_usuarios>();
            this.imagens = new HashSet<imagens>();
            this.usuarios_conquistas = new HashSet<usuarios_conquistas>();
            this.usuarios_grupos = new HashSet<usuarios_grupos>();
            this.percursos = new HashSet<percursos>();
        }
    
        public long id_usuario { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public Nullable<System.DateTime> data_nascimento { get; set; }
        public string sexo { get; set; }
        public Nullable<decimal> altura { get; set; }
        public string sobrenome { get; set; }
        public string ativo { get; set; }
        public string senha { get; set; }
        public string login { get; set; }
        public string nome { get; set; }
    
        public virtual ICollection<comentarios> comentarios { get; set; }
        public virtual ICollection<funcoes_usuarios> funcoes_usuarios { get; set; }
        public virtual ICollection<imagens> imagens { get; set; }
        public virtual ICollection<usuarios_conquistas> usuarios_conquistas { get; set; }
        public virtual ICollection<usuarios_grupos> usuarios_grupos { get; set; }
        public virtual ICollection<percursos> percursos { get; set; }
    }
    
}
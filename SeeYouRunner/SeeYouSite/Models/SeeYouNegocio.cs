using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace SeeYouSite.Models
{
    public class SeeYouNegocio
    {
        SYRContext syrDb;

        public SeeYouNegocio()
        {
            syrDb = new SYRContext();
        }

        public SeeYouNegocio(SYRContext DBcontext)
        {
            syrDb = DBcontext;
        }

        /// <summary>
        /// Metodo inicial que retorna o percurso mais recente do corredor
        /// e as métricas calculadas até o momento. As metricas podem vir nulas ou zeradas
        /// caso o percurso seja o atual e nao esteja ainda finalizado
        /// </summary>
        /// <param name="id_usuario">ID do usuario que se deseja recuperar o percurso</param>
        /// <returns>Objeto do tipo percursos</returns>
        public percursos percursoAtual(int id_usuario)
        {
            percursos percursoAtual = syrDb.percursos.OrderByDescending(x => x.data_percurso).Include("pontos").FirstOrDefault(x => x.id_usuario == id_usuario);
            return percursoAtual;
        }

        /// <summary>
        /// Retorna uma lista de pontos com as metricas correspondentes para o percurso de ID
        /// especificado
        /// </summary>
        /// <param name="id_percurso">ID do percurso que se deseja recuperar a informacao</param>
        /// <returns>Lista de pontos do percurso</returns>
        public List<pontos> pontosPercurso(long id_percurso)
        {
            var pts = syrDb.pontos.Where(pt => pt.id_percurso == id_percurso);
            return pts.ToList<pontos>();
        }

    }
}
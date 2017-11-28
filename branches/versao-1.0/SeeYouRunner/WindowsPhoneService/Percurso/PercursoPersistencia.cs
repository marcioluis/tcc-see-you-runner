using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindowsPhoneService.Percurso
{
    public class PercursoPersistencia
    {
        public PercursoPersistencia()
        { }

        /**
         * Cria e salva novo percurso na base de dados e retorna id do percurso 
        **/
        public int NovoPercurso(percursos per)
        {
            try
            {
                int id = 0;
                using (SeeYouEntities syr = new SeeYouEntities())
                {
                    syr.percursos.AddObject(per);
                    syr.SaveChanges();
                    id = (int)per.id_percurso;
                }
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /**
         * Atualiza percurso com métricas finais
        **/
        public void AtualizaPercurso(percursos per)
        {
            try
            {
                using (SeeYouEntities syr = new SeeYouEntities())
                {
                    var percurso = (from p in syr.percursos
                                    where p.id_percurso == per.id_percurso
                                    select p).FirstOrDefault();
                    
                    percurso.altitude_max = per.altitude_max;
                    percurso.altitude_med = per.altitude_med;
                    percurso.altitude_min = per.altitude_min;
                    percurso.caloria = per.caloria;
                    percurso.descricao = per.descricao;
                    percurso.distancia = per.distancia;
                    percurso.duracao = per.duracao;
                    percurso.Pace = per.Pace;
                    percurso.velocidade_max = per.velocidade_max;
                    percurso.velocidade_med = per.velocidade_med;
                    syr.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /**
         *  Adiciona pontos
         * */
        public void AddPonto(pontos po)
        {
            try
            {
                using (SeeYouEntities syr = new SeeYouEntities())
                {
                    syr.pontos.AddObject(po);
                    syr.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
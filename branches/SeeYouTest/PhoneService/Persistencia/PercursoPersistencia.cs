using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneService.Bussines
{
    public class PercursoPersistencia
    {
        //private SeeYouEntities SeeYouContext;
        
        public PercursoPersistencia()
        {     }

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
                    percurso = per;
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

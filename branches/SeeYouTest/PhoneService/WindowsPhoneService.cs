using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PhoneService.Model;
using PhoneService.Bussines;

namespace PhoneService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class WindowsPhoneService : IWindowsPhoneService
    {
        public PercursoPersistencia persitencia;
        
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public WindowsPhoneService ()
        {
            persitencia = new PercursoPersistencia();
        }

        int IWindowsPhoneService.NovoPercurso(int idUser, string isMetrico)
        {
            percursos pe = new percursos();
            pe.id_usuario = idUser;
            pe.data_percurso = DateTime.Now;
            pe.isMetrico = isMetrico;
            return persitencia.NovoPercurso(pe);
        }

        public void AtualizaPercurso(int id, int idUser, string descr, DateTime data, double velocidadeMedia, double velocidadeMaxima, double distancia, double ritimo, double altitudeMaxima, double altitudeMinima, double altitudeVariacao, double calorias, string duracao)
        {
            percursos pe = new percursos();
            pe.id_usuario = idUser;
            pe.id_percurso = id;
            pe.descricao = descr;
            pe.data_percurso = data;
            pe.velocidade_med = velocidadeMedia;
            pe.velocidade_max = velocidadeMaxima;
            pe.distancia = distancia;
            pe.altitude_max = altitudeMaxima;
            pe.altitude_med = altitudeVariacao;
            pe.altitude_min = altitudeMinima;
            pe.duracao = duracao;
            persitencia.AtualizaPercurso(pe);
        }

        public void AddPontos(int idPercurso, DateTime data, double velocidade, double distancia, double ritimo, double altitudeMaxima, double altitudeMinima, double altitudeVariacao, double calorias, int duracao, string longitude, string latitude, string isMetrico)
        {
            pontos po = new pontos();
            po.id_percurso = idPercurso;
            po.distancia = distancia;
            po.velocidade = velocidade;
            po.altitude_max = altitudeMaxima;
            po.altitude_med = altitudeVariacao;
            po.altitude_min = altitudeMinima;
            po.duracao = duracao;
            po.longitude = longitude;
            po.latitude = latitude;
            po.caloria = calorias;
            po.data_ponto = data;
            po.isMetrico = isMetrico;
            persitencia.AddPonto(po);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeeYouSite.ViewModels
{
    public class PontoViewModel
    {
        //constantes para conversao
        private const double MILHAS = 1.6D;
        const double PES = 3.28D;

        public long id_ponto { get; set; }
        public long id_percurso { get; set; }
        public string data_ponto { get; set; }
        //variaveis para verificar se o percurso/ponto e atual
        public DateTime ultima_data { get; set; }
        public bool ativo { get; set; }
        
        public string latitude { get; set; }
        public string longitude { get; set; }
        public double caloria { get; set; }
        public double distancia { get; set; }
        //variaveis de ritmo
        public double pace { get; set; }
        public string paceFormat { get; set; }
        public string impPaceFormat { get; set; }

        public string duracao { get; set; }
        public double velocidade { get; set; }
        public string isMetrico { get; set; }
        public double altitude { get; set; }

        //para conversao em imperial imp=imperial
        public double impVelocidade { get; set; }
        public double impDistancia { get; set; }
        public double impPace { get; set; }
        public double impAltitude { get; set; }

        public double getImperialVelocidade()
        {
            return Math.Round(velocidade / MILHAS);
        }
        public double getImperialDistancia()
        {
            return Math.Round(distancia / MILHAS);
        }
        public double getImperialAltitude()
        {
            return Math.Round(altitude / PES);
        }
        //fim

        /// <summary>
        /// Verifica se o ultimo ponto e de um percurso ativo(em andamento)
        /// se o ultimo ponto marcado foi a mais de 5 minutos atras, considera
        /// como percurso inativo
        /// </summary>
        /// <returns>false- nao esta ativo, true- esta ativo</returns>
        public bool estaAtivo()
        {
            TimeSpan diferenca = new TimeSpan();
            diferenca = DateTime.Now.Subtract(ultima_data);
            Console.WriteLine(diferenca);

            if (diferenca.TotalMinutes > 5)
                return false;
            else
                return true;
        }

        /// <summary>
        /// seta a duracao calculando os segundos recebidos como parametro para
        /// 00:00:00 >> hh:mm:ss
        /// </summary>
        /// <param name="timeSegundos"></param>
        public void setDuracao(int timeSegundos)
        {
            int Segundos = timeSegundos % 60;
            timeSegundos /= 60;
            int Minutos = timeSegundos % 60;
            timeSegundos /= 60;
            int Horas = timeSegundos % 24;

            duracao = Horas.ToString("00") + ":" + Minutos.ToString("00") + ":" + Segundos.ToString("00");
        }

        /// <summary>
        /// Formata a string de ritmo(pace)
        /// </summary>
        /// <param name="pace em imperial ou metrico"></param>
        public string getPaceFormat(double ritmoParam)
        {
            int ritmo = (int)ritmoParam;

            int Segundos = ritmo % 60;
            ritmo /= 60;
            int Minutos = ritmo % 60;
            ritmo /= 60;
            int Horas = ritmo % 24;

            return Horas.ToString("00") + ":" + Minutos.ToString("00") + ":" + Segundos.ToString("00");
        }

        /// <summary>
        /// Retorna o calculo de ritmo para a distancia em km
        /// Ritmo e quanto o atleta demora para percorrer um km
        /// </summary>
        /// <param name="timeSegundos">Duracao do percurso ate o momento</param>
        /// <returns></returns>
        public double getPace(int timeSegundos)
        {
            if (timeSegundos == 0)
                return 0;
            if (distancia == 0)
                return 0;

            return Math.Round(timeSegundos / distancia);
        }

        /// <summary>
        /// Retorna o calculo de ritmo para a distancia em imperial (milhas)
        /// Ritmo e quanto o atleta demora para percorrer uma milha
        /// </summary>
        /// <param name="timeSegundos">Duracao do percurso ate o momento</param>
        /// <returns></returns>
        public double getImperialPace(int timeSegundos)
        {
            if (timeSegundos == 0)
                return 0;
            if (getImperialDistancia() == 0)
                return 0;

            return timeSegundos / getImperialDistancia();
        }
    }
}
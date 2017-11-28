using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeeYouSite.ViewModels
{
    public class PercursoViewModel
    {
        const double MILHAS = 1.6D;
        const double PES = 3.28D;

        public long id_percurso { get; set; }
        public long id_usuario { get; set; }
        public string data_percurso { get; set; }
        public string percurso { get; set; }
        public string descricao { get; set; }
        public double caloria { get; set; }
        public double distancia { get; set; }
        public double altitude_min { get; set; }
        public double altitude_med { get; set; }
        public double altitude_max { get; set; }
        //variaves de ritmo
        public double Pace { get; set; }
        public string PaceFormat { get; set; }
        public string impPaceFormat { get; set; }

        public double velocidade_med { get; set; }
        public double velocidade_max { get; set; }
        public string duracao { get; set; }
        public string isMetrico { get; set; }

        //para conversao em imperial imp=imperial
        public double impVelocidade_med { get; set; }
        public double impVelocidade_max { get; set; }
        public double impDistancia { get; set; }
        //imperial pace nao tem como pois a duracao e recebida como string
        //impossibilitando o calculo
        public double impPace { get; set; }
        public double impAltitude_min { get; set; }
        public double impAltitude_med { get; set; }
        public double impAltitude_max { get; set; }

        public double getImperialMilhas(double medida)
        {
            return Math.Round(medida / MILHAS, 3);
        }

        public double getImperialPes(double medida)
        {
            return Math.Round(medida / PES, 3);
        }
        //fim

        /// <summary>
        /// Formata a string de ritmo(pace)
        /// </summary>
        /// <param name="ritmoParam"></param>
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
    }


}
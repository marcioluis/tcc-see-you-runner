using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneService.Model
{
    public class Percurso
    {
        public DateTime data { get; set; }
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public double VelocidadeMedia { get; set; }
        public double VelocidadeMaxima { get; set; }
        public double Distancia { get; set; }
        public double Ritimo { get; set; }
        public int Duracao { get; set; }
        public double AltitudeMaxima { get; set; }
        public double AltitudeMinima { get; set; }
        public double AltitudeVariacao { get; set; }
        public double Calorias { get; set; }
    }
}

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Model
{
    public class Ponto
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
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}

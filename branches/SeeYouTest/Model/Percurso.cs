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
using System.Collections.ObjectModel;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;

namespace Model
{
    public class Percurso
    {
        public DateTime data { get; set; }
        public int Id { get; set; }
        public int Segundos { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public double VelocidadeMedia { get; set; }
        public double VelocidadeMaxima { get; set; }
        public double Distancia { get; set; }
        public double Ritimo { get; set; }
        public string Duracao { get; set; }
        public double AltitudeMaxima { get; set; }
        public double AltitudeMinima { get; set; }
        public double AltitudeVariacao { get; set; }
        public double Calorias { get; set; }
        public string SistemaMetrico { get; set; }
        public string TituloPercurso { get; set; }
        public bool IsSave { get; set; }
        public ObservableCollection<GeoPosition<GeoCoordinate>> Locations { get; set; }
        public LocationCollection DesenhoCaminho { get; set; }
    }
}

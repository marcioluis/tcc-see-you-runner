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
using MVVM_Core;
using System.Collections.ObjectModel;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;
using SeeYouRunner.Model;
using SeeYouRunner.Negocio;
using Microsoft.Phone.Shell;

namespace SeeYouRunner.ViewModels
{
    public class MetricsViewModel : ViewModelBase
    {

        WindowsPhoneServiceProducao.WindowsPhoneServiceClient service;
        private Percurso percurso;
        private PercursoNegocio perNeg;
        private ConfiguracaoNegocio confNeg;

        private string _descricao;
        private string _velocidadeApre;
        public double Velocidade { get; set; }
        private string _velocidadeMediaApre;
        public double VelocidadeMedia { get; set; }
        private string _velocidadeMaximaApre;
        public double VelocidadeMaxima { get; set; }
        private string _distanciaApre;
        public double Distancia { get; set; }
        private string _ritimoApre;
        public double Ritimo { get; set; }
        private string _duracao = "00:00:00";
        private double _altitude;
        private string _altitudeMaximaApre;
        public double AltitudeMaxima { get; set; }
        private double _altitudeMinima = 99999;
        private string _altitudeVariacaoApre;
        public double AltitudeVariacao { get; set; }
        private string _caloriasApre;
        public double Calorias { get; set; }
        private int _segundos;
        private string _sistemaMetrico;
        private DateTime _data;

        private ObservableCollection<GeoPosition<GeoCoordinate>> _location;
        private LocationCollection _desenhoCaminho;


        #region Notify

        public ObservableCollection<GeoPosition<GeoCoordinate>> Locations
        {
            get
            {
                return _location;
            }
            set
            {
                if (_location == value)
                {
                    return;
                }
                _location = value;
                base.NotifyPropertyChanged("Locations");
            }
        }

        public LocationCollection DesenhoCaminho
        {
            get { return _desenhoCaminho; }
            set
            {
                _desenhoCaminho = value;
                base.NotifyPropertyChanged("DesenhoCaminho");
            }
        }

        public void InicializaListas()
        {
            DesenhoCaminho = new LocationCollection();
            Locations = new ObservableCollection<GeoPosition<GeoCoordinate>> { };
        }

        public string Descricao
        {
            get
            {
                return _descricao;
            }
            set
            {
                _descricao = value;
                base.NotifyPropertyChanged("Descricao");
            }
        }

        public string SistemaMetrico
        {
            get
            {
                _sistemaMetrico = confNeg.ConfIsMetrico() == true ? "km/h" : "mph";
                return _sistemaMetrico;
            }
            set
            {

                _sistemaMetrico = value;
                base.NotifyPropertyChanged("SistemaMetrico");
            }
        }

        public DateTime Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                base.NotifyPropertyChanged("Data");
            }
        }

        public string VelocidadeApre
        {
            get
            {
                return _velocidadeApre;
            }
            set
            {
                _velocidadeApre = value;
                base.NotifyPropertyChanged("VelocidadeApre");
            }
        }


        public string VelocidadeMediaApre
        {
            get
            {
                return _velocidadeMediaApre;
            }
            set
            {
                _velocidadeMediaApre = value;
                base.NotifyPropertyChanged("VelocidadeMediaApre");
            }
        }

        public string VelocidadeMaximaApre
        {
            get
            {
                return _velocidadeMaximaApre;
            }
            set
            {
                _velocidadeMaximaApre = value;
                base.NotifyPropertyChanged("VelocidadeMaximaApre");
            }
        }

        public string DistanciaApre
        {
            get
            {
                return _distanciaApre;
            }
            set
            {
                _distanciaApre = value;
                base.NotifyPropertyChanged("DistanciaApre");
            }
        }

        public string RitimoApre
        {
            get
            {
                return _ritimoApre;
            }
            set
            {
                _ritimoApre = value;
                base.NotifyPropertyChanged("RitimoApre");
            }
        }

        public string Duracao
        {
            get
            {
                return _duracao;
            }
            set
            {
                _duracao = value;
                base.NotifyPropertyChanged("Duracao");
            }
        }

        public int Segundos
        {
            get
            {
                return _segundos;
            }
            set
            {
                _segundos = value;
                base.NotifyPropertyChanged("Segundos");
            }
        }

        public double Altitude
        {
            get
            {
                return _altitude;
            }
            set
            {
                _altitude = value;
                base.NotifyPropertyChanged("Altitude");
            }
        }

        public string AltitudeMaximaApre
        {
            get
            {
                return _altitudeMaximaApre;
            }
            set
            {
                _altitudeMaximaApre = value;
                base.NotifyPropertyChanged("AltitudeMaximaApre");
            }
        }

        public double AltitudeMinima
        {
            get
            {
                return _altitudeMinima;
            }
            set
            {
                _altitudeMinima = value;
                base.NotifyPropertyChanged("AltitudeMinima");
            }
        }

        public string AltitudeVariacaoApre
        {
            get
            {
                return _altitudeVariacaoApre;
            }
            set
            {
                _altitudeVariacaoApre = value;
                base.NotifyPropertyChanged("AltitudeVariacaoApre");
            }
        }

        public string CaloriasApre
        {
            get
            {
                return _caloriasApre;
            }
            set
            {
                _caloriasApre = value;
                base.NotifyPropertyChanged("CaloriasApre");
            }
        }

        #endregion

        public MetricsViewModel()
        {
            service = new WindowsPhoneServiceProducao.WindowsPhoneServiceClient();
            percurso = new Percurso();
            perNeg = new PercursoNegocio();
            confNeg = new ConfiguracaoNegocio();

        }

        public void NovoPercursoService(string isMetrico)
        {
            service.NovoPercursoAsync(2, isMetrico);
            service.NovoPercursoCompleted += new EventHandler
            <WindowsPhoneServiceProducao.NovoPercursoCompletedEventArgs>(service_NovoPercursoCompleted);

        }

        private void service_NovoPercursoCompleted
        (object sender, WindowsPhoneServiceProducao.NovoPercursoCompletedEventArgs e)
        {
            percurso.Id = e.Result;
            percurso.data = DateTime.Now;
            Data = percurso.data;
            percurso.IdUsuario = 2;
        }

        public void AdicionaPontosService(double velocidade, double distancia, double ritimo, double altitude, double calorias, int duracao, string longitude, string latitude, string isMetrico)
        {
            try
            {
                latitude = latitude.Replace(",", ".");
                longitude = longitude.Replace(",", ".");
                service.AddPontosAsync(percurso.Id, DateTime.Now, velocidade, distancia, ritimo, altitude, calorias, duracao, longitude, latitude, isMetrico);
            }
            catch (Exception ex)
            {
            }
        }

        public void FimDePercurso()
        {
            percurso.VelocidadeMaxima = VelocidadeMaxima;
            percurso.VelocidadeMedia = VelocidadeMedia;
            percurso.Distancia = Distancia;
            percurso.Ritimo = Ritimo;
            percurso.AltitudeMaxima = AltitudeMaxima;
            percurso.AltitudeMinima = AltitudeMinima;
            percurso.AltitudeVariacao = AltitudeVariacao;
            percurso.Calorias = Calorias;
            percurso.Duracao = Duracao;
            percurso.Segundos = Segundos;
            percurso.TituloPercurso = DateTime.Now.ToString("dd/MM/yyyy");
            percurso.Locations = Locations;
            percurso.SistemaMetrico = confNeg.ConfIsMetrico() == true ? "Métrico" : "Imperial";
            percurso.data = Data;
            percurso.DesenhoCaminho = DesenhoCaminho;
            percurso.IsSave = false;
            PhoneApplicationService.Current.State.Remove("DataSaida");
            PhoneApplicationService.Current.State.Remove("SegundosSaida");
            
            perNeg.SetPrePercurso(percurso);
        }

        public void PrePercurso()
        {
            PercursoNegocio perNeg = new PercursoNegocio();
            percurso = perNeg.GetPrePercurso();


            VelocidadeMaxima = percurso.VelocidadeMaxima;
            VelocidadeMedia = percurso.VelocidadeMedia;
            Distancia = percurso.Distancia;
            Ritimo = percurso.Ritimo;
            AltitudeMaxima = percurso.AltitudeMaxima;
            AltitudeMinima = percurso.AltitudeMinima;
            AltitudeVariacao = percurso.AltitudeVariacao;
            Calorias = percurso.Calorias;
            Duracao = percurso.Duracao;
            Locations = percurso.Locations;
            Data = percurso.data;
            Segundos = percurso.Segundos;
            //return percurso;
        }

        public void CalculaApresentacao()
        {
            if (confNeg.ConfIsMetrico())
            {
                VelocidadeApre = Velocidade == 0 ? "0.00" : Velocidade.ToString();
                VelocidadeMaximaApre = VelocidadeMaxima == 0 ? "0.00" : VelocidadeMaxima.ToString();
                VelocidadeMediaApre = VelocidadeMedia == 0 ? "0.00" : VelocidadeMedia.ToString();
                DistanciaApre = Distancia == 0 ? "0.00 km" : Distancia.ToString() + " km";
                RitimoApre = CalculaRitimo(confNeg.ConfIsMetrico());
                AltitudeMaximaApre = AltitudeMaxima.ToString() + " m";
                AltitudeVariacaoApre = AltitudeVariacao.ToString() + " m";
                CaloriasApre = Calorias.ToString() + " Kcal";
            }
            else
            {
                VelocidadeApre = Velocidade == 0 ? "0.00" : (Velocidade / 1.6).ToString("0.00");
                VelocidadeMaximaApre = VelocidadeMaxima == 0 ? "0.00" : (VelocidadeMaxima / 1.6).ToString("0.00");
                VelocidadeMediaApre = VelocidadeMedia == 0 ? "0.00" : (VelocidadeMedia / 1.6).ToString("0.00");
                DistanciaApre = Distancia == 0 ? "0.00 mi" : (Distancia / 1.6).ToString("0.00") + " mi";
                RitimoApre = CalculaRitimo(confNeg.ConfIsMetrico());
                AltitudeMaximaApre = (AltitudeMaxima / 0.3048).ToString("0.00") + " p";
                AltitudeVariacaoApre = (AltitudeVariacao / 0.3048).ToString("0.00") + " p";
                CaloriasApre = Calorias.ToString() + " Kcal";
            }
        }

        public string CalculaRitimo(bool metrico)
        {
            double aux;
            if (Segundos == 0)
            {
                return metrico == true ? "- /km" : "- /mi";
            }
            else
            {
                Ritimo = metrico == true ? Segundos / Distancia : Segundos / (Distancia / 1.6);
                aux = Ritimo;

                int seg = (int)aux % 60;
                aux /= 60;
                int min = (int)aux % 60;
                aux /= 60;
                int horas = (int)aux % 24;

                return metrico == true ? min.ToString("00") + ":" + seg.ToString("00" + " /km") : min.ToString("00") + ":" + seg.ToString("00" + " /mi");
            }
        }
    }
}

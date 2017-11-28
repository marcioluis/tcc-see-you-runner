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
using MvvmCore;
using Model;
using PhoneNegocio;
using System.Collections.ObjectModel;
using System.Device.Location;

namespace SeeYouTest.ViewModels
{
    public class InicialViewModel : ViewModelBase
    {
        WindowsPhoneService.WindowsPhoneServiceClient service;
        private Percurso percurso;
        private PercursoNegocio perNeg;
        private ConfiguracaoNegocio confNeg;

        private string _descricao;
        private double _velocidade;
        private double _velocidadeMedia;
        private double _velocidadeMaxima;
        private double _distancia;
        private double _ritimo;
        private string _duracao = "00:00:00";
        private double _altitude;
        private double _altitudeMaxima;
        private double _altitudeMinima;
        private double _altitudeVariacao;
        private double _calorias;
        private int _segundos;
        private string _sistemaMetrico;
        private DateTime _data;
        private ObservableCollection<GeoCoordinate> _location;
        
        private string _duracaoTotal;
        private int _numPercursos;
        private double _caloriasTotal;
        private double _distanciaTotal;
        private ObservableCollection<Percurso> _percursos;

        #region Notify

        public string DuracaoTotal
        {
            get
            {
                return _duracaoTotal;
            }
            set
            {
                _duracaoTotal = value;
                base.NotifyPropertyChanged("DuracaoTotal");
            }
        }

        public int NumPercursos
        {
            get
            {
                return _numPercursos;
            }
            set
            {
                _numPercursos = value;
                base.NotifyPropertyChanged("NumPercursos");
            }
        }

        public double CaloriasTotal
        {
            get
            {
                return _caloriasTotal;
            }
            set
            {
                _caloriasTotal = value;
                base.NotifyPropertyChanged("CaloriasTotal");
            }
        }

        public double DistanciaTotal
        {
            get
            {
                return _distanciaTotal;
            }
            set
            {
                _distanciaTotal = value;
                base.NotifyPropertyChanged("DistanciaTotal");
            }
        }



        public ObservableCollection<GeoCoordinate> Locations
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

        public ObservableCollection<Percurso> Percursos
        {
            get
            {
                return _percursos;
            }
            set
            {
                if (_percursos == value)
                {
                    return;
                }
                _percursos = value;
                base.NotifyPropertyChanged("Locations");
            }
        }

        public void InicializaListas()
        {
            Locations = new ObservableCollection<GeoCoordinate> { };
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
                _sistemaMetrico = confNeg.ConfIsMetrico() == true ? "Km" : "Mi";
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

        public double Velocidade
        {
            get
            {
                return _velocidade;
            }
            set
            {
                _velocidade = value;
                base.NotifyPropertyChanged("Velocidade");
            }
        }


        public double VelocidadeMedia
        {
            get
            {
                return _velocidadeMedia;
            }
            set
            {
                _velocidadeMedia = value;
                base.NotifyPropertyChanged("VelocidadeMedia");
            }
        }

        public double VelocidadeMaxima
        {
            get
            {
                return _velocidadeMaxima;
            }
            set
            {
                _velocidadeMaxima = value;
                base.NotifyPropertyChanged("VelocidadeMaxima");
            }
        }

        public double Distancia
        {
            get
            {
                return _distancia;
            }
            set
            {
                _distancia = value;
                base.NotifyPropertyChanged("Distancia");
            }
        }

        public double Ritimo
        {
            get
            {
                return _ritimo;
            }
            set
            {
                _ritimo = value;
                base.NotifyPropertyChanged("Ritimo");
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
                base.NotifyPropertyChanged("Duracao");
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

        public double AltitudeMaxima
        {
            get
            {
                return _altitudeMaxima;
            }
            set
            {
                _altitudeMaxima = value;
                base.NotifyPropertyChanged("AltitudeMaxima");
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

        public double AltitudeVariacao
        {
            get
            {
                return _altitudeVariacao;
            }
            set
            {
                _altitudeVariacao = value;
                base.NotifyPropertyChanged("AltitudeVariacao");
            }
        }

        public double Calorias
        {
            get
            {
                return _calorias;
            }
            set
            {
                _calorias = value;
                base.NotifyPropertyChanged("Calorias");
            }
        }

        #endregion
    }
}

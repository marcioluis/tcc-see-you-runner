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
using Microsoft.Phone.Controls.Maps;

namespace SeeYouTest.ViewModels
{
    public class PercursoViewModel : ViewModelBase
    {
        private Percurso percurso;
        private PercursoNegocio perNeg;
        private ConfiguracaoNegocio confNeg;
        private PercursoApresentacao percursoApre;
        private WindowsPhoneServiceProducao.WindowsPhoneServiceClient service;

        private string _descricao = "add descrição...";
        private string _tituloPercurso;
        private string _velocidade;
        private string _velocidadeMedia;
        private string _velocidadeMaxima;
        private string _distancia;
        private string _ritimo;
        private string _duracao = "00:00:00";
        private string _altitude;
        private string _altitudeMaxima;
        private string _altitudeMinima;
        private string _altitudeVariacao;
        private string _calorias;
        private int _segundos;
        private string _data;
        private ObservableCollection<GeoPosition<GeoCoordinate>> _location;
        private LocationCollection _desenhoCaminho;
        private Percurso _percurso;

        private string _duracaoTotal;
        private int _numPercursos;
        private string _caloriasTotal;
        private string _distanciaTotal;
        private ObservableCollection<Percurso> _percursos;

        public int IdLista { get; set; }
        private string _distanciaLista { get; set; }
        private string _velMediaLista { get; set; }
        private string _duracaoLista { get; set; }
        private string _tituloPercursoLista { get; set; }
        private PercursoApresentacao _percursoApre;
        private ObservableCollection<PercursoApresentacao> _percursosLista;

        #region Notify

        public string DistanciaLista
        {
            get
            {
                return _distanciaLista;
            }
            set
            {
                _distanciaLista = value;
                base.NotifyPropertyChanged("DistanciaLista");
            }
        }

        public string VelMediaLista
        {
            get
            {
                return _velMediaLista;
            }
            set
            {
                _velMediaLista = value;
                base.NotifyPropertyChanged("VelMediaLista");
            }
        }

        public string DuracaoLista
        {
            get
            {
                return _duracaoLista;
            }
            set
            {
                _duracaoLista = value;
                base.NotifyPropertyChanged("DuracaoLista");
            }
        }

        public string TituloPercursoLista
        {
            get
            {
                return _tituloPercursoLista;
            }
            set
            {
                _tituloPercursoLista = value;
                base.NotifyPropertyChanged("TituloPercursoLista");
            }
        }

        public PercursoApresentacao PercursoApre
        {
            get
            {
                return _percursoApre;
            }
            set
            {
                if (value != null)
                {
                    _percursoApre = value;
                    base.NotifyPropertyChanged("PercursoApre");
                }
            }
        }

        public Percurso Percurso
        {
            get
            {
                return _percurso;
            }
            set
            {
                if (value != null)
                {
                    _percurso = value;
                    base.NotifyPropertyChanged("Percurso");
                }
            }
        }

        public ObservableCollection<PercursoApresentacao> PercursosLista
        {
            get
            {
                return _percursosLista;
            }
            set
            {
                if (_percursosLista == value)
                {
                    return;
                }
                _percursosLista = value;
                base.NotifyPropertyChanged("PercursosLista");
            }
        }

        public string TituloPercurso
        {
            get
            {
                return _tituloPercurso;
            }
            set
            {
                _tituloPercurso = value;
                base.NotifyPropertyChanged("TituloPercurso");
            }
        }

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

        public string CaloriasTotal
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

        public string DistanciaTotal
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
                base.NotifyPropertyChanged("Percursos");
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
            Locations = new ObservableCollection<GeoPosition<GeoCoordinate>> { };
            DesenhoCaminho = new LocationCollection();
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

        public string Data
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

        public string Velocidade
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


        public string VelocidadeMedia
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

        public string VelocidadeMaxima
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

        public string Distancia
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

        public string Ritimo
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
                base.NotifyPropertyChanged("Segundos");
            }
        }

        public string Altitude
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

        public string AltitudeMaxima
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

        public string AltitudeMinima
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

        public string AltitudeVariacao
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

        public string Calorias
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

        public PercursoViewModel()
        {
            service = new WindowsPhoneServiceProducao.WindowsPhoneServiceClient();
            percurso = new Percurso();
            percursoApre = new PercursoApresentacao();
            perNeg = new PercursoNegocio();
            confNeg = new ConfiguracaoNegocio();

            _percursoApre = new PercursoApresentacao();
            _percursos = new ObservableCollection<Percurso>();
            _percursosLista = new ObservableCollection<PercursoApresentacao>();
            _location = new ObservableCollection<GeoPosition<GeoCoordinate>>();

        }

        public void GetPrePercursoToString()
        {
            PercursoNegocio perNeg = new PercursoNegocio();
            percurso = perNeg.GetPrePercurso();
            Percurso = percurso;

            if (confNeg.ConfIsMetrico())
            {
                VelocidadeMaxima = percurso.VelocidadeMaxima.ToString() + " Km/h";
                VelocidadeMedia = percurso.VelocidadeMedia.ToString() + " Km/h";
                Distancia = percurso.Distancia.ToString() + " Km";
                AltitudeMaxima = percurso.AltitudeMaxima.ToString() + " m";
                AltitudeMinima = percurso.AltitudeMinima.ToString() + " m";
                AltitudeVariacao = percurso.AltitudeVariacao.ToString() + " m";
                Calorias = percurso.Calorias.ToString() + " Kcal";
                Duracao = percurso.Duracao.ToString();
                Locations = percurso.Locations;
                Data = percurso.data.ToString();
                Segundos = percurso.Segundos;
                TituloPercurso = percurso.TituloPercurso;
                DesenhoCaminho = percurso.DesenhoCaminho;
                Ritimo = CalculaRitimo(true);
                Descricao = percurso.Descricao == "" ? "add descrição..." : percurso.Descricao;
            }
            else
            {
                VelocidadeMaxima = (percurso.VelocidadeMaxima / 1.6).ToString("0.00") + " mph";
                VelocidadeMedia = (percurso.VelocidadeMedia / 1.6).ToString("0.00") + " mph";
                Distancia = (percurso.Distancia / 1.6).ToString("0.00") + " mi";
                AltitudeMaxima = (percurso.AltitudeMaxima / 0.3048).ToString("0.00") + " p";
                AltitudeMinima = (percurso.AltitudeMinima / 0.3048).ToString("0.00") + " p";
                AltitudeVariacao = (percurso.AltitudeVariacao / 0.3048).ToString("0.00") + " p";
                Calorias = percurso.Calorias.ToString() + " Kcal";
                Duracao = percurso.Duracao.ToString();
                Locations = percurso.Locations;
                Data = percurso.data.ToString();
                Segundos = percurso.Segundos;
                TituloPercurso = percurso.TituloPercurso;
                DesenhoCaminho = percurso.DesenhoCaminho;
                Ritimo = CalculaRitimo(false);
                Descricao = percurso.Descricao == "" ? "add descrição..." : percurso.Descricao;
            }
        }

        public void AtualizaPercursoServico()
        {
            percurso.Descricao = Descricao;
            if (percurso.Descricao == "add descrição..." || percurso.Descricao == null)
                percurso.Descricao = "";
            service.AtualizaPercursoAsync(percurso.Id, percurso.IdUsuario, percurso.Descricao, percurso.data, percurso.VelocidadeMedia, percurso.VelocidadeMaxima, percurso.Distancia, percurso.Ritimo, percurso.AltitudeMaxima, percurso.AltitudeMinima, percurso.AltitudeVariacao, percurso.Calorias, percurso.Duracao);
        }

        public void AddPercursoToLita()
        {
            percurso.Descricao = Descricao;
            if (percurso.Descricao == "add descrição..." || percurso.Descricao == null)
                percurso.Descricao = "";
            percurso.IsSave = true;
            perNeg.AddToListaPercurso(percurso);
        }

        public void GetPercursosFromLista()
        {
            Percursos = perNeg.GetListaPercursos();
        }

        public Percurso GetPercursoById(int id)
        {
            Percurso perc = new Percurso();
            foreach (Percurso per in Percursos)
            {
                if (per.Id == id)
                {
                    perc = per;
                    break;
                }               
            }
            return perc;
        }

        public void SetPrePercursoFromLista(Percurso per)
        {
            perNeg.SetPrePercurso(per);
        }

        public void ListaApresentacao()
        {
            PercursosLista.Clear();
            if (confNeg.ConfIsMetrico())
            {
                foreach (Percurso per in Percursos)
                {
                    PercursoApresentacao perApre = new PercursoApresentacao();
                    perApre.Id = per.Id;
                    perApre.Titulo = per.TituloPercurso;
                    perApre.Distancia = per.Distancia.ToString() + " km";
                    perApre.VelMedia = per.VelocidadeMedia.ToString() + " km/h";
                    perApre.Duracao = per.Duracao;
                    PercursosLista.Add(perApre);
                }
            }
            else
            {
                foreach (Percurso per in Percursos)
                {
                    PercursoApresentacao perApre = new PercursoApresentacao();
                    perApre.Id = per.Id;
                    perApre.Titulo = per.TituloPercurso;
                    perApre.Distancia = (per.Distancia / 1.6).ToString("0.00") + " mi";
                    perApre.VelMedia = (per.VelocidadeMedia / 1.6).ToString("0.00") + " mph";
                    perApre.Duracao = per.Duracao;
                    PercursosLista.Add(perApre);
                }
            }
            IdLista = percursoApre.Id;
            DistanciaLista = percursoApre.Distancia;
            VelMediaLista = percursoApre.VelMedia;
            DuracaoLista = percursoApre.Duracao;
            TituloPercursoLista = percursoApre.Titulo;
            
        }

        public void DeletePercurso()
        {
            perNeg.RemoveFromListaPercurso(Percurso);
        }

        public void AtualizaPercursoInLista()
        {
            percurso.Descricao = Descricao;
            if (percurso.Descricao == "add descrição..." || percurso.Descricao == null)
                percurso.Descricao = "";
            perNeg.UpdatePercursoToListaPercurso(percurso);
        }
        
        public void CalculaTotais()
        {
            int segundos = 0;
            NumPercursos = 0;
            double distancia = 0;
            double calorias = 0;
            DuracaoTotal = "00:00:00";
            CaloriasTotal = "0 Kcal";
            GetPercursosFromLista();
            foreach (Percurso per in Percursos)
            {
                NumPercursos += 1;
                distancia += per.Distancia;
                calorias += per.Calorias;
                segundos += per.Segundos;
            }

            DistanciaTotal = confNeg.ConfIsMetrico() == true ? FormatDouble(distancia,"0.00") + " km" : FormatDouble((distancia / 1.6), "0.00") + " mph";
            CaloriasTotal = FormatDouble(calorias, "0.00") + " kcal";
            int timeAux = segundos;

            int segundosm = timeAux % 60;
            timeAux /= 60;
            int minutosm = timeAux % 60;
            timeAux /= 60;
            int horasm = timeAux % 24;

            DuracaoTotal = horasm.ToString("00") + ":" + minutosm.ToString("00") + ":" + segundosm.ToString("00");
        }

        private string FormatDouble(double value, string format)
        {
            if (double.IsNaN(value))
                return "0.00";

            return value.ToString(format);
        }

        private string CalculaRitimo(bool metrico)
        {
            
            string ritimo;
            if (Segundos == 0)
            {
                return metrico == true ? "- /km" : "- /mi";
            }
            else
            {
                if (metrico == true)
                {
                    ritimo = FormatRitimo(percurso.Ritimo);
                    return ritimo + " /km";
                }
                else
                {
                    double ritimoSegundos;
                    ritimoSegundos = percurso.Segundos / (percurso.Distancia / 1.6);

                    ritimo = FormatRitimo(percurso.Ritimo);
                    return ritimo + " /mi";
                }
            }
        }

        private string FormatRitimo(double ritimo)
        {
            double aux;
            aux = ritimo;
            int seg = (int)aux % 60;
            aux /= 60;
            int min = (int)aux % 60;
            aux /= 60;
            int horas = (int)aux % 24;

            return min.ToString("00") + ":" + seg.ToString("00");
        }
    }
}

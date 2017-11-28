using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using System.Threading;
using PhoneNegocio;
using SeeYouTest.ViewModels;
using Microsoft.Phone.Shell;

namespace SeeYouTest.Views
{

    //Valores Met

    //corrida 8
    //natação 8
    //bicileta 6.5
    //futebol 7

    //Formula calorias
    //(mets da atividade x Kg do usuário) x (duração / 60 min);
    //(índice x peso) x Tempo em minutos = Gasto Calórico Total
    //(0.250 x 66) x 


//    Ciclismo - pedalando< 15 Km/h, lazer, trabalho 0,066 
//Ciclismo – pedalando 16 - 19 Km/h, lazer, esforço leve 0,100 
//Ciclismo – pedalando 20 - 22 Km/h, lazer, esforço moderado 0,133 
//Ciclismo – pedalando 23 - 25 Km/h, esforço vigoroso, competição 0,166 
//Ciclismo – pedalando 26 - 30 Km/h, esforço vigoroso, competição 0,200 
//Ciclismo – pedalando >30 Km/h, esforço muito vigoroso, competição 0,266 
 
//Bicicleta ergométrica - pedalando 50 W, esforço muito leve 0,050 
//Bicicleta ergométrica - pedalando 100 W, esforço fraco 0,091 
//Bicicleta ergométrica - pedalando 150 W, esforço moderado 0,116 
//Bicicleta ergométrica - pedalando 200 W, esforço vigoroso 0,175 
//Bicicleta ergométrica - pedalando 250 W, esforço muito vigoroso 0,207 
 
//Musculação – esforço leve 0,050 
//Musculação – esforço vigoroso 0,100 
 
//Ginástica aeróbica - baixo impacto 0,083 
//Ginástica aeróbica - alto impacto 0,116 
//Spep – plataforma baixa 0,142 
//Spep – plataforma alta 0,166 
 
//Caminhar – 4 Km/h, passo lento 0,050 
//Caminhar – 5 Km/h, passo moderado 0,083 
//Caminhar – 6,5 Km/h, passo rápido 0,108 
//Caminhar – 7,0 Km/h, passo muito rápido 0,116 
 
//Corrida – trote/caminhada 0,100 
//Corrida – trote 0,116 
//Corrida – 8 Km/h 0,133 
//Corrida – 9,5 Km/h 0,166 
//Corrida – 10,5 Km/h 0,183 
//Corrida – 12 Km/h 0,207 
//Corrida – 14,5 Km/h 0,250 
//Corrida – 16 Km/h 0,266 
 
//Hidroginástica 0,066 
//Natação – lazer 0,100 
//Natação – estilo livre, esforço leve a moderado 0,133 
//Natação – estilo livre, esforço vigoroso 0,166 
 
//Basquete - lazer 0,100 
//Basquete - competição 0,133 
 
//Futebol – lazer 0,116 
//Futebol – competição 0,166 
 
//Tênis – dupla 0,100 
//Tênis – individual 0,133 
 
//Vôlei – lazer 0,050 
//Vôlei – competição 0,066 
//Vôlei – praia 0,133 
 
//Judô, Karatê, Taekwondo 0,166 
//Boxe – saco de pancada 0,100 
//Boxe – luta 0,200 
 
//Rappel 0,133 
//Escalada 0,183 
 
//Surf 0,050 
//Golfe 0,075 
//Dança de salão 0,075 

    public partial class MetricsView : PhoneApplicationPage
    {
        ConfiguracaoNegocio Configuracao;
        bool IsMetrico;
        int Peso;

        public MetricsViewModel ViewModel { get; set; }
        
        GeoCoordinateWatcher watcher; // main location object
        bool trackingOn = false; // switches on w/button push
        Pushpin myPushpin = new Pushpin(); // marks current spot on Map
        double Distancia;
        GeoCoordinate Last;
        private MapPolyline linha;
        private LocationCollection _path;
        double VelMax;
        double altitudeMax, altitudeMin = 99999;
        bool controleIniciar = false;
        bool calcula = false;

        System.Windows.Threading.DispatcherTimer Timer;
        int timeSegundos = 0;

        // Constructor
        public MetricsView()
        {
            InitializeComponent();
            
            //para rodar com a tela bloqueada.
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;

            ViewModel = new MetricsViewModel();
            ViewModel.InicializaListas();
            //Instatiate watcher, setting its accuracy level and movement threshold.
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High); // using high accuracy.
            watcher.MovementThreshold = 25.0f; // meters of change before "PositionChanged" event fires.

            // wire up event handlers
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_statusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            // start up the Location Service on app startup. watcher_StatusChanged
            // will fire when start up of LocServ is complete.
            new Thread(startLocServInBackground).Start();
            //statusTextBlock.Text = "Starting Location Service...";

            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 1);
            Timer.Tick += new EventHandler(Timer_Tick);

            //Linha de percurso
            linha = new MapPolyline();
            linha.Stroke = new SolidColorBrush(Colors.Yellow);
            linha.StrokeThickness = 5;
            linha.Opacity = 0.8;
            map1.Children.Add(linha);
            _path = new LocationCollection();
            Configuracao = new ConfiguracaoNegocio();

            //Configurção
            IsMetrico = Configuracao.ConfIsMetrico();
            Peso = Configuracao.ConfiPeso();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModel;
            //ViewModel.InicializaListas();
            ViewModel.CalculaApresentacao();
            //if (IsMetrico == true)
            //{
            //    tbSMetrico.Text = "Km";
            //}
            //else
            //    tbSMetrico.Text = "Mi";
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            ViewModel.Segundos += 1;
            int timeAux = ViewModel.Segundos;

            int Segundos = timeAux % 60;
            timeAux /= 60;
            int Minutos = timeAux % 60;
            timeAux /= 60;
            int Horas = timeAux % 24;

            ViewModel.Duracao = Horas.ToString("00") + ":" + Minutos.ToString("00") + ":" + Segundos.ToString("00");
        }
        void startLocServInBackground()
        {
            watcher.TryStart(true, TimeSpan.FromSeconds(60));
        }
        void watcher_statusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // The Location Service is disabled or unsupported.
                    // Check to see if the user has disabled the location service.
                    if (watcher.Permission == GeoPositionPermission.Denied)
                    {
                        // the user has disabled LocServ on their device.
                        //statusTextBlock.Text = "You have disabled Location Service.";
                    }
                    else
                    {
                        //statusTextBlock.Text = "Location Service is not functioning on this device.";
                    }
                    break;

                case GeoPositionStatus.Initializing:
                    // The location service is initializing.
                    //statusTextBlock.Text = "Location Service is retrieving data...";
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data
                    // due to poor signal fidelity (most likely)
                    //statusTextBlock.Text = "Location data is not available.";
                    break;

                case GeoPositionStatus.Ready:
                    // The location service is working and is receiving location data.
                    //statusTextBlock.Text = "Location data is available.";
                    break;
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            // update the textblock readouts.
            //latitudeTextblock.Text = e.Position.Location.Latitude.ToString("0.0000000000");
            //tbDistancia.Text = e.Position.Location.Longitude.ToString("0.0000000000");
            //tbVel.Text = this.FormatDouble(e.Position.Location.Speed * 3.6, "0.00");
            //tbVel.Text = e.Position.Location.Speed.ToString("0.0") + " meters per second";
            //coursereadout.Text = e.Position.Location.Course.ToString("0.0") + " degrees";
            //altitudereadout.Text = e.Position.Location.Altitude.ToString("0.0") + " meters above sea level";
            if (map1.Children.Contains(myPushpin) == false) { 
                map1.Children.Add(myPushpin);
                map1.ZoomLevel = 16.0f;
                myPushpin.Location = e.Position.Location;
                map1.Center = e.Position.Location;
                ViewModel.Locations.Add(e.Position);
            };

            // update the map if the user has asked to be tracked.
            if (calcula)
            {
                if (trackingOn)
                {
                    // center the pushpin and map on the current position
                    ViewModel.Locations.Add(e.Position);
                    myPushpin.Location = e.Position.Location;
                    map1.Center = e.Position.Location;
                    _path.Add(e.Position.Location);
                    linha.Locations = _path;
                    // if this is the first time that myPushPin is being plotted, add it to the object
                    // hierarchy!
                    
                    ///////////////////////

                    if (this.Last != null)
                    {
                        this.Distancia += e.Position.Location.GetDistanceTo(this.Last);

                    }
                        //Distancia
                        ViewModel.Distancia = Convert.ToDouble(this.FormatDouble(Distancia / 1000, "0.00"));
                        //ViewModel.this.lbMetricaDistanciaVal.Text = this.FormatDouble(this.Distancia / 1000, "0.00");

                        //Velocidade
                        ViewModel.Velocidade = Convert.ToDouble(this.FormatDouble(e.Position.Location.Speed * 3.6, "0.00"));
                        ViewModel.VelocidadeMaxima = Convert.ToDouble(this.FormatDouble(e.Position.Location.Speed * 3.6 > ViewModel.VelocidadeMaxima ? e.Position.Location.Speed * 3.6 : ViewModel.VelocidadeMaxima, "0.00"));
                        ViewModel.VelocidadeMedia = Convert.ToDouble(this.FormatDouble(((Distancia / ViewModel.Segundos) * 3.6), "0.00"));

                        //Altitude; 
                        ViewModel.Altitude = e.Position.Location.Altitude.ToString() == "NaN" ? 0 :  Convert.ToDouble(FormatDouble(e.Position.Location.Altitude,"0.00"));
                        if (e.Position.Location.Altitude < 99999)
                        {
                            ViewModel.AltitudeMinima = ViewModel.Altitude < ViewModel.AltitudeMinima ? ViewModel.Altitude : ViewModel.AltitudeMinima;
                        }
                        //ViewModel.AltitudeMaxima = e.Position.Location.Altitude > ViewModel.AltitudeMaxima ? e.Position.Location.Altitude : ViewModel.AltitudeMaxima;
                        ViewModel.AltitudeMaxima = Convert.ToDouble(FormatDouble(ViewModel.Altitude > ViewModel.AltitudeMaxima ? ViewModel.Altitude : ViewModel.AltitudeMaxima, "0.00"));
                        ViewModel.AltitudeVariacao = Convert.ToDouble(FormatDouble((ViewModel.AltitudeMaxima - ViewModel.AltitudeMinima) / 1000, "0.00"));

                        ViewModel.AdicionaPontosService(ViewModel.Velocidade, ViewModel.Distancia, ViewModel.Ritimo, ViewModel.Altitude, ViewModel.Calorias, ViewModel.Segundos, e.Position.Location.Longitude.ToString(), e.Position.Location.Latitude.ToString(), "s");
                        ViewModel.CalculaApresentacao();
                    }
                this.Last = e.Position.Location;
                ViewModel.Calorias = Convert.ToDouble(FormatDouble(((0.250 * Peso) * ViewModel.Segundos / 60),"0.00"));
            }
        }


        private string FormatDouble(double value, string format)
        {
            if (double.IsNaN(value))
                return "0.00";

            return value.ToString(format);
        }


        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            if (btnIniciar.Content.ToString() == "Parar")
            {
                
                btnIniciar.Content = "Iniciar";
                trackingOn = false;
                calcula = false;
                //map1.ZoomLevel = 1.0f; // zoom out to see whole world.
                //watcher.Stop();
                Timer.Stop();
                btnIniciar.Visibility = Visibility.Collapsed;
                btnEncerrar.Visibility = Visibility.Visible;
                btnContinuar.Visibility = Visibility.Visible;
            }
            else if(btnIniciar.Content.ToString() == "Iniciar") 
            {
                new Thread(startLocServInBackground).Start();
                btnIniciar.Content = "Parar";
                trackingOn = true;
                calcula = true;
                //map1.ZoomLevel = 16.0f; // zoom to street level.
                if (controleIniciar == false)
                {
                    if (IsMetrico == true)
                    {
                        ViewModel.NovoPercursoService("s");
                    }
                    else
                    {
                        ViewModel.NovoPercursoService("n");
                    }
                        controleIniciar = true;
                }
                Timer.Start();
                
            }
        }

        private void btnEncerrar_Click(object sender, RoutedEventArgs e)
        {
            watcher.Stop();
            ViewModel.FimDePercurso();
            NavigationService.Navigate(new Uri("/Views/FimDePercursoView.xaml", UriKind.Relative));
               // NavigationService.Navigate(new Uri("/Views/MetricsView.xaml", UriKind.Relative));
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            //new Thread(startLocServInBackground).Start();
            btnIniciar.Content = "Parar";
            trackingOn = true;
            calcula = true;
            //map1.ZoomLevel = 16.0f; // zoom to street level.
            //if (controleIniciar == false)
            //{
            //    if (IsMetrico == true)
            //    {
            //        //ViewModel.NovoPercursoService("s");
            //    }
            //    else
            //    {
            //        //ViewModel.NovoPercursoService("n");
            //    }
            //    controleIniciar = true;
            //}
            Timer.Start();
            btnIniciar.Visibility = Visibility.Visible;
            btnEncerrar.Visibility = Visibility.Collapsed;
            btnContinuar.Visibility = Visibility.Collapsed;
        }
    }
}
//using System;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
//using System.Device.Location;
//using Microsoft.Phone.Controls.Maps;
//using System.Threading;

//namespace MapHelper
//{
//    public class MapHelp
//    {
//        GeoCoordinateWatcher watcher; // main location object
//        bool trackingOn = false; // switches on w/button push
//        Pushpin myPushpin = new Pushpin(); // marks current spot on Map
//        double Distancia;
//        GeoCoordinate Last;
//        private MapPolyline linha;
//        private LocationCollection _path;
//        double VelMax;
//        double altitudeMax, altitudeMin = 99999;

//        System.Windows.Threading.DispatcherTimer Timer;
//        int timeSegundos = 0;

//        // Constructor
//        public MapHelp()
//        {
//            //InitializeComponent();

//            //Instatiate watcher, setting its accuracy level and movement threshold.
//            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High); // using high accuracy.
//            watcher.MovementThreshold = 10.0f; // meters of change before "PositionChanged" event fires.

//            // wire up event handlers
//            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_statusChanged);
//            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

//            // start up the Location Service on app startup. watcher_StatusChanged
//            // will fire when start up of LocServ is complete.
//            new Thread(startLocServInBackground).Start();
//            //statusTextBlock.Text = "Starting Location Service...";

//            Timer = new System.Windows.Threading.DispatcherTimer();
//            Timer.Interval = new TimeSpan(0, 0, 0, 1);
//            Timer.Tick += new EventHandler(Timer_Tick);

//            //Linha de percurso
//            linha = new MapPolyline();
//            linha.Stroke = new SolidColorBrush(Colors.Yellow);
//            linha.StrokeThickness = 5;
//            linha.Opacity = 0.8;
//            map1.Children.Add(linha);
//            _path = new LocationCollection();
//        }

//        void Timer_Tick(object sender, EventArgs e)
//        {
//            timeSegundos += 1;

//            int Horas = timeSegundos / 1000;
//            int Minutos = timeSegundos / 60;
//            int Segundos = timeSegundos % 60;

//            tbTempo.Text = Horas.ToString("00") + ":" + Minutos.ToString("00") + ":" + Segundos.ToString("00");
//            lbMetricaDuracaoVal.Text = Horas.ToString("00") + ":" + Minutos.ToString("00") + ":" + Segundos.ToString("00");
//        }
//        void startLocServInBackground()
//        {
//            watcher.TryStart(true, TimeSpan.FromSeconds(60));
//        }
//        void watcher_statusChanged(object sender, GeoPositionStatusChangedEventArgs e)
//        {
//            switch (e.Status)
//            {
//                case GeoPositionStatus.Disabled:
//                    // The Location Service is disabled or unsupported.
//                    // Check to see if the user has disabled the location service.
//                    if (watcher.Permission == GeoPositionPermission.Denied)
//                    {
//                        // the user has disabled LocServ on their device.
//                        //statusTextBlock.Text = "You have disabled Location Service.";
//                    }
//                    else
//                    {
//                        //statusTextBlock.Text = "Location Service is not functioning on this device.";
//                    }
//                    break;

//                case GeoPositionStatus.Initializing:
//                    // The location service is initializing.
//                    //statusTextBlock.Text = "Location Service is retrieving data...";
//                    break;

//                case GeoPositionStatus.NoData:
//                    // The Location Service is working, but it cannot get location data
//                    // due to poor signal fidelity (most likely)
//                    //statusTextBlock.Text = "Location data is not available.";
//                    break;

//                case GeoPositionStatus.Ready:
//                    // The location service is working and is receiving location data.
//                    //statusTextBlock.Text = "Location data is available.";
//                    break;
//            }
//        }

//        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
//        {
//            // update the textblock readouts.
//            //latitudeTextblock.Text = e.Position.Location.Latitude.ToString("0.0000000000");
//            //tbDistancia.Text = e.Position.Location.Longitude.ToString("0.0000000000");
//            tbVel.Text = this.FormatDouble(e.Position.Location.Speed * 3.6, "0.00");
//            //tbVel.Text = e.Position.Location.Speed.ToString("0.0") + " meters per second";
//            //coursereadout.Text = e.Position.Location.Course.ToString("0.0") + " degrees";
//            //altitudereadout.Text = e.Position.Location.Altitude.ToString("0.0") + " meters above sea level";

//            // update the map if the user has asked to be tracked.
//            if (trackingOn)
//            {
//                // center the pushpin and map on the current position
//                tbVel.Text = this.FormatDouble(e.Position.Location.Speed * 3.6, "0.00");
//                myPushpin.Location = e.Position.Location;
//                map1.Center = e.Position.Location;
//                _path.Add(e.Position.Location);
//                linha.Locations = _path;
//                // if this is the first time that myPushPin is being plotted, add it to the object
//                // hierarchy!
//                if (map1.Children.Contains(myPushpin) == false) { map1.Children.Add(myPushpin); };
//                ///////////////////////

//                if (this.Last != null)
//                {
//                    this.Distancia += e.Position.Location.GetDistanceTo(this.Last);
                    
//                }
//                //Distancia
//                this.tbDistancia.Text = this.FormatDouble(this.Distancia / 1000, "0.00");
//                this.lbMetricaDistanciaVal.Text = this.FormatDouble(this.Distancia / 1000, "0.00");
                
//                //Velocidade
//                VelMax = e.Position.Location.Speed * 3.6 > VelMax ? e.Position.Location.Speed * 3.6 : VelMax;
//                this.tbMetricaVelMediaVal.Text = this.FormatDouble(VelMax, "0.00");
//                this.tbVelm.Text = this.FormatDouble(((Distancia / timeSegundos) * 3.6), "0.00");
//                this.tbMetricaVelMediaVal.Text = this.FormatDouble(((Distancia / timeSegundos) * 3.6), "0.00");
//                this.tbMetricaVelMaxVal.Text = this.FormatDouble(VelMax, "0.00");

//                //Altitude
//                if (e.Position.Location.Altitude < 99999)
//                {
//                    altitudeMin = e.Position.Location.Altitude < altitudeMin ? e.Position.Location.Altitude : altitudeMin;
//                }
//                altitudeMax = e.Position.Location.Altitude > altitudeMax ? e.Position.Location.Altitude : altitudeMax;
//                tbMetricaAltitudeVal.Text = FormatDouble((altitudeMax - altitudeMin)/1000, "0.00");
//                tbMetricaAltitudeMaxVal.Text = FormatDouble(altitudeMax, "0.00");
                
//                this.Last = e.Position.Location;
//                this.tbMetricaCaloriaVal.Text = ((0.250 * 66) * timeSegundos/60).ToString();
//            }
//        }


//        private string FormatDouble(double value, string format)
//        {
//            if (double.IsNaN(value))
//                return "---";

//            return value.ToString(format);
//        }


//        private void btnIniciar_Click(object sender, RoutedEventArgs e)
//        {
//            if (btnIniciar.Content.ToString() == "Parar")
//            {
                
//                btnIniciar.Content = "Iniciar";
//                trackingOn = false;
//                map1.ZoomLevel = 1.0f; // zoom out to see whole world.
//                watcher.Stop();
//                Timer.Stop();
//            }
//            else if(btnIniciar.Content.ToString() == "Iniciar") 
//            {
//                new Thread(startLocServInBackground).Start();
//                btnIniciar.Content = "Parar";
//                trackingOn = true;
//                map1.ZoomLevel = 16.0f; // zoom to street level.
//                Timer.Start();
//            }
//        }
//    }
//}

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

namespace LocationServiceViewer
{
    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher; // main location object
        bool trackingOn = false; // switches on w/button push
        Pushpin myPushpin = new Pushpin(); // marks current spot on Map

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //Instatiate watcher, setting its accuracy level and movement threshold.
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High); // using high accuracy.
            watcher.MovementThreshold = 10.0f; // meters of change before "PositionChanged" event fires.

            // wire up event handlers
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_statusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            // start up the Location Service on app startup. watcher_StatusChanged
            // will fire when start up of LocServ is complete.
            new Thread(startLocServInBackground).Start();
            statusTextBlock.Text = "Starting Location Service...";
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
                        statusTextBlock.Text = "You have disabled Location Service.";
                    }
                    else
                    {
                        statusTextBlock.Text = "Location Service is not functioning on this device.";
                    }
                    break;

                case GeoPositionStatus.Initializing:
                    // The location service is initializing.
                    statusTextBlock.Text = "Location Service is retrieving data...";
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data
                    // due to poor signal fidelity (most likely)
                    statusTextBlock.Text = "Location data is not available.";
                    break;

                case GeoPositionStatus.Ready:
                    // The location service is working and is receiving location data.
                    statusTextBlock.Text = "Location data is available.";
                    break;
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            // update the textblock readouts.
            latitudeTextblock.Text = e.Position.Location.Latitude.ToString("0.0000000000");
            longitudeTextblock.Text = e.Position.Location.Longitude.ToString("0.0000000000");
            speedreadout.Text = e.Position.Location.Speed.ToString("0.0") + " meters per second";
            coursereadout.Text = e.Position.Location.Course.ToString("0.0") + " degrees";
            altitudereadout.Text = e.Position.Location.Altitude.ToString("0.0") + " meters above sea level";

            // update the map if the user has asked to be tracked.
            if (trackingOn)
            {
                // center the pushpin and map on the current position
                myPushpin.Location = e.Position.Location;
                myMap.Center = e.Position.Location;

                // if this is the first time that myPushPin is being plotted, add it to the object
                // hierarchy!
                if (myMap.Children.Contains(myPushpin) == false) { myMap.Children.Add(myPushpin); };
            }
        }

        private void trackMe_Click(object sender, RoutedEventArgs e)
        {
            if (trackingOn)
            {
                trackMe.Content = "Track Me On Map";
                trackingOn = false;
                myMap.ZoomLevel = 1.0f; // zoom out to see whole world.
            }
            else
            {
                trackMe.Content = "Stop Tracking";
                trackingOn = true;
                myMap.ZoomLevel = 16.0f; // zoom to street level.
            }
        }

        private void startStop_Click(object sender, RoutedEventArgs e)
        {
            if (startStop.Content.ToString() == "Stop LocServ")
            {
                startStop.Content = "Start LocServ";
                statusTextBlock.Text = "Location Services Stopped...";
                watcher.Stop();
            }
            else if (startStop.Content.ToString() == "Start LocServ")
            {
                startStop.Content = "Stop LocServ";
                statusTextBlock.Text = "Starting Location Services...";
                new Thread(startLocServInBackground).Start(); // StatusChanged will fire when complete.
            }
        }


    }
}
﻿#pragma checksum "C:\svn\tcc-see-you-runner\branches\SeeYouTest\SeeYouTest\Views\MetricsView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A55C683B0EE67DEBAAFA1D09C4765814"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SeeYouTest.Views {
    
    
    public partial class MetricsView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock textBlock1;
        
        internal System.Windows.Controls.TextBlock textBlock2;
        
        internal System.Windows.Controls.TextBlock textBlock3;
        
        internal System.Windows.Controls.TextBlock textBlock4;
        
        internal Microsoft.Phone.Controls.Maps.Map map1;
        
        internal System.Windows.Controls.Button btnIniciar;
        
        internal System.Windows.Controls.TextBlock tbTempo;
        
        internal System.Windows.Controls.TextBlock tbDistancia;
        
        internal System.Windows.Controls.TextBlock tbVel;
        
        internal System.Windows.Controls.TextBlock tbVelm;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/SeeYouTest;component/Views/MetricsView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock1")));
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock2")));
            this.textBlock3 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock3")));
            this.textBlock4 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock4")));
            this.map1 = ((Microsoft.Phone.Controls.Maps.Map)(this.FindName("map1")));
            this.btnIniciar = ((System.Windows.Controls.Button)(this.FindName("btnIniciar")));
            this.tbTempo = ((System.Windows.Controls.TextBlock)(this.FindName("tbTempo")));
            this.tbDistancia = ((System.Windows.Controls.TextBlock)(this.FindName("tbDistancia")));
            this.tbVel = ((System.Windows.Controls.TextBlock)(this.FindName("tbVel")));
            this.tbVelm = ((System.Windows.Controls.TextBlock)(this.FindName("tbVelm")));
        }
    }
}

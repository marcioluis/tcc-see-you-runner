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
using MVVM_Core;
using SeeYouRunner.ViewModels;

namespace SeeYouRunner.Views
{
    public partial class ConfiguracoesView : PhoneApplicationPage, IView<ConfiguracoesViewModel>
    {
        public ConfiguracoesViewModel ViewModel { get; set; }

        public ConfiguracoesView()
        {
            InitializeComponent();
            ViewModel = new ConfiguracoesViewModel();
        }

        //Atrela a View ao ViewModel
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModel;

            ViewModel.InicializaSexo();
            ViewModel.InicializaSMetrico();
            ViewModel.CarregaConfiguracao();
            //lpMetrica.SelectedIndex = 0;
            //lpSexo.SelectedIndex = 0;
        }

        private void OnBackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.SalvaConfiguracao();
        }

    }
}
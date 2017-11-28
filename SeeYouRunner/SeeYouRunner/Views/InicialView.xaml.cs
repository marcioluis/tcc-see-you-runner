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
using SeeYouRunner.ViewModels;
using System.Net.NetworkInformation;

namespace SeeYouRunner.Views
{
    public partial class InicialView : PhoneApplicationPage
    {
        public PercursoViewModel ViewModel { get; set; }

        public InicialView()
        {
            InitializeComponent();
            ViewModel = new PercursoViewModel();
        }

        private void btnConfiguracao_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ConfiguracoesView.xaml", UriKind.Relative));
        }

        private void btnNovoPercurso_Click(object sender, RoutedEventArgs e)
        {
            bool nit = NetworkInterface.GetIsNetworkAvailable();
            if (nit == true)
                NavigationService.Navigate(new Uri("/Views/MetricsView.xaml", UriKind.Relative));
            else
                MessageBox.Show("Para começar um novo percurso é necessaria uma conexão com a internet");
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModel;
            ViewModel.GetPercursosFromLista();
            ViewModel.CalculaTotais();
            ViewModel.ListaApresentacao();
        }

        private void lbPercursos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbPercursos.SelectedIndex != -1)
            {
                ViewModel.SetPrePercursoFromLista(ViewModel.GetPercursoById(ViewModel.PercursoApre.Id));
                NavigationService.Navigate(new Uri("/Views/PercursoView.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            lbPercursos.SelectedIndex = -1;
        }

        private void btnSobre_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SobreView.xaml", UriKind.Relative));
        }
    }
}
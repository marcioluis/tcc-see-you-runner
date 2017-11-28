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
using SeeYouTest.ViewModels;

namespace SeeYouTest.Views
{
    public partial class InicialView : PhoneApplicationPage
    {
        public PercursoViewModel ViewModel { get; set; }

        public InicialView()
        {
            InitializeComponent();
            ViewModel = new PercursoViewModel();
        }



        private void btnMensal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfiguracao_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ConfiguracoesView.xaml", UriKind.Relative));
        }

        private void btnNovoPercurso_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/MetricsView.xaml", UriKind.Relative));
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
            ViewModel.SetPrePercursoFromLista(ViewModel.GetPercursoById(ViewModel.PercursoApre.Id));
            NavigationService.Navigate(new Uri("/Views/FimDePercursoView.xaml", UriKind.Relative));
        }
    }
}
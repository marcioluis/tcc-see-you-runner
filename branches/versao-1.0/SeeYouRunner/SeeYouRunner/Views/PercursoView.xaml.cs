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
using Microsoft.Phone.Controls.Maps;

namespace SeeYouRunner.Views
{
    public partial class PercursoView : PhoneApplicationPage
    {

        public PercursoViewModel ViewModel { get; set; }

        Pushpin pInicial, pFinal;
        private MapPolyline linha;
        private LocationCollection _path;

        public PercursoView()
        {
            ViewModel = new PercursoViewModel();
            InitializeComponent();

            //Map pushins
            pInicial = new Pushpin();
            pFinal = new Pushpin();

            //Linha de percurso
            linha = new MapPolyline();
            linha.Stroke = new SolidColorBrush(Colors.Yellow);
            linha.StrokeThickness = 5;
            linha.Opacity = 0.8;
            map1.Children.Add(linha);
            _path = new LocationCollection();

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModel;
            ViewModel.GetPrePercursoToString();
            DesenhaPercurso();
            if (!ViewModel.Percurso.IsSave)
            {
                NavigationService.RemoveBackEntry();
            }
            //NavigationService.RemoveBackEntry();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AppbarSave_Click(object sender, EventArgs e)
        {
            if (ViewModel.Percurso.IsSave)
            {
                ViewModel.Descricao = tbDescricao.Text;
                ViewModel.AtualizaPercursoInLista();
                NavigationService.Navigate(new Uri("/Views/InicialView.xaml", UriKind.Relative));
            }
            else
            {
                ViewModel.Descricao = tbDescricao.Text;
                ViewModel.AtualizaPercursoServico();
                ViewModel.AddPercursoToLita();
                NavigationService.Navigate(new Uri("/Views/InicialView.xaml", UriKind.Relative));
            }
        }

        private void AppbarDelete_Click(object sender, EventArgs e)
        {
            if (ViewModel.Percurso.IsSave)
            {
                ViewModel.DeletePercurso();
                NavigationService.Navigate(new Uri("/Views/InicialView.xaml", UriKind.Relative));
            }
            else
            {
                ViewModel.Descricao = tbDescricao.Text;
                ViewModel.AtualizaPercursoServico();
                NavigationService.Navigate(new Uri("/Views/InicialView.xaml", UriKind.Relative));
            }
        }

        private void DesenhaPercurso()
        {
            if (ViewModel.Locations.Count > 0)
            {
                pInicial.Location = ViewModel.Locations[0].Location;
                pFinal.Location = ViewModel.Locations[ViewModel.Locations.Count - 1].Location;
                map1.ZoomLevel = 16.0f;
                map1.Center = pInicial.Location;

                map1.Children.Add(pInicial);
                linha.Locations = ViewModel.DesenhoCaminho;
                map1.Children.Add(pFinal);
            }

        }

        private void tbDescricao_GotFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.Descricao = tbDescricao.Text;
            if (ViewModel.Descricao == "add descrição..." || ViewModel.Descricao == null)
                ViewModel.Descricao = "";
        }

        private void tbDescricao_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.Descricao = tbDescricao.Text;
            if (ViewModel.Descricao == "")
                ViewModel.Descricao = "add descrição...";
        }

        private void OnBackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (e.Cancel)
                return;

            if (ViewModel.Percurso.IsSave)
            {
                
            }
            else
            {
                var buttonInfo = MessageBox.Show("Descartar percurso?", "Descartar", MessageBoxButton.OKCancel);
                if (buttonInfo == MessageBoxResult.OK)
                {
                    ViewModel.Descricao = tbDescricao.Text;
                    ViewModel.AtualizaPercursoServico();
                    NavigationService.Navigate(new Uri("/Views/InicialView.xaml", UriKind.Relative));
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
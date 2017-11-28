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
using System.Collections.ObjectModel;
using MVVM_Core;
using SeeYouRunner.Negocio;
using SeeYouRunner.Model;

namespace SeeYouRunner.ViewModels
{
        public class ConfiguracoesViewModel : ViewModelBase
        {
            private ObservableCollection<String> _SexodaPessoa;
            private string _SexoTipo;
            private int _Idade;
            private int _Peso;
            private ObservableCollection<String> _SMetrico;
            private string _unidade;
            private ConfiguracaoNegocio confNegocio = new ConfiguracaoNegocio();
            //Configuracao conf = new Configuracao();


            # region Sexo
            public string SexoTipo
            {
                get
                {
                    return _SexoTipo;
                }
                set
                {
                    _SexoTipo = value;
                    base.NotifyPropertyChanged("SexoTipo");
                }
            }

            public ObservableCollection<String> Sexo
            {
                get
                {
                    return _SexodaPessoa;
                }
                set
                {
                    _SexodaPessoa = value;
                    base.NotifyPropertyChanged("Sexo");
                }
            }

            public void InicializaSexo()
            {
                Sexo = new ObservableCollection<String>{
                "Masculino","Feminino"
            };
            }

            # endregion

            # region Idade
            public int Idade
            {
                get
                {
                    return _Idade;
                }
                set
                {
                    _Idade = value;
                    base.NotifyPropertyChanged("Idade");
                }
            }
            # endregion

            # region Peso
            public int Peso
            {
                get
                {
                    return _Peso;
                }
                set
                {
                    _Peso = value;
                    base.NotifyPropertyChanged("Peso");
                }
            }
            # endregion

            # region SistemaMetrico
            public string Unidade
            {
                get
                {
                    return _unidade;
                }
                set
                {
                    _unidade = value;
                    base.NotifyPropertyChanged("Unidade");
                }
            }

            public ObservableCollection<string> SMetrico
            {
                get
                {
                    return _SMetrico;
                }
                set
                {
                    _SMetrico = value;
                    base.NotifyPropertyChanged("SMetrico");
                }
            }

            public void InicializaSMetrico()
            {
                SMetrico = new ObservableCollection<String>{
                "Métrico", "Imperial"};
            }

            # endregion

            //Model métodos

            //Save Isoleted Storage
            public void SalvaConfiguracao()
            {
                Configuracao conf = new Configuracao();
                conf.Sexo = SexoTipo;
                conf.Idade = Idade;
                conf.Peso = Peso;
                conf.SistemaMetrico = Unidade;
                confNegocio.SalvarConfigurao(conf);
                //confNegocio.NovaConfiguracao(SexoTipo, Idade, Peso, Unidade);
            }

            public void CarregaConfiguracao()
            {
                Configuracao conf = new Configuracao();
                //SistemaMetrico m = new SistemaMetrico();
                conf = confNegocio.CarregaConfiguracao();
                SexoTipo = conf.Sexo;
                Idade = conf.Idade;
                Peso = conf.Peso;
                Unidade = conf.SistemaMetrico;
            }

        }
}

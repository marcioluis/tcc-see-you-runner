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
using System.ComponentModel;

namespace SeeYouTest.Model
{
    public class Usuario : INotifyPropertyChanged
    {
        private string _nome;
        public string Nome {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
                RaisePropertyChanged("Nome");
            }
        }

        private string _sobrenome;
        public string SobreNome {
            get
            {
                return _sobrenome;
            }
            set
            {
                _sobrenome = value;
                RaisePropertyChanged("SobreNome");
            }
        }

        private string _nickname;
        public string NickName {
            get
            {
                return _nickname;
            }
            set
            {
                _nickname = value;
                RaisePropertyChanged("NickName");
            }
        }

        public enum Metric {Km, Mh}

        public Guid ID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

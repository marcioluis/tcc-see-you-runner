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

namespace SeeYouRunner.Views
{
    public partial class SobreView : PhoneApplicationPage
    {
        public SobreView()
        {
            InitializeComponent();
            tb_sobre.Text = "O See You Runner é uma ferramenta para&#x0a; auxiliar no acompanhamento do desenvolvimento de corredores e atletas em geral. Visualize seu histórico de corridas, desempenho, dasafie seus limites em busca de novas conquistas e esteja entre os melhores esportistas da atualidade.";
        }
    }
}
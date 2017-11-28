using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeeYouSite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Title = "See You Runner";
            ViewBag.Descricao =
                "O See You Runner é uma ferramenta para auxiliar no acompanhamento do desenvolvimento de corredores e atletas em geral."
                + "\nVisualize seu histórico de corridas, desempenho, dasafie seus limites em busca de novas conquistas e esteja entre os melhores esportistas da atualidade.";

            return View();
        }

        /// <summary>
        /// Get da página principal de inicio do site
        /// </summary>
        /// <returns></returns>
        public ActionResult Inicio()
        {

            ViewBag.Title = "SYR - Inicio";

            ViewBag.Descricao = "O aplicativo See You Runner ajuda a você acompanhar seu desempenho nas corridas através de seu GPS "
            + "fornecendo dados suficientes para você e seu treinador em tempo real de forma prática e simples.";

            return View();
        }

        public ActionResult Sobre()
        {
            ViewBag.Title = "SYR - Sobre";

            ViewBag.Sobre = "Trabalho de conclusão de curso de graduação apresentado à Faculdade de Informática da Pontifícia Universidade Católica do Rio Grande do Sul, como requisito parcial para obtenção do grau de Bacharel em Sistemas de Informação. ";
            ViewBag.Autores = "Desenvolvido por Márcio L. S. Arrosi e Marcos M. N. Borba com orientação do Prof. Dr. Alfio Ricardo de Brito Martini.";
            ViewBag.Local = "Porto Alegre 2012";

            return View();
        }
    }
}

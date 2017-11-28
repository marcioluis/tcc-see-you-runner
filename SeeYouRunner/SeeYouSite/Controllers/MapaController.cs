using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeeYouSite.Models;
using SeeYouSite.ViewModels;

namespace SeeYouSite.Controllers
{
    public class MapaController : Controller
    {

        private SYRContext syrDb;

        public MapaController()
        {
            this.syrDb = new SYRContext();
        }

        //
        // GET: /Mapa/
        public ActionResult Index()
        {
            ViewBag.Title = "SYR - Acompanhamento";
            ViewBag.StatusMsg = "Utilize os controles abaixo para acompanhar um corredor de sua escolha";
            var usuarios = syrDb.usuarios;
            ViewBag.ListaUsuarios = new SelectList(syrDb.usuarios, "id_usuario", "Nome");

            return View();
        }

        /// <summary>
        /// Recebe a ID do usuário apartir da view e retorna o percurso do mesmo para acompanhamento
        /// em formato JSON
        /// </summary>
        /// <param name="id_usuario">Id do usuário que sera buscado os percursos</param>
        /// <returns>View com os percursos</returns>
        [HttpPost]
        public ActionResult Percurso(int id_usuario)
        {
            SeeYouNegocio syNegocio = new SeeYouNegocio(syrDb);
            var perc = syNegocio.percursoAtual(id_usuario);
            List<object> vm = new List<object>();

            if (perc != null)
            {
                var pts = syNegocio.pontosPercurso(perc.id_percurso);

                if (pts != null)
                {

                    foreach (pontos p in pts)
                    {
                        var viewModel = new PontoViewModel
                        {
                            velocidade = p.velocidade.GetValueOrDefault(),
                            distancia = p.distancia.GetValueOrDefault(),
                            altitude =p.altitude.GetValueOrDefault(),
                            latitude = p.latitude,
                            longitude = p.longitude,
                            ultima_data = p.data_ponto.GetValueOrDefault()
                        };
                        //getter para o ritmo pois necessita de calculo a partir da duracao
                        viewModel.pace = viewModel.getPace(p.duracao.GetValueOrDefault());
                        viewModel.paceFormat = viewModel.getPaceFormat(viewModel.pace);

                        //getters para as medidas em imperial
                        viewModel.impVelocidade = viewModel.getImperialVelocidade();
                        viewModel.impDistancia = viewModel.getImperialDistancia();
                        viewModel.impAltitude = viewModel.getImperialAltitude();
                        viewModel.impPace = viewModel.getImperialPace(p.duracao.GetValueOrDefault());
                        viewModel.impPaceFormat = viewModel.getPaceFormat(viewModel.impPace);

                        //setter do percurso ativo
                        viewModel.ativo = viewModel.estaAtivo();
                        
                        //set da duracao do percurso pois é mais complexo o calculo
                        viewModel.setDuracao(p.duracao.GetValueOrDefault());
                        
                        vm.Add(viewModel);
                    }
                }
            }
            
            return Json(vm);
        }
    }
}

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
    public class HistoricoController : Controller
    {
        private SYRContext syrDb;

        public HistoricoController() {
            this.syrDb = new SYRContext();
        }
        //
        // GET: /Historico/

        public ActionResult Index()
        {
            ViewBag.Title = "SYR - Histórico";
            var usuarios = syrDb.usuarios;
            ViewBag.ListaUsuarios = new SelectList(syrDb.usuarios, "id_usuario", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult percursosUsuario(int id_usuario)
        {
            //recuperar percursos em json
            var percurso = syrDb.percursos.Where(p => p.id_usuario == id_usuario);
            List<object> vm = new List<object>();

            foreach (var perc in percurso)
            {
                var viewModel = new PercursoViewModel { 
                    altitude_max = perc.altitude_max.GetValueOrDefault(),
                    altitude_med = perc.altitude_med.GetValueOrDefault(),
                    altitude_min = perc.altitude_min.GetValueOrDefault(),
                    caloria = perc.caloria.GetValueOrDefault(),
                    descricao = perc.descricao,
                    distancia = perc.distancia.GetValueOrDefault(),
                    percurso = perc.percurso,
                    velocidade_max = perc.velocidade_max.GetValueOrDefault(),
                    velocidade_med = perc.velocidade_med.GetValueOrDefault(),
                    Pace = perc.Pace.GetValueOrDefault(),
                    duracao = perc.duracao,

                    id_percurso = perc.id_percurso,
                    id_usuario = perc.id_usuario.GetValueOrDefault()
                };
                //formata as strings de ritmo
                viewModel.PaceFormat = viewModel.getPaceFormat(viewModel.Pace);
                //viewModel.impPaceFormat = viewModel.getPaceFormat(viewModel.impPace);

                vm.Add(viewModel);
            }

            return Json(vm);
        }

        [HttpPost]
        public ActionResult detalhesPercurso(int id_percurso)
        {
            SeeYouNegocio syNegocio = new SeeYouNegocio(syrDb);
            var pontosPercurso = syNegocio.pontosPercurso(id_percurso);
            List<object> vm = new List<object>();

            if (pontosPercurso != null)
            {
                foreach (pontos p in pontosPercurso)
                {
                    var viewModel = new PontoViewModel
                    {
                        distancia = p.distancia.GetValueOrDefault(),
                        altitude = p.altitude.GetValueOrDefault(),
                        velocidade = p.velocidade.GetValueOrDefault(),
                        caloria = p.caloria.GetValueOrDefault(),
                        id_ponto = p.id_ponto,
                        id_percurso = p.id_percurso.GetValueOrDefault(),

                        latitude = p.latitude,
                        longitude = p.longitude
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

                    //set da duracao do percurso pois é mais complexo o calculo
                    viewModel.setDuracao(p.duracao.GetValueOrDefault());
                    vm.Add(viewModel);
                }
            }
            return Json(vm);
        }

        [HttpPost]
        public ActionResult percursosData(int id_usuario, string dataInicial, string dataFinal)
        {
            DateTime dInicial;
            DateTime dFinal;

            if (string.IsNullOrEmpty(dataInicial) || string.IsNullOrEmpty(dataFinal))
            {
                if (string.IsNullOrEmpty(dataInicial))
                {
                    dInicial = DateTime.MinValue;
                    dFinal = dFinal = Convert.ToDateTime(dataFinal).AddHours(23).AddMinutes(59);
                }
                else
                {
                    dInicial = Convert.ToDateTime(dataInicial);
                    dFinal = DateTime.Today;
                }
            }
            else
            {
                dInicial = Convert.ToDateTime(dataInicial);
                dFinal = Convert.ToDateTime(dataFinal).AddHours(23).AddMinutes(59);
            }

            var resultSet = syrDb.percursos
                .Where(qry => qry.id_usuario == id_usuario)
                .Where(qry => qry.data_percurso >= dInicial & qry.data_percurso <= dFinal );

            List<object> vm = new List<object>();

            foreach (var perc in resultSet)
            {
                var viewModel = new PercursoViewModel
                {
                    altitude_max = perc.altitude_max.GetValueOrDefault(),
                    altitude_med = perc.altitude_med.GetValueOrDefault(),
                    altitude_min = perc.altitude_min.GetValueOrDefault(),
                    caloria = perc.caloria.GetValueOrDefault(),
                    descricao = perc.descricao,
                    distancia = perc.distancia.GetValueOrDefault(),
                    percurso = perc.percurso,
                    velocidade_max = perc.velocidade_max.GetValueOrDefault(),
                    velocidade_med = perc.velocidade_med.GetValueOrDefault(),
                    Pace = perc.Pace.GetValueOrDefault(),
                    duracao = perc.duracao,
                    data_percurso = perc.data_percurso.GetValueOrDefault().ToString("d"),

                    id_percurso = perc.id_percurso,
                    id_usuario = perc.id_usuario.GetValueOrDefault()
                };
                //mudar formato da data buscada no nome do percurso: vem do celular
                //formatando o ritmos para string
                viewModel.PaceFormat = viewModel.getPaceFormat(viewModel.Pace);
                viewModel.impPaceFormat = viewModel.getPaceFormat(viewModel.impPace);

                vm.Add(viewModel);
            }

            return Json(vm);
        }
        

    }//class
}//namespace

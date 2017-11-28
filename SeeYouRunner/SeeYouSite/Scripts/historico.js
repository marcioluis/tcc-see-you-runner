var PERCURSO;

///Function que lista todos os percursos do
///corredor selecionado do dropdown atraves do
///atributo value
function listaPercursos(id, url) {

    //esconde a lista de percursos e o filtro de data
    $("#lista_percursos ul").slideUp(function () {
        $("#lista_percursos .data-filtro").slideUp();
        //remove as tags <li> da lista de percursos apos o slideUp concluir
        $("#lista_percursos ul li").remove();
        hideGrafs();
        limpaFiltro();
        removeMetricasTabela();

        //remove o loader
        $('#graf_container').removeClass("alt").removeClass("loader").html('');

        if ((id === undefined) || (id == '')) {
            $(".loader").removeClass("alt").html('');

        } else {
            $(".loader").addClass("alt").html('');

            $.post(url, { "id_usuario": id }, function (percursos) {

                if ((percursos.length > 0) && (percursos !== undefined)) {

                    PERCURSO = percursos;

                    //cria a listagem de percursos
                    for (x = 0; x < percursos.length; x++) {
                        $(".ls_percurso").append("<li id='" + percursos[x].id_percurso + "' value='" + x + "' onClick='detalhesPercurso(this.id, this.value)'>" + percursos[x].percurso + "</li>");
                    }
                    //paginacao pelo plugin
                    $("#lista_percursos ul").quickPagination();
                    //muda o atributo 'value' do botao de filtro com o id do usuario a ser atual.
                    $('#pesquisar').attr({ value: id });
                    //animacoes para mostrar o conteudo da div
                    $("#lista_percursos .data-filtro").slideDown(function () {
                        $("#lista_percursos ul").slideDown();
                    });
                    //remove o loader
                    $(".loader").removeClass("alt").html('');
                }
                else {
                    //caso o post ajax tenha retornado vazio ou null, informa ao usuário
                    $(".loader").removeClass("alt").html("<span>Não há dados!</span>");
                    $('#pesquisar').attr({ value: '' });
                }
            }); //post ajax
        } //else
    });   //slide up
}

/// Detalha um percurso recebido por parametro id, faz uma consulta ajax
/// a base de dados que consulta os pontos do percurso e metricas correspondentes
/// id: id do percursos para o ajax
/// index: index do percurso no array global
function detalhesPercurso(id, index) {

    //url server side para o post ajax
    var url = "/Historico/detalhesPercurso";

    //remove as metricas da tabela
    removeMetricasTabela();
    hideGrafs();
    limpaFiltro();
    //mostra o loader
    $('#graf_container').addClass("loader").addClass("alt").html('');

    //post ajax
    $.post(url, { "id_percurso": id }, function (data) {

        if ((data !== undefined) && (data.length > 0)) {

            //remove o loader do container do grafico
            $('#graf_container').removeClass("alt").removeClass("loader").html('');
            //function auxiliar para desenhar o grafico
            graficoVelocidade(data);
            graficoAltitude(data);
            graficoRitmo(data);

            $('#graf_velocidade').show('fast', function () {
                //adiciona as metricas na tabela
                addMetricasTabela(index);
            });

        }
        else {
            $('#graf_container').removeClass("alt").removeClass("loader").html("<span>Não há dados!</span>");
        }

    });     //post ajax
}
function addMetricasTabela(index) {
    //adiciona as metrias na tabela

    $('#mCaloria').text(PERCURSO[index].caloria);
    $('#mDuracao').text(PERCURSO[index].duracao);
    $('#mRitmo').text(PERCURSO[index].PaceFormat);
    $('#mDistancia').text(PERCURSO[index].distancia);
    $('#mVelMax').text(PERCURSO[index].velocidade_max);
    $('#mVelMed').text(PERCURSO[index].velocidade_med);
    $('#mAltitude').text(PERCURSO[index].altitude_max);

    $('#uV').text('\tKm/h');
    $('#uVMX').text('\tKm/h');
    $('#uD').text('\tKm');
    $('#uH').text('\tMetros');
    $('#uR').text('\t/Km');

    //mostra a div com os detalhes das metricas do percurso
    $('.detalhes_percurso').slideDown();
}
function removeMetricasTabela() {
    //esconde a div com os detalhes das metricas do percurso
    $('.detalhes_percurso').slideUp();

    //remove as metrias na tabela
    $('#mCaloria').text('');
    $('#mDuracao').text('');
    $('#mRitmo').text('');
    $('#mDistancia').text('');
    $('#mVelMax').text('');
    $('#mVelMed').text('');
    $('#mAltitude').text('');

    $('#uV').text('');
    $('#uVMX').text('');
    $('#uD').text('');
    $('#uH').text('');
    $('#uR').text('');
}
//esconde todos os graficos
function hideGrafs() {
    $('#graf_velocidade').hide();
    $('#graf_ritmo').hide();
    $('#graf_altitude').hide();
}
function limpaFiltro() {
    $('#from').val('');
    $('#to').val('');
}
///
/// funcao auxiliar que adiciona os datepickers
/// evento de click no botao de filtro e outras coisas
$(function () {
    //configurando a apresentacao do datepicker
    var semana = ["Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo"],
    semanaShort = ["Seg", "Ter", "Qua", "Qui", "Sex", "Sáb", "Dom"],
    mes = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
    mesShort = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];

    //datepicker do textbox "DE"
    $("#from").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        dayNames: semana,
        dayNamesMin: semanaShort,
        monthNames: mes,
        monthNamesShort: mesShort,
        onClose: function (selectedDate) {
            $("#to").datepicker("option", "minDate", selectedDate);
        }
    });
    //datepicker do textbox "ATE"
    $("#to").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        dayNames: semana,
        dayNamesMin: semanaShort,
        monthNames: mes,
        monthNamesShort: mesShort,
        onClose: function (selectedDate) {
            $("#from").datepicker("option", "maxDate", selectedDate);
        }
    });

    //evento de click no botao de filtro(gerar grafico)
    $('#pesquisar').click(function () {
        //id do usuario selecionado no dropdown
        var id = this.value;
        //data de inicio e fim
        var dataInicial = $('#from').val();
        var dataFinal = $('#to').val();
        //url para o post ajax
        var url = " /Historico/percursosData";
        //remove as metricas da tabela
        removeMetricasTabela();
        hideGrafs();

        $('#graf_container').addClass("loader").addClass("alt").html('');

        if (dataInicial == '' && dataFinal == '') {
            $('#graf_container').removeClass("alt").removeClass("loader").html("<span>Os dois per&iacute;odos n&atilde;o podem ser vazios!</span>");
            $('#from').focus();
        }
        else {
            $.post(url, { 'id_usuario': id, 'dataInicial': dataInicial, 'dataFinal': dataFinal }, function (result) {

                if ((result !== undefined) && (result.length > 0)) {
                    //remove o loader do container do grafico
                    $('#graf_container').removeClass("alt").removeClass("loader").html('');

                    //esconder graficos
                    //criar os graficos
                    //mostrar inicial
                    hideGrafs();
                    graficoDataVelocidade(result);
                    graficoDataAltitude(result);
                    graficoDataRitmo(result);

                    $('#graf_velocidade').show('slow', function () {
                        $('#opt_percursos').slideDown();
                    });

                } else {
                    $('#graf_container').removeClass("alt").removeClass("loader").html("<span>Não há dados!</span>");
                } //ifelse

            }); //ajax post
        } //else
    }); //click function

    //evento de click no botao de mostrar tipos de graficos
    $('.vGraf').click(function (e) {

        if (this.id == 'vel') {
            hideGrafs();
            $('#graf_velocidade').show();
        } else if (this.id == 'hlt') {
            hideGrafs();
            $('#graf_altitude').show();
        } else if (this.id == 'rit') {
            hideGrafs();
            $('#graf_ritmo').show();
        }
    });

});          //function
/// Cria um grafico velocidade do percurso armazenado desenhando
/// a velocidade em funcao do tempo
/// pontos: todos os pontos gravados do percurso
function graficoVelocidade(pontos) {

    var tempo = new Array(),
        velocidade = new Array(),
        chart;
    var media = 1;
    var ultimo;

    if (pontos.length > 1) {
        media += Math.ceil((pontos.length / 2) / 4);
        ultimo = pontos.length - 1;
    }

    ///adiciona nos arrays as metricas de cada ponto
    for (x = 0; x < pontos.length; x += 2) {
        tempo.push(pontos[x].duracao);
        velocidade.push(pontos[x].velocidade);
    }

    ///constroi o grafico com os arrays de valores e metricas
    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'graf_velocidade',
            type: 'line'
        },
        title: {
            text: "Desempenho em velocidade"
        },
        xAxis: {
            categories: tempo,
            labels: { step: media },
            title: { text: "Tempo" }
        },
        yAxis: {
            min: 0,
            title: { text: "Velocidade" },
            labels: {
                formatter: function () {
                    return this.value + ' Km/h';
                }
            }
        },
        series: [{
            name: 'Velocidade',
            data: velocidade
        }],
        exporting: {
            filename: "Meu grafico-velocidade"
        }
    });  //chart
}
/// Cria um grafico altitude do percurso armazenado desenhando
/// a altitude em funcao do tempo
/// pontos: todos os pontos gravados do percurso
function graficoAltitude(pontos) {

    var tempo = new Array(),
        altitude = new Array(),
        chart;
    var media = 1;
    var ultimo;

    if (pontos.length > 1) {
        media += Math.ceil((pontos.length / 2) / 4);
        ultimo = pontos.length - 1;
    }

    ///adiciona nos arrays as metricas de cada ponto
    for (x = 0; x < pontos.length; x += 2) {
        tempo.push(pontos[x].duracao);
        altitude.push(pontos[x].altitude);
    }
    ///constroi o grafico com os arrays de valores e metricas
    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'graf_altitude',
            type: 'line'
        },
        title: {
            text: "Desempenho em Altitude"
        },
        xAxis: {
            categories: tempo,
            labels: { step: media },
            title: { text: "Tempo" }
        },
        yAxis: {
            min: 0,
            title: { text: "Altitude" },
            labels: {
                formatter: function () {
                    return this.value + ' m';
                }
            }
        },
        series: [{
            name: 'Altitude',
            data: altitude
        }],
        exporting: {
            filename: "Meu grafico-altitude"
        },
        colors: ['#AA4643']
    });    //chart
}
/// Cria um grafico ritmo do percurso armazenado desenhando
/// a ritmo em funcao do tempo
/// pontos: todos os pontos gravados do percurso
function graficoRitmo(pontos) {

    var tempo = new Array(),
        ritmo = new Array(),
        chart;
    var media = 1;
    var ultimo;

    if (pontos.length > 1) {
        media += Math.ceil((pontos.length / 2) / 4);
        ultimo = pontos.length - 1;
    }

    ///adiciona nos arrays as metricas de cada ponto
    for (x = 0; x < pontos.length; x += 2) {
        tempo.push(pontos[x].duracao);
        ritmo.push(pontos[x].pace);
    }
    ///constroi o grafico com os arrays de valores e metricas
    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'graf_ritmo',
            type: 'line'
        },
        title: {
            text: "Desempenho Geral"
        },
        xAxis: {
            categories: tempo,
            labels: { step: media },
            title: { text: "Tempo" }
        },
        yAxis: {
            min: 0,
            title: { text: "Ritmo" }
        },
        series: [{
            name: 'Ritmo',
            data: ritmo
        }],
        exporting: {
            filename: "Meu grafico-ritmo"
        },
        tooltip: {
            formatter: function () {
                var va = this.y;
                var s = Math.floor(va % 60);
                va /= 60;
                var m = Math.floor(va % 60);

                return this.x + '<br />' + this.series.name + ': ' + m + ':' + s + '/Km';
            }
        },
        colors: ['#89A54E']
    }); //chart
}
/// Graficos de percursos
/// Para pesquisa por data
function graficoDataVelocidade(result) {

    var tempo = new Array(),
                velocidade = new Array(),
                chart;

    var media, ultimo;
    if (result.length > 1) {
        media += Math.round((result.length / 2) / 4);
        ultimo = result.length - 1;
    }

    for (x = 0; x < result.length; x += 2) {
        tempo.push(result[x].data_percurso);
        velocidade.push(result[x].velocidade_med);
    }

    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'graf_velocidade',
            type: 'line'
        },
        title: {
            text: "Velocidade Geral"
        },
        xAxis: {
            categories: tempo,
            title: { text: "Tempo" }
        },
        yAxis: {
            min: 0,
            title: { text: "Velocidade" },
            labels: {
                formatter: function () {
                    return this.value + ' Km/h';
                }
            }
        },
        series: [{
            name: 'Velocidade',
            data: velocidade
        }],
        exporting: {
            filename: "grafico-velocidade-temporal"
        }
    }); //new chart
}
//
function graficoDataAltitude(result) {

    var tempo = new Array(),
                altitude = new Array(),
                chart;

    var media, ultimo;
    if (result.length > 1) {
        media += Math.round((result.length / 2) / 4);
        ultimo = result.length - 1;
    }

    for (x = 0; x < result.length; x += 2) {
        tempo.push(result[x].data_percurso);
        altitude.push(result[x].altitude_med);
    }

    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'graf_altitude',
            type: 'line'
        },
        title: {
            text: "Altitude Geral"
        },
        xAxis: {
            categories: tempo,
            title: { text: "Tempo" }
        },
        yAxis: {
            min: 0,
            title: { text: "Altitude" },
            labels: {
                formatter: function () {
                    return this.value + ' m';
                }
            }
        },
        series: [{
            name: 'Altitude',
            data: altitude
        }],
        exporting: {
            filename: "grafico-altitude-temporal"
        },
        colors: ['#AA4643']
    }); //new chart
}
//
function graficoDataRitmo(result) {

    var tempo = new Array(),
                ritmo = new Array(),
                chart;

    var media, ultimo;
    if (result.length > 1) {
        media += Math.round((result.length / 2) / 4);
        ultimo = result.length - 1;
    }

    for (x = 0; x < result.length; x += 2) {
        tempo.push(result[x].data_percurso);
        ritmo.push(result[x].Pace);
    }

    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'graf_ritmo',
            type: 'line'
        },
        title: {
            text: "Ritmo Geral"
        },
        xAxis: {
            categories: tempo,
            title: { text: "Tempo" }
        },
        yAxis: {
            min: 0,
            title: { text: "Ritmo" }
        },
        series: [{
            name: 'Ritmo',
            data: ritmo
        }],
        exporting: {
            filename: "grafico-ritmo-temporal"
        },
        tooltip: {
            formatter: function () {
                var va = this.y;
                var s = Math.floor(va % 60);
                va /= 60;
                var m = Math.floor(va % 60);

                return this.x + '<br />' + this.series.name + ': ' + m + ':' + s + '/Km';
            }
        },
        colors: ['#89A54E']
    }); //new chart
}

/// Realizam as tarefas de conversoes dos valores
/// para imperial>>metrico e vice versa
function converterParaMilhas(valor) {
    var v = $('#velocidade').text();
    v = v / valor;
    $('#velocidade').text(v);
    $('#uV').text('Mph');

    var d = $('#distancia').text();
    d = d / valor;
    $('#distancia').text(d);
    $('#uD').text('Milhas');

    var h = $('#hMax').text();
    h = h / 3.28;
    $('#hMax').text(h);
    $('#uH').text('Pés');
}
function converterParaMetrico(valor) {

    var v = $('#velocidade').text();
    v = v * valor;
    $('#velocidade').text(v);
    $('#uV').text('Km/h');

    var d = $('#distancia').text();
    d = d * valor;
    $('#distancia').text(d);
    $('#uD').text('Metros');

    var h = $('#hMax').text();
    h = h * 3.28;
    $('#hMax').text(h);
    $('#uH').text('Metros');
}
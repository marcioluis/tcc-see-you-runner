
var atualizaInterval;
var counter;
/**
** Inicia o acompanhamento com o id do usuario e a url
** e executa um post ajax na url para recuperar os dados
**/
function iniciaAcompanhamento(id, url) {

    map.entities.clear();

    if (atualizaInterval != undefined)
        clearTimeout(atualizaInterval);
    if (counter != undefined)
        clearTimeout(counter);

    if ((id === undefined) || (id == '')) {
        $(".loader").removeClass("alt").html('');
        $(".contador").slideUp('fast');

        apagaMetricas();

    } else {
        //imagem de loading
        $(".loader").addClass("alt").html('');

        //executa o post ajax
        $.post(url, { "id_usuario": id }, function (data) {

            if ((data.length <= 0) || (data === undefined)) {
                $(".loader").removeClass("alt").html("<span>Não há dados!</span>");

                apagaMetricas();

                $(".contador").slideUp('fast');
            } else {

                var ultimoRegistro = data.length - 1;
                var lat = new Array();
                var lon = new Array();

                if (data[ultimoRegistro].ativo) {
                    for (x = 0; x < data.length; x++) {
                        lat.push(data[x].latitude);
                        lon.push(data[x].longitude);
                    }

                    $(".loader").removeClass("alt").html('');

                    insereMetricas(data, ultimoRegistro);

                    desenhaPercurso(lat, lon);
                    atualizaMetricas(id, url, 15000);
                } else {
                    $(".loader").removeClass("alt").html("<span>Não há percursos no momento!</span>");

                    apagaMetricas();

                    $(".contador").slideUp('fast');
                }
            } //else
        }); //ajax
    }
}
/**
** Desenha o percurso do corredor de acordo com a colecao de 
** latitudes e longitudes recebidas como parametros
**/
function desenhaPercurso(latitudes, longitudes) {

    //limpando as entidades existentes do mapa
    map.entities.clear();

    var coordenadas = new Array();
    var pushPin, pushPinInicial;
    //retorna o maior entre os dois
    var qtdPontos = Math.max(latitudes.length, longitudes.length);

    //cria o vetor de locations para a polyline
    for (i = 0; i < qtdPontos; i++) {
        coordenadas[i] = new Microsoft.Maps.Location(latitudes[i], longitudes[i]);
    }

    var polylinePercurso = new Microsoft.Maps.Polyline(coordenadas);
    var ultimoPonto = coordenadas.length - 1;

    //adiciona a polyline no mapa
    map.entities.push(polylinePercurso);
    //cria e adiciona um pushpin no mapa
    pushPin = new Microsoft.Maps.Pushpin(coordenadas[ultimoPonto], { draggable: false });
    pushPinInicial = new Microsoft.Maps.Pushpin(coordenadas[0], { draggable: false, color: 'red' });
    map.entities.push(pushPin);
    map.entities.push(pushPinInicial);

    map.setView({ center: coordenadas[ultimoPonto], zoom: 18 });
    //site para simular coordenadas
    //http://codigopostal.ciberforma.pt/ferramentas/coordenadas.asp
}

/**
** Atualiza as metricas na tabela com o ultimo resultado dos pontos
**/
function atualizaMetricas(id, url, timeInterval) {

    var deltaTempo = timeInterval / 1000;
    $("#counter").text(deltaTempo + 's');

    $(".contador").slideDown('fast', function () {
        counter = setInterval(function () {
            if (deltaTempo < 0)
                deltaTempo = 0;
            $("#counter").text(deltaTempo-- + 's');
        }, 1000);
    });

    atualizaInterval = setInterval(function () {
        //executa o post ajax
        deltaTempo = timeInterval / 1000;
        $.post(url, { "id_usuario": id }, function (data) {

            var ultimoRegistro = data.length - 1;
            var lat = new Array();
            var lon = new Array();

            for (x = 0; x < data.length; x++) {
                lat.push(data[x].latitude);
                lon.push(data[x].longitude);
            }

            if ((data.length <= 0) || (data == undefined)) {
                apagaMetricas();
            } else {
                insereMetricas(data, ultimoRegistro);

                atualizaPercurso(lat, lon);
            }
        });
    }, timeInterval);

}
/**
** Atualiza o percurso com a posicao mais recente do corredor
** apagando todas as entidades do mapa e inserindo tudo novamente
**/
function atualizaPercurso(latitudes, longitudes) {
    //limpando as entidades existentes do mapa
    map.entities.clear();

    var coordenadas = new Array();
    var pushPin, pushPinInicial;
    //retorna o maior entre os dois
    var qtdPontos = Math.max(latitudes.length, longitudes.length);

    //cria o vetor de locations para a polyline
    for (i = 0; i < qtdPontos; i++) {
        coordenadas[i] = new Microsoft.Maps.Location(latitudes[i], longitudes[i]);
    }

    var polylinePercurso = new Microsoft.Maps.Polyline(coordenadas);
    var ultimoPonto = coordenadas.length - 1;

    //adiciona a polyline no mapa
    map.entities.push(polylinePercurso);
    //cria e adiciona um pushpin no mapa
    pushPin = new Microsoft.Maps.Pushpin(coordenadas[ultimoPonto], { draggable: false });
    pushPinInicial = new Microsoft.Maps.Pushpin(coordenadas[0], { draggable: false });
    map.entities.push(pushPin);
    map.entities.push(pushPinInicial);

    map.setView({ center: coordenadas[ultimoPonto], zoom: 18 });
}
///
///funcoes auxiliares
///
function apagaMetricas() {
    $("#duracao").text('');
    $("#distancia").text('');
    $("#velocidade").text('');
    $("#ritmo").text('');
    $("#hMax").text('');
    removeTags();
}
function insereMetricas(data, index) {
    if ($('input[name="sistemaMetrico"]:checked').val() == 'imperial') {

        $("#duracao").text(data[index].duracao);
        $("#distancia").text(data[index].impDistancia);
        $("#velocidade").text(data[index].impVelocidade);
        $("#ritmo").text(data[index].impPaceFormat);
        $("#hMax").text(data[index].impAltitude);
        insereTagsImp();
    } else {
        $("#duracao").text(data[index].duracao);
        $("#distancia").text(data[index].distancia);
        $("#velocidade").text(data[index].velocidade);
        $("#ritmo").text(data[index].paceFormat);
        $("#hMax").text(data[index].altitude);
        insereTagsKM();
    }
}
///funcao auxiliar para inserir as unidades
///de medida
function insereTagsKM() {
    $('#uV').text('\tKm/h');
    $('#uD').text('\tKm');
    $('#uH').text('\tMetros');
    $('#uR').text('\t/Km');
}
///funcao auxiliar para inserir as unidades
///de medida
function insereTagsImp() {
    $('#uV').text('\tMph');
    $('#uD').text('\tMilhas');
    $('#uH').text('\tPés');
    $('#uR').text('\t/Mi');
}
///funcao auxiliar para remover as unidades
///de medida
function removeTags() {
    $('#uV').text('');
    $('#uD').text('');
    $('#uH').text('');
    $('#uR').text('');
}
/// Realizam as tarefas de conversoes dos valores
/// para imperial>>metrico e vice versa
function converterParaMilhas(valor) {
    var v = $('#velocidade').text();
    v = Math.round(v / valor);
    $('#velocidade').text(v);

    var d = $('#distancia').text();
    d = Math.round(d / valor);
    $('#distancia').text(d);

    var h = $('#hMax').text();
    h = Math.round(h / 3.28);
    $('#hMax').text(h);

    insereTagsImp();
}
function converterParaMetrico(valor) {

    var v = $('#velocidade').text();
    v = Math.round( v * valor);
    $('#velocidade').text(v);

    var d = $('#distancia').text();
    d = Math.round(d * valor);
    $('#distancia').text(d);

    var h = $('#hMax').text();
    h = Math.round(h * 3.28);
    $('#hMax').text(h);

    insereTagsKM();
}
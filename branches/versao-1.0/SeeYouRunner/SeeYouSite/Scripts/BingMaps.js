
var map;

/**
** Funcao get map recebe como parametro a div que contera
** o mapa completo. Carrega o modulo com os overlays mais
** atualizados.
**/
function GetMap(container) {
    Microsoft.Maps.loadModule('Microsoft.Maps.Overlays.Style', { callback: function () { InitMap(container) } });
}
/**
** Inicia o mapa com a opcoes especificadas e desabilita
** o evento de scroll para zoom
**/
function InitMap(container) {

    var mapOptions = { credentials: "AhUwGgX7MyfKn-WUrppGlB-5dlmUOxdSA8UGBIrmlPGaSJGE-3VSzh7tjzC-TJk0", mapTypeId: Microsoft.Maps.MapTypeId.birdseye,
        enableSearchLogo: false, enableClickableLogo: false, showScalebar: false, customizeOverlays: true, showMapTypeSelector: false
    };

    map = new Microsoft.Maps.Map(document.getElementById(container.toString()), mapOptions);

    Microsoft.Maps.Events.addHandler(map, 'mousewheel', function (e) {
        if (e.targetType == 'map')
            e.handled = true;
    });
}
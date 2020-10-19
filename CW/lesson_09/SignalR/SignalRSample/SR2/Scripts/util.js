$(function () {

    var hb = $.connection.myHub1;

    $("#startBtn").on('click', () => {
        hb.server.hello();
    });


    hb.client.tick = function () {
        var counter = parseInt($("#ku").html());
        counter++;
        $("#ku").html(counter);
    }

    $.connection.hub.start();



});
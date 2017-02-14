$(document).ready(function () {
    var tempDomain = "";
    if (document.domain === "localhost") {
        tempDomain = "/BasketBallMVC"
    }
    $.ajax({
        type: "GET",
        url: tempDomain + "/Home/GetBusyTime",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var obj = jQuery.parseJSON(data);
            if (obj.isBusy == false) {
                var countdown = document.getElementById('countdown');
                countdown.innerHTML = '<b>Twoja postać nie wykonuje obecnie żadnych działań!</b>';
            } else {
                var target_date = new Date(obj.busyEndTime).getTime() - 3600000;
                var hours, minutes, seconds;
                var countdown = document.getElementById('countdown');
                var refreshIntervalId = setInterval(function () {
                    var current_date = new Date().getTime();
                    if (current_date < target_date) {
                        var seconds_left = (target_date - current_date) / 1000;
                        hours = parseInt(seconds_left / 3600);
                        seconds_left = seconds_left % 3600;
                        minutes = parseInt(seconds_left / 60);
                        seconds = parseInt(seconds_left % 60);
                        countdown.innerHTML = '<b style="font-size:15px">Zajęty przez:</b> <span class="hours">' + hours + ' <b>Godziny</b></span> <span class="minutes">'
                        + minutes + ' <b>Minuty</b></span> <span class="seconds">' + seconds + ' <b>Sekundy</b></span>';
                    } else {
                        countdown.innerHTML = '<b>Twoja postać nie wykonuje obecnie żadnych działań!</b>';
                        clearInterval(refreshIntervalId);
                    }
                }, 1000);
            }
        }
    });
});
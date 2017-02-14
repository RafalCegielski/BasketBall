$(document).on('click', '[name="characterProfile"]', function () {
    var tempDomain = "";
    if (document.domain === "localhost") {
        tempDomain = "/BasketBallMVC"
    }
    window.location.href = tempDomain + "/Admin/CharacterProfile?userId=" + $(this).attr("data-UserId");
});

$(document).on('click', '[name="unbanButton"]', function () {
    var userId = $(this).attr("data-UserId");

    $.ajax({
        type: "GET",
        url: "UnbanUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { userId: userId },
        complete: function () {
            location.reload();
        }
    });
});

$(document).on('click', '[name="banButton"]', function () {
    var userId = $(this).attr("data-UserId");
    bootbox.prompt({
        title: "Ban użytkownika",
        inputType: 'select',
        inputOptions: [
            {
                text: 'Choose one...',
                value: '',
            },
            {
                text: 'Godzina',
                value: '1',
            },
            {
                text: 'Dzien',
                value: '2',
            },
            {
                text: 'Miesiąc',
                value: '3',
            },
            {
                text: 'Rok',
                value: '4',
            },
            {
                text: '10 Lat',
                value: '5',
            }
        ],
        callback: function (result) {
            $.ajax({
                type: "GET",
                url: "BanUser",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { value: result, userId: userId },
                complete: function () {
                    location.reload();
                }
            });
        }
    });
});

$(document).on('click', '[name="message"]', function () {
    var btnId = $(this).attr("id");
    bootbox.prompt({
        title: "Nowa Wiadomość - text",
        inputType: 'textarea',
        callback: function (resultText) {
            if (resultText == "") {
                bootbox.alert("Pusta wiadomość nie może być wysłana!")
            } else {
                $.ajax({
                    type: "GET",
                    url: "/Messages/SendMessageAddresseeFromList",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: { message: resultText, addressee: btnId },
                    success: function (data) {

                    }
                });
            }
        }
    });
});
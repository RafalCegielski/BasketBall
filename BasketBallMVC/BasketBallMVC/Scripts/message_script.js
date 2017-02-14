$(document).on('click', '[name="BtnNewMessage"]', function () {
    var tempDomain = "";
    if (document.domain === "localhost") {
        tempDomain = "/BasketBallMVC"
    }
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
                    url: tempDomain + "/Messages/SendMessageAddresseeFromList",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: { message: resultText, addressee: btnId }
                });
            }
        }
    });
});
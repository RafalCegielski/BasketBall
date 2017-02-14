$(document).ready(function () {
    var tempDomain = "";
    if (document.domain === "localhost") {
        tempDomain = "/BasketBallMVC"
    }
    $(document).on('click', '[name="RemoveMessage"]', function () {
        button = $(this);
        $.ajax({
            type: "GET",
            url: "RemoveMessage",
            data: { messageId: $(this).attr("id") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {
                location.reload();
            }
        });
    });

    $(document).on('click', '[name="Message"]', function () {

        button = $(this);
        $.ajax({
            type: "GET",
            url: tempDomain + "/Messages/MessageDetails",
            data: { messageId: $(this).attr("id") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                bootbox.confirm({
                    title: "Wiadomość",
                    message: data["message"],
                    buttons: {
                        cancel: {
                            label: '<i class="glyphicon glyphicon-eye-close"></i> Zamknij'
                        },
                        confirm: {
                            label: '<i class="glyphicon glyphicon-send"></i> Odpowiedz'
                        }
                    },
                    callback: function (result) {
                        if (result) {
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
                                            data: { message: resultText, addressee: button.attr("data-addressee") }
                                        });
                                    }
                                }
                            });
                        }
                    }
                });
                button.removeAttr("style")

                $.ajax({
                    type: "GET",
                    url: tempDomain + "/Messages/CheckAllMessageStatus",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data["status"] == "True") {
                            clearTimeout(message);
                            document.getElementById("Messages").style.color = "#9d9d9d"
                        }
                    }
                });
            }
        });
    });

    $(document).on('touchstart', '[name="Message"]', function () {

        button = $(this);
        $.ajax({
            type: "GET",
            url: tempDomain + "/Messages/MessageDetails",
            data: { messageId: $(this).attr("id") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                bootbox.confirm({
                    title: "Wiadomość",
                    message: data["message"],
                    buttons: {
                        cancel: {
                            label: '<i class="glyphicon glyphicon-eye-close"></i> Zamknij'
                        },
                        confirm: {
                            label: '<i class="glyphicon glyphicon-send"></i> Odpowiedz'
                        }
                    },
                    callback: function (result) {
                        if (result) {
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
                                            data: { message: resultText, addressee: button.attr("data-addressee") }
                                        });
                                    }
                                }
                            });
                        }
                    }
                });
                button.removeAttr("style")

                $.ajax({
                    type: "GET",
                    url: tempDomain + "/Messages/CheckAllMessageStatus",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data["status"] == "True") {
                            clearTimeout(message);
                            document.getElementById("Messages").style.color = "#9d9d9d"
                        }
                    }
                });
            }
        });
    });

    $(document).on('click', '[id="BtnNewMessage"]', function () {
        bootbox.prompt({
            title: "Nowa Wiadomość - text",
            inputType: 'textarea',
            callback: function (resultText) {
                if (resultText == "") {
                    bootbox.alert("Pusta wiadomość nie może być wysłana!")
                } else if (resultText == null) {

                } else {
                    $.ajax({
                        type: "GET",
                        url: tempDomain + "/Messages/FriendList",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            obj = $.parseJSON(data);

                            var person = [];
                            for (var i = 0; i < obj.length; i++) {
                                person.push({
                                    text: obj[i].Nick,
                                    value: obj[i].FriendId
                                });
                            }
                            bootbox.prompt({
                                title: "Nowa Wiadomość - odbiorca",
                                inputType: 'select',
                                inputOptions: person,
                                callback: function (resultUser) {
                                    if (resultUser != null && resultText != null) {
                                        $.ajax({
                                            type: "GET",
                                            url: tempDomain + "/Messages/SendMessage",
                                            data: { message: resultText, addressee: resultUser },
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json"
                                        });
                                    }
                                }
                            });
                        }
                    });
                }

            }
        });
    });
});
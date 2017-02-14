$(document).ready(function () {
    $("[name='CategoryButton']").click(function () {
        $.ajax({
            type: "GET",
            url: "GetCtegoriesForShop",
            data: { buttonId: $(this).attr("id") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var obj = jQuery.parseJSON(data);
                htmlToAppend = "<img class=\"col-md-1 arowShop\" src=\"../Content/Images/arrow.png\"/>" +
                               "<div class=\"col-md-3 col-xs-12 shopSecondStep\" >" +
                                   "<button type=\"button\" name=\"DeviceCategoryButton\" id=\"DeviceCategoryButton_" + obj[0].name + "\" class=\"btn btn-orange col-md-12 col-xs-3 shopSecondStepBtnFirst\">" + obj[0].name + "</button>" +
                                   "<button type=\"button\" name=\"DeviceCategoryButton\" id=\"DeviceCategoryButton_" + obj[1].name + "\" class=\"btn btn-orange col-md-12 col-xs-3 shopSecondStepBtn\">" + obj[1].name + "</button>" +
                                   "<button type=\"button\" name=\"DeviceCategoryButton\" id=\"DeviceCategoryButton_" + obj[2].name + "\" class=\"btn btn-orange col-md-12 col-xs-3 shopSecondStepBtn\">" + obj[2].name + "</button>" +
                                   "<button type=\"button\" name=\"DeviceCategoryButton\" id=\"DeviceCategoryButton_" + obj[3].name + "\" class=\"btn btn-orange col-md-12 col-xs-3 shopSecondStepBtn\">" + obj[3].name + "</button>" +
                               "</div>";
                $("#shopSecondStep").html(htmlToAppend);
                $("#shopThirdStep").html("");
            }
        });
    });

    $(document).on('click', '[name="DeviceCategoryButton"]', function () {
        $.ajax({
            type: "GET",
            url: "GetCtegoriesForShop",
            data: { buttonId: $(this).attr("id") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var obj = jQuery.parseJSON(data);

                htmlToAppend = "<img class=\"col-md-1 arowShop\" src=\"../Content/Images/arrow.png\"/> " +
                                "<div class=\"col-md-5 col-xs-12 shopThirdStep\">";
                if (obj[0].isOur == true) {
                    htmlToAppend += "<div class=\"col-md-12 selectedFirstBtn\">" +
                                       "<span class=\"btn col-md-6 col-xs-6 btnThirdStep\" disabled>" + obj[0].Name + "</span>" +
                                       "<div class=\"col-md-2 col-xs-2 shopTextThirdStep\">" + obj[0].Price + " gold</div>" +
                                       "<div class=\"col-md-3 col-xs-3 shopTextThirdStep\">" + obj[0].Description + "</div>" +
                                       "<button name=\"DeviceSaleButton\" id=\"DeviceSaleButton_" + obj[0].Id + "\" type=\"button\" class=\"btn col-md-1 col-xs-1 btn-orange shopSaleBtn\"><span class='glyphicon glyphicon-trash'> </span></button>" +
                                   "</div>";
                } else {
                    htmlToAppend += "<div class=\"col-md-12 col-xs-12 unselectedFirstBtn\">" +
                                        "<button name=\"DeviceButton\" id=\"DeviceButton_" + obj[0].Id + "\" type=\"button\" class=\"btn col-md-6 col-xs-6 btn-orange btnThirdStep\">" + obj[0].Name + "</button>" +
                                        "<div class=\"col-md-2 col-xs-2 shopTextThirdStep\">" + obj[0].Price + " gold</div>" +
                                        "<div class=\"col-md-3 col-xs-3 shopTextThirdStep\">" + obj[0].Description + "</div>" +
                                    "</div>";
                }
                if (obj[1].isOur == true) {
                    htmlToAppend += "<div class=\"col-md-12 col-xs-12  selectedBtn\">" +
                                    "<span class=\"btn col-md-6 col-xs-6 btnThirdStep\" disabled>" + obj[1].Name + "</span>" +
                                    "<div class=\"col-md-2 col-xs-2 shopTextThirdStep\">" + obj[1].Price + " gold</div>" +
                                    "<div class=\"col-md-3 col-xs-3 shopTextThirdStep\">" + obj[1].Description + "</div>" +
                                    "<button name=\"DeviceSaleButton\" id=\"DeviceSaleButton_" + obj[1].Id + "\" type=\"button\" class=\"btn col-md-1 btn-orange shopSaleBtn\"><span class='glyphicon glyphicon-trash'> </span></button>" +

                                "</div>";
                } else {
                    htmlToAppend += "<div class=\"col-md-12 col-xs-12 unselectedBtn\">" +
                                        "<button name=\"DeviceButton\"  id=\"DeviceButton_" + obj[1].Id + "\" type=\"button\" class=\"btn btn-orange col-md-6 col-xs-6 btnThirdStep\">" + obj[1].Name + "</button>" +
                                        "<div class=\"col-md-2 col-xs-2 shopTextThirdStep\">" + obj[1].Price + " gold</div>" +
                                        "<div class=\"col-md-3 col-xs-3 shopTextThirdStep\">" + obj[1].Description + "</div>" +
                                    "</div>";
                }
                if (obj[2].isOur == true) {
                    htmlToAppend += "<div class=\"col-md-12 col-xs-12 selectedBtn\">" +
                                    "<span class=\"btn col-md-6 col-xs-6 btnThirdStep\" disabled>" + obj[2].Name + "</span>" +
                                    "<div class=\"col-md-2 col-xs-2 shopTextThirdStep\">" + obj[2].Price + " gold</div>" +
                                    "<div class=\"col-md-3 col-xs-3 shopTextThirdStep\">" + obj[2].Description + "</div>" +
                                    "<button name=\"DeviceSaleButton\" id=\"DeviceSaleButton_" + obj[2].Id + "\" type=\"button\" class=\"btn col-md-1 btn-orange shopSaleBtn\"><span class='glyphicon glyphicon-trash'> </span></button>" +

                                "</div>";
                } else {
                    htmlToAppend += "<div class=\"col-md-12 col-xs-12 unselectedBtn\">" +
                                    "<button name=\"DeviceButton\"  id=\"DeviceButton_" + obj[2].Id + "\" type=\"button\" class=\"btn btn-orange col-md-6 col-xs-6 btnThirdStep\">" + obj[2].Name + "</button>" +
                                    "<div class=\"col-md-2 col-xs-2 shopTextThirdStep\">" + obj[2].Price + " gold</div>" +
                                    "<div class=\"col-md-3 col-xs-3 shopTextThirdStep\">" + obj[2].Description + "</div>" +
                                "</div>";
                }
                if (obj[3].isOur == true) {
                    htmlToAppend += "<div class=\"col-md-12 col-xs-12 selectedBtn\">" +
                                        "<span class=\"btn col-md-6 col-xs-6 btnThirdStep\" disabled>" + obj[3].Name + "</span>" +
                                        "<div class=\"col-md-2 col-xs-2 shopTextThirdStep\">" + obj[3].Price + " gold</div>" +
                                        "<div class=\"col-md-3 col-xs-3 shopTextThirdStep\">" + obj[3].Description + "</div>" +
                                        "<button name=\"DeviceSaleButton\" id=\"DeviceSaleButton_" + obj[3].Id + "\" type=\"button\" class=\"btn col-md-1 btn-orange shopSaleBtn\"><span class='glyphicon glyphicon-trash'> </span></button>" +

                                    "</div>" +
                                "</div>";
                } else {
                    htmlToAppend += "<div class=\"col-md-12 col-xs-12 unselectedBtn\">" +
                                        "<button name=\"DeviceButton\"  id=\"DeviceButton_" + obj[3].Id + "\" type=\"button\" class=\"btn btn-orange col-md-6 col-xs-6 btnThirdStep\">" + obj[3].Name + "</button>" +
                                        "<div class=\"col-md-2 col-xs-2 shopTextThirdStep\">" + obj[3].Price + " gold</div>" +
                                        "<div class=\"col-md-3 col-xs-3 shopTextThirdStep\">" + obj[3].Description + "</div>" +
                                    "</div>" +
                                "</div>";
                }


                $("#shopThirdStep").html(htmlToAppend);
            }
        });
    });

    $(document).on('click', '[name="DeviceButton"]', function () {
        clickedBtnId = $(this).attr("id")
        bootbox.confirm({
            message: "Czy na pewno chcesz kupić ten przedmiot?",
            buttons: {
                confirm: {
                    label: 'Tak',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'Nie',
                    className: 'btn-success'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        type: "GET",
                        url: "BuyNewDevice",
                        data: { buttonId: clickedBtnId },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var obj = jQuery.parseJSON(data);
                            if (obj.status == "true") {
                                location.reload();
                            } else {
                                bootbox.alert({
                                    message: "Masz za mało złota",
                                    backdrop: true
                                });
                            }
                        }
                    });
                }
            }
        })
    });

    $(document).on('click', '[name="DeviceSaleButton"]', function () {
        clickedBtnId = $(this).attr("id")
        bootbox.confirm({
            message: "Czy na pewno chcesz sprzedać przedmiot za połowę jego ceny?",
            buttons: {
                confirm: {
                    label: 'Tak',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'Nie',
                    className: 'btn-success'
                }
            },
            callback: function (result) {
                if (result) {

                    $.ajax({
                        type: "GET",
                        url: "SaleDevice",
                        data: { buttonId: clickedBtnId },
                        success: function () {
                            location.reload();
                        }
                    });
                }
            }
        });
    });
});
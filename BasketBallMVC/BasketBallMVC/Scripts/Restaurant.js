$(document).ready(function () {
    $("[name='CategoryButton']").click(function () {
        btn = $(this);
        htmlToAppend = "<img class=\"col-md-1 restaurantArrow\" src=\"../Content/Images/arrow.png\"/>" +
                       "<div class=\"col-md-3  col-xs-12\" id=\"restaurntAbilitiesSecond\">" +
                           "<button type=\"button\" name=\"DeviceCategoryButton\" id=\"DeviceCategoryButton_Small_" + btn.attr("data-type") + "\" class=\"btn btn-orange col-md-12 col-xs-4 restaurantSecondStepFirst\">Mała porcja</button>" +
                           "<button type=\"button\" name=\"DeviceCategoryButton\" id=\"DeviceCategoryButton_Medium_" + btn.attr("data-type") + "\" class=\"btn btn-orange col-md-12 col-xs-4 restaurantSecondStep\">Średnia porcja</button>" +
                           "<button type=\"button\" name=\"DeviceCategoryButton\" id=\"DeviceCategoryButton_Big_" + btn.attr("data-type") + "\" class=\"btn btn-orange col-md-12 col-xs-4 restaurantSecondStep\">Duża porcja</button>" +
                       "</div>";
        $("#restaurantSecondStep").html(htmlToAppend);
        $("#restaurantThirdStep").html("");

    });

    $(document).on('click', '[name="restaurantBtn"]', function () {
        btn = $(this);
        $.ajax({
            type: "GET",
            url: "BuyFood",
            data: { btnId: btn.attr("id"), size: btn.attr("data-size") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {
                location.reload();
            }
        });
    });

});
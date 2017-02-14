$(document).ready(function () {
    $("#shopFirstStep").find("#shopAbilities").find("button").on("click", function () {
        if ($(this).id == "shopFirstStep_1") {
        }
        $("#shopSecondStep").removeClass("hide");
    });
    $("#shopSecondStep").find("#shopAbilities").find("button").on("click", function () {
        $("#shopThirdStep").removeClass("hide");
    });
});
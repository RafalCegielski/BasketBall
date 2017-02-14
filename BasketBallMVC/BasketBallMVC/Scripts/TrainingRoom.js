$(document).ready(function () {
    $('[name="CategoryButton"]').on('click', function () {
        $.ajax({
            type: "GET",
            url: "GetExerciseCategories",
            data: { distance: $(this).attr("data-distance") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var obj = jQuery.parseJSON(data);
                htmlToAppend = "<img class=\"col-md-1 arrowTraingingRoom\" src=\"../Content/Images/arrow.png\"/>" +
                               "<div class=\"col-md-3 shopAbilitiesSecond\" id=\"shopAbilities\">" +
                                   "<button type=\"button\" name=\"ExerciseCategoryButton\" id=\"ExerciseCategoryButton_" + obj[0].Name + "\" class=\"btn btn-orange col-md-12 col-xs-3 secondTraingingBtnFirst\">" + obj[0].Name + "</button>" +
                                   "<button type=\"button\" name=\"ExerciseCategoryButton\" id=\"ExerciseCategoryButton_" + obj[1].Name + "\" class=\"btn btn-orange col-md-12 col-xs-3 secondTraingingBtn\">" + obj[1].Name + "</button>" +
                                   "<button type=\"button\" name=\"ExerciseCategoryButton\" id=\"ExerciseCategoryButton_" + obj[2].Name + "\" class=\"btn btn-orange col-md-12 col-xs-3 secondTraingingBtn\">" + obj[2].Name + "</button>" +
                                   "<button type=\"button\" name=\"ExerciseCategoryButton\" id=\"ExerciseCategoryButton_" + obj[3].Name + "\" class=\"btn btn-orange col-md-12 col-xs-3 secondTraingingBtn\">" + obj[3].Name + "</button>" +
                               "</div>";
                $("#TrainingRoomSecondStep").html(htmlToAppend);
                $("#TrainingRoomThirdStep").html("");
            }
        });
    });

});

$(document).on("click", '[name="practice"]', function () {
    btn = $(this);
    $.ajax({
        type: "GET",
        url: "StartTraining",
        data: { btnId: btn.attr("id"), exerciseName: btn.attr("data-exercname") },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            obj = jQuery.parseJSON(data);
            if (obj.status === "Busy") {
                bootbox.alert("Twoja postać wykonuje obecnie inne ćwiczenie!");
            } else if (obj.status == "Missing") {
                bootbox.alert("Twoja postać nie spełnia wymagań!");
            } else {
                location.reload();
            }
        }
    });
});

$(document).on("click", '[name="cancel"]', function () {
    btn = $(this);
    $.ajax({
        type: "GET",
        url: "CancelTraining",
        data: { btnId: btn.attr("id"), exerciseName: btn.attr("data-exercname") },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function () {
            location.reload();
        }
    });
});
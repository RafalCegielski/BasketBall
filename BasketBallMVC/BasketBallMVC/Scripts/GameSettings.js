$(document).ready(function () {
    exercise1 = $("#Exercise1");
    exercise2 = $("#Exercise2");
    exercise3 = $("#Exercise3");

    exercise1.change(function () {
        selectedItem = exercise1.val();
        selectedItem2 = exercise2.val();
        selectedItem3 = exercise3.val();

        exercise2.find('option:disabled').removeAttr("disabled");
        exercise3.find('option:disabled').removeAttr("disabled");

        exercise2.find('option[value=' + selectedItem + ']').attr("disabled", "disabled");
        if (selectedItem3 != "")
            exercise2.find('option[value=' + selectedItem3 + ']').attr("disabled", "disabled");
        exercise3.find('option[value=' + selectedItem + ']').attr("disabled", "disabled");
        if (selectedItem2 != "")
            exercise3.find('option[value=' + selectedItem2 + ']').attr("disabled", "disabled");
    });

    exercise2.change(function () {
        selectedItem = exercise2.val();
        selectedItem1 = exercise1.val();
        selectedItem3 = exercise3.val();

        exercise1.find('option:disabled').removeAttr("disabled");
        exercise3.find('option:disabled').removeAttr("disabled");

        exercise1.find('option[value=' + selectedItem + ']').attr("disabled", "disabled");
        exercise3.find('option[value=' + selectedItem + ']').attr("disabled", "disabled");
        if (selectedItem3 != "")
            exercise1.find('option[value=' + selectedItem3 + ']').attr("disabled", "disabled");
        if (selectedItem1 != "")
            exercise3.find('option[value=' + selectedItem1 + ']').attr("disabled", "disabled");
    });

    exercise3.change(function () {
        selectedItem = exercise3.val();
        selectedItem1 = exercise1.val();
        selectedItem2 = exercise2.val();

        exercise1.find('option:disabled').removeAttr("disabled");
        exercise2.find('option:disabled').removeAttr("disabled");

        exercise1.find('option[value=' + selectedItem + ']').attr("disabled", "disabled");
        exercise2.find('option[value=' + selectedItem + ']').attr("disabled", "disabled");
        if (selectedItem2 != "")
            exercise1.find('option[value=' + selectedItem2 + ']').attr("disabled", "disabled");
        if (selectedItem1 != "")
            exercise2.find('option[value=' + selectedItem1 + ']').attr("disabled", "disabled");
    });

});
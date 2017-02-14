$("#applyBtn").click(function () {
    var tempDomain = "";
    if (document.domain === "localhost") {
        tempDomain = "/BasketBallMVC"
    }
	$.ajax({
		type: "POST",
		url: tempDomain + "/Game/GameStyle",
		data: { value: $("#slider").val() },
		dataType: "json",
		complete: function () {
			location.reload();
		}
	});
});
$(document).ready(function () {
	$("[name='Invitation']").click(function () {
		$.ajax({
			type: "GET",
			url: "AcceptRejectInvitation",
			data: { id: $(this).attr("id") },
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			complete: function () {
				location.reload();
			}
		});
	});

	$("[name='Battle']").click(function () {
		$.ajax({
			type: "GET",
			url: "DeleteNotification",
			data: { id: $(this).attr("id") },
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			complete: function () {
				location.reload();
			}
		});
	});
});
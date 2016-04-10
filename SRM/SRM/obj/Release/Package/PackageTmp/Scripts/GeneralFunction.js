function getDataFromServer(id, url, controlId, text) {
    try {
        if (id === null || id.length <= 0) { $("#" + controlId).data("kendoDropDownList").enable(false); return; }
        $.ajax({
            url: url,
            data: { id: id },
            cache: false,
            type: "POST",
            success: function (data) {
                if (JSON.stringify(data) !== "[]") {
                    var ddl = $('#' + controlId).data("kendoDropDownList");
                    ddl.setDataSource(data);
                    ddl.refresh();
                    if (data.length > 0) {
                        $("#" + controlId).data("kendoDropDownList").enable(true);
                    }
                } else {
                    $("#" + controlId).data("kendoDropDownList").enable(false);
                    $("#" + controlId).select(0);
                }
            },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
    } catch (err) {
        alert(err.description);
    }
}
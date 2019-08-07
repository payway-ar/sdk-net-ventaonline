$(".onlyOtherEnvironment").hide();
$("#AmbienteId").change(() => {
    if ($("#AmbienteId").val() == -1) {
        $(".onlyOtherEnvironment").show();
    } else {
        $("#request_host,#request_path").val("")
        $(".onlyOtherEnvironment").hide();
    }
});
function spinner_On() {
    $('.spinner').css('display', 'block');
}
function spinner_Off() {
    $('.spinner').css('display', 'none');
}

function hideSuccessErrorDiv() {
    $("#divError").hide();
    $("#divSuccess").hide();
}

function enableSucessDiv(message) {
    $("#divSuccess").show();
    $("#divSuccessMess").html(message);
    $("#divError").hide();
}

function enableErrorDiv(message) {
    $("#divError").show();
    $("#divErrorMess").html(message);
    $("#divSuccess").hide();
}


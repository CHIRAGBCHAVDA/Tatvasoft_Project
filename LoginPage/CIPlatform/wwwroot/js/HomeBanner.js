$(document).ready(function(){

    $.ajax({
        type: "POST",
        url: "/Home/GetBanner",
        data: {},
        success: function (result) {
            $("#home-banner").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    })

});
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function display(item) {
    try {
        document.getElementById("video-area").style.display = "none";
        document.getElementById("image-area").style.display = "block";
    } catch (e) {
        console.log(e);
    }


    let image = document.getElementById("image-area");
    image.src = item.src;
}
function displayVideo(item) {
    try {
        document.getElementById("image-area").style.display = "none";
        document.getElementById("video-area").style.display = "block";

    } catch (e) {
        console.log(e)
    }

    let video = document.getElementById("video-area");
    let jovamate = $(item).attr("data-vpath");
    video.src = jovamate;
}




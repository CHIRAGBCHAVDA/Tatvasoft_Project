$(document).ready(function(){

    let UserImgSrc = $("#admin-user-editImg").src;
    $(".admin-sidebar-btn").click(function () {

        switch (this.id) {

            case "v-pills-user-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialUser",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;

            case "v-pills-cms-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialCMS",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;

            case "v-pills-mission-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialMission",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;

            case "v-pills-missionapplication-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialMissionApplication",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;

            case "v-pills-story-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialStory",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;
        }

    });



    try {
        const avatarUpload = document.querySelector('#avatar-upload');
        const userImage = document.querySelector('.user-img-left');

        avatarUpload.addEventListener('change', function () {
            const file = avatarUpload.files[0];
            const reader = new FileReader();

            reader.addEventListener('load', function () {
                userImage.src = reader.result;
                userImgSrc = userImage.src;
            });

            if (file) {
                reader.readAsDataURL(file);
            }
        });

        userImage.addEventListener('click', function (e) {
            e.preventDefault();
            avatarUpload.click();
        });
    } catch (error) {
        console.error(error);
    }

    $(document).on('click', "#admin-user-add", function () {
        GetAllCities();
        GetAllCountries();
    });

});

function GetAllCities() {
    $.ajax({
        type: "GET",
        url: "/Admin/GetAllCities",
        success: function (result) {
            for (var city of result) {
                $("#CITYID-0").append(`<option value =${city.cityid}> ${city.name}</option>`)
            };
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}
function GetAllCountries() {
    $.ajax({
        type: "GET",
        url: "/Admin/GetAllCountries",
        success: function (result) {
            for (var country of result) {
                $("#CID-0").append(`<option value=${country.countryid}> ${country.name}</option>`)
            };
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function UpdateUser(e) {
    var UserId = parseInt($(e).attr("data-userid"));
    var form = document.getElementById(`admin-user-editForm-${UserId}`);
    form.addEventListener('submit', function (event) {
        event.preventDefault();
    });
    var formData = new FormData(form);

    $.ajax({
        type: "POST",
        url: "/Admin/UserEditFormPost",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            //console.log(result);
            //$(".admin-main-wrapper").html(result);
            if (result) {
                $("#v-pills-user-tab").click();
            }
            else {
                return;
            }

        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });



}

var searchKeyword = "";

$(document).ready(function () {
    getUserFilter(1);
    //getCmsFilter(1);

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

    $(".admin-main-wrapper").on("keyup", "#search-user-query", function () {
        searchUsers();
    });

    $("#1").addClass('active');
    $('.admin-main-wrapper').on('click', '.page-item-user', function () {
        $('.pagination .page-item-user').removeClass('active');
        $(this).addClass('active');
        getUserFilter($(this).attr('id'));
    });

    $(".admin-main-wrapper").on("keyup", "#search-cms-query", function () {
        searchCms();
    });
    $('.admin-main-wrapper').on('click', '.page-item-cms', function () {
        $('.pagination .page-item-cms').removeClass('active');
        $(this).addClass('active');
        getCmsFilter($(this).attr('id'));
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

function searchUsers() {
    searchKeyword = document.getElementById("search-user-query").value;
    console.log(searchKeyword)
    getUserFilter();
}
function searchCms() {
    searchKeyword = document.getElementById("search-cms-query").value;
    console.log(searchKeyword);
    getCmsFilter();
}

function getUserFilter(pg) {
    $.ajax({
        type:"POST",
        url: "/Admin/getUserFilter",
        data: {
            pageNum:pg,
            searchKeyword: searchKeyword
        },
        success: function (result) {
            $(".admin-user-table-body-wrapper").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function getCmsFilter(pg) {
    $.ajax({
        type: "POST",
        url: "/Admin/getCmsFilter",
        data: {
            pageNum: pg,
            searchKeyword: searchKeyword
        },
        success: function (result) {
            $(".admin-cms-table-body-wrapper").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function GetAllCities() {
    $.ajax({
        type: "GET",
        url: "/Admin/GetAllCities",
        success: function (result) {
            for (var city of result) {
                $("#CityId").append(`<option value =${city.cityId}> ${city.name}</option>`)
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
                $("#CountryId").append(`<option value=${country.countryId}> ${country.name}</option>`)
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
    let addEditUrl = "";
    if (UserId == 0) { addEditUrl = "/Admin/UserAddFormPost" }
    else { addEditUrl = "/Admin/UserEditFormPost" }
    console.log("Submit button is clicked");
    form.addEventListener('submit', function (event) {
        event.preventDefault();

        var formData = new FormData(form);

        $.ajax({
            type: "POST",
            url: addEditUrl,
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                console.log("Going to console the result");
                console.log(result);
                //$(".admin-main-wrapper").html(result);
                if (result == true) {
                    $("#v-pills-user-tab").click();
                }
                else {
                    return;
                }

            },
            error: function (xhr, status, error) {
                console.log("IN error function");
                console.log(error);
                return;
            }
        });
        
    });
}

function adminAddCMS(e) {
    debugger
    var cmsId = parseInt($(e).attr("data-cmsid"));
    var form = document.getElementById(`adminCmsAddEditForm-${cmsId}`);
    let addEditUrl = "";
    if (cmsId == 0) { addEditUrl = "/Admin/CmsAddFormPost" }
    else { addEditUrl = "/Admin/CmsEditFormPost" }

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        var formData = new FormData(form);

        $.ajax({
            type: "POST",
            url: addEditUrl,
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                console.log("Going to console the result");
                console.log(result);
                //$(".admin-main-wrapper").html(result);
                if (result == true) {
                    if (cmsId == 0) {
                        //toastr.success("Cms Page Added successfully");
                    }
                    else {
                        //toastr.success("Cms page Updated Successfully");
                    }
                    $("#v-pills-cms-tab").click();
                    $(`#cms-modal-dismiss-${cmsId}`).click();
                }
                else {
                    return;
                }
            },
            error: function (xhr, status, error) {
                console.log("IN error function");
                console.log(error);
                return;
            }
        });

    });

}
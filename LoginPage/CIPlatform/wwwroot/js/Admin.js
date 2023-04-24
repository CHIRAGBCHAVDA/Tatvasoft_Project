

var searchKeyword = "";
var sources = [];
var pdfs = [];

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




    try {
        $(document).on('change', '.file-upload',async function () {
            console.log("File uplpoad function called");
            var files = $(this)[0].files;
            

            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var reader = new FileReader();

                var src = await new Promise((resolve, reject) => {
                    reader.onload = () => {
                        resolve(reader.result);
                    };

                    reader.onerror = reject;
                    reader.readAsDataURL(file);
                });

                if (file.type.includes('image')) {
                    $(this).closest('.input-group').next('.file-preview').append('<div class="col-auto"><div class="position-relative"><img class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                    sources.push(src);
                }else {
                    alert("Please upload a supported file type (image or video).");
                }
            }

            console.log("Sources array: ", sources);
        });


        $(document).on('click', '.file-preview .btn-close', function () {
            var imageSrc = $(this).siblings('img').attr('src'); // get the src of the image
            var index = sources.indexOf(imageSrc); // get the index of the image src in the sources array
            if (index !== -1) {
                sources.splice(index, 1); // remove the image src from the sources array
            }
            $(this).closest('.col-auto').remove();
            console.log("Sources array: ", sources);

        });

    } catch (e) {
        console.log(e);
    }

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

function getMissionSkills(e) {
    var missionId = parseInt($(e).attr("data-missionid"));
    var form = $(`#AdminAddEditForm-${missionId}`);
    $.ajax({
        type: "post",
        url: "/Admin/getMissionSkills",
        data: { missionId: missionId },
        success: function (result) {
            console.log(result);
            for (var i = 0; i < result.length; i++) {
                var missionSkill = result[i];
                var inputId = 'mission-skill-' + missionSkill.missionId + '-' + missionSkill.skillId;
                $(`#${inputId}`).prop('checked', true);
            }
            
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}
var files = [];
$(document).on("change", ".admin-files", function () {
    for (var i = 0; i < this.files.length; i++)
    {
        files.push(this.files[i]);
    }
});
function AdminAddEditMission(e) {
    console.log("HERE FROM MIISSION");
    var missionId = parseInt($(e).attr("data-missionid"));

    console.log("Printing the sources array in form submit method",sources);
    let skillIds = [];
    let temp = $(`#AdminAddEditForm-${missionId} .form-check-input:checked`);
    temp.map(function () {
        skillIds.push(parseInt(this.id.toString().split("-")[3]));
    });

    console.log("Mission Id",missionId);
    var form = document.getElementById(`AdminAddEditForm-${missionId}`);
    console.log(form);

    var videourls = $(`#videourl-${missionId}`).val().toString().split(",");
    console.log("Video urls are : ",videourls);
    
    var formData = new FormData(form);
    for (var i = 0; i < files.length; i++) {
        formData.append("files", files[i]);
    }
    for (var i = 0; i < skillIds.length; i++) {
        formData.append("skillids", skillIds[i]);
    }
    for (var i = 0; i < sources.length; i++) {
        formData.append("missionphotos", sources[i]);
    }
    for (var i = 0; i < videourls.length; i++) {
        formData.append("videourls", videourls[i]);
    }

        $.ajax({
            type: "POST",
            url: "/Admin/AddEditMission",
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
               
            },
            error: function (xhr, status, error) {
                
                console.log(error);
                return;
            }
    });
}


$(document).on("click", "#AdminAddMissionButton-0", function () {
    debugger
    $.ajax({
        type: "post",
        url: "/Admin/getThemeCountryCityAvailabiltySkills",
        complete: function (result) {
            console.clear();
            console.log(result);
            console.log(result.responseJSON.missionThemes);

            result.responseJSON.missionThemes.forEach(function (theme) {
                $("#missionthemeid-0").append(`<option value=${theme.missionThemeId}> ${theme.title}</option>`)
            });

            result.responseJSON.cities.forEach(function (city){
                $("#cityid-0").append(`<option value=${city.cityId}> ${city.name}</option>`)
            });

            result.responseJSON.countries.forEach(function(country){
                $("#countryid-0").append(`<option value=${country.countryId}> ${country.name}</option>`)
            });

            result.responseJSON.availabilities.forEach(function(availability){
                $("#availabilityid-0").append(`<option value=${availability.availabilityId}> ${availability.name}</option>`)
            });

            var skillsContainer = $("#skills-container-0");
            skillsContainer.empty();
            // Add new checkboxes for each skill in the result list
            $.each(result.skills, function (index, item) {
                var checkboxHtml = '<div class="form-check ms-2 col">' +
                    '<input class="form-check-input" type="checkbox" id="mission-skill-0' + '-' + item.skillId + '">' +
                    '<label class="form-check-label" for="mission-skill-0' + '-' + item.skillId + '">' + item.skillName + '</label>' +
                    '</div>';
                skillsContainer.append(checkboxHtml);
            });

        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });

});
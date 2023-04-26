

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

            case "v-pills-missiontheme-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialMissionTheme",
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

            case "v-pills-missionskills-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialMissionSkill",
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

            case "v-pills-bannermanagement-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialBanner",
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

    $(".admin-main-wrapper").on("keyup", "#search-missiontheme-query", function () {
        searchMissionTheme();
    });
    $('.admin-main-wrapper').on('click', '.page-item-missiontheme', function () {
        $('.pagination .page-item-missiontheme').removeClass('active');
        $(this).addClass('active');
        getMissionThemeFilter($(this).attr('id'));
    });

    $(".admin-main-wrapper").on("keyup", "#search-missionskills-query", function () {
        searchMissionSkill();
    });
    $('.admin-main-wrapper').on('click', '.page-item-missionskill', function () {
        $('.pagination .page-item-missionskill').removeClass('active');
        $(this).addClass('active');
        getMissionSkillFilter($(this).attr('id'));
    });

    $(".admin-main-wrapper").on("keyup", "#search-missionapplication-query", function () {
        searchMissionApplication();
    });
    $('.admin-main-wrapper').on('click', '.page-item-missionapplication', function () {
        $('.pagination .page-item-missionapplication').removeClass('active');
        $(this).addClass('active');
        getMissionApplicationFilter($(this).attr('id'));
    });

    $(".admin-main-wrapper").on("keyup", "#search-story-query", function () {
        searchStory();
    });
    $('.admin-main-wrapper').on('click', '.page-item-story', function () {
        $('.pagination .page-item-story').removeClass('active');
        $(this).addClass('active');
        getStoryFilter($(this).attr('id'));
    });

    $(".admin-main-wrapper").on("keyup", "#search-banner-query", function () {
        searchBanner();
    });
    $('.admin-main-wrapper').on('click', '.page-item-banner', function () {
        $('.pagination .page-item-banner').removeClass('active');
        $(this).addClass('active');
        getBannerFilter($(this).attr('id'));
    });


    $(".admin-main-wrapper").on("keyup", "#search-mission-query", function () {
        searchMission();
    });
    $('.admin-main-wrapper').on('click', '.page-item-mission', function () {
        $('.pagination .page-item-mission').removeClass('active');
        $(this).addClass('active');
        getMissionFilter($(this).attr('id'));
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
function searchMissionTheme() {
    searchKeyword = document.getElementById("search-missiontheme-query").value;
    console.log(searchKeyword);
    getMissionThemeFilter();
}
function searchMissionSkill() {
    searchKeyword = document.getElementById("search-missionskills-query").value;
    console.log(searchKeyword);
    getMissionSkillFilter();
}
function searchMissionApplication() {
    searchKeyword = document.getElementById("search-missionapplication-query").value;
    console.log(searchKeyword);
    getMissionApplicationFilter();
}
function searchStory() {
    searchKeyword = document.getElementById("search-story-query").value;
    console.log(searchKeyword);
    getStoryFilter();
}
function searchBanner() {
    searchKeyword = document.getElementById("search-banner-query").value;
    console.log(searchKeyword);
    getBannerFilter();
}
function searchMission() {
    searchKeyword = document.getElementById("search-mission-query").value;
    console.log(searchKeyword);
    getMissionFilter();
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
            console.log("IS HITTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
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


function getMissionThemeFilter(pg) {
    $.ajax({
        type: "POST",
        url: "/Admin/getMissionThemeFilter",
        data: {
            pageNum: pg,
            searchKeyword: searchKeyword
        },
        success: function (result) {
            $("#admin-missiontheme-table-body-wrapper").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}
function getMissionSkillFilter(pg) {
    $.ajax({
        type: "POST",
        url: "/Admin/getMissionSkillFilter",
        data: {
            pageNum: pg,
            searchKeyword: searchKeyword
        },
        success: function (result) {
            $("#admin-missionskills-table-body-wrapper").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function getMissionApplicationFilter(pg) {
    $.ajax({
        type: "POST",
        url: "/Admin/getMissionApplicationFilter",
        data: {
            pageNum: pg,
            searchKeyword: searchKeyword
        },
        success: function (result) {
            $("#admin-missionapplication-table-body-wrapper").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function getStoryFilter(pg) {
    $.ajax({
        type: "POST",
        url: "/Admin/getStoryFilter",
        data: {
            pageNum: pg,
            searchKeyword: searchKeyword
        },
        success: function (result) {
            $("#admin-story-table-body-wrapper").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function getBannerFilter(pg) {
    $.ajax({
        type: "POST",
        url: "/Admin/getBannerFilter",
        data: {
            pageNum: pg,
            searchKeyword: searchKeyword
        },
        success: function (result) {
            $(".banner-table-wrapper").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function getMissionFilter(pg) {
    $.ajax({
        type: "POST",
        url: "/Admin/getMissionFilter",
        data: {
            pageNum: pg,
            searchKeyword: searchKeyword
        },
        success: function (result) {
            $(".admin-mission-table-wrapper").html(result);
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
            complete: function (result) {
                $(".dismiss-modal-button").click();
                toastr.success("Changes has been saved...!!");
            },
            error: function (xhr, status, error) {
                
                console.log(error);
                return;
            }
        });
}



//To get the country city theme availability and skills
$(document).on("click", "#AdminAddMissionButton-0", function () {
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
            console.log("SKills dekhlo : ", result.responseJSON.skills);
            // Add new checkboxes for each skill in the result list
            $.each(result.responseJSON.skills, function (index, item) {
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


function adminMissionThemeAddUpdate(e) {
    var missionThemeId = parseInt($(e).attr("data-missionthemeid"));
    var form = document.getElementById(`adminMissionThemeAddEditForm-${missionThemeId}`);
    var formData = new FormData(form);
    
    $.ajax({
        type: "POST",
        url: "/Admin/AddEditMissionTheme",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log(result);
            $(".dismiss-modal-button").click();
            $("#admin-missiontheme-table-body-wrapper").html(result);
            toastr.success("Changes has been Saved...!!");
            
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

$(document).on("submit", 'form', function (e) {
    e.preventDefault();
});

function adminMissionSkillAddUpdate(e) {
    var skillId = parseInt($(e).attr("data-missionskillid"));
    var form = document.getElementById(`adminMissionSkillAddEditForm-${skillId}`);
    var formData = new FormData(form);

    $.ajax({
        type: "POST",
        url: "/Admin/AddEditMissionSkill",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log(result);
            $(".dismiss-modal-button").click();
            $("#admin-missionskills-table-body-wrapper").html(result);
            toastr.success("Changes has been Saved...!!");

        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function MissionApplicationApprove(e) {
    var missionApplicationId = parseInt($(e).attr("data-missionapplicationid"));
    $.ajax({
        type: "POST",
        url: "/Admin/MissionApplicationApprove",
        data: {missionApplicationId : missionApplicationId},
        success: function (result) {
            console.log(result);
            $("#admin-missionapplication-table-body-wrapper").html(result);
            toastr.success("Mission Application Approved...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function MissionApplicationReject(e) {
    var missionApplicationId = parseInt($(e).attr("data-missionapplicationid"));
    $.ajax({
        type: "POST",
        url: "/Admin/MissionApplicationReject",
        data: { missionApplicationId: missionApplicationId },
        success: function (result) {
            console.log(result);
            $("#admin-missionapplication-table-body-wrapper").html(result);
            toastr.error("Mission Application Rejected...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function StoryApprove(e) {
    var storyId = parseInt($(e).attr("data-storyid"));
    $.ajax({
        type: "POST",
        url: "/Admin/StoryApprove",
        data: { storyId: storyId},
        success: function (result) {
            console.log(result);
            $("#admin-story-table-body-wrapper").html(result);
            toastr.success("Story Request Approved...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function StoryReject(e) {
    var storyId = parseInt($(e).attr("data-storyid"));
    $.ajax({
        type: "POST",
        url: "/Admin/StoryReject",
        data: { storyId: storyId},
        success: function (result) {
            console.log(result);
            $("#admin-story-table-body-wrapper").html(result);
            toastr.error("Story Request Rejected...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function adminAddEditBanner(e) {
    var bannerId = parseInt($(e).attr("data-bannerid"));
    console.log("Banner id isssssssssssssssssssssssssss ",bannerId);
    var form = document.getElementById(`adminBannerAddEditForm-${bannerId}`);
    var formData = new FormData(form);
    console.log("hello from the form data : ", formData);
    //set the image to img src
    var imgSrc = "";
    var fileInput = document.querySelector(`#files-${bannerId}`);
    var file = fileInput.files[0];
    readAsDataURL(file)
        .then(function (imgSrc) {
            console.log("The image src is : ", imgSrc);
            formData.set("image", imgSrc.toString());

            $.ajax({
                type: "POST",
                url: "/Admin/AddEditBanner",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    console.log(result);
                    $(".dismiss-modal-btn").click();
                    $(".banner-table-wrapper").html(result);
                    toastr.success("Changes has been saved...!!");
                },
                error: function (xhr, status, error) {
                    console.log(error);
                }
            });
        })
        .catch(function (error) {
            console.error(error);
        });

    
}

function readAsDataURL(file) {
    return new Promise(function (resolve, reject) {
        var reader = new FileReader();
        reader.onload = function (event) {
            resolve(event.target.result);
        };
        reader.onerror = function (event) {
            reject(event.target.error);
        };
        reader.readAsDataURL(file);
    });
}



function userDelete(e) {
    var userId = parseInt($(e).attr("data-userid"));
    $.ajax({
        type: "POST",
        url: "/Admin/DeleteUser",
        data: { userId: userId},
        success: function (result) {
            console.log(result);
            $(".admin-user-table-body-wrapper").html(result);
            toastr.error("User Deleted Successfully...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function cmsDelete(e) {
    var cmsId = parseInt($(e).attr("data-cmsid"));
    $.ajax({
        type: "POST",
        url: "/Admin/DeleteCms",
        data: { cmsId: cmsId },
        success: function (result) {
            console.log(result);
            $(".admin-cms-table-body-wrapper").html(result);
            toastr.error("CMS Deleted Successfully...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function missionDelete(e) {
    var missionId = parseInt($(e).attr("data-missionid"));
    $.ajax({
        type: "POST",
        url: "/Admin/DeleteMission",
        data: { missionId: missionId },
        success: function (result) {
            console.log(result);
            $(".admin-mission-table-wrapper").html(result);
            toastr.error("Mission Deleted Successfully...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function missionThemeDelete(e) {
    var missionthemeId = parseInt($(e).attr("data-missionthemeid"));
    $.ajax({
        type: "POST",
        url: "/Admin/DeleteMissionTheme",
        data: { missionthemeId: missionthemeId },
        success: function (result) {
            console.log(result);
            $("#admin-missiontheme-table-body-wrapper").html(result);
            toastr.error("Mission Theme Deleted Successfully...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function missionSkillDelete(e) {
    var missionskillId = parseInt($(e).attr("data-missionskillid"));
    $.ajax({
        type: "POST",
        url: "/Admin/DeleteMissionSkill",
        data: { missionskillId: missionskillId },
        success: function (result) {
            console.log(result);
            $("#admin-missionskills-table-body-wrapper").html(result);
            toastr.error("Skill Deleted Successfully...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function bannerDelete(e) {
    var bannerId = parseInt($(e).attr("data-bannerid"));
    $.ajax({
        type: "POST",
        url: "/Admin/DeleteBanner",
        data: { bannerId: bannerId },
        success: function (result) {
            console.log(result);
            $(".banner-table-wrapper").html(result);
            toastr.error("Banner Deleted Successfully...!!");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}






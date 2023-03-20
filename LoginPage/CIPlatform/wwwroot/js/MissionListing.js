


var selectedCountries = [];
var selectedCities = [];
var selectedThemes = [];
var selectedSkills = [];
var selectedOption = 1;
var flag = 1;
var searchKeyword = "";


getTotalCount();
$(document).ready(function () {
    $('.city-item').hide();


    $("#partialView").load('/MissionListing/GetGridView');
    $('#list').click(function () {
        flag = 2;
        $("#partialView").load('/MissionListing/GetListView');
    });
    $('#grid').click(function () {
        flag = 1;
        $("#partialView").load('/MissionListing/GetGridView');
    });

    $('.country-checkbox').change(function () {
        var countryId = $(this).val();
        if (!$(this).prop('checked')) {
            $('.city-checkbox[data-country="' + countryId + '"]').prop('checked', false);
            var index = selectedCountries.indexOf(countryId);
            if (index > -1 && !$(this).prop('checked')) {
                selectedCountries.splice(index);
                getFilter();
            }
        } else {
            var index = selectedCountries.indexOf(countryId);
            if (index == -1) {
                selectedCountries.push($(this).val());
            }
        }

        $.ajax({
            type: "POST",
            url: "/MissionListing/GetCities",
            data: { countryIds: selectedCountries },
            success: function (data) {
                $('.city-item').hide();
                $.each(data, function (index, value) {
                    var cityItem = $('.city-item[data-country="' + value.countryId + '"]');
                    cityItem.show();
                    cityItem.find('.city-checkbox[value="' + value.cityId + '"][data-country="' + value.countryId + '"]').show();
                    getBadge();
                });
            },
            error: function () {
                alert("An error occurred while fetching cities.");
            }
        });
    });

    
    $('.city-checkbox').change(function () {
        var cityId = $(this).next().text();
        if (!$(this).prop('checked')) {
            $('.city-checkbox[value="' + cityId + '"]').prop('checked', false);
            var index = selectedCities.indexOf(cityId);
            if (index > -1 && !$(this).prop('checked')) {
                selectedCities.splice(index);
                getFilter();
            }
        } else {
            var index = selectedCities.indexOf(cityId.trim());
            if (index == -1) {
                selectedCities.push(cityId.trim());
            }
        }
    });

    $('.theme-checkbox').change(function () {
        var themeName = $(this).next().text().trim();
        if (!$(this).prop('checked')) {
            $('.theme-checkbox[value="' + themeName.trim() + '"]').prop('checked', false);
            var index = selectedThemes.indexOf(themeName);
            if (index > -1 && !$(this).prop('checked')) {
                selectedThemes.splice(index);
                getFilter();
            }
        } else {
            var index = selectedThemes.indexOf(themeName);
            if (index == -1) { 
                selectedThemes.push(themeName.trim());
            }
        }
    });

    $("#search-query").keyup(function () {
        searchMissions();
    });

    $('.skill-checkbox').on('change', function () {
        getBadge();
    });
    $('.theme-checkbox').on('change', function () {
        getBadge();
    });

    getTotalCount();


    $("#1").addClass('active');
    $('.pagination').on('click', '.page-item', function () {
        getFilter($(this).attr('id'));
    });

    $(".rating-star-images").on("click", "i", function () {
        addRating($(this).attr('data-star'));
    });

    $(document).on("click","#partialView .reccommendMissionBtn, .reccommendMissionBtn",function () {
        var selected = $('.co-worker-checkbox input[type="checkbox"]:checked');
        var values = [];
        selected.each(function () {
            values.push($(this).val().trim());
        });
        var result = values.join(","); // combine the values with a comma and space
        result = result.replace(/,\s*$/, "");

        console.log(result);
        let missionTitle = $("#list-group-in-reccoworker").attr('data-missiontitle');
        let missionTheme = $("#list-group-in-reccoworker").attr('data-missiontheme');
        let missionSkills = $("#list-group-in-reccoworker").attr('data-missionskills');



        let subject = " Reccommendation to join " + missionTitle;
        console.log("The type of  missions skills is " + typeof (missionSkills));
        var currentUrl = window.location.href;
        console.log(currentUrl);
        let missionLink = "https://localhost:44383/?returnUrl=" + currentUrl;
        let body = `<!DOCTYPE html>
                    <html>

                    <head>
                      <meta charset="utf-8">
                      <meta name="viewport" content="width=device-width, initial-scale=1.0">
                      <title>Recommendation Email</title>
                    </head>

                    <body>
                      <div style="margin: 0 auto; max-width: 900px; font-family: Arial, sans-serif; font-size: 16px; line-height: 1.5em; color: #555;">
                        <div style="background-color: aqua; padding: 30px; border-radius: 10px; box-shadow: 0 50px 100px rgba(3, 0, 0, 0.5);">
                          <h1 style="text-align: center;">Recommendation for a Volunteer Mission</h1>
                          <p>Hello,</p>
                          <p>We would like to recommend a volunteer mission for you:</p>
                          <div style="background-color: #f1f1f1; padding: 20px; border-radius: 5px; margin-top: 20px;box-shadow: 2px 5px 10px yellow;">
                            <h2 style="margin-top: 0; font-size: 24px;">${missionTitle}</h2>
                            <p style="margin-top: 20px;"><strong>Theme:</strong> ${missionTheme}</p>
                            <p style="margin-top: 20px;"><strong>Skills:</strong> ${missionSkills}</p>
                            <p style="margin-top: 20px;">Please click the button below to view the mission:</p>
                            <p style="text-align: center; margin-top: 30px;">
                              <a href="${missionLink}" style="display: inline-block; padding: 10px 20px; background-color: #0053b3; color: #fff; text-decoration: none; border-radius: 5px;">View Mission</a>
                            </p>
                          </div>
                          <p style="margin-top: 30px;">Thank you,</p>
                          <p>The Volunteer Mission Team</p>
                        </div>
                      </div>
                    </body>

                    </html>



                `;
        //body += "https://localhost:44383/?returnUrl=" + currentUrl;


        $.ajax({
            type: 'POST',
            url: '/Home/SendEmail',
            data: {
                "email": result,
                "body": body,
                "subject": subject
            },
            success: function () {
                console.log("Mail is sent...");
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });
});

function gridListRecommend() {
    console.log("lkjhikgyuftyubvhj");
    $.ajax({
        type: 'GET',
        url: '/MissionListing/GetListOfUserRecommendation',
        success: function (data) {
            let newHtml = `<ul class="list-group" id="list-group-in-reccoworker" data-missiontitle="@ViewBag.missionTitle" data-missiontheme="@ViewBag.missionTheme" data-missionskills="@ViewBag.missionSkill">`;

            $.each(data,function (index,item) {
                newHtml += `
                                <li class="list-group-item d-flex align-items-center co-worker-checkbox">
                                <input class="form-check-input me-1" type="checkbox" value="${item.email}" id="checkbox_${item.email}">
                                <label for="checkbox_${item.email}" class="user-image d-flex ms-1">
                                <img src="${item.avatar}" alt="" class="rounded-circle" style="height:40px">
                                <div class="d-flex flex-column ms-2">
                                    <p class="mb-0 ">${item.firstName} ${item.lastName}</p>
                                    <p class="my-0" style="font-size:13px ;">${item.email}</p>
                                </div>
                                </label>
                            </li>`
            });
            newHtml += `</ul>`;

            $('.grid_list_modal_body').html(newHtml);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function addRating(toTill) {
    let imgsToAdd = "";
    for (let i = 1; i <= toTill; i++) {
        imgsToAdd += `<i class="bi bi-star-fill text-warning fs-2" id="star${i}" data-star=${i}></i>`;
    }
    for (let j = toTill; j < 5; j++) {
        imgsToAdd += `<i class="bi bi-star-fill text-muted fs-2" id="star${parseInt(j)+1}" data-star=${parseInt(j)+1}></i>`;
    }
    $(".rating-star-images").html(imgsToAdd);
}

function getTotalCount() {
    $.ajax({
        type: 'GET',
        url: '/MissionListing/getMissionCount',
        success: function (result) {
            $('#totalMissionFilterSpan').html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}



$(document).on('change', function () {
    getTotalCount();
    $('.city-checkbox').on('change', function () {
        getBadge();
    });
    $('.theme-checkbox').on('change', function () {
        getBadge();
    });
    $('.skill-checkbox').on('change', function () {
        getBadge();
    });

    $(".rating-star-images i").on("click", function () {
        addRating($(this).attr('data-star'));
    });


    $.ajax({
        type: 'GET',
        url: '/MissionListing/getMissionCount',
        success: function (result) {
            $('#totalMissionFilterSpan').html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
});

$('#sortingMission').change(function () {
    selectedOption = $(this).val();
    console.log("The value of sorting is : " + selectedOption);// Get the selected option value
    getFilter(); // Call the sortMissions function with the selected option
});

getFilter();
function searchMissions() {
    searchKeyword = document.getElementById("search-query").value;
    console.log(searchKeyword)
    getFilter();
}


function getBadge() {

    $('.city-checkbox:checked').each(function () {
        var index = selectedThemes.indexOf($(this).next().text().trim());
        if (index == -1) {
            selectedCities.push($(this).next().text());
        }
    });
   
    $('.skill-checkbox:checked').each(function () {
        var index = selectedSkills.indexOf($(this).next().text().trim());
        if (index == -1) {
            selectedSkills.push($(this).next().text().trim());
        }
    });

    $('.filter-badge').empty(); // Clear the badge container

    $('.country-checkbox:checked').each(function () { // Add badges for all selected countries
        var countryId = $(this).val();
        var countryName = $(this).next().text();

        if (selectedCountries.includes(countryId)) {
            var badge = $('<div>', {
                'class': 'd-flex bg-light rounded-pill p-2 m-2',
                'data-country': countryId,
                'html': $('<span>', { 'text': countryName }),
            }).append($('<button>', {
                'type': 'button',
                'class': 'btn-close btn-sm',
                'aria-label': 'Close',
                'on': {
                    'click': function () {
                        var countryCheckbox = $('.country-checkbox[value="' + countryId + '"]');
                        countryCheckbox.prop('checked', false);
                        countryCheckbox.trigger('change');
                        $(this).parent().remove();
                    },
                },
            }));
            $('.filter-badge').append(badge);
            getFilter();
        }
    });

    $('.city-checkbox:checked').each(function () {// Add badges for all selected cities
        var cityId = $(this).val();
        var cityName = $(this).next().text();
        var countryId = $(this).data('country');

        if (selectedCities.includes(cityName)) {
            var badge = $('<div>', {
                'class': 'd-flex bg-light rounded-pill p-2 m-2',
                'data-country': countryId,
                'data-city': cityId,
                'html': $('<span>', { 'text': cityName }),
            }).append($('<button>', {
                'type': 'button',
                'class': 'btn-close btn-sm',
                'aria-label': 'Close',
                'on': {
                    'click': function () {
                        var cityCheckbox = $('.city-checkbox[value="' + cityId + '"]');
                        cityCheckbox.prop('checked', false);
                        cityCheckbox.trigger('change');
                        $(this).parent().remove();
                    },
                },
            }));
            console.log("You have reached to city badge append");
            $('.filter-badge').append(badge);
            getFilter();
        }
    });

    $('.theme-checkbox:checked').each(function () {
        var title = $(this).val();
        if (selectedThemes.includes(title.toString())) {
            var badge = $('<div>', {
                'class': 'd-flex bg-light rounded-pill p-2 m-2',
                'html': $('<span>', { 'text': title }),
            }).append($('<button>', {
                'type': 'button',
                'class': 'btn-close btn-sm',
                'aria-label': 'Close',
                'on': {
                    'click': function () {
                        var themeCheckbox = $('.theme-checkbox[value="' + title + '"]');
                        themeCheckbox.prop('checked', false);
                        themeCheckbox.trigger('change');
                        $(this).parent().remove();
                    },
                },
            }));
            $('.filter-badge').append(badge);
            getFilter();
        }
    });


    $('.skill-checkbox:checked').each(function () {
        var skillName = $(this).val();
        if (selectedSkills.includes(skillName.toString())) {
            var badge = $('<div>', {
                'class': 'd-flex bg-light rounded-pill p-2 m-2',
                'html': $('<span>', { 'text': skillName }),
            }).append($('<button>', {
                'type': 'button',
                'class': 'btn-close btn-sm',
                'aria-label': 'Close',
                'on': {
                    'click': function () {
                        var skillCheckbox = $('.skill-checkbox[value="' + skillName + '"]');
                        skillCheckbox.prop('checked', false);
                        skillCheckbox.trigger('change');
                        $(this).parent().remove();
                    },
                },
            }));
            $('.filter-badge').append(badge);
            getFilter();
        }
    });
}

function getFilter(pg) {
    $.ajax({
        url: "/MissionListing/filterMission",
        type: "POST",
        data: {
            "countryId": selectedCountries,
            "cityName": selectedCities,
            "themeId": selectedThemes,
            "skillId": selectedSkills,
            "searchKeyword":searchKeyword,
            "sortBy": selectedOption,
            "flag": flag,
            "pageNum" : pg
        },
        success: function (result) {
            $("#partialView").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function btnAddRmFav() {

    var mID = $("#button-to-fav").attr('data-mid');
    var favFlag = $("#button-to-fav").attr('data-isfav');
    $.ajax({
        url: "/MissionListing/ToggleFav",
        type: "GET",
        data: {
            mID: mID,
            "favFlag": favFlag
        },
        success: function (result) {
            $("#volMissionRightUpper").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function volunteeringMissionDetails(missionId) {
    window.location.href = '@Url.Action("VolunteeringMissionPage","MissionListing")?missionId=' + missionId;
}


function postTheComment() {
    console.log(new URLSearchParams(window.location.href))
    let comment = $('#comment-area-volmission').val();
    
    console.log(comment);
    var missionId = new URLSearchParams(window.location.href).get("missionId");

    $.ajax({
        url: "/MissionListing/postTheComment",
        type: "POST",
        data: {
            comment: comment
        },
        success: function (result) {
            //console.log(result)
            console.log("The comment has been added");
            var f = document.getElementById("commentMissionDiv");
            f.innerHTML = "";
            f.innerHTML = result;
            console.log("*************************************************\n +  " + result)
            //$("#commentMissionDiv").html(result);
            $('#comment-area-volmission').val() = "";

        },
        error: function (xhr, status, error) {
            console.log("There are some errors....");
        }
    });
}


function applyMission() {
    let missionId = $(".volMission-applybtn").attr('data-missionid');
    console.log(typeof(missionId));
    console.log(missionId);
    $.ajax({
        url: "/MissionListing/applyMission",
        type: "GET",
        data: {
            missionId:missionId
        },
        success: function (result) {
            console.log("Application is submitted successfully...");
            $('#volMissionRightUpper').html(result);
            //let xApplyBtn = `
            //                <button type="submit" class="btn bg-white  rounded-pill px-4 volMission-applybtn" data-missionid = "@Model.myMission.mission.MissionId" onclick="applyMission()" disabled>
            //                    Applied
            //                    &nbsp; &nbsp;<img src="~/assets/right-arrow.png" alt="">
            //                </button>
            //                `
            //$(".apply-missionbtn-div-volmission").html(xApplyBtn);

        },
        error: function (xhr, status, error) {
            console.log("Some error occured while applying in mission...\n" + error);
        }
    });
}
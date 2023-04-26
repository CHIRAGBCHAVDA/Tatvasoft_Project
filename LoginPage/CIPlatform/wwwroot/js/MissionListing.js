


let selectedCountries = [];
let selectedCities = [];
let selectedThemes = [];
let selectedSkills = [];
let selectedOption = 1;
let flag = 1;
let searchKeyword = "";


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
                });
                getBadge();
            },
            error: function () {
                alert("An error occurred while fetching cities.");
            }
        });
    });

    
    //$('.city-checkbox').change(function () {
    //    var cityId = $(this).next().text();
    //    if (!$(this).prop('checked')) {
    //        $('.city-checkbox[value="' + cityId + '"]').prop('checked', false);
    //        var index = selectedCities.indexOf(cityId);
    //        if (index > -1 && !$(this).prop('checked')) {
    //            selectedCities.splice(index);
    //            getFilter();
    //        }
    //    } else {
    //        var index = selectedCities.indexOf(cityId.trim());
    //        if (index == -1) {
    //            selectedCities.push(cityId.trim());
    //        }
    //    }
    //});

    //$('.theme-checkbox').change(function () {
    //    var themeName = $(this).next().text().trim();
    //    if (!$(this).prop('checked')) {
    //        $('.theme-checkbox[value="' + themeName.trim() + '"]').prop('checked', false);
    //        var index = selectedThemes.indexOf(themeName);
    //        if (index > -1 && !$(this).prop('checked')) {
    //            selectedThemes.splice(index);
    //            getFilter();
    //        }
    //    } else {
    //        var index = selectedThemes.indexOf(themeName);
    //        if (index == -1) { 
    //            selectedThemes.push(themeName.trim());
    //        }
    //    }
    //});

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
                toastr.success("Recommendation has been sent..!!");
                return;
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
        complete: function (data) {
            console.log(data);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function addRating(toTill) {
    let imgsToAdd = "";
    for (let i = 1; i <= toTill; i++) {
        imgsToAdd += `<i class="bi bi-star-fill text-warning fs-2" id="star${i}" data-star=${i} onclick="addRating(${i})" ></i>`;
    }
    for (let j = toTill; j < 5; j++) {
        imgsToAdd += `<i class="bi bi-star-fill text-muted fs-2" id="star${parseInt(j) + 1}" data-star=${parseInt(j) + 1} onclick="addRating(${j})" ></i>`;
    }
    $(".rating-star-images").html(imgsToAdd);

    $.ajax({
        type: "post",
        url: "/MissionListing/addRating",
        data: {
            rate: toTill
        },
        success: function () {
            console.log("Successfully rated the given mission");
        },
        error: function (xhr, status, error) {
            console.log("Some error occured while doing rating" + error);
        }
    });
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
        debugger
        getBadge();
    });
    $('.skill-checkbox').on('change', function () {
        getBadge();
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

getFilter(1);


function searchMissions() {
    searchKeyword = document.getElementById("search-query").value;
    console.log(searchKeyword)
    getFilter();
}


function getBadge() {

    $('.city-checkbox:checked').each(function () {
        var index = selectedCities.indexOf($(this).next().text().trim());
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
        }
    });

    $('.theme-checkbox:checked').each(function () {
        debugger;
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
        }
    });
    getFilter();

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

//function btnAddRmFav() {

//    var mID = $("#button-to-fav").attr('data-mid');
//    var favFlag = $("#button-to-fav").attr('data-isfav');
//    $.ajax({
//        url: "/MissionListing/ToggleFav",
//        type: "POST",
//        data: {
//            mID: mID,
//            favFlag: favFlag
//        },
//        success: function (result) {
//            console.log(result);
//            $("#volMissionRightUpper").html(result);
//        },
//        error: function (xhr, status, error) {
//            console.log(error);
//            console.log("SOME Error in Fav");
//        }
//    });
//}

//function btnAddRmFav() {
//    debugger
//    var mID = $("#button-to-fav").attr('data-mid');
//    $.ajax({
//        url: "/MissionListing/addRmFav",
//        type: "POST",
//        data: {
//            mID: mID
//        },
//        success: function (result) {
//            if (result == 1) {
//                console.log("ADD BUTTON IS ADDED RETURNED 1");
//                console.log($("#button-to-fav"));
//                //add btn
//                $("#button-to-fav").replaceWith(
//                    `<button class="btn-primary form-control bg-white text-secondary border border-secondary p-2 rounded-pill d-flex flex-wrap flex-md-nowrap justify-content-center align-items-center" id="button-to-fav" data-isfav="@favFlag" data-mid='@Model.myMission.mission.MissionId' onclick="btnAddRmFav()">
//                        <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" stroke="white" class="bi bi-heart" viewBox="0 0 16 16" style="height:20px;width:20px">
//                            <path fill-rule="evenodd" d="M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314z" />
//                        </svg>

//                        <span>
//                            &nbsp; &nbsp; Add To Favourite
//                        </span>
//                    </button>`
//                );
//            }
//            else if (result == 0) {
//                console.log("ADD BUTTON IS REMOVED RETURNED 0")
//                console.log($("#button-to-fav"));

//                //ren btn
//                $("#button-to-fav").replaceWith(
//                    `
//                    <button class="btn-primary form-control bg-white text-secondary border border-secondary p-2 rounded-pill d-flex flex-wrap flex-md-nowrap justify-content-center align-items-center" id="button-to-fav" data-isfav="@favFlag" data-mid='@Model.myMission.mission.MissionId' onclick="btnAddRmFav()">
//                        <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-heart-fill" viewBox="0 0 16 16" style="color: red;height:20px;width:20px">
//                            <path fill-rule="evenodd" d="M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314z" />
//                        </svg>

//                        <span>
//                            &nbsp; &nbsp; Remove From Favourite
//                        </span>
//                    </button>`
//                );

//            }
//            else {
//                console.log("SOMETHING ERROR OCCURED WHILE PERFORMING ADD/RM FAV");
//            }
//        },
//        error: function (xhr, status, error) {
//            console.log(error);
//            console.log("SOME Error in Fav");
//        }
//    });
//}



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
            $('#comment-area-volmission').val("");

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
        },
        error: function (xhr, status, error) {
            console.log("Some error occured while applying in mission...\n" + error);
        }
    });
}


$(document).on('click', '#add-rm-fav-div', function () {
    var mID = $("#button-to-fav").attr('data-mid');
    $.ajax({
        url: "/MissionListing/addRmFav",
        type: "POST",
        data: {
            mID: mID
        },
        success: function (result) {
            debugger
            //$("#button-to-fav").html("");
            if (result == 0) {
                console.log("ADD BUTTON IS ADDED RETURNED 1");
                console.log($("#button-to-fav"));
                
                $("#button-to-fav").html(`
                  <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" stroke="white" class="bi bi-heart" viewBox="0 0 16 16" style="height:20px;width:20px">
                        <path fill-rule="evenodd" d="M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314z" />
                  </svg>
                  <span>
                        &nbsp; &nbsp; Add To Favourite
                  </span>
                `);






            }
            else if (result == 1) {
                console.log("ADD BUTTON IS REMOVED RETURNED 0")
                console.log($("#button-to-fav"));

               

                $("#button-to-fav").html(`
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-heart-fill" viewBox="0 0 16 16" style="color: red;height:20px;width:20px">
                            <path fill-rule="evenodd" d="M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314z" />
                        </svg>

                        <span>
                            &nbsp; &nbsp; Remove From Favourite
                        </span>
                `);

            }
            else {
                console.log("SOMETHING ERROR OCCURED WHILE PERFORMING ADD/RM FAV");
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
            console.log("SOME Error in Fav");
        }
    });
});


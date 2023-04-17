
var selectedCountries = [];
var selectedCities = [];
var selectedThemes = [];
var selectedSkills = [];
var selectedOption = 1;
var flag = 1;
var searchKeyword = "";
var sources = [];


//const cloudinary = require('cloudinary').v2;
//cloudinary.config({
//    cloud_name: 'dtgjan6zj',
//    api_key: '778668586946964',
//    api_secret: 'DVRogEOZ8x8Vdq2xsIi-o2PucH4'
//});



getTotalCount();
$(document).ready(function () {
    gridListRecommend()

    $("#storydetailbtn-tomission").on('click', function () {
        let mid = $(this).attr("data-missionid");
        console.log("chiragbhai jovo to missionid shu aayu : " + mid)
        window.location.href = '/MissionListing/VolunteeringMissionPage?missionId=' + mid;
    });

    $("#file-preview").find("img").each(function () {
        sources.push($(this).attr("src"));
    });
    $.ajax({
        type: "post",
        url: "/StoryListing/getAppliedMission",
        success: function (data) {
            console.log(data);
            $.each(data, function (i, mission) {
                $('#storyMissionName').append($('<option>', {
                    value: mission.missionId,
                    text: mission.title
                }));
                console.log("mid:", mission.missionId);
                console.log("title:",mission.title);
                if (viewdataMissionId == mission.missionId) {
                    console.log(viewdataMissionId);
                    $("#storyMissionName option[value='" + mission.missionId + "']").prop("selected", true);
                }
            });
            
        },
        error: function (xhr, status, error) {
            console.log("Some error occurred while getting mission applied by user ", error);
        }
    });




    $('#storySaveBtn').on('click', function (e) {
        debugger;
        e.preventDefault();
        var storyMissionName = $("#storyMissionName").val();
        var storyTitle = $("#storyTitle").val();
        var storyDate = $("#storyDate").val();
        var story = CKEDITOR.instances['ck-editor'].getData();
        var storyVideoUrl = $("#storyVideoUrl").val();
        var srcs = sources;

        $.ajax({
            type: "POST",
            url: "/StoryListing/draftStory",
            data: {
                storyMissionName: storyMissionName,
                storyTitle: storyTitle,
                storyDate: storyDate,
                story: story,
                storyVideoUrl: storyVideoUrl,
                srcs : srcs
            },
            success: function (response) {
                console.log(response);



                //$("body").html(response); // We do not need to append the data evrytime just change the window location
                //window.location.href = "/StoryListing/StoryListing";
            },
            error: function (error) {
                console.log(error); // print error in console
            }
        });

    });




    $('#storyForm').submit(function (e) {
        e.preventDefault();
        var ifSaved = confirm("Last saved story will be submitted....Make sure you have saved the latest one!!");

        

        //var storyMissionName = $("#storyMissionName").val();
        //var storyTitle = $("#storyTitle").val();
        //var storyDate = $("#storyDate").val();
        //var story = CKEDITOR.instances['ck-editor'].getData();
        //var storyVideoUrl = $("#storyVideoUrl").val();
        //var srcs = sources;
        if (ifSaved) {
            $.ajax({
                type: "POST",
                url: "/StoryListing/newStory",
                //data: {
                //    storyMissionName: storyMissionName,
                //    storyTitle: storyTitle,
                //    storyDate: storyDate,
                //    story: story,
                //    storyVideoUrl: storyVideoUrl,
                //    srcs: srcs
                //},

                success: function (response) {
                    window.location.href = "/StoryListing/StoryListing";
                },
                error: function (error) {
                    console.log(error); // print error in console
                }
            });

        } else {
            return;
        }
        
    });







    

    $('#file-upload').on('change', async function () {
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
                $('#file-preview').append('<div class="col-auto"><div class="position-relative"><img class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                sources.push(src);
            } else if (file.type.includes('video')) {
                $('#file-preview').append('<div class="col-auto"><div class="position-relative"><video class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"></video><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                sources.push(src);
            } else {
                alert("Please upload a supported file type (image or video).");
            }
        }

        //console.log("Sources array: ", sources);
    });



    $('#file-preview').on('click', '.btn-close', function () {
        var imageSrc = $(this).siblings('img').attr('src'); // get the src of the image
        var index = sources.indexOf(imageSrc); // get the index of the image src in the sources array
        if (index !== -1) {
            sources.splice(index, 1); // remove the image src from the sources array
        }
        $(this).closest('.col-auto').remove();
        console.log("Sources array: ", sources);

    });


    $('#file-label').on('drop', async function (e) {
        e.preventDefault();
        var files = e.originalEvent.dataTransfer.files;
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
                $('#file-preview').append('<div class="col-auto"><div class="position-relative"><img class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                sources.push(src);
            } else if (file.type.includes('video')) {
                $('#file-preview').append('<div class="col-auto"><div class="position-relative"><video class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"></video><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                sources.push(src);
            } else {
                alert("Please upload a supported file type (image or video).");
            }
        }

        console.log("Sources array: ", sources);
    });

    $('#file-label').on('dragover', function (e) {
        e.preventDefault();
    });


    $(".storyDeleteIcon").on('click', function () {
        var storyId = $(this).attr("data-storyid");
        $.ajax({
            type: 'POST',
            url: '/StoryListing/DeleteStory',
            data: {
                storyId:storyId
            },
            success: function () {
                console.log("Your story has been deleted successfully");
            },
            error: function (xhr, status, error) {
                console.log("this error occured while doing deletion of story", error);
            }
        });
    });



    //$('#file-label').on('drop', function (e) {
    //    e.preventDefault();
    //    var files = e.originalEvent.dataTransfer.files;
    //    for (var i = 0; i < files.length; i++) {
    //        var file = files[i];
    //        var reader = new FileReader();
    //        reader.onload = function (e) {
    //            var imageType = /image/;
    //            var videoType = /video/;
    //            var src = e.target.result;
    //            if (imageType.test(file.type)) {
    //                $('#file-preview').append('<div class="col-auto"><div class="position-relative"><img class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
    //            } else if (videoType.test(file.type)) {
    //                $('#file-preview').append('<div class="col-auto"><div class="position-relative"><video class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"></video><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
    //            }

    //            sources.push(src); //push into the array
    //        }
    //        reader.readAsDataURL(file);
    //    }
    //    console.log("GOING TO PRINT THE SRC FILEEEEEEEE");
    //    sources.forEach(function (source) {
    //        console.log(source);
    //    });
    //});

    //$('#file-label').on('dragover', function (e) {
    //    e.preventDefault();
    //});


    


    $('.city-item').hide();

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
    
    $('.story-cards').on('click', '.page-item', function () {
        $('.pagination .page-item').removeClass('active');
        $(this).addClass('active');
        getFilter($(this).attr('id'));
    });

    $(".rating-star-images").on("click", "i", function () {
        addRating($(this).attr('data-star'));
    });


    $(document).on("click", "#partialView .reccommendMissionBtn, .reccommendMissionBtn", function () {
        var selected = $('.co-worker-checkbox input[type="checkbox"]:checked');
        var values = [];
        selected.each(function () {
            values.push($(this).val().trim());
        });
        var result = values.join(","); // combine the values with a comma and space
        result = result.replace(/,\s*$/, "");

        console.log(result);
   
        let username = $('#name-input').val();
        let storyTitle = $('#storytitle-input').val();


        console.log("Story Tt",storyTitle)
        console.log("User name",username)
        let queryStr = window.location.search;
        let urlParams = new URLSearchParams(queryStr);
        let storyId = urlParams.get("storyId")
        let missionId = urlParams.get("missionId")
        let userId = urlParams.get("userId")


        let subject = " Reccommendation to join " + storyTitle;
        //console.log("The type of  missions skills is " + typeof (missionSkills));
        console.log(currentUrl);
        var currentUrl = window.location.href;
        //let storyLink = `https://localhost:44383/?returnUrl=${currentUrl}&missionId=${missionId}&storyId=${storyId}&userId=${userId}`;

        let encodedUrl = encodeURIComponent(currentUrl);
        let storyLink = `https://localhost:44383/?returnUrl=${encodedUrl}&missionId=${missionId}&storyId=${storyId}&userId=${userId}`;

        console.log(storyLink);
        let body = `<!DOCTYPE html>
                    <html>

                    <head>
                      <meta charset="utf-8">
                      <meta name="viewport" content="width=device-width, initial-scale=1.0">
                      <title>Recommendation Email</title>
                    </head>

                    <body>
                      <div style="margin: 0 auto; max-width: 900px; font-family: Arial, sans-serif; font-size: 16px; line-height: 1.5em; color: #555;">
                        <div style="background-color: #c780e1; padding: 30px; border-radius: 10px; box-shadow: 0 50px 100px rgba(3, 0, 0, 0.5);">
                          <h1 style="text-align: center;">Recommendation for a Volunteer Mission</h1>
                          <p>Hello,</p>
                          <p>We would like to recommend a volunteer mission for you:</p>
                          <div style="background-color: #f1f1f1; padding: 20px; border-radius: 5px; margin-top: 20px;box-shadow: 2px 5px 10px yellow;">
                            <h2 style="margin-top: 0; font-size: 24px;">${storyTitle}</h2>
                            <p style="margin-top: 20px;"><strong>Written By:</strong> ${username}</p>
                            <p style="margin-top: 20px;">Please click the button below to view the mission:</p>
                            <p style="text-align: center; margin-top: 30px;">
                              <a href="${storyLink}" style="display: inline-block; padding: 10px 20px; background-color: #0053b3; color: #fff; text-decoration: none; border-radius: 5px;">View Story</a>
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

    $('#storyMissionName').on('change', function () {
        var selectedMissionId = $(this).val();
        // Make an AJAX request to fetch more data based on the selected mission ID
        $.ajax({
            type: "post",
            url: "/StoryListing/ShareStory",
            data: { missionId: selectedMissionId },
            success: function (result) {
                console.log("MALI DRAFT STORY MALI?!");
                $("body").html(result);
                //$("#storyMissionName").val() = selectedMissionId;
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });






    
});


function getTotalCount() {
    $.ajax({
        type: 'GET',
        url: '/StoryListing/getStoryCount',
        success: function (result) {
            $('#totalStoryFilterSpan').html(result);
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

  
    
});


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
        url: "/StoryListing/filterStory",
        type: "POST",
        data: {
            "countryId": selectedCountries,
            "cityName": selectedCities,
            "themeId": selectedThemes,
            "skillId": selectedSkills,
            "searchKeyword": searchKeyword,
            "sortBy": selectedOption,
            "flag": flag,
            "pageNum": pg
        },
        success: function (result) {
            $(".story-cards").html(result);
            $(`.page-item#${pg}`).addClass("active");
            
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function getEditorHTML() {
    debugger
    // Get the CKEditor instance
    const editor = CKEDITOR.instances['ck-editor'];

    // Get the HTML data from the editor
    const htmlData = editor.getData();

    // Do something with the HTML data
    console.log(htmlData);
}



function tempcc(storyId) {
    $.ajax({
        type: 'POST',
        url: '/StoryListing/DeleteStory',
        data: {
            storyId: storyId
        },
        complete: function (result) {
            console.log("Your story has been deleted successfully");
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log("this error occured while doing deletion of story", error);
        }
    });
}


function viewStoryDetails(btn) {
    var mid = $(btn).attr("data-mid");
    var sid = $(btn).attr("data-sid");
    var uid = $(btn).attr("data-uid");

    var url = '/StoryListing/StoryDetail?missionId=' + mid + '&storyId=' + sid + '&userId=' + uid;
    window.location.href = url;

}





function gridListRecommend() {
    console.log("lkjhikgyuftyubvhj");
    $.ajax({
        type: 'GET',
        url: '/MissionListing/GetListOfUserRecommendation',
        success: function (data) {
            let newHtml = `<ul class="list-group" id="list-group-in-reccoworker" data-name="@Model.Name" data-storytitle="@Model.ShareStory.StoryTitle" data-missionid="@Model.ShareStory.MissionId">`;

            $.each(data, function (index, item) {
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

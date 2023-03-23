var selectedCountries = [];
var selectedCities = [];
var selectedThemes = [];
var selectedSkills = [];
var selectedOption = 1;
var flag = 1;
var searchKeyword = "";

//const cloudinary = require('cloudinary').v2;
//cloudinary.config({
//    cloud_name: 'dtgjan6zj',
//    api_key: '778668586946964',
//    api_secret: 'DVRogEOZ8x8Vdq2xsIi-o2PucH4'
//});



getTotalCount();
$(document).ready(function () {



    //$('#file-upload').on('change', function () {
    //    var files = $(this)[0].files;
    //    for (var i = 0; i < files.length; i++) {
    //        var file = files[i];
    //        var formData = new FormData();
    //        formData.append('file', file);
    //        formData.append('upload_preset', 'ci_preset');
    //        cloudinary.uploader.upload(formData, function (error, result) {
    //            if (error) {
    //                console.log('Cloudinary upload error: ', error);
    //            } else {
    //                var imageType = /image/;
    //                var videoType = /video/;
    //                var src = result.secure_url;
    //                if (imageType.test(file.type)) {
    //                    $('#file-preview').append('<div class="col-auto"><div class="position-relative"><img class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
    //                } else if (videoType.test(file.type)) {
    //                    $('#file-preview').append('<div class="col-auto"><div class="position-relative"><video class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"></video><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
    //                }
    //            }
    //        });
    //    }
    //});

    //$('#file-preview').on('click', '.btn-close', function () {
    //    $(this).closest('.col-auto').remove();
    //});

    //$('#file-label').on('drop', function (e) {
    //    e.preventDefault();
    //    var files = e.originalEvent.dataTransfer.files;
    //    for (var i = 0; i < files.length; i++) {
    //        var file = files[i];
    //        var formData = new FormData();
    //        formData.append('file', file);
    //        formData.append('upload_preset', 'ci_preset');
    //        cloudinary.uploader.upload(formData, function (error, result) {
    //            if (error) {
    //                console.log('Cloudinary upload error: ', error);
    //            } else {
    //                var imageType = /image/;
    //                var videoType = /video/;
    //                var src = result.secure_url;
    //                if (imageType.test(file.type)) {
    //                    $('#file-preview').append('<div class="col-auto"><div class="position-relative"><img class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
    //                } else if (videoType.test(file.type)) {
    //                    $('#file-preview').append('<div class="col-auto"><div class="position-relative"><video class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"></video><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
    //                }
    //            }
    //        });
    //    }
    //});





    $('#file-upload').on('change', function () {
        debugger
        var files = $(this)[0].files;
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var reader = new FileReader();
            reader.onload = function (e) {
                var imageType = /image/;
                var videoType = /video/;
                var src = e.target.result;
                if (imageType.test(file.type)) {
                    $('#file-preview').append('<div class="col-auto"><div class="position-relative"><img class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                } else if (videoType.test(file.type)) {
                    $('#file-preview').append('<div class="col-auto"><div class="position-relative"><video class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"></video><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                }
            }
            reader.readAsDataURL(file);
        }
    });

    $('#file-preview').on('click', '.btn-close', function () {
        $(this).closest('.col-auto').remove();
    });

    $('#file-label').on('drop', function (e) {
        e.preventDefault();
        var files = e.originalEvent.dataTransfer.files;
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var reader = new FileReader();
            reader.onload = function (e) {
                var imageType = /image/;
                var videoType = /video/;
                var src = e.target.result;
                if (imageType.test(file.type)) {
                    $('#file-preview').append('<div class="col-auto"><div class="position-relative"><img class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                } else if (videoType.test(file.type)) {
                    $('#file-preview').append('<div class="col-auto"><div class="position-relative"><video class="img-thumbnail" style="width: 150px; height: 150px;" src="' + src + '"></video><button type="button" class="btn-close bg-dark position-absolute top-0 end-0" aria-label="Close"></button></div></div>');
                }
            }
            reader.readAsDataURL(file);
        }
    });

    $('#file-label').on('dragover', function (e) {
        e.preventDefault();
    });




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
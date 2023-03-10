

var selectedCountries = [];
var selectedCities = [];
var selectedThemes = [];
var selectedSkills = [];
var selectedOption = 1;

$(document).ready(function () {
    $('.city-item').hide();
    $("#partialView").load('/MissionListing/GetGridView');
    $('#list').click(function () {
        console.log("hi");
        $("#partialView").load('/MissionListing/GetListView');
    });
    $('#grid').click(function () {
        $("#partialView").load('/MissionListing/GetGridView');
    });

    const btn1 = document.getElementById("grid");
    const btn2 = document.getElementById("list");
    btn1.addEventListener("click", function () {
        btn2.classList.toggle("bg-white");
        btn1.classList.remove("bg-white");
        console.log("Button 1 clicked")
    });
    btn2.addEventListener("click", function () {
        btn1.classList.toggle("bg-white");
        btn2.classList.remove("bg-white");
        console.log("Button 2 clicked");
    });

 
    $('.country-checkbox').change(function () {
        var countryId = $(this).val();
        if (!$(this).prop('checked')) {
            debugger;
            $('.city-checkbox[data-country="' + countryId + '"]').prop('checked', false);
            var index = selectedCountries.indexOf(countryId);
            if (index > -1 && !$(this).prop('checked')) {
                selectedCountries.splice(index);
                getFilter();
            }
        } else {
            selectedCountries.push($(this).val());
        }

        //$('.country-checkbox:checked').each(function () {
        //});

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
            debugger
            $('.city-checkbox[value="' + cityId + '"]').prop('checked', false);
            var index = selectedCities.indexOf(cityId);
            if (index > -1 && !$(this).prop('checked')) {
                selectedCities.splice(index);
                getFilter();
            }
        }
    });
    //$('.theme-checkbox:checked').each(function () {
    //    selectedThemes.push($(this).val());
    //    console.log("The value of selected theme is : " + selectedThemes);
    //});

    $('.theme-checkbox').change(function () {
        var themeName = $(this).next().text().trim();
        debugger
        if (!$(this).prop('checked')) {
            $('.theme-checkbox[value="' + themeName.trim() + '"]').prop('checked', false);
            var index = selectedThemes.indexOf(themeName);
            if (index > -1 && !$(this).prop('checked')) {
                selectedThemes.splice(index);
                getFilter();
            }
        } else {
            selectedThemes.push(themeName.trim());
        }
    });

    //$('.city-checkbox').change(function () {
    //    getBadge();
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

    $('#sortingMission').on('change', function () {
        debugger;
        selectedOption = $(this).val();
        console.log("The value of sorting is : " + selectedOption);// Get the selected option value
        getFilter(); // Call the sortMissions function with the selected option
    });


});


$(document).on('change', function () {
    $('.city-checkbox').on('change',function () {
        getBadge();
    });
    $('.theme-checkbox').on('change', function () {
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

    $('#sortingMission').on('change', function () {
        debugger;
        selectedOption = $(this).val();
        console.log("The value of sorting is : " + selectedOption);// Get the selected option value
        getFilter(); // Call the sortMissions function with the selected option
    });
})
$('#sortingMission').change(function () {
    debugger;
    selectedOption = $(this).val();
    console.log("The value of sorting is : " + selectedOption);// Get the selected option value
    getFilter(); // Call the sortMissions function with the selected option
});


function searchMissions() {
    let query = document.getElementById("search-query").value;
    $.ajax({
        type: "GET",
        url: "/MissionListing/Search",
        data: { query: query },
        success: function (result) {
            console.log(result);
            $("#partialView").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function getBadge() {


    $('.city-checkbox:checked').each(function () {
        console.log("hello data after push thisis selected cities" + selectedCities);
        selectedCities.push($(this).next().text());
    });
   
    $('.skill-checkbox:checked').each(function () {
        console.log("hello data after push" + selectedSkills);
        selectedSkills.push($(this).next().text().trim());
    });



    // Clear the badge container
    $('.filter-badge').empty();

    // Add badges for all selected countries
    $('.country-checkbox:checked').each(function () {
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
            console.log("hello above ajax");
            $('.filter-badge').append(badge);

            getFilter();

        }
    });

    // Add badges for all selected cities
    $('.city-checkbox:checked').each(function () {
        debugger
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
        debugger

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

function getFilter() {

    //debugger;
    $.ajax({
        url: "/MissionListing/filterMission",
        type: "POST",
        data: {
            "countryId": selectedCountries,
            "cityName": selectedCities,
            "themeId": selectedThemes,
            "skillId": selectedSkills,
            "sortBy" : selectedOption,
        },
        success: function (result) {
            console.log(result);
            $("#partialView").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}





//function getBadge() {
//    $('.filter-badge').empty();

//    $('.country-checkbox:checked').each(function () {
//        var countryName = $(this).next().text();
//        console.log(countryName);
//        var badge = '<div class="d-flex bg-light rounded-pill p-2 m-2" data-country="@item.CountryId">';
//        badge += '<span>' + countryName + '</span>';
//        badge += '<button type="button" class="btn-close btn-sm" data-bs-dismiss="alert" aria-label="Close"></button>';
//        badge += '</div>';
//        $('.filter-badge').append(badge);
//    });

//    $('.city-checkbox:checked').each(function () {
//        var cityName = $(this).next().text();
//        console.log(cityName);
//        var badge = '<div class="d-flex bg-light rounded-pill p-2 m-2">';
//        badge += '<span>' + cityName + '</span>';
//        badge += '<button type="button" class="btn-close btn-sm" data-bs-dismiss="alert" aria-label="Close"></button>';
//        badge += '</div>';
//        $('.filter-badge').append(badge);
//    });
//}
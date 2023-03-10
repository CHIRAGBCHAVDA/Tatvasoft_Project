

var selectedCountries = [];
var selectedCities = [];
var selectedThemes = [];
var selectedSkills = [];
var selectedOption = 1;
var flag = 1;



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
            debugger;
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
            debugger
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
        debugger
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

});



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
    debugger;
    selectedOption = $(this).val();
    console.log("The value of sorting is : " + selectedOption);// Get the selected option value
    getFilter(); // Call the sortMissions function with the selected option
});

getFilter();
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
    $.ajax({
        url: "/MissionListing/filterMission",
        type: "POST",
        data: {
            "countryId": selectedCountries,
            "cityName": selectedCities,
            "themeId": selectedThemes,
            "skillId": selectedSkills,
            "sortBy": selectedOption,
            "flag" : flag
        },
        success: function (result) {
            $("#partialView").html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

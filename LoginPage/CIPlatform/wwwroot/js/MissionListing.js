$(document).ready(function () {
    $('.city-item').hide();
    $("#partialView").load('/MissionListing/GetGridView');
    $('#list').click(function () {
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
        console.log("Button 2 clicked")
    });

    //$('.country-checkbox').on('change', function () {
    //    debugger;

    //});


    $('.country-checkbox').change(function () {
        
        var selectedCountries = [];
        var countryId = $(this).val();
        if (!$(this).prop('checked')) {
            debugger;
            $('.city-checkbox[data-country="' + countryId + '"]').prop('checked', false);
        }


        //if (!$(this).prop('checked')) {
        //    debugger;
        //    $('.city-checkbox[data-country="' + countryId + '"]').prop('checked', false).removeAttr('checked');
        //    let x = [...document.getElementsByClassName("city-item")]
        //    x.forEach(function (city, ind) {
        //        if (city.countryId == countryId)
        //            city.firstElementChild.checked = false;
        //    })
        //}

        $('.country-checkbox:checked').each(function () {
            selectedCountries.push($(this).val());

        });
        $.ajax({
            type: "POST",
            url: "/MissionListing/GetCities",
            data: { countryIds: selectedCountries },
            success: function (data) {
                $('.city-item').hide();

                $.each(data, function (index, value) {
                    var cityItem = $('.city-item[data-country="' + value.countryId + '"]');
                    cityItem.show();
                    cityItem.find('.city-checkbox[value="' + value.cityId + '"]').show();
                    getBadge();

                });
            },
            error: function () {
                alert("An error occurred while fetching cities.");
            }
        });
    });

    $("#search-query").keyup(function () {
        searchMissions();
    });

    //$('.country-checkbox, .city-checkbox').change(function () {
    //});
});

$(document).on('change', function () {
    $('.city-checkbox').on('change', function () {
        getBadge();
    });
})

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


//function getBadge() {
//    $('.filter-badge').empty();

//    // Loop through all checked country checkboxes
//    $('.country-checkbox:checked').each(function () {
//        var countryId = $(this).val();
//        var countryName = $(this).next().text();

//        // Create a badge for the country
//        var countryBadge = '<div class="d-flex bg-light rounded-pill p-2 m-2" data-country="' + countryId + '">';
//        countryBadge += '<span>' + countryName + '</span>';
//        countryBadge += '<button type="button" class="btn-close btn-sm" data-bs-dismiss="alert" aria-label="Close"></button>';
//        countryBadge += '</div>';
//        $('.filter-badge').append(countryBadge);

//        // Loop through all checked city checkboxes associated with this country
//        $('.city-checkbox[data-country="' + countryId + '"]:checked').each(function () {
//            var cityName = $(this).next().text();

//            // Create a badge for the city
//            var cityBadge = '<div class="d-flex bg-light rounded-pill p-2 m-2">';
//            cityBadge += '<span>' + cityName + '</span>';
//            cityBadge += '<button type="button" class="btn-close btn-sm" data-bs-dismiss="alert" aria-label="Close"></button>';
//            cityBadge += '</div>';
//            $('.filter-badge').append(cityBadge);
//        });
//    });
//}


function getBadge() {
    var selectedCountries = [];
    var selectedCities = [];

    // Get the IDs of all selected countries and cities
    $('.country-checkbox:checked').each(function () {
        selectedCountries.push($(this).val());
    });

    $('.city-checkbox:checked').each(function () {
        selectedCities.push($(this).val());
    });

    // Clear the badge container
    $('.filter-badge').empty();

    // Add badges for all selected countries
    $('.country-checkbox:checked').each(function () {
        var countryId = $(this).val();
        var countryName = $(this).next().text();

        // Only add the badge if it was not already added
        if (selectedCountries.includes(countryId)) {
            var badge = '<div class="d-flex bg-light rounded-pill p-2 m-2" data-country="' + countryId + '">';
            badge += '<span>' + countryName + '</span>';
            badge += '<button type="button" class="btn-close btn-sm" data-bs-dismiss="alert" aria-label="Close"></button>';
            badge += '</div>';
            $('.filter-badge').append(badge);
        }
    });

    // Add badges for all selected cities
    $('.city-checkbox:checked').each(function () {
        var cityId = $(this).val();
        var cityName = $(this).next().text();
        var countryId = $(this).data('country');

        // Only add the badge if it was not already added
        if (selectedCities.includes(cityId)) {
            var badge = '<div class="d-flex bg-light rounded-pill p-2 m-2" data-country="' + countryId + '" data-city="' + cityId + '">';
            badge += '<span>' + cityName + '</span>';
            badge += '<button type="button" class="btn-close btn-sm" data-bs-dismiss="alert" aria-label="Close"></button>';
            badge += '</div>';
            $('.filter-badge').append(badge);
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
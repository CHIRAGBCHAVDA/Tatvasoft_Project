$(document).ready(function () {

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




    $('.country-checkbox').change(function () {
        var selectedCountries = [];

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
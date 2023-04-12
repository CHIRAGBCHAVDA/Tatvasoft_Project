$(document).ready(function () {

    $("#addVolHrsBtn").on("click", function () {
        let userId = $(this).attr('data-uid');
        $.ajax({
            type: "POST",
            url: "/User/getMissionApplicationsByUserId",
            data: {
                userId:userId
            },
            success: function (result) {
                $("#volMission").empty();
                $.each(result, function (index, value) {
                    if (!value.isGoalBased) {
                        $("#volMission").append($('<option>', {
                            'value': value.missionId,
                            'data-mid': value.missionId,
                            'text': value.missionName
                        }));
                    }
                    
                });
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        })

    });

    $("#volHrsSubmitForm").submit(function (e) {
        e.preventDefault()
        let missionId = $("#volMission :selected").attr('data-mid');
        let date = $('#dateVolunteered').val();
        let hrs = $("#volHrs").val();
        let mins = $("#volMins").val();
        let message = $("#volMessage").val();
        let dateObj = new Date();
        dateObj.setHours(hrs);
        dateObj.setMinutes(mins);
        let formattedTime = dateObj.toLocaleTimeString('en-US', { hour12: false });

        $.ajax({
            type: "POST",
            url: "/User/AddHourVolunteer",
            data: {
                MissionId: missionId,
                VolunteeredDate: date,
                Message: message,
                FormattedTime: formattedTime,
            },
            success: function (result) {
                console.log(result);
                $("#mainTimeBasedCancel").click();
                $("#addVolHrsBtn").click();
                $("#volTimesheetTimeBased").html(result);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

    })



});
$(document).ready(function () {

    $(document).on('click', 'a[data-bs-target="#EditvolHrsForm"]', function (e) {
        e.preventDefault();
        console.log("anchor tag event");
        var missionName = $(this).data("mission-name");
        var dateVolunteered = $(this).data("date-volunteered");
        var parts = dateVolunteered.split("-");
        var formattedDate = parts[2].trim() + "-" + parts[1] + "-" + parts[0];
        var hours = $(this).data("hours");
        var mins = $(this).data("mins");
        var message = $(this).data("message");
        var timeSheetId = $(this).data("timesheetid");
        var userId = $(this).data("userid");
        var mid = $(this).data("mid");

        $.ajax({
            type: "POST",
            url: "/User/getMissionApplicationsByUserId",
            data: {
                userId: userId
            },
            success: function (result) {
                $("#EditvolMission").empty();
                $.each(result, function (index, value) {
                    if (!value.isGoalBased) {
                        $("#EditvolMission").append($('<option>', {
                            'value': value.missionId,
                            'data-mid': value.missionId,
                            'text': value.missionName
                        }));
                    }

                });

                $("#EditvolMission option").filter(function () {
                    return $(this).val() == mid;
                }).prop("selected", true);

                $("#EditdateVolunteered").val(formattedDate);
                $("#EditvolHrs").val(hours);
                $("#EditvolMins").val(mins);
                $("#EditvolMessage").val(message);
                $("#EditTimeSheetId").val(timeSheetId);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

       
    })

    $(document).on("click","#addVolHrsBtn", function () {
        console.log("add vol hrs btn hit");
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

    $(document).on("submit", "#volHrsSubmitForm", function (e)  {
        console.log("submit event");
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
                toastr.success("New Timesheet data has been added..!!");
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

    })

    $(document).on("submit", "#EditvolHrsSubmitForm", function (e) {
        e.preventDefault();
        let missionId = $("#EditvolMission :selected").attr('data-mid');
        let date = $('#EditdateVolunteered').val();
        let hrs = $("#EditvolHrs").val();
        let mins = $("#EditvolMins").val();
        let message = $("#EditvolMessage").val();
        let timesheetId = $("#EditTimeSheetId").val();
        let dateObj = new Date();
        dateObj.setHours(hrs);
        dateObj.setMinutes(mins);
        let formattedTime = dateObj.toLocaleTimeString('en-US', { hour12: false });

        $.ajax({
            type: "POST",
            url: "/User/EditHourVolunteer",
            data: {
                TimesheetId: timesheetId,
                MissionId: missionId,
                VolunteeredDate: date,
                Message: message,
                FormattedTime: formattedTime,
            },
            success: function (result) {
                console.log(result);
                $("#EditmainTimeBasedCancel").click();
                $("#volTimesheetTimeBased").html(result);
                toastr.success("Timesheet has been updated..!!");
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });



    })

    $(document).on("click", "#deleteTimebasedTimesheet", function (e) {
        e.preventDefault();
        var timesheetId = $(this).data("timesheetid");
        $.ajax({
            type: "POST",
            url: "/User/DeleteHourVolunteer",
            data: {
                TimesheetId : timesheetId
            },
            success: function (result) {
                console.log(result);
                $("#volTimesheetTimeBased").html(result);
                toastr.success("Timesheet data has been deleted..!!");
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });

    

    /*******************************************       Goal Based area         *******************************************************************/

    $(document).on("click", "#addVolGoalBtn", function () {
        console.log("add vol hrs btn hit");
        let userId = $(this).attr('data-uid');
        $.ajax({
            type: "POST",
            url: "/User/getMissionApplicationsByUserId",
            data: {
                userId: userId
            },
            success: function (result) {
                $("#GoalvolMission").empty();
                $.each(result, function (index, value) {
                    if (value.isGoalBased) {
                        $("#GoalvolMission").append($('<option>', {
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

    $(document).on("submit", "#volGoalSubmitForm", function (e) {
        console.log("submit event");
        e.preventDefault()
        let missionId = $("#GoalvolMission :selected").attr('data-mid');
        let date = $('#GoaldateVolunteered').val();
        let action = $("#GoalvolAction").val();
        let message = $("#GoalvolMessage").val();
       

        $.ajax({
            type: "POST",
            url: "/User/AddGoalVolunteer",
            data: {
                MissionId: missionId,
                VolunteeredDate: date,
                Action : action,
                Message: message
            },
            success: function (result) {
                console.log(result);
                $("#mainGoalBasedCancel").click();
                $("#volTimesheetGoalBased").html(result);
                $("#addVolGoalBtn").click();
                toastr.success("New Goal Based Timesheet data has been added..!!");
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

    })

    $(document).on('click', 'a[data-bs-target="#EditvolGoalForm"]', function (e) {
        e.preventDefault();
        console.log("anchor tag event");
        var missionName = $(this).data("mission-name");
        var dateVolunteered = $(this).data("date-volunteered");
        var parts = dateVolunteered.split("-");
        var formattedDate = parts[2].trim() + "-" + parts[1] + "-" + parts[0];
        var message = $(this).data("message");
        var timeSheetId = $(this).data("timesheetid");
        var userId = $(this).data("userid");
        var mid = $(this).data("mid");
        var action = $(this).data("act");
        console.log(missionName
            , dateVolunteered
            , formattedDate
            , message
            , timeSheetId
            , userId
            , mid
            , action
        );


        $.ajax({
            type: "POST",
            url: "/User/getMissionApplicationsByUserId",
            data: {
                userId: userId
            },
            success: function (result) {
                $("#EditGoalvolMission").empty();
                $.each(result, function (index, value) {
                    if (value.isGoalBased) {
                        $("#EditGoalvolMission").append($('<option>', {
                            'value': value.missionId,
                            'data-mid': value.missionId,
                            'text': value.missionName
                        }));
                    }

                });

                $("#EditGoalvolMission option").filter(function () {
                    return $(this).val() == mid;
                }).prop("selected", true);
                console.log("Idhar pahunche or timesheet id is",timeSheetId)
                $("#EditGoaldateVolunteered").val(formattedDate);
                $("#EditGoalvolAction").val(action);
                $("#EditGoalvolMessage").val(message);
                $("#EditGoalTimesheetId").val(timeSheetId);
                console.log("done ig 00");
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });


    })

    $(document).on("submit", "#EditvolGoalSubmitForm", function (e) {
        e.preventDefault();
        let missionId = $("#EditGoalvolMission :selected").attr('data-mid');
        let date = $('#EditGoaldateVolunteered').val();
        let action = parseInt($("#EditGoalvolAction").val());
        let message = $("#EditGoalvolMessage").val();
        let timesheetId = $("#EditGoalTimesheetId").val();

        $.ajax({
            type: "POST",
            url: "/User/EditGoalVolunteer",
            data: {
                TimesheetId: timesheetId,
                MissionId: missionId,
                Action : action,
                VolunteeredDate: date,
                Message: message,
            },
            success: function (result) {
                console.log(result);
                $("#EditmainGoalBasedCancel").click();
                $("#volTimesheetGoalBased").html(result);
                toastr.success("Timesheet has been updated..!!");
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });



    })

    $(document).on("click", "#deleteGoalbasedTimesheet", function (e) {
        e.preventDefault();
        var timesheetId = $(this).data("timesheetid");
        $.ajax({
            type: "POST",
            url: "/User/DeleteGoalVolunteer",
            data: {
                TimesheetId: timesheetId
            },
            success: function (result) {
                console.log(result);
                $("#volTimesheetGoalBased").html(result);
                toastr.success("Timesheet data has been deleted..!!");
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });
});
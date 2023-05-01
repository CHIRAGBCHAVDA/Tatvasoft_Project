let userAppliedDate = new Date();



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

        userAppliedDate = $(this).data("userapplied-date");
        let appliedDate = userAppliedDate.substring(0, 11).split("-")[0];
        let appliedMonth = userAppliedDate.substring(0, 11).split("-")[1];
        let appliedYr = userAppliedDate.substring(0, 11).split("-")[2];
        let finalAppliedStr = appliedYr.trim() + "-" + appliedMonth.trim() + "-" + appliedDate.trim();
        let todaysDate = new Date().toISOString().slice(0, 10);
        console.log("User applied date", userAppliedDate);

        $("#EditdateVolunteered").attr('min', finalAppliedStr);
        $("#EditdateVolunteered").attr('max', todaysDate);


        $.ajax({
            type: "POST",
            url: "/User/getMissionApplicationsByUserId",
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
            //data: {
            //    userId:userId
            //},
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


        
        if (!(parseInt(hrs) >= 0 && parseInt(hrs) <= 23)) {
            toastr.error("Hours must be between the 0 to 23");
            return;
        }
        if (!(parseInt(mins) >= 0 && parseInt(mins) <= 59)) {
            toastr.error("Mins must be between 0 to 59");
            return;
        }

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


        if (!(parseInt(hrs) >= 0 && parseInt(hrs) <= 23)) {
            toastr.error("Hours must be between the 0 to 23");
            return;
        }
        if (!(parseInt(mins) >= 0 && parseInt(mins) <= 59)) {
            toastr.error("Mins must be between 0 to 59");
            return;
        }

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

    $(document).on("change", "#volMission", function () {
        let mid = $(this).val();
        console.log("Changed mission to mid : ", mid);
        $.ajax({
            type: "post",
            url: "/User/getAppliedDateByMissionId",
            data: {
                MissionId: mid
            },
            success: function (userAppliedDateNew) {
                console.log(userAppliedDateNew);
                userAppliedDate = userAppliedDateNew;
                let appliedYr = userAppliedDateNew.substring(0, 11).split("-")[0].substring(0, 4);
                let appliedMonth = userAppliedDateNew.substring(0, 11).split("-")[1].substring(0, 2);
                let appliedDate = userAppliedDateNew.substring(0, 11).split("-")[2].substring(0, 2);
                let finalAppliedStr = appliedYr.trim() + "-" + appliedMonth.trim() + "-" + appliedDate.trim();
                let todaysDate = new Date().toISOString().slice(0, 10);
                $("#dateVolunteered").attr('min', finalAppliedStr);
                $("#dateVolunteered").attr('max', todaysDate);
                console.log(finalAppliedStr);
                console.log(todaysDate);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });

    $(document).on("change", "#EditvolMission", function () {
        let mid = $(this).val();
        console.log("Changed mission to mid : ", mid);
        $.ajax({
            type: "post",
            url: "/User/getAppliedDateByMissionId",
            data: {
                MissionId: mid
            },
            success: function (userAppliedDateNew) {
                userAppliedDate = userAppliedDateNew;
                let appliedDate = userAppliedDateNew.substring(0, 11).split("-")[0];
                let appliedMonth = userAppliedDateNew.substring(0, 11).split("-")[1];
                let appliedYr = userAppliedDateNew.substring(0, 11).split("-")[2];
                let finalAppliedStr = appliedYr.trim() + "-" + appliedMonth.trim() + "-" + appliedDate.trim();
                let todaysDate = new Date().toISOString().slice(0, 10);
                $("#EditdateVolunteered").attr('min', finalAppliedStr);
                $("#EditdateVolunteered").attr('max', todaysDate);
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


        let newdate = formatDate(new Date(date));
        let newUserApplied = new Date(userAppliedDate).toLocaleDateString();
        let today = formatDate(new Date());
        console.log("lets solve \n newdate >= newUserApplied && newdate <= today \n ", newdate, newUserApplied, today);
        //if (newdate >= newUserApplied && newdate <= today) {
        //    console.log("yes you're right");
        //} else {
        //    toastr.error("Date must be between user applied date and today's date..!!");
        //    return;
        //}



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
                $("#addVolGoalBtn").click();
                $("#volTimesheetGoalBased").html(result);
                $("#mainGoalBasedCancel").click();
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
        userAppliedDate = $(this).data("userapplied-date");
        let appliedDate = userAppliedDate.substring(0, 11).split("-")[0];
        let appliedMonth = userAppliedDate.substring(0, 11).split("-")[1];
        let appliedYr = userAppliedDate.substring(0, 11).split("-")[2];
        let finalAppliedStr = appliedYr.trim() + "-" + appliedMonth.trim() + "-" + appliedDate.trim();
        let todaysDate = new Date().toISOString().slice(0, 10);
        console.log("User applied date", userAppliedDate);


        console.log(missionName
            , dateVolunteered
            , formattedDate
            , message
            , timeSheetId
            , userId
            , mid
            , action
        );
        $("#EditGoaldateVolunteered").attr('min', finalAppliedStr);
        $("#EditGoaldateVolunteered").attr('max', todaysDate);



        $.ajax({
            type: "POST",
            url: "/User/getMissionApplicationsByUserId",
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
        let action = parseInt($("#EditGoalvolAction").val());
        let message = $("#EditGoalvolMessage").val();
        let timesheetId = $("#EditGoalTimesheetId").val();
        let date = $('#EditGoaldateVolunteered').val();

        //let newdate = new Date(date).toLocaleDateString();
        //let newUserApplied = new Date(userAppliedDate).toLocaleDateString()
        //let today = new Date().toLocaleDateString();
        //if (newdate >= newUserApplied && newdate <= today) {
        //    console.log("yes youre right");
        //}
        //else {
        //    toastr.error("Date must be between user applied date and today's date..!!");
        //    return;
        //}

        let newdate = formatDate(new Date(date));
        let newUserApplied = new Date(userAppliedDate).toLocaleDateString();
        let today = formatDate(new Date());
        console.log("lets solve \n newdate >= newUserApplied && newdate <= today \n ", newdate, newUserApplied, today);
        if (newdate >= newUserApplied && newdate <= today) {
            console.log("yes you're right");
        } else {
            toastr.error("Date must be between user applied date and today's date..!!");
            return;
        }

      


        console.log("Lets check the date : ", userAppliedDate);
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


    $(document).on("change", "#GoalvolMission", function () {
        let mid = $(this).val();
        console.log("Changed mission to mid : ", mid);
        $.ajax({
            type: "post",
            url: "/User/getAppliedDateByMissionId",
            data: {
                MissionId: mid
            },
            success: function (userAppliedDateNew) {
                console.log(userAppliedDateNew);
                userAppliedDate = userAppliedDateNew;
                let appliedYr = userAppliedDateNew.substring(0, 11).split("-")[0].substring(0,4);
                let appliedMonth = userAppliedDateNew.substring(0, 11).split("-")[1].substring(0,2);
                let appliedDate = userAppliedDateNew.substring(0, 11).split("-")[2].substring(0,2);
                let finalAppliedStr = appliedYr.trim() + "-" + appliedMonth.trim() + "-" + appliedDate.trim();
                let todaysDate = new Date().toISOString().slice(0, 10);
                $("#GoaldateVolunteered").attr('min', finalAppliedStr);
                $("#GoaldateVolunteered").attr('max', todaysDate);
                console.log(finalAppliedStr);
                console.log(todaysDate);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });

    $(document).on("change", "#EditGoalvolMission", function () {
        let mid = $(this).val();
        console.log("Changed mission to mid : ", mid);
        $.ajax({
            type: "post",
            url: "/User/getAppliedDateByMissionId",
            data: {
                MissionId : mid
            },
            success: function (userAppliedDateNew) {
                userAppliedDate = userAppliedDateNew;
                let appliedDate = userAppliedDateNew.substring(0, 11).split("-")[0];
                let appliedMonth = userAppliedDateNew.substring(0, 11).split("-")[1];
                let appliedYr = userAppliedDateNew.substring(0, 11).split("-")[2];
                let finalAppliedStr = appliedYr.trim() + "-" + appliedMonth.trim() + "-" + appliedDate.trim();
                let todaysDate = new Date().toISOString().slice(0, 10);
                $("#EditGoaldateVolunteered").attr('min', finalAppliedStr);
                $("#EditGoaldateVolunteered").attr('max', todaysDate);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });

});



function formatDate(date) {
    let year = date.getFullYear();
    let month = ('0' + (date.getMonth() + 1)).slice(-2);
    let day = ('0' + date.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
}
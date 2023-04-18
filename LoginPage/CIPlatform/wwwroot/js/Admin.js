$(document).ready(function(){

    $(".admin-sidebar-btn").click(function () {

        switch (this.id) {

            case "v-pills-user-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialUser",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;

            case "v-pills-cms-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialCMS",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;

            case "v-pills-mission-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialMission",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;

            case "v-pills-missionapplication-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialMissionApplication",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;

            case "v-pills-story-tab":
                $.ajax({
                    type: "get",
                    url: "/Admin/GetPartialStory",
                    data: {

                    },
                    success: function (result) {
                        console.log(result);
                        $(".admin-main-wrapper").html(result);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
                break;
        }

    });


});
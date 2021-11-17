var NewCourseRequestManagement = (function () {
    var self = this;

    this.addNewAddCourseRequest = function () {
        debugger;
        console.log($('#TextBox_NewCourseRequest_Message').val());

        if ($('#TextBox_NewCourseRequest_Message').val() == '') {
            ShowAlert('Please enter your idea.');
            return;
        }

        var newCourseRequest = {
            Message: $('#TextBox_NewCourseRequest_Message').val(),
            StartedByUserId: $('#hidden_logged_username').val(),
            NoOfVotes: 1
        };

        $("#talk-sendmessage-btn").val("Please wait");
        azyncLockPost("../Api/NewCourseRequest/SaveNewRequestCourse", newCourseRequest, NewCourseRequestManagement.SaveSucsussfull, ConnectionError);
        $('#TextBox_NewCourseRequest_Message').val('');
    };

    this.saveSucsussfull = function (id) {
        alert('Your idea submited successfully.');
        $("#talk-sendmessage-btn").val("Submit Idea");
        hidemainframe();
    };

    this.addVote = function (id) {
        var addVoteRequest = {
            UserId: $('#hidden_logged_username').val(),
            NewCourseRequestId: id
        };

        azyncLockPost("../Api/NewCourseRequest/AddNewVote", addVoteRequest, function (response) {
            console.log(response);
            NewCourseRequestManagement.AddVoteSuccess(id);
        }, function (response) {
            console.log(response);
            if (response.statusText == "User already voted") {
                CustomAlert('You have already voted for this idea.');
            } else {
                CustomAlert(response.statusText);
            }
        });
        console.log("completed adding a vote");
    };

    this.addVoteSuccess = function (id) {
        $('#newCourseReqvote_' + id).text(parseInt($('#newCourseReqvote_' + id).text()) + 1);
    }

    this.toggleAddSchedule = function () {
        $('#DIV_new_Request').toggle();
        location.reload();
    }

    this.addNewCourseRequests = function () {
        $(".overlay-model-background").css("display", "block");
    }

    function ConnectionError(err) {

    }

    return {
        AddNewAddCourseRequest: addNewAddCourseRequest,
        SaveSucsussfull: saveSucsussfull,
        Addvote: addVote,
        ToggleAddSchedule: toggleAddSchedule,
        AddNewCourseRequests: addNewCourseRequests,
        AddVoteSuccess: addVoteSuccess
    };

})();

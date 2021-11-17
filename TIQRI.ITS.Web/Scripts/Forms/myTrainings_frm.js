var MyTrainingManagement = (function () {
    var self = this;
    var scheduleDateList = [];
    var scheduleAttendanceList = [];
    
    this.searchCourses = function () {
        var searchText = GetSearchTextBoxText('TextBox_Course_SearchBox');
        $('#DIV_CourseManagementList').html($("#DIV_InnerPageContent_Loading").html());
        $('#DIV_CourseManagementList').load("/MyTrainings/SearchCourseList?searchText=" + searchText, new function () {

        });
    };

    this.findCourses = function (txt) {
        initFunctionDelayedCall(MyTrainingManagement.SearchCourses);
    };

    this.loadCourseDetailForm = function (id, type) {
        $('#DIV_PageInner_OperationDetails').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_PageInner_OperationDetails').load("/MyTrainings/CourseDetails?id=" + id + '&type=' + type, new function () {

        });
    };

    this.resetSchedule = function () {
        scheduleDateList = [];

        var batchId = $('#Batch_Id').val();
        if (batchId != 0) {
            $.get("../Api/Batch/GetSchedulesForBatch?batchId=" + batchId, function (data) {
                console.log(data);
                for (var i = 0; i < data.BatchSchedules.$values.length; i++) {
                    scheduleDateList.push({
                        id: data.BatchSchedules.$values[i].$id,
                        ScheduleDate: data.BatchSchedules.$values[i].ScheduleDate,
                        FromTime: data.BatchSchedules.$values[i].FromTime,
                        ToTime: data.BatchSchedules.$values[i].ToTime
                    });

                    var batchCalendar = $('#batchcalendar');
                    batchCalendar.fullCalendar();

                    var eventObject = {
                        title: "Session:" + scheduleDateList.length,
                        allDay: false,
                        start: data.BatchSchedules.$values[i].FromTime,
                        end: data.BatchSchedules.$values[i].ToTime,
                        id: data.BatchSchedules.$values[i].$id
                    };
                    $('#batchcalendar').fullCalendar('renderEvent', eventObject, true);

                    if (i == 0) {
                        $('#batchcalendar').fullCalendar('gotoDate', data.BatchSchedules.$values[i].ScheduleDate);
                    }
                }

                for (var i = 0; i < data.AttendanceFailSchedule.$values.length; i++) {
                    addAttendaceEntryToList(data.AttendanceFailSchedule.$values[i].ScheduleDateStr,
                                            data.AttendanceFailSchedule.$values[i].Username, false);
                }
                
                MyTrainingManagement.DrawSchedule();
            });
        } else {
            MyTrainingManagement.DrawSchedule();
        }

    };

    this.drawSchedule = function () {
        $('#Div_Batch_ScheduleDateList').html('');
        var str = '';
        var attandanceStr = '';
        for (var i = 0; i < scheduleDateList.length; i++) {

            var momentDateObj = moment(scheduleDateList[i].ScheduleDate);
            var momentFromObj = moment(scheduleDateList[i].FromTime);
            var momentToObj = moment(scheduleDateList[i].ToTime);

            str += '<div class="schedule-item">';
            str += '<div class="schedule-date">' + momentDateObj.format("dddd, MMMM Do YYYY") + '</div>';
            str += '<div class="schedule-time">From: ' + momentFromObj.format("h:mm a") + ' To: ' + momentToObj.format("h:mm a");
            str += '</div></div>';
            
            attandanceStr += '<div class="schedule-item" onclick="MyTrainingManagement.ShowAttandanceForSchedule(' + i + ');">';
            attandanceStr += '<div class="schedule-date">' + momentDateObj.format("dddd, MMMM Do YYYY") + '</div>';
            attandanceStr += '<div class="schedule-time">From: ' + momentFromObj.format("h:mm a") + ' To: ' + momentToObj.format("h:mm a");
            attandanceStr += '</div></div>';
        }

        if (str == '')
            str = 'No schedules found.';
        if (attandanceStr == '')
            attandanceStr = 'No schedules found.';

        $('#Div_Batch_ScheduleDateList').html(str);
        $('#Div_Batch_AttandanceDateList').html(attandanceStr);
    };
    
    this.showAttandanceForSchedule = function (index) {
        var participantListStr = $("#Participant_IdList").val();
        console.log(participantListStr);
        var attandanceStr = '';

        if (participantListStr) {
            var participants = participantListStr.split(",");
            var momentDateObj = moment(scheduleDateList[index].ScheduleDate);
            $("#Hidden_Attendace_Date").val(momentDateObj.format("MM/DD/YYYY"));

            attandanceStr += '<div class="form-group">';
            attandanceStr += '<div class="col-sm-10" style="font-size: 15px;">';
            attandanceStr += 'Please mark the attendance on ' + momentDateObj.format("dddd, MMMM Do YYYY");
            attandanceStr += '</div></div>';

            for (var i = 0; i < participants.length; i++) {
                for (var j = 0; j < userProfiles.length; j++) {
                    if (userProfiles[j].email == participants[i]) {
                        console.log(userProfiles[j].email);

                        var chekedStatus = 'checked';
                        if (scheduleAttendanceList.length > 0) {
                            for (var k = 0; k < scheduleAttendanceList.length; k++) {
                                if (scheduleAttendanceList[k].ScheduleDate == $("#Hidden_Attendace_Date").val()) {
                                    for (var l = 0; l < scheduleAttendanceList[k].AttendanceList.length; l++) {
                                        if (scheduleAttendanceList[k].AttendanceList[l].UserName == userProfiles[j].email) {
                                            if (scheduleAttendanceList[k].AttendanceList[l].Status == false) {
                                                chekedStatus = '';
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        attandanceStr += '<div class="form-group">';
                        attandanceStr += '<div class="col-sm-5">' + userProfiles[j].name + '</div>';
                        attandanceStr += '<div class="col-sm-3">';
                        attandanceStr += '<input type="checkbox" id="chk_attandance_' + j + '" onchange="MyTrainingManagement.UpdateAttendanceEntry(this)" class="chk_attandance"  data-on-text="Yes" data-off-text="No"  data-size="small" ' + chekedStatus + '>';
                        attandanceStr += '</div></div>';
                        break;
                    }
                }
            }
            
            attandanceStr += '<div class="form-group">';
            attandanceStr += '<div class="col-sm-10" style="font-size: 15px;">';
            attandanceStr += '<div onclick="MyTrainingManagement.SaveAttendance();" class="btn btn-default">Update Attendance</div>';
            attandanceStr += '</div></div>';

            $('#Div_Batch_AttandanceParticipantList').html(attandanceStr);
            $(".chk_attandance").bootstrapSwitch();
        }
    };

    this.cancelAddEdit = function (id) {
        $('#DIV_PageInner_OperationDetails').html($('#PageInnerSubFormContent').html());
        CourseManagement.ResetHeight();
    };

    this.rerenderCalendar = function () {
        setTimeout(function () {
            $('#batchcalendar').fullCalendar('render');
        }, 500);
    };

    this.resetHeight = function () {

        $("#Div_PageInner_SearchPanel").css('height', '');

        var h1 = $("#Div_PageInner_SearchPanel").height();
        var h2 = $("#DIV_PageInner_OperationDetails").height();
        console.log(h1 + ':' + h2);
        if (h2 > h1) {
            $("#Div_PageInner_SearchPanel").css('height', h2 + 'px');
        }
    };

    this.resetAttandance = function () {
        $('#Div_Batch_AttandanceParticipantList').html('Please select a shcedule date.');
    };

    this.updateAttendanceEntry = function (chk) {
        var profileIndex = chk.id.replace("chk_attandance_", "");
        var profile = userProfiles[parseInt(profileIndex)];
        var dateStr = $("#Hidden_Attendace_Date").val();
        var status = $(chk).is(':checked');

        console.log(profile.email);
        addAttendaceEntryToList(dateStr, profile.email, status);

        console.clear();
        console.log(scheduleAttendanceList);
    };

    this.addAttendaceEntryToList = function (dateStr, userName, status) {
        var foundDate = false;
        var attendanceFound = false;
        for (var i = 0; i < scheduleAttendanceList.length; i++) {
            if (scheduleAttendanceList[i].ScheduleDate == dateStr) {

                for (var j = 0; j < scheduleAttendanceList[i].AttendanceList.length; j++) {
                    if (scheduleAttendanceList[i].AttendanceList[j].UserName == userName) {
                        scheduleAttendanceList[i].AttendanceList[j].Status = status;
                        attendanceFound = true;
                    }
                }

                if (!attendanceFound) {
                    scheduleAttendanceList[i].AttendanceList.push({
                        UserName: userName,
                        Status: status
                    });
                }
                foundDate = true;
                break;
            }
        }

        if (!foundDate) {
            scheduleAttendanceList.push({
                ScheduleDate: dateStr,
                AttendanceList: [{
                    UserName: userName,
                    Status: status
                }]
            });
        }
    };

    this.saveAttendance = function () {
        var batch = {
            Id: $("#Batch_Id").val(),
            UserName: $("#System_Logged_UserName").val(),
            ScheduleAttendanceList: scheduleAttendanceList
        };

        azyncLockPost("../Api/Batch/SaveBatchAttendance", batch, MyTrainingManagement.SaveAttendanceSucsussfull, ConnectionError);
    };

    this.saveAttendanceSucsussfull = function () {
        CustomSuccessMessage('Record saved successfully.');
    };

    this.rerenderAssignments = function () {
        $('#Div_Batch_AssignmentList').html('Loading Assignments .. Please wait.');
        $('#Div_Batch_AssignmentList').load("/MyTrainings/AssignmentList?batchId=" + $("#Batch_Id").val(), new function () {

        });
    };

    this.showFeedbackScreen = function (id) {

        $('#DIV_MyTrainings_Feedback').toggle();
        $('#Feedback_AssignmentId').val(id);
        $('#TextBox_Feedback_Message').val('');

    },

    this.clearAssignment = function () {
        $('#TextBox_Assignment_Url').val('');
        $('#TextBox_Assignment_Notes').val('');
    };

    this.submitFeedback = function () {
        var review = {
            AssignmentId: $("#Feedback_AssignmentId").val(),
            Notes: $("#TextBox_Feedback_Message").val(),
            ReviewedByUserId: $("#System_Logged_UserName").val()
        };

        if (review.Notes.replace(/\s/g, "") == "") {
            ShowAlert("Please fill all the required fields and try again.");
            return;
        }

        azyncLockPost("../Api/Assignment/SaveFeedback", review, MyTrainingManagement.SaveFeedbackSucsussfull, ConnectionError);
    };

    this.saveFeedbackSucsussfull = function (result) {
        CustomSuccessMessage('Record saved successfully.');
        $('#DIV_MyTrainings_Feedback').hide();
        MyTrainingManagement.RerenderAssignments();
    };

    this.showFeedbackDetails = function (id) {
        $('#Review_Feedback_review_' + id).toggle();
    };

    return {
        SearchCourses: searchCourses,
        FindCourses: findCourses,
        LoadCourseDetailForm: loadCourseDetailForm,
        CancelAddEdit: cancelAddEdit,
        ResetSchedule: resetSchedule,
        ResetHeight: resetHeight,
        RerenderCalendar: rerenderCalendar,
        DrawSchedule: drawSchedule,
        ShowFeedbackScreen: showFeedbackScreen,
        ClearAssignment: clearAssignment,
        SubmitFeedback: submitFeedback,
        SaveFeedbackSucsussfull: saveFeedbackSucsussfull,
        RerenderAssignments: rerenderAssignments,
        ResetAttandance: resetAttandance,
        ShowAttandanceForSchedule : showAttandanceForSchedule,
        UpdateAttendanceEntry: updateAttendanceEntry,
        ShowFeedbackDetails: showFeedbackDetails,
        SaveAttendance: saveAttendance,
        SaveAttendanceSucsussfull: saveAttendanceSucsussfull
    };

})();

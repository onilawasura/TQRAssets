var MyCourseManagement = (function () {
    var self = this;

    this.searchCourses = function () {
        var searchText = GetSearchTextBoxText('TextBox_Course_SearchBox');
        $('#DIV_CourseManagementList').html($("#DIV_InnerPageContent_Loading").html());
        $('#DIV_CourseManagementList').load("/MyCourses/SearchCourseList?searchText=" + searchText, new function () {

        });
    };

    this.findCourses = function (txt) {
        initFunctionDelayedCall(MyCourseManagement.SearchCourses);
    };

    this.loadCourseDetailForm = function (id) {
        $('#DIV_PageInner_OperationDetails').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_PageInner_OperationDetails').load("/MyCourses/CourseDetails?id=" + id, new function () {

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

                MyCourseManagement.DrawSchedule();
            });
        } else {
            MyCourseManagement.DrawSchedule();
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
        }

        if (str == '')
            str = 'No schedules found.';

        $('#Div_Batch_ScheduleDateList').html(str);
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
    
    this.rerenderAssignments = function () {
        $('#Div_Batch_AssignmentList').html('Loading Assignments .. Please wait.');
        $('#Div_Batch_AssignmentList').load("/MyCourses/AssignmentList?batchId=" + $("#Batch_Id").val(), new function () {

        });
    };

    this.clearAssignment = function () {
        $('#TextBox_Assignment_Url').val('');
        $('#TextBox_Assignment_Notes').val('');
    };

    this.submitAssignment = function () {
        var assignment = {
            BatchId: $("#Batch_Id").val(),
            UserId: $("#Participant_UserName").val(),
            DownloadURL: $("#TextBox_Assignment_Url").val(),
            Notes: $("#TextBox_Assignment_Notes").val()
        };

        if (assignment.DownloadURL.replace(/\s/g, "") == "") {
            ShowAlert("Please fill all the required fields and try again.");
            return;
        }
        
        azyncLockPost("../Api/Assignment/SaveAssignment", assignment, MyCourseManagement.SaveAssignmentSucsussfull, ConnectionError);
    };

    this.saveAssignmentSucsussfull = function (result) {
        CustomSuccessMessage('Record saved successfully.');
        MyCourseManagement.ClearAssignment();
        MyCourseManagement.RerenderAssignments();
    };

    return {
        SearchCourses: searchCourses,
        FindCourses: findCourses,
        LoadCourseDetailForm: loadCourseDetailForm,
        CancelAddEdit: cancelAddEdit,
        ResetSchedule : resetSchedule,
        ResetHeight: resetHeight,
        RerenderCalendar: rerenderCalendar,
        DrawSchedule: drawSchedule,
        ClearAssignment: clearAssignment,
        SubmitAssignment: submitAssignment,
        SaveAssignmentSucsussfull: saveAssignmentSucsussfull,
        RerenderAssignments: rerenderAssignments
    };

})();

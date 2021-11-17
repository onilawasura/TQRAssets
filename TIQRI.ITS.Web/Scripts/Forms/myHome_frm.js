var MyHomeManagement = (function () {
    var self = this;
    var scheduleDateList = [];

    this.loadScheduleCalender = function () {
        var userId = $('#User_Id').val();

        $.get("../Api/Batch/GetSchedulesForUser?userId=" + userId, function (data) {
            console.log(data);
            for (var i = 0; i < data.$values.length; i++) {
                scheduleDateList.push({
                    id: data.$values[i].$id,
                    ScheduleDate: data.$values[i].ScheduleDate,
                    FromTime: data.$values[i].FromTime,
                    ToTime: data.$values[i].ToTime
                });

                var batchCalendar = $('#batchcalendar');
                batchCalendar.fullCalendar();
                var eventObject = {
                    title: "(" + data.$values[i].Text + ")",
                    allDay: false,
                    start: data.$values[i].FromTime,
                    end: data.$values[i].ToTime,
                    id: data.$values[i].$id
                };
                $('#batchcalendar').fullCalendar('renderEvent', eventObject, true);

                //if (i == 0) {
                //    $('#batchcalendar').fullCalendar('gotoDate', data.$values[i].ScheduleDate);
                //}
            }

            console.log(scheduleDateList);
        });
    };

    return {
        LoadScheduleCalender: loadScheduleCalender
    };

})();
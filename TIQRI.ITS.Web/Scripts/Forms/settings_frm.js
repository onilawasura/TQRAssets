var SettingsManagement = (function () {
    var self = this;

    this.saveSystemSettings = function () {
        
        var settings = {
            AdministratorLisrtStr: $("#TextBox_Settings_Administrator").val(),
            CourseCoordinatorLisrtStr: $("#TextBox_Settings_Coordinator").val(),
            UserName: $("#System_Logged_UserName").val()
        };
        console.log(settings);
        
        azyncLockPost("../Api/SystemSettings/SaveSystemSettings", settings, SettingsManagement.SaveSucsussfull, ConnectionError);
    };
    
    this.saveSucsussfull = function (id) {
        CustomSuccessMessage('Record saved successfully.');
        $('#DIV_PageInner_OperationDetails').html($('#PageInnerSubFormContent').html());
        CourseManagement.SearchCourses();
    };

    return {
        SaveSystemSettings: saveSystemSettings,
        SaveSucsussfull: saveSucsussfull
    };

})();

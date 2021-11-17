var ReviewTemplateManagement = (function () {
    var self = this;

    this.searchReviewTemplates = function () {
        var searchText = GetSearchTextBoxText('TextBox_ReviewTemplate_SearchBox');
        $('#DIV_ReviewTemplateManagementList').html($("#DIV_InnerPageContent_Loading").html());
        $('#DIV_ReviewTemplateManagementList').load("/ReviewTemplate/SearchReviewTemplateList?searchText=" + searchText, new function () {

        });
    };

    this.findReviewTemplates = function (txt) {
        initFunctionDelayedCall(ReviewTemplateManagement.SearchReviewTemplates);
    };

    this.loadAddEditForm = function (id) {
        $('#DIV_PageInner_OperationDetails').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_PageInner_OperationDetails').load("/ReviewTemplate/AddEditReviewTemplate?id=" + id, new function () {

        });
    };

    this.cancelAddEdit = function (id) {
        $('#DIV_PageInner_OperationDetails').html($('#PageInnerSubFormContent').html());
        ReviewTemplateManagement.ResetHeight();
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

    this.saveAddEditReviewTemplate = function () {

        var ReviewTemplate = {
            Id: $("#ReviewTemplate_Id").val(),
            Name: $("#TextBox_ReviewTemplate_ReviewTemplateName").val(),
            ReviewTemplateType: $("#DDL_ReviewTemplate_ReviewTemplateType").val(),
            UserName: $("#System_Logged_UserName").val()
        };

        if (ReviewTemplate.Name.replace(/\s/g, "") === "") {
            ShowAlert("Please fill all the required fields and try again.");
            return;
        }


        azyncLockPost("../Api/ReviewTemplate/SaveReviewTemplate", ReviewTemplate, ReviewTemplateManagement.SaveSucsussfull, ConnectionError);
    };

    this.saveSucsussfull = function (id) {
        CustomSuccessMessage('Record saved successfully.');
        $('#DIV_PageInner_OperationDetails').html($('#PageInnerSubFormContent').html());
        ReviewTemplateManagement.SearchReviewTemplates();
    };

    return {
        SearchReviewTemplates: searchReviewTemplates,
        FindReviewTemplates: findReviewTemplates,
        LoadAddEditForm: loadAddEditForm,
        CancelAddEdit: cancelAddEdit,
        SaveAddEditReviewTemplate: saveAddEditReviewTemplate,
        SaveSucsussfull: saveSucsussfull,
        ResetHeight: resetHeight
    };

})();

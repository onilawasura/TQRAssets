$(document).ready(function () {

    $('#DDL_Asset_Approve').hide();
    $('#LBL_status').hide();

    $('#DDL_Asset_AssetStatus').change(function () {

        var ddlAssetStatusValue = $(this).val();
        if (ddlAssetStatusValue == "Disposed" || ddlAssetStatusValue == "Donated") {
            $('#DDL_Asset_Approve').show();
            $('#LBL_status').show();

            var assetObj = {
                AssetStatus: $("#DDL_Asset_AssetStatus").val(),
            };
            $.ajax({
                type: "POST",
                url: "../Asset/GetAssetStatusUpdateList",
                data: JSON.stringify(assetObj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var s = '<option value="-1">Please Select a Admin for Approval</option>';
                    for (var i = 0; i < data.length; i++) {
                        s += '<option value="' + data[i].Value + '">' + data[i].Text + '</option>';
                    }
                    $("#DDL_Asset_Approve").html(s);
                }
            });

        } else {
            $('#DDL_Asset_Approve').hide();
                $('#LBL_status').hide();
        }
        
    });
    

});
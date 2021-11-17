var AssetManagement = (function () {
    var self = this;

    this.searchAssets = function () {
        var searchText = GetSearchTextBoxText('TextBox_Asset_SearchBox');
        var asset_Type = $('#Asset_Type').val();
        $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
        $('#DIV_AssetManagementList').load("/Asset/SearchAssetList?assetType=" + asset_Type + "&searchText=" + searchText, new function () {

        });
    };
    this.searchModelsOrManuf = function () {
        var searchText = GetSearchTextBoxText('TextBox_Asset_SearchBox');
        var category = $('#Search_Category_Dropdown').find('option:selected').text();
        if (category=="Model") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchModelList?searchText=" + searchText, new function () {

            });
        }
        else if (category == "Manufacturer") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchManufList?searchText=" + searchText, new function () {

            });
        }
        else if (category == "Memory") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchMemoryList?searchText=" + searchText, new function () {

            });
        }
        else if (category == "Processor") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchProcessorList?searchText=" + searchText, new function () {

            });
        }
        else if (category == "Hard Disk") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchHardDiskList?searchText=" + searchText, new function () {

            });
        }
        else if (category == "Screen Size") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchScreenSizeList?searchText=" + searchText, new function () {

            });
        }
        else if (category == "Vendor") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchVendorList?searchText=" + searchText, new function () {

            });
        }
        else if (category == "Lease Period") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchLeasePeriodList?searchText=" + searchText, new function () {

            });
        }
        else if (category == "Warranty Period") {
            $('#DIV_AssetManagementList').html($("#DIV_InnerPageContent_Loading").html());
            $('#DIV_AssetManagementList').load("/AddNewData/SearchWarrantyPeriodList?searchText=" + searchText, new function () {

            });
        }
        
    };
    this.findAssets = function () {
        initFunctionDelayedCall(AssetManagement.SearchAssets);
    };

    this.findModels = function () {
        initFunctionDelayedCall(AssetManagement.SearchModelsOrManuf);
    };

    this.loadAddEditForm = function (id) {
        var asset_Type = $('#Asset_Type').val();
        $('#DIV_PageInner_OperationDetails').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_PageInner_OperationDetails').load("/Asset/AddEditAsset?assetType=" + asset_Type + "&id=" + id, new function () {
            //$("#DIV_PageInner_OperationDetails").load("Asset_Increment_Number", function () {
            //    alert(id)
            //if ($('#TextBox_Asset_ManufactureSN').val().length !== 0) {
            //    if (typeof id === 'undefined') {
            //        document.getElementById("TextBox_Asset_ManufactureSN").readOnly = false;
            //        alert('id')
            //    }
            //    else {
            //        document.getElementById("TextBox_Asset_ManufactureSN").readOnly = true;
            //        alert('no id')
            //    }
            //}
            //});
        });
     
    };

    this.loadNewAddEditForm = function (id) {
        $('#DIV_PageInner_OperationDetails').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_PageInner_OperationDetails').load("/AddNewData/NewDataView?id=" + id, new function () {

        });

    };

    this.loadDataEditForm = function (id,type) {
        $('#DIV_PageInner_OperationDetails').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_PageInner_OperationDetails').load("/AddNewData/EditDataView?id=" + id + "&type=" + type, new function () {

        });

    };

    this.cancelAddEdit = function (id) {
        $('#DIV_PageInner_OperationDetails').html($('#PageInnerSubFormContent').html());
        AssetManagement.ResetHeight();
    };

    this.resetHeight = function () {

        $("#Div_PageInner_SearchPanel").css('height', '');

        var h1 = $("#Div_PageInner_SearchPanel").height();
        var h2 = $("#DIV_PageInner_OperationDetails").height();

        if (h2 > h1)
            h1 = h2;

        var windowHeight = $(window).height() - 240;
        console.log(h1 + ':' + windowHeight);
        if (h1 > windowHeight) {
            $("#Div_PageInner_SearchPanel").css('height', windowHeight + 'px');
            $("#DIV_PageInner_OperationDetails").css('height', windowHeight + 'px');
        }


    };
    this.saveAddEditModel = function () {
        var Model = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_ModelName").val(),
            Id: $("#TextBox_Model_ID").val()
        }
        console.log(Model)
        azyncLockPost("../Api/Asset/SaveModel", Model, AssetManagement.SaveSucsussfull, ConnectionError);
    }

    this.saveAddEditManufacturer = function () {
        var Manufacturer = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_ManufactName").val(),
            Id: $("#TextBox_Manufact_ID").val()
        }
        azyncLockPost("../Api/Asset/SaveManufacturer", Manufacturer, AssetManagement.SaveSucsussfull, ConnectionError);
    }
    this.addSaveMemory = function () {
        
        var Memory = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_MemoryName").val(),
            Id: $("#TextBox_Memory_ID").val()
        }
        console.log(Memory)
        azyncLockPost("../Api/Asset/SaveMemory", Memory, AssetManagement.SaveSucsussfull, ConnectionError);
    }

    this.addSaveProcessor = function () {
        var Processor = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_ProcessorName").val(),
            Id: $("#TextBox_Processor_ID").val()
        }
        azyncLockPost("../Api/Asset/SaveProcessor", Processor, AssetManagement.SaveSucsussfull, ConnectionError);
    }

    this.addSaveHardDisk = function () {
        var HardDisk = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_HardDiskName").val(),
            Id: $("#TextBox_HardDisk_ID").val()
        }
        azyncLockPost("../Api/Asset/SaveHardDisk", HardDisk, AssetManagement.SaveSucsussfull, ConnectionError);
    }

    this.addSaveScreenSize = function () {
        var ScreenSize = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_ScreenSize").val(),
            Id: $("#TextBox_ScreenSize_ID").val()
        }
        azyncLockPost("../Api/Asset/SaveScreenSize", ScreenSize, AssetManagement.SaveSucsussfull, ConnectionError);
    }
    this.addSaveVendor = function () {
        var Vendor = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_Vendor").val(),
            Id: $("#TextBox_Vendor_ID").val()
        }
        azyncLockPost("../Api/Asset/SaveVendor", Vendor, AssetManagement.SaveSucsussfull, ConnectionError);
    }
    this.addSaveLeasePeriod = function () {
        var LeasePeriod = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_LeasePeriod").val(),
            Id: $("#TextBox_LeasePeriod_ID").val()
        }
        azyncLockPost("../Api/Asset/SaveLeasePeriod", LeasePeriod, AssetManagement.SaveSucsussfull, ConnectionError);
    }
    this.addSaveWarrantyPeriod = function () {
        var Warranty = {
            UserName: $("#System_Logged_UserName").val(),
            Name: $("#TextBox_Asset_WarrantyPeriod").val(),
            Id: $("#TextBox_WarrantyPeriod_ID").val()
        }
        azyncLockPost("../Api/Asset/SaveWarrantyPeriod", Warranty, AssetManagement.SaveSucsussfull, ConnectionError);
    }

    this.saveAddEditAsset = function () {

        var Asset = {
            Id: $("#Asset_Id").val(),
            AssetType: $("#Asset_Type").val(),
            AssetNumber: $("#TextBox_Asset_AssetNumber").val(),
            Manufacture: $("#DDL_Asset_Manufacture").val(),
            Model: $("#DDL_Asset_Model").val(),
            ManufactureSN: $("#TextBox_Asset_ManufactureSN").val(),
            ComputerName: $("#TextBox_Asset_ComputerName").val(),
            UserId: $("#DDL_Asset_UserId").val(),
            Group: $("#TextBox_Asset_Group").val(),
            Customer: $("#TextBox_Asset_Customer").val(),
            Availability: $("#DDL_Asset_Availability").val(),
            AssetStatus: $("#DDL_Asset_AssetStatus").val(),
            Vendor: $("#DDL_Asset_Vendor").val(),
            DatePurchasedOrleased: $("#TextBox_Asset_DatePurchasedOrleased").val(),
            WarrantyPeriod: $("#DDL_Asset_WarrantyPeriod").val(),
            Location: $("#TextBox_Asset_Location").val(),
            Memory: $("#DDL_Asset_Memory").val(),
            Processor: $("#DDL_Asset_Processor").val(),
            Speed: $("#TextBox_Asset_Speed").val(),
            HDD: $("#DDL_Asset_HDD").val(),
            LeasePeriod: $("#DDL_Asset_LeasePeriod").val(),
            Cost: $("#TextBox_Asset_Cost").val(),
            IsSSD: $('#Chk_Asset_IsSSD').is(":checked"),
            ProblemReported: $('#Chk_Asset_ProblemReported').is(":checked"),
            NOTE: $("#TextBox_Asset_Notes").val(),
            Rating: $("#TextBox_Asset_Rating").val(),
            Screen: $("#DDL_Asset_Screen").val(),
            ConferenceRoom: $("#TextBox_Asset_ConferenceRoom").val(),
            Building: $("#TextBox_Asset_Building").val(),
            Floor: $("#TextBox_Asset_Floor").val(),
            DeviceType: $("#TextBox_Asset_DeviceType").val(),
            MobileName: $("#TextBox_Asset_MobileName").val(),
            UserName: $("#System_Logged_UserName").val(),
            IncrementNumber: $("#Asset_Increment_Number").val()
        };

        if (Asset.AssetNumber.replace(/\s/g, "") === "") {
            ShowAlert("Please fill all the required fields and try again.");
            return;
        }

        azyncLockPost("../Api/Asset/SaveAsset", Asset, AssetManagement.SaveSucsussfull, ConnectionError);
    };

    this.saveSucsussfull = function (id) {
        CustomSuccessMessage('Record saved successfully.');
        $('#DIV_PageInner_OperationDetails').html($('#PageInnerSubFormContent').html());
        AssetManagement.SearchAssets();
    };

    this.showFilter = function () {
        var objId = "assetreport-filterpanel";
        if (document.getElementById(objId).style.display === 'none') {
            show(objId);
            $('#assetreport-filtershowhidebtn').val("Hide Filters");
        }
        else {
            hide(objId);
            $('#assetreport-filtershowhidebtn').val("Show Filters");
        }
    };

    this.getSearchModel = function () {
        var searchModel = {
            GlobalText: $("#TextBox_Asset_GlobalText").val(),
            AssetType: $("#DDL_Asset_AssetType").val(),
            Availability: $("#DDL_Asset_Availability").val(),
            AssetStatus: $("#DDL_Asset_AssetStatus").val(),
            AssetNumber: $("#TextBox_Asset_AssetNumber").val(),
            Manufacture: $("#DDL_Asset_Manufacture").val(),
            AssetModel: $("#DDL_Asset_Model").val(),
            ManufactureSN: $("#TextBox_Asset_ManufactureSN").val(),
            ComputerName: $("#TextBox_Asset_ComputerName").val(),
            UserId: $("#DDL_Asset_UserId").val(),
            Group: $("#TextBox_Asset_Group").val(),
            Customer: $("#TextBox_Asset_Customer").val(),
            ConferenceRoom: $("#TextBox_Asset_ConferenceRoom").val(),
            Building: $("#TextBox_Asset_Building").val(),
            DatePurchasedOrleasedFrom: $("#TextBox_Asset_DatePurchasedOrleased_from").val(),
            DatePurchasedOrleasedTo: $("#TextBox_Asset_DatePurchasedOrleased_to").val()
        };
        return searchModel;
    };

    this.filterAssetSearch = function () {
        var objId = "assetreport-filterpanel";
        hide(objId);
        $('#assetreport-filtershowhidebtn').val("Show Filters");
        $('#DIV_AssetFilterList').html($("#DIV_InnerPageContent_Loading").html());

        var searchModel = AssetManagement.GetSearchModel();
        $('#DIV_AssetFilterList').load("/AssetReport/SearchAssetList", searchModel, new function () {
        });
    };

    this.exportAssetSearch = function () {
        var searchModel = AssetManagement.GetSearchModel();
        $('#DIV_AssetFilterList').load("/AssetReport/ExportToExcel", searchModel, new function () {
        });
    };

    return {
        SearchAssets: searchAssets,
        FindAssets: findAssets,
        LoadAddEditForm: loadAddEditForm,
        CancelAddEdit: cancelAddEdit,
        SaveAddEditAsset: saveAddEditAsset,
        SaveSucsussfull: saveSucsussfull,
        ResetHeight: resetHeight,
        ShowFilter: showFilter,
        GetSearchModel: getSearchModel,
        FilterAssetSearch: filterAssetSearch,
        ExportAssetSearch: exportAssetSearch,
        LoadNewAddEditForm: loadNewAddEditForm,
        FindModels: findModels,
        SearchModelsOrManuf: searchModelsOrManuf,
        SaveAddEditModel: saveAddEditModel,
        SaveAddEditManufacturer: saveAddEditManufacturer,
        LoadDataEditForm: loadDataEditForm,
        AddSaveMemory: addSaveMemory,
        AddSaveProcessor: addSaveProcessor,
        AddSaveHardDisk: addSaveHardDisk,
        AddSaveScreenSize: addSaveScreenSize,
        AddSaveVendor: addSaveVendor,
        AddSaveLeasePeriod: addSaveLeasePeriod,
        AddSaveWarrantyPeriod: addSaveWarrantyPeriod
    };

})();

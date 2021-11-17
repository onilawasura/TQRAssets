var NavigationManager = (function () {
    var self = this;

    function loadHome() {
        window.location.href = "../../";
    }
    
    function loadAdminDashboard() {
        changeHeading("Admin", "Dashboard", "menu_lvl1_dashboard");
        $('#DIV_InnerPageContent').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_InnerPageContent').load('../Home/AdminDashboard', new function () { });
        //NavigationManager.LoadAssetReport();
    }

    function loadAssetReport() {
        changeHeading("Admin", "Asset Report", "menu_lvl1_assetreport");
        $('#DIV_InnerPageContent').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_InnerPageContent').load('../AssetReport/Index', new function () { });
    }

    function loadNewDataPage() {
        console.log("a222");
        changeHeading("Admin", "New Data", "addNewAssetData");
        $('#DIV_InnerPageContent').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_InnerPageContent').load('../AddNewData/Index', new function () { });
    }

    function loadAssetManagement(assetType) {
        console.log("a111");
        if (assetType === 'Laptop') {
            changeHeading("Admin", 'Manage Laptops', "menu_lvl1_laptop");
        }
        if (assetType === 'ConferenceRoomDevice') {
            changeHeading("Admin", 'Manage Conference Room Devices', "menu_lvl1_conferenceroomdevice");
        }
        if (assetType === 'Display') {
            changeHeading("Admin", 'Manage Displays', "menu_lvl1_display");
        }
        if (assetType === 'KeyboardMouse') {
            changeHeading("Admin", 'Manage Keyboards & Mouse', "menu_lvl1_keyboardmouse");
        }
        if (assetType === 'Mobile') {
            changeHeading("Admin", 'Manage Mobiles', "menu_lvl1_mobile");
        }
        if (assetType === 'DisplayPanel') {
            changeHeading("Admin", 'Manage Display Panels', "menu_lvl1_displaypanel");
        }
        if (assetType === 'PowerAdapter') {
            changeHeading("Admin", 'Manage Power Adapters', "menu_lvl1_powerAdapter");
        }
        if (assetType === 'Other') {
            changeHeading("Admin", 'Manage Other Assets', "menu_lvl1_other");
        }
        if (assetType === 'InventoryAsset') {
            changeHeading("Admin", 'Manage Inventory Assets', "menu_lvl1_inventoryAsset");
        }
        if (assetType === 'NewData') {
            changeHeading("Admin", 'Add New Data', "addNewAssetData");
        }
        
        $('#DIV_InnerPageContent').html($('#DIV_InnerPageContent_Loading').html());
        $('#DIV_InnerPageContent').load('../Asset/Index?assetType=' + assetType, new function () { });
    }
    

    function changeHeading(module, page, link) {
        $('#ModuleTitleContent').html(module);
        $('#FormTitleContent').html(page);
        $('.navigation-item').removeClass("active");
        $('#' + link).addClass("active");
    }

    return {
        LoadHome : loadHome,
        LoadAdminDashboard: loadAdminDashboard,
        LoadAssetManagement: loadAssetManagement,
        LoadAssetReport: loadAssetReport,
        LoadNewDataPage: loadNewDataPage
    };

})();
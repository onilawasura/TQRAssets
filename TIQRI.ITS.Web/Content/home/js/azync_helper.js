// Maintain the lock for the operation.
// will be released after the completion of previous request.
var _systemUpdateOperationLock = false;

// Option to call the server operation with the locking facility. Ex. Saving/Deleting operation
// Param : URL - URL of the operation action to be called.
// Param : dataObj - Object to be passed to the server as a json. Ex-Viewmodel javascript object
// Param : succesFnc - Function to be called after the sucsessfull completion of the operation,
//         Required to implement the functionality in the implementing form.
// Param : errorFnc - Function to be called if any error occured in the function call
//         Required to implement the functionality in the implementing form.

function azyncPost(URL, dataObj, succesFnc, errorFnc, checkLock) {

    // Check if the lock is on, If its locked then return without 
    // calling the server function.
    
    if (_systemUpdateOperationLock == true) {
        if (checkLock != false) {
            //customAlert("Current operation is in progress. Please wait.");
            return;
        }
    }
    
    // Server call with jQuery
    $.ajax({
        url: URL,
        data: JSON.stringify(dataObj),
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        beforeSend: function () {
            // Locking the operation to avoid conflicts.
            //platformConfig.ShowAjaxIndicator();
            _systemUpdateOperationLock = true;
        },
        statusCode: {
            200: function (result) {
                // Comment by Harinda Dias
                // Releasing the lock for enable other operations.
                _systemUpdateOperationLock = false;
                $("#SystemMainAzyncLoadingDiv").css("display", "none");
                // Calling the sucesess function 
                succesFnc(result);
            },
            406: function (err) {
                // Releasing the lock for enable other operations.
                _systemUpdateOperationLock = false;
                // Calling the function to be handled when an error occured.
                $("#SystemMainAzyncLoadingDiv").css("display", "none");
                console.log(err);
                ShowAlert(err.responseText);
            },
            500: function (xhr, desc, err) {
                // Releasing the lock for enable other operations.
                _systemUpdateOperationLock = false;
                console.log(xhr);
                console.log("Desc: " + desc + "\nErr:" + err);
                // Calling the function to be handled when an error occured.
                $("#SystemMainAzyncLoadingDiv").css("display", "none");
                errorFnc(err, xhr);
            }
        }
    });
}

// Option to call the server operation without any locking requirement. Ex. Selection operation
// Param : URL - URL of the operation action to be called.
// Param : succesFnc - Function to be called after the sucsessfull completion of the operation,
//         Required to implement the functionality in the implementing form.
// Param : errorFnc - Function to be called if any error occured in the function call
//         Required to implement the functionality in the implementing form.
function azyncGet(URL, succesFnc, errorFnc, withLoading) {
    // Server call with jQuery
    $.ajax({
        url: URL,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        beforeSend: function () {
            //if (withLoading)
                //platformConfig.ShowAjaxIndicator();
        },
        success: function (result) {
            //platformConfig.HideAjaxIndicator();
            // Calling the sucesess function 
            succesFnc(result);
        },
        error: function (xhr, desc, err) {
            //platformConfig.HideAjaxIndicator();
            console.log(xhr);
            console.log("Desc: " + desc + "\nErr:" + err);
            // Calling the function to be handled when an error occured.
            errorFnc(err);
        }
    });
}
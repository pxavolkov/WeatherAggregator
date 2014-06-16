weatherAggregator.proxy = function () {

    var sendAjaxRequest = function(type, url, data, onSuccess) {

        var jsonRequest = JSON.stringify(data);

        $.ajax({
            url: url,
            type: type,
            data: jsonRequest,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: onSuccess,
            error: function (xhr) {
                alert(xhr.status);
            }
        });
    };

    return {
        getSources: function (onSuccess) {
            sendAjaxRequest("Get", "/Weather/Sources", onSuccess);
        },

        getWeather: function (onSuccess) {
            sendAjaxRequest("POST", "", onSuccess);
        }
    };
}();
(function() {

    'use strict';

    function weatherService($http) {
        var urls = {
            sources: "/api/Weather/Sources",
            getWeather: "/api/Weather/GetWeather",
            sendFeedback: "/api/Feedback/Add",
            subscribe: "/api/Subscription/Subscribe"
        };
        return {
            getSources: function() {
                return $http.get(urls.sources);
            },
            getWeather: function(data) {
                return $http.post(urls.getWeather, data);
            },
            sendFeedback: function(data) {
                return $http.post(urls.sendFeedback, data);
            },
            subscribe: function(data) {
                return $http.post(urls.subscribe, data);
            }
        };
    };

    weatherService.$inject = ['$http'];
    angular.module('weatherApp').service('weatherService', weatherService);
})();
weatherAggregator.weatherPage = {
    init: function () {

        var weatherApp = angular.module('weatherApp', []);

        weatherApp.controller('WeatherController', function ($scope) {
            weatherAggregator.proxy.getSources(function (response) {
                $scope.$apply(function() { $scope.sources = response; });
            });

            $scope.getWeather = function () {
                alert("The weather is good!");
            };
        });
    }
};
$(weatherAggregator.weatherPage.init());
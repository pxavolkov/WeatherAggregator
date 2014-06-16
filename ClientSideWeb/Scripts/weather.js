window.weatherAggregator = {
    init: function () {

        weatherAggregator.proxy.getSources(weatherAggregator.onGetSources);
    },

    onGetSources: function() {
        var weatherApp = angular.module('weatherApp', []);

        weatherApp.controller('WeatherController', function ($scope) {
            $scope.sources = [
              {
                  Id: "123",
                  Name: 'GisMeteo'
              },
              {
                  Id: "1234",
                  Name: 'Site1'
              },
              {
                  Id: "12345",
                  Name: 'Site2'
              }
            ];

            $scope.getWeather = function() {
                alert("The weather is good!");
            };
        });
    }
};
$(weatherAggregator.init());
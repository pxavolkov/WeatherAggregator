weatherAggregator.weatherPage = {
    initWeatherApp: function() {

            var weatherApp = angular.module('weatherApp', []);

            weatherApp.controller('WeatherController', function($scope) {
                weatherAggregator.proxy.getSources(function(response) {
                    $scope.$apply(function() { $scope.sources = response; });
                });

                $scope.getWeather = weatherAggregator.weatherPage.getWeather;
            });
    },

    init: function (){
        $("#tabDay2").text(weatherAggregator.utils.getDayOfWeek(2));
        $("#tabDay3").text(weatherAggregator.utils.getDayOfWeek(3));
    },

    getWeather: function () {
        weatherAggregator.utils.showWaiter();
        var selectedSources = $("#sourcesDiv input:checked").map(function () {
            return $(this).data("sourceid");
        }).get();

        if (!selectedSources.length) {
            alert("Пожалуйста, выберите хотя бы 1 сайт");
        } else {
            var data = {
                Sources: selectedSources,
                Location: {
                    Latitude: 44.35,
                    Longitude: 33.31
                },
                DateRange: {
                    From: new Date(),
                    To: new Date()
                }
            };

            data.DateRange.To.setDate(data.DateRange.To.getDate() + 2);
            weatherAggregator.proxy.getWeather(data, weatherAggregator.weatherPage.bindWeather);
        }
    },

    bindWeather: function (data) {
        if (data && data.length) {
            $(data).each(function(index, element) {
                element.index = index;
                element.active = index == 0 ? "active" : "";
                element.Precipitation = weatherAggregator.utils.getPrecipitation(element.Precipitation);
                element.CloudIcon = weatherAggregator.utils.getCloudIconUrl(element.Cloudness);
            });
            var $scope = angular.element($("body")).scope();
            $scope.$apply(function() { $scope.weatherModels = data; });
        }
        weatherAggregator.utils.hideWaiter();
    }
};
weatherAggregator.weatherPage.initWeatherApp();
$(weatherAggregator.weatherPage.init);
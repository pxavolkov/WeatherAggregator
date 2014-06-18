weatherAggregator.weatherPage = {

    initWeatherApp: function() {

            var weatherApp = angular.module('weatherApp', []);

            weatherApp.controller('WeatherController', function($scope) {
                weatherAggregator.proxy.getSources(function(response) {
                    $scope.$apply(function() {
                        $scope.sources = response;
                    });

                });

                $scope.weatherModels = [{ index: 0 }, { index: 1}, { index: 2 }, { index: 3 }];

                $scope.getWeather = weatherAggregator.weatherPage.getWeather;
            });
    },

    init: function (){
        $("#tabsDiv").on('click', "a[id^='tabDay']", function () {
            $("[id^='contenttabDay']").removeClass('active');
            var location = "[id$='content" + $(this).attr('id') + "']";
            $(location).addClass('active');
            $(".tab-title").removeClass('active');
            $(this).parent().addClass('active');
        });
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
                element.day = weatherAggregator.utils.getDay(index);
            });
            var $scope = angular.element($("body")).scope();
            $scope.$apply(function () { $scope.weatherModels = data; });
            $("#tabsDiv").removeClass('hidden');
        }
        weatherAggregator.utils.hideWaiter();
    }
};

weatherAggregator.weatherPage.initWeatherApp();
$(weatherAggregator.weatherPage.init);
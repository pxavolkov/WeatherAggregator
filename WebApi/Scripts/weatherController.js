(function() {

    'use strict';

    function weatherController($scope, $timeout, $sce, weatherUtils, weatherService) {
        weatherUtils.showWaiter();

        //load weather sources
        weatherService.getSources()
            .success(function(response) {
                $scope.sources = response;
                $scope.sources.forEach(function (source) {
                    source.IsChecked = true;
                });
                $timeout(restoreSourceChecks);
            })
            .finally(function() {
                weatherUtils.hideWaiter();
            });

        //init foundation 
        $(document).foundation();

        $scope.activateTab = function (weatherModel) {
            $scope.weatherModels.forEach(function(model) {
                 model.active = false;
            });
            weatherModel.active = true;
        };

        $scope.weatherModels = [
            { index: 0, active: true },
            { index: 1, active: false },
            { index: 2, active: false },
            { index: 3, active: false }
        ];

        $scope.detailsVisible = false;

        $scope.feedback = {
            send: function() {
                weatherService.sendFeedback({
                    Email: this.email,
                    Name: this.name,
                    Text: this.text
                });
            }
        };

        $scope.subscription = {
            email: null,
            subscribe: function() {
                weatherService.subscribe({
                    Email: this.email,
                    Latitude: weatherAggregator.requestData.Location.Latitude,
                    Longitude: weatherAggregator.requestData.Location.Longitude,
                    AddressText: weatherAggregator.requestData.Location.AddressText,
                });
            }
        };

         var restoreSourceChecks = function() {
            var storedSources = localStorage.getItem('Sources');
            if (storedSources != null) {
                storedSources = JSON.parse(storedSources);
                $.each(storedSources, function(j, s) {
                    $.each($scope.sources, function(i, source) {
                        if (source.id == s.id) {
                            source.IsChecked = s.IsChecked;
                            return false;
                        }
                    });
                });
            }
         };

        $scope.saveSourceChecks = function() {
            var sources = [];
            $scope.sources.each(function(i, source) {
                sources.push({
                    Id: source.id,
                    IsChecked: source.IsChecked
                });
            });
            localStorage.setItem('Sources', JSON.stringify(sources));
        };

        $scope.toggleDetails= function () {
            $scope.detailsVisible = !$scope.detailsVisible;
        };

        $scope.getWeather = function () {
            var selectedSources = $("#sourcesDiv input:checked").map(function () {
                return $(this).data("sourceid");
            }).get();

            if (!selectedSources.length) {
                alert("Please, select at least 1 source");
            } else if (weatherAggregator.requestData.Location.Latitude == null || weatherAggregator.requestData.Location.Longitude == null) {
                alert("Please, select location");
            } else {
                weatherUtils.showWaiter();
                var data = {
                    Sources: selectedSources,
                    Location: {
                        Latitude: weatherAggregator.requestData.Location.Latitude,
                        Longitude: weatherAggregator.requestData.Location.Longitude,
                        AddressText: weatherAggregator.requestData.Location.AddressText,
                        Country: weatherAggregator.requestData.Location.Country,
                        Region: weatherAggregator.requestData.Location.Region,
                        City: weatherAggregator.requestData.Location.City
                    },
                    DateRange: {    
                        From: new Date(),
                        To: new Date()
                    }
                };

                data.DateRange.To.setDate(data.DateRange.To.getDate() + 3);

                weatherService.getWeather(data)
                    .success($scope.bindWeather)
                    .finally(function () {
                        weatherUtils.hideWaiter();
                    });
            }
        };

        $scope.bindWeather= function (data) {
            if (data && data.length) {
                $(data).each(function(index, element) {
                    element.index = index;
                    element.active = index == 0 ? "active" : "";
                    element.Precipitation = weatherUtils.getPrecipitation(element.Precipitation);
                    element.cloudInfo = weatherUtils.getCloudInfo(element.Cloudness);
                    element.day = weatherUtils.getDay(index);
                    $(element.Sources).each(function (ind, elm) {
                        elm.Precipitation = weatherUtils.getPrecipitation(elm.Precipitation);
                        elm.cloudInfo = weatherUtils.getCloudInfo(elm.Cloudness);
                    });
                    element.sourcedWeatherModels = element.Sources;
                });
                $scope.weatherModels = data;
                $("#tabsDiv").removeClass('hidden');
            }
        }
    };

    weatherController.$inject = ['$scope', '$timeout', '$sce', 'weatherUtils', 'weatherService'];
    angular.module('weatherApp').controller('weatherController', weatherController);
})();
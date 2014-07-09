﻿weatherAggregator.weatherPage = {

    requestData: { Location: {} },
    initWeatherApp: function() {
        var weatherApp = angular.module('weatherApp', []).config(function($sceProvider) {
            $sceProvider.enabled(false); //Yes, I've disabled it. And I'm happy with it.
        });

        //1 день, 2 дня, 3 дня, 4 дня, 5 дней, ..., 10 дней, 11 дней, ..., 20 дней, 21 день, 22 дня, 23 дня, 24 дня, 25 дней...
        weatherApp.filter('days', function () {
            return function (input) {
                var result = 'дней';

                if (input < 10 || input > 20) {
                    var modulus = input % 10;
                    if (modulus == 1) {
                        result = 'день';
                    } else if (modulus >= 2 && modulus <= 4) {
                        result = 'дня';
                    }
                }

                return input + ' ' + result;
            };
        });

        weatherApp.controller('WeatherController', function($scope, $timeout) {
            weatherAggregator.proxy.getSources(function(response) {
                $scope.$apply(function() {
                    $scope.sources = response;
                    $timeout(weatherAggregator.weatherPage.restoreSourceChecks);
                });
            }, weatherAggregator.weatherPage.onGetWeatherComplete);


            $scope.weatherModels = [{ index: 0 }, { index: 1 }, { index: 2 }, { index: 3 }];
            $scope.detailsVisible = false;
            $scope.detailsText = function() {
                return $scope.detailsVisible ? "Скрыть" : "Подробнее";
            };

            $scope.getWeather = weatherAggregator.weatherPage.getWeather;
            $scope.toggleDetails = weatherAggregator.weatherPage.toggleDetails;
            $scope.feedback = weatherAggregator.weatherPage.feedback;
            $scope.subscription = weatherAggregator.weatherPage.subscription;
        });
        
        $(window).on('beforeunload', function() {
            weatherAggregator.weatherPage.saveSourceChecks();
        });
    },

    feedback: {
        email: null,
        name: null,
        text: null,
        send: function() {
            weatherAggregator.proxy.sendFeedback({
                Email: this.email,
                Name: this.name,
                Text: this.text
            });
        }
    },

    subscription: {
        email: null,
        subscribe: function () {
            weatherAggregator.proxy.subscribe({
                Email: this.email,
                Latitude: weatherAggregator.weatherPage.requestData.Location.Latitude,
                Longitude: weatherAggregator.weatherPage.requestData.Location.Longitude,
                AddressText: weatherAggregator.weatherPage.requestData.Location.AddressText,
            });
        }
    },

    restoreSourceChecks: function () {
        var storedSources = localStorage.getItem('Sources');
        if (storedSources != null) {
            storedSources = JSON.parse(storedSources);
            $.each(storedSources, function (j, s) {
                $('#sourcesDiv').find('input[data-sourceid=' + s.Id + ']').prop('checked', s.IsChecked);
            });
        } else {
            $('#sourcesDiv').find('input[type=checkbox]').prop('checked', true);
        }
    },

    saveSourceChecks: function () {
        var sources = [];
        $('#sourcesDiv').find('input[type=checkbox]').each(function (i, e) {
            sources.push({
                Id: $(e).data('sourceid'),
                IsChecked: $(e).is(':checked')
            });
        });
        localStorage.setItem('Sources', JSON.stringify(sources));
    },

    init: function () {
        $(document).foundation();
        weatherAggregator.utils.showWaiter();
        $("#tabsDiv").on('click', "a[id^='tabDay']", function () {
            $("[id^='contenttabDay']").removeClass('active');
            var location = "[id$='content" + $(this).attr('id') + "']";
            $(location).addClass('active');
            $(".tab-title").removeClass('active');
            $(this).parent().addClass('active');
        });
    },

    toggleDetails: function () {
        var $scope = angular.element($("body")).scope();
        $scope.detailsVisible = !$scope.detailsVisible;
    },

    getWeather: function () {
        var selectedSources = $("#sourcesDiv input:checked").map(function () {
            return $(this).data("sourceid");
        }).get();

        if (!selectedSources.length) {
            alert("Пожалуйста, выберите хотя бы 1 сайт");
        } else if (weatherAggregator.weatherPage.requestData.Location.Latitude == null || weatherAggregator.weatherPage.requestData.Location.Longitude == null) {
            alert("Пожалуйста, выберите место на карте");
        } else {
            weatherAggregator.utils.showWaiter();
            var data = {
                Sources: selectedSources,
                Location: {
                    Latitude: weatherAggregator.weatherPage.requestData.Location.Latitude,
                    Longitude: weatherAggregator.weatherPage.requestData.Location.Longitude,
                    AddressText: weatherAggregator.weatherPage.requestData.Location.AddressText,
                    Country: weatherAggregator.weatherPage.requestData.Location.Country,
                    Region: weatherAggregator.weatherPage.requestData.Location.Region,
                    City: weatherAggregator.weatherPage.requestData.Location.City
                },
                DateRange: {    
                    From: new Date(),
                    To: new Date()
                }
            };

            data.DateRange.To.setDate(data.DateRange.To.getDate() + 3);
            weatherAggregator.proxy.getWeather(data, weatherAggregator.weatherPage.bindWeather, weatherAggregator.weatherPage.onGetWeatherComplete);
        }
    },

    bindWeather: function (data) {
        if (data && data.length) {
            $(data).each(function(index, element) {
                element.index = index;
                element.active = index == 0 ? "active" : "";
                element.Precipitation = weatherAggregator.utils.getPrecipitation(element.Precipitation);
                element.cloudInfo = weatherAggregator.utils.getCloudInfo(element.Cloudness);
                element.day = weatherAggregator.utils.getDay(index);
                $(element.Sources).each(function (ind, elm) {
                    elm.Precipitation = weatherAggregator.utils.getPrecipitation(elm.Precipitation);
                    elm.cloudInfo = weatherAggregator.utils.getCloudInfo(elm.Cloudness);
                });
                element.sourcedWeatherModels = element.Sources;
            });
            var $scope = angular.element($("body")).scope();
            $scope.$apply(function () { $scope.weatherModels = data; });
            $("#tabsDiv").removeClass('hidden');
        }
    },

    onGetWeatherComplete: function() {
        weatherAggregator.utils.hideWaiter();
    }
};

weatherAggregator.weatherPage.initWeatherApp();
$(weatherAggregator.weatherPage.init);
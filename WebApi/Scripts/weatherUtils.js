(function() {

    'use strict';

    function weatherUtils(days) {
        return {
            getDay: function(index) {
                if (index < 2) {
                    return days.recentDays[index];
                }
                var currentDayIndex = (new Date()).getDay();
                return days.daysOfWeek[(currentDayIndex + index) % 7];
            },

            getPrecipitation: function(index) {
                return days.precipitation[index];
            },

            showWaiter: function() {
                window.waiter.show({ targetId: 'body' });
            },
            hideWaiter: function() {
                window.waiter.hide({ targetId: 'body' });
            },

            getCloudInfo: function(cloudness) {
                var index = parseInt(days.cloudIcons.length * cloudness / 100, 10);
                return {
                    icon: days.cloudIcons[index],
                    title: days.cloudTitles[index],
                    percentage: cloudness
                };
            }
        }
    };

    weatherUtils.$inject = ['days'];
    angular.module('weatherApp').factory('weatherUtils', weatherUtils);
})();
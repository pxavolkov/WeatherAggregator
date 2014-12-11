(function () {
    "use strict";
    angular.module('weatherApp').constant('days', {
        recentDays : ["Today", "Tomorrow"],
        daysOfWeek: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
        precipitation: ['No precipitation', 'Rain', 'Snow', 'Hail'],
        cloudIcons : ['Sun.png', 'SunCloud.png', 'Cloud.png'],
        cloudTitles: ['Sunny', 'Partly cloudy', 'Cloudy']
    });
}());
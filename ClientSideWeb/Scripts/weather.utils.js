weatherAggregator.utils = {
    days: ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'],
    precipitation: ['Без осадков', 'Дождь', 'Снег', 'Град'],
    cloudIcons: ['Sun.png', 'SunCloud.png', 'Cloud.png'],

    getDayOfWeek: function (index) {
        var currentDayIndex = (new Date()).getDay();
        return this.days[(currentDayIndex + index) % 7];
    },

    getPrecipitation: function(index) {
        return this.precipitation[index];
    },

    showWaiter: function() {
        $("#waiterDiv").show();
    },
    hideWaiter: function () {
        $("#waiterDiv").hide();
    },

    getCloudIconUrl: function (cloudness) {
        var index = Math.trunc(this.cloudIcons.length * cloudness / 100);
        return this.cloudIcons[index];
    }
};
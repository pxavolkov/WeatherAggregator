weatherAggregator.utils = {
    recentDays : ["Сегодня", "Завтра"],
    daysOfWeek: ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'],
    precipitation: ['Без осадков', 'Дождь', 'Снег', 'Град'],
    cloudIcons: ['Sun.png', 'SunCloud.png', 'Cloud.png'],

    getDay: function (index) {
        if (index < 2) {
            return this.recentDays[index];
        }
        var currentDayIndex = (new Date()).getDay();
        return this.daysOfWeek[(currentDayIndex + index) % 7];
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
ymaps.ready(init);
defaultPlace = [55.76, 37.64]; // по умолчанию москва


function init() {
    var myPlacemark,
    myMap = new ymaps.Map('map-canvas', {
        center: defaultPlace,
        zoom: 9,
        controls: ['smallMapDefaultSet']
    });

    // Слушаем клик на карте
    myMap.events.add('click', function (e) {
        var coords = e.get('coords');
        setPlacemark(coords, false);

    });

    setPlacemark(defaultPlace, true);

    function setPlacemark(coords, centerize) {
        // Если метка уже создана – просто передвигаем ее
        if (myPlacemark) {
            myPlacemark.geometry.setCoordinates(coords);
        }
            // Если нет – создаем.
        else {
            myPlacemark = createPlacemark(coords);
            myMap.geoObjects.add(myPlacemark);

            // Слушаем событие окончания перетаскивания на метке.
            myPlacemark.events.add('dragend', function () {
                getAddress(myPlacemark.geometry.getCoordinates());
            });
        }

        storingCoordinates(coords);

        getAddress(coords);


        if (centerize) {
            myMap.setCenter(coords);
        }
        myPlacemark.balloon.close();
        myPlacemark.balloon.open();


    }

    // Создание метки
    function createPlacemark(coords) {
        return new ymaps.Placemark(coords, {
            iconContent: 'поиск...',
            balloonContent: 'поиск...'
        }, {
            preset: 'islands#violetStretchyIcon',
            draggable: true,
            balloonPanelMaxMapArea: 0
        });
    };

    // Определяем адрес по координатам (обратное геокодирование)
    function getAddress(coords) {
        myPlacemark.properties.set('iconContent', 'поиск...');
        myPlacemark.properties.set('balloonContent', 'поиск...');
        ymaps.geocode(coords).then(function (res) {
            var firstGeoObject = res.geoObjects.get(0);
            myPlacemark.properties.set({
                iconContent: firstGeoObject.properties.get('name'),
                balloonContent: firstGeoObject.properties.get('text')
            });

        });
    };

    function storingCoordinates(coords) {
        weatherAggregator.weatherPage.requestData.Location.Latitude = coords[0];
        weatherAggregator.weatherPage.requestData.Location.Longitude = coords[1];
    };

}
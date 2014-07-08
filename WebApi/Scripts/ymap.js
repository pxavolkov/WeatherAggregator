ymaps.ready(init);
defaultPlace = [55.76, 37.64]; // по умолчанию москва


function init() {
    var isSearch = false;
    var myPlacemark,
        myMap = new ymaps.Map('map-canvas', {
            center: defaultPlace,
            zoom: 9,
            controls: ['smallMapDefaultSet']
        });

    var searchControl = new ymaps.control.SearchControl({
        options: {
            kind: 'locality',
            useMapBounds : true,
            noSelect: false,
            noPlacemark: true,
            noPopup: false,

        }
    });
    searchControl.events.add('resultshow', function (event) {
        isSearch = true;
   
        //var coords = myMap.getCenter();
        //setPlacemark(coords, false);
    });

    searchControl.events.add('load', function (event) {
        // Проверяем, что это событие не "дозагрузки" результатов и
        // по запросу найден хотя бы один результат.
        if (!event.get('skip') && searchControl.getResultsCount()) {
            searchControl.showResult(0);
        }
    });

    myMap.controls.add(searchControl);

    // Слушаем клик на карте
    myMap.events.add('click', function(e) {
        var coords = e.get('coords');
        setPlacemark(coords, false);

    });

    myMap.events.add('boundschange', function(e) {
        if (isSearch) {
            var coords = e.get('newCenter');
            setPlacemark(coords, false);
            isSearch = false;
        }
    });

    setPlacemark(defaultPlace, true);
    setPlaceMartByGeolocation();


    function setPlaceMartByGeolocation() {
        ymaps.geolocation.get({
                provider: 'yandex',
                mapStateAutoApply: true
            })
            .then(function(result) {
                var coords = result.geoObjects.position;
                setPlacemark(coords, true);
            });

        ymaps.geolocation.get({
                provider: 'browser',
                mapStateAutoApply: true
            })
            .then(function(result) {
                var coords = result.geoObjects.position;
                setPlacemark(coords, true);
            });
    }

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
            myPlacemark.events.add('dragend', function() {
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
        ymaps.geocode(coords).then(function(res) {
            var firstGeoObject = res.geoObjects.get(0);
            myPlacemark.properties.set({
                iconContent: firstGeoObject.properties.get('name'),
                balloonContent: firstGeoObject.properties.get('text')
            });

            storingAddressText(firstGeoObject);
        });
    };

    function storingCoordinates(coords) {
        weatherAggregator.weatherPage.requestData.Location.Latitude = coords[0];
        weatherAggregator.weatherPage.requestData.Location.Longitude = coords[1];
    };

    function storingAddressText(firstGeoObject) {
        weatherAggregator.weatherPage.requestData.Location.AddressText =
            firstGeoObject.properties.get('text');
        weatherAggregator.weatherPage.requestData.Location.Country =
            firstGeoObject.properties.get('metaDataProperty.GeocoderMetaData.AddressDetails.Country.CountryName');
        weatherAggregator.weatherPage.requestData.Location.Region =
            firstGeoObject.properties.get('metaDataProperty.GeocoderMetaData.AddressDetails.Country.AdministrativeArea.AdministrativeAreaName');
        weatherAggregator.weatherPage.requestData.Location.City =
            firstGeoObject.properties.get('metaDataProperty.GeocoderMetaData.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea.SubAdministrativeAreaName');
    }

}
map = {};
var geocoder;
var map;
var marker;

function initialize() {
    geocoder = new google.maps.Geocoder();

    var latlng = new google.maps.LatLng(40.730885, -73.997383); // just hardcoded location for situation when user turn off geolocation in his browser. Can be changed to any other location

    var mapOptions = {
        zoom: 8,
        center: latlng,
        mapTypeId: 'roadmap'
    };

    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    //If geolocation is turned off move map center to users location
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = new google.maps.LatLng(position.coords.latitude,
                position.coords.longitude);
            map.setCenter(pos);
        });
    }

    //Add mouse click event
    google.maps.event.addListener(map, 'click', function (e) {
        codeLatLng(e.latLng, map);
    });
}

function codeLatLng(latlng, map) {
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                marker = new google.maps.Marker({
                    position: latlng,
                    map: map
                });
                weatherAggregator.weatherPage.requestData.Location.Latitude = latlng.lat();
                weatherAggregator.weatherPage.requestData.Location.Longitude = latlng.lng();
            } else {
                alert('No results found');
            }
        } else {
            alert('Geocoder failed due to: ' + status);
        }
    });
}

google.maps.event.addDomListener(window, 'load', initialize);
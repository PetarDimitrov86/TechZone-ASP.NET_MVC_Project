﻿function distance(lon1, lat1, lon2, lat2) {
    var R = 6371; // Radius of the earth in km
    var dLat = (lat2-lat1).toRad();  // Javascript functions in radians
    var dLon = (lon2-lon1).toRad();
    var a = Math.sin(dLat/2) * Math.sin(dLat/2) +
			Math.cos(lat1.toRad()) * Math.cos(lat2.toRad()) *
			Math.sin(dLon/2) * Math.sin(dLon/2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    var d = R * c; // Distance in km
    return d;
}

/** Converts numeric degrees to radians */
if (typeof(Number.prototype.toRad) === "undefined") {
    Number.prototype.toRad = function() {
        return this * Math.PI / 180;
    }
}

window.navigator.geolocation.getCurrentPosition(function (pos) {
    var finalDistance = distance(pos.coords.longitude, pos.coords.latitude, 23.3219, 42.6977);
    console.log(finalDistance);  // Sofia Coordinates
    var distanceText = 'Distance to you : ' + Math.round(finalDistance) + ' km.';
    $("#distanceCalc").text(distanceText);
    var shippingCosts = Math.round((Number(finalDistance) / 40) * 100) / 100;
    $("#shippingCosts").text('$' + shippingCosts);
    var itemsPrice = $('#itemsPrice').text();
    var priceWithShipment = '$' + Math.round((shippingCosts + Number(itemsPrice.substr(1))) * 100) / 100;
    $("#priceWithShipment").text(priceWithShipment);
});
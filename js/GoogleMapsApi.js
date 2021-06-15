var defaultBounds = new google.maps.LatLngBounds(new google.maps.LatLng(-30.5595, 30.9375),
    new google.maps.LatLng(-30.5595, 30.9375));


//map creation
var tuberMap = new google.maps.Map(document.getElementById('<%= map %>'), {
    center: { lat: -26.2485, lng: 27.8540 },
    bound: defaultBounds,
    zoom: 8
});

//marker creation
var marker = new google.maps.Marker({
    position: { lat: 30.5595, lng: 22.9375 },
    map: tuberMap
});
//var map = new google.maps.Map(document.getElementById("map"),mapOptions);
var input = document.getElementById('<%= location %>');

//map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

let autocomplete;

autocomplete = new google.maps.places.Autocomplete(input, {
    type: ['geocode'],
    componentRestrictions: { 'country': ['ZA'] },
    fields: ['place_id', 'geometry', 'name']
});

autocomplete.setFields(
    ['address_components', 'geometry', 'icon', 'name']);

// when a clicking event occurs the fields should be autocompleted  
var place;

/* Doesn't seem to be working
*
autocomplete.addListener('place_change',function (){
place = autocomplete.getPlace();
if(!place.geometry)
{
    //user didn't select a prediction; reset the input field
    document.getAnimations('<%= autocomplete%>').placeholder = "enter a place";
}else {
    document.getAnimations('autocomplete').innerHtML = place.name;
    var lat = place.geometry.location.lat();    
    
}
}); */

google.maps.event.addListener(autocomplete, 'place_changed', function () {
    var place = autocomplete.getPlace();
    if (!place.geometry) {
        return;
    }
    if (place.geometry.viewport) {

        //reload map with new property
        var lat_ = place.geometry.location.lat();
        var lng_ = place.geometry.location.lng();
        console.log(place.name);
        //put a marker for the selected location
        addMarkerWithProps({ lat: lat_, lng: lng_ });

        //addMarkerWithProps({lat: lat_, lng: lng_})

    } else {
        map.setCenter(place.geometry.location);
        map.setZoom(17);
    }
});

function addMarkerWithProps(coords) {
    //adding a marker
    var marker = new google.maps.Marker({
        position: coords,
        animation: google.maps.Animation.DROP,
        map: tuberMap
    });
}
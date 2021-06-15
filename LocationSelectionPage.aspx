<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationSelectionPage.aspx.cs" Inherits="QuadCore_Website.ApiTesting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tuber</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <link href="https://fonts.googleapis.com/css?family=Poppins:200,300,400,500,600,700,800,900&display=swap" rel="stylesheet" />

    <link rel="stylesheet" href="css/open-iconic-bootstrap.min.css" />
    <link rel="stylesheet" href="css/animate.css" />

    <link rel="stylesheet" href="css/owl.carousel.min.css" />
    <link rel="stylesheet" href="css/owl.theme.default.min.css" />
    <link rel="stylesheet" href="css/magnific-popup.css" />

    <link rel="stylesheet" href="css/aos.css" />

    <link rel="stylesheet" href="css/ionicons.min.css" />

    <link rel="stylesheet" href="css/flaticon.css" />
    <link rel="stylesheet" href="css/icomoon.css" />
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/HomePageStyling.css" />

    <link rel='stylesheet' type='text/css' media='screen' href='css/notification_styling.css' />
    <script src="https://kit.fontawesome.com/867944bbac.js" crossorigin="anonymous"></script>
</head>
<body>

    <form id="buttonForm" runat="server">
    
        <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
            <div class="container">

                <div class="form-group row justify-content-center">
                    <h1>Please enter the address</h1>
                </div>
                <div class="form-group row justify-content-center heading-section">
                    <h6 class="heading">Bringing the tutor to you. :)</h6>
                </div>

                <div class="row d-flex align-items-stretch no-gutters">

                    <div class="col-md-6 p-4 p-md-5 order-md-first" runat="server">
                        <!--Addres entered-->
                        <div class="form-group">
                            <label class="font-weight-bold">Location</label>
                            <input type="text" id="address" placeholder="Enter Meeting Location Here" class="form-control" runat="server" />
                        </div>

                        <!--logitude of location-->
                        <div class="form-group" runat="server" visible="true">
                            <label class="font-weight-bold">Longitude</label>
                            <input type="text" id="longitude" placeholder="" class="form-control" runat="server" readonly />
                        </div>

                        <!--Latitude of location-->
                        <div class="form-group" runat="server" visible="true">
                            <label class="font-weight-bold">latitude</label>
                            <input type="text" id="latitude" placeholder="" class="form-control" runat="server" readonly />
                        </div>

                        <div class="form-group row justify-content-center">
                            <asp:Button ID="btn_submit" runat="server" Text="return" class="btn btn-primary py-3 px-5" OnClick="btn_submit_Click" />
                        </div>
                        <div id="tutorViewButton" runat="server" visible="false" class="form-group row justify-content-center">
                            <asp:Button ID="enableTutorMaker" runat="server" Text=" " class="btn btn-primary py-3 px-5" />
                            <div id="Div1" runat="server" visible="false" class="form-group row justify-content-center">
                           
                        </div>
                        </div>
                    </div>
                    <div class="col-md-6 p-4 p-md-5 order-md-last" runat="server">
                        <div id="map" style="height: 600px; width: 150%;" runat="server"></div>
                    </div>

                </div>
            </div>

        </section>
    </form>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAf02KGmhqrFCU9DTbgiKy8dXSgFpb1hx4&libraries=places"></script>

    <script type="text/javascript" >
        var defaultBounds = new google.maps.LatLngBounds(new google.maps.LatLng(-30.5595, 30.9375),
            new google.maps.LatLng(-30.5595, 30.9375));


        //map creation
        var tuberMap = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -26.2485, lng: 27.8540 },
            bound: defaultBounds,
            zoom: 8
        });

        //var map = new google.maps.Map(document.getElementById("map"),mapOptions);
        var input = document.getElementById('address');

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
         
        google.maps.event.addListener(autocomplete, 'place_changed', function () {
            var place = autocomplete.getPlace();
            if (!place.geometry) {
                return;
            }
            if (place.geometry.viewport) {

                //reload map with new property
                var lat_ = place.geometry.location.lat();
                document.getElementById('<%= latitude.ClientID %>').value = lat_;
                    var lng_ = place.geometry.location.lng();
                    document.getElementById('<%= longitude.ClientID %>').value = lng_;
                    console.log(place.name);
                    //put a marker for the selected location
                    //tuberMap.center({ lat: lat_, lng: lng_ });
                    addMarkerWithProps({ lat: lat_, lng: lng_ });z

                    //addMarkerWithProps({lat: lat_, lng: lng_})

                } else {
                    map.setCenter(place.geometry.location);
                    map.setZoom(17);
                }
        });

        //
        function putMarkerForTutor() {
            var lat_ = ocument.getElementById('<%= latitude.ClientID %>').value;
            var lng_ = ocument.getElementById('<%= longitude.ClientID %>').value;

            if (lat_ != null) {
                addMarkerWithProps({ lat: lat_, lng: lng_ });
                console.log(lat_);
            }
           
        }
        //
        function addMarkerWithProps(coords) {
            //adding a marker
            var marker = new google.maps.Marker({
                position: coords,
                draggable: true,
                animation: google.maps.Animation.DROP,
                map: tuberMap
            });

            google.maps.event.addListener(marker, 'dragend', function () {
                geocodePosition(marker.getPosition());
            });

            function geocodePosition(pos) {
                geocoder = new google.maps.Geocoder();
                geocoder.geocode
                    ({
                        latLng: pos
                    },
                        function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK) {
                                document.getElementById('<%= latitude.ClientID %>').value = pos.lat();
                                    document.getElementById('<%= longitude.ClientID %>').value = pos.lng();

                                    //recenter map 
                                    tuberMap.setCenter(marker.position);
                                }
                                else {
                                    //alert location abstract
                                }
                            }
                        );
            }
        }


    </script>
     <script type="text/javascript" >
         var defaultBounds = new google.maps.LatLngBounds(new google.maps.LatLng(-30.5595, 30.9375),
             new google.maps.LatLng(-30.5595, 30.9375));


         //map creation
         var tuberMap = new google.maps.Map(document.getElementById('map'), {
             center: { lat: -26.2485, lng: 27.8540 },
             bound: defaultBounds,
             zoom: 8
         });

         //var map = new google.maps.Map(document.getElementById("map"),mapOptions);
         var input = document.getElementById('address');

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

         google.maps.event.addListener(autocomplete, 'place_changed', function () {
             var place = autocomplete.getPlace();
             if (!place.geometry) {
                 return;
             }
             if (place.geometry.viewport) {

                 //reload map with new property
                 var lat_ = place.geometry.location.lat();
                 document.getElementById('<%= latitude.ClientID %>').value = lat_;
                var lng_ = place.geometry.location.lng();
                document.getElementById('<%= longitude.ClientID %>').value = lng_;
                console.log(place.name);
                //put a marker for the selected location
                //tuberMap.center({ lat: lat_, lng: lng_ });
                addMarkerWithProps({ lat: lat_, lng: lng_ });

                //addMarkerWithProps({lat: lat_, lng: lng_})

            } else {
                map.setCenter(place.geometry.location);
                map.setZoom(17);
            }
        });

         //
         function putMarkerForTutor() {
             var lat_ = ocument.getElementById('<%= latitude.ClientID %>').value;
            var lng_ = ocument.getElementById('<%= longitude.ClientID %>').value;

             if (lat_ != null) {
                 addMarkerWithProps({ lat: lat_, lng: lng_ });
                 console.log(lat_);
             }

         }
         //
         function addMarkerWithProps(coords) {
             //adding a marker
             var marker = new google.maps.Marker({
                 position: coords,
                 draggable: true,
                 animation: google.maps.Animation.DROP,
                 map: tuberMap
             });

             google.maps.event.addListener(marker, 'dragend', function () {
                 geocodePosition(marker.getPosition());
             });

             function geocodePosition(pos) {
                 geocoder = new google.maps.Geocoder();
                 geocoder.geocode
                     ({
                         latLng: pos
                     },
                         function (results, status) {
                             if (status == google.maps.GeocoderStatus.OK) {
                                 document.getElementById('<%= latitude.ClientID %>').value = pos.lat();
                                document.getElementById('<%= longitude.ClientID %>').value = pos.lng();

                                //recenter map 
                                tuberMap.setCenter(marker.position);
                            }
                            else {
                                //alert location abstract
                            }
                        }
                    );
             }
         }


    </script>
</body>
</html>

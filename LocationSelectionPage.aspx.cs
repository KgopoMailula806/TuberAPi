using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class ApiTesting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.Session["Email"] != null)
                {
                    if (Request.QueryString["RirectType"] != null)
                    {
                        switch (Request.QueryString["RirectType"].ToString())
                        {
                            case "TutorMapViewBooking":
                                {
                                    if (Request.QueryString["EventType"] != null)
                                    {
                                        if (Request.QueryString["location"] != null)
                                        {
                                            //set the property visibility
                                            btn_submit.Visible = false;

                                            string[] locationContants = HelperMethods.separateString(Request.QueryString["location"].ToString(), '_');

                                            //Redirect to request tutor 
                                            address.Value = locationContants[0];
                                            latitude.Value = locationContants[1];
                                            longitude.Value = locationContants[2];
                                            //set run Marker script                                          
                                            doMapJustice();
                                        }
                                    }
                                }
                                break;
                            case "":
                                {

                                }
                                break;
                        }
                    }                                                                                 
                }
            }            
        } 

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            //Redirect to request tutor 
            string address_ = address.Value;
            string latitude_ = latitude.Value;
            string longitude_ = longitude.Value;
            if (longitude != null)
            {
                Response.Redirect("RequestTutor.aspx?Address=" + address_ + "&Latitude=" + latitude_ + "&Longitude=" + longitude_);
            }
            
        } 
        protected void doMapJustice()
        {
           string javaScript_ = "<script type=\"text/javascript\" >";
            javaScript_ += "var defaultBounds = new google.maps.LatLngBounds(new google.maps.LatLng(-30.5595, 30.9375),";
            javaScript_ += "new google.maps.LatLng(-30.5595, 30.9375));";

            //map creation
            javaScript_ += "var tuberMap = new google.maps.Map(document.getElementById('map'), {";
            javaScript_ += "center: { lat: -26.2485, lng: 27.8540 },";
            javaScript_ += "bound: defaultBounds,";
            javaScript_ += "zoom: 8";
            javaScript_ += "});";

            javaScript_ += "//var map = new google.maps.Map(document.getElementById(\"map\"),mapOptions);";
            javaScript_ += "var input = document.getElementById('address');";

            javaScript_ += "//map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);";

            javaScript_ += "let autocomplete;";

            javaScript_ += "autocomplete = new google.maps.places.Autocomplete(input, {";
            javaScript_ += "type: ['geocode'],";
            javaScript_ += "componentRestrictions: { 'country': ['ZA'] },";
            javaScript_ += "fields: ['place_id', 'geometry', 'name']";
            javaScript_ += "});";

            javaScript_ += "autocomplete.setFields(";
            javaScript_ += "['address_components', 'geometry', 'icon', 'name']);";
           
            javaScript_ += "function putMarkerForTutor() {";
            javaScript_ += "var lat_ = ocument.getElementById('<%= latitude.ClientID %>').value;";
            javaScript_ += "var lng_ = ocument.getElementById('<%= longitude.ClientID %>').value;";

            javaScript_ += "if (lat_ != null) {";
            javaScript_ += "addMarkerWithProps({ lat: lat_, lng: lng_ });";
            javaScript_ += "console.log(lat_);";
            javaScript_ += "}";

            javaScript_ += "}";


            javaScript_ += "function addMarkerWithProps(coords) {";
            javaScript_ += "//adding a marker";
            javaScript_ += "var marker = new google.maps.Marker({";
            javaScript_ += "position: coords,";
            javaScript_ += " draggable: true,";
            javaScript_ += " animation: google.maps.Animation.DROP,";
            javaScript_ += "map: tuberMap";
            javaScript_ += "});";

            javaScript_ += "google.maps.event.addListener(marker, 'dragend', function () {";
            javaScript_ += "geocodePosition(marker.getPosition());";
            javaScript_ += "});";

            javaScript_ += " function geocodePosition(pos) {";
            javaScript_ += " geocoder = new google.maps.Geocoder();";
            javaScript_ += " geocoder.geocode";
            javaScript_ += "({";
            javaScript_ += "latLng: pos";
            javaScript_ += "},";
            javaScript_ += "function (results, status) {";
            javaScript_ += "if (status == google.maps.GeocoderStatus.OK) {";
            javaScript_ += "    document.getElementById('<%= latitude.ClientID %>').value = pos.lat();";
            javaScript_ += "document.getElementById('<%= longitude.ClientID %>').value = pos.lng();";

            javaScript_ += "tuberMap.setCenter(marker.position);";
            javaScript_ += "}";
            javaScript_ += "else {";
            //alert location abstract
            javaScript_ += "}";
            javaScript_ += "}";
            javaScript_ += " );";
            javaScript_ += " }";
            javaScript_ += " }";


            Response.Write(javaScript_);
        }

    }
}
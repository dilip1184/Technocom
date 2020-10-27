function SetMapData(workoutHistoryId, wcfUrl) {
    $.ajax({
        url: "../../ExternalService.ashx",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { 'WorkoutHistoryId': workoutHistoryId },
        responseType: "json",
        success: function (historyList) {
            var markers = historyList;
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].Latitude, markers[0].Longitude),
                zoom: 15,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            //var infowindow = new google.maps.InfoWindow();
            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            var line;
            for (i = 0; i < markers.length; i++) {

                var data = markers[i]
                if (i == 0 || i == markers.length - 1) {
                    var myLatlng = new google.maps.LatLng(data.Latitude, data.Longitude);
                    var marker = new google.maps.Marker({
                        position: myLatlng,
                        map: map,
                        title: data.CreatedDate + "\n" + data.Timestamp
                    });
                }
                if (i == markers.length - 1) {
                    // alert(markers.length);
                    return;
                }
                else {
                    var data2 = markers[i + 1]
                    line = new google.maps.Polyline({
                        // path: [new google.maps.LatLng(18.641400, 72.872200), new google.maps.LatLng(26.084617537223437, 83.1977328658104)],
                        path: [new google.maps.LatLng(data.Latitude, data.Longitude), new google.maps.LatLng(data2.Latitude, data2.Longitude)],
                        strokeColor: "#FF0000",
                        strokeOpacity: 1.0,
                        strokeWeight: 1,
                        map: map
                    });

                }
                (function (map, data) {
                    google.maps.event.addListener(map, "click", function (event) {
                        var infowindow = new google.maps.InfoWindow();
                        // infoWindow.setContent(data.description);
                        var contentString = "<b>Location Window</b><br />";
                        contentString += "Clicked Location: <br />" + data.Latitude + "," + data.Longitude + "<br />";
                        contentString += "Date & Time: <br />" + data.CreatedDate + "," + data.Timestamp + "<br />";
                        contentString += "Speed: <br />" + data.Speed;
                        infowindow.setContent(contentString);
                        infowindow.setPosition(event.latLng);
                        infowindow.open(map);
                        //infowindow.open(map, marker);
                    });
                })(map, data);
            }
        },
        error: function (xhr) {
            alert("sdss");
            alert("ERROR : " + xhr.responseText);
        }
    });
}


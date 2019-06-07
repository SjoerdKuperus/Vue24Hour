<template>
    <div>
        <h2>Mijn locatie</h2>
        <div class="horizontalLine"></div>
        <p>
            <div>{{locationName}}</div>
            <div>Om de locatie op te halen moet je dit toestaan in de browser.</div>
            <br />
            <button class="btn btn-secondary" @click="getLocation()">Geef locatie</button>
        </p>
        <div class="horizontalLine"></div>
        <!--MapboxGLjs CSS-->
        <link href='https://api.tiles.mapbox.com/mapbox-gl-js/v0.44.0/mapbox-gl.css' rel='stylesheet' />
        <!--The div in which the map will be created-->
        <div id="map-container"></div>
        <div class="horizontalLine"></div>
        <router-link to="/">
            <button class="btn btn-secondary">Terug naar overzicht</button>
        </router-link>
    </div>
</template>

<script>
    export default {
        computed: {
            locationName() {
                return this.$store.state.locationName;
            },
        },
        created() {
            let mapBoxScript = document.createElement('script')
            mapBoxScript.setAttribute('src', 'https://api.tiles.mapbox.com/mapbox-gl-js/v0.44.0/mapbox-gl.js')
            document.head.appendChild(mapBoxScript)
        },
        methods: {
            savePosition(position) {
                this.$store.dispatch('setPostion', position);
            },
            getLocation() {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(this.savePosition);
                    this.doMapStuff();
;                } else {
                    x.innerHTML = "Geolocation is not supported by this browser.";
                }
            },
            doMapStuff() {
                var coords = this.$store.state.locationCoords;
                var center = [coords.longtitude, coords.latitude];
                console.log("center = " + center);
                var map = new mapboxgl.Map({
                    container: 'map-container',
                    style: 'https://geodata.nationaalgeoregister.nl/beta/topotiles-viewer/styles/achtergrond.json',
                    zoom: 10.5,
                    center: center
                });
                // Add zoom and rotation controls to the map.
                map.addControl(new mapboxgl.NavigationControl(), "top-left");


                //Fake data for grid (16 posities)

                //Start coordinates (around houten)
                var lat_start = 51.98;
                var lon_start = 5.10;

                var startPolyGon = this.getPolygon(lat_start, lon_start, 0, 0);
                var startPolyGon2 = this.getPolygon(lat_start, lon_start, 1, 0);

                var gridData2 = [];

                for (var i = 0; i < 10; i++) {
                    for (var j = 0; j < 10; j++) {
                        gridData2.push(this.getPolygon(lat_start, lon_start, i, j));
                    }
                }

                map.on('load', function () {
                    // add custom grid layer?
                    var customGridLayer = {
                        "id": "gridTest",
                        "type": "background",
                        "paint": {
                            "background-color": "#00ffff"
                        }
                    };
                    //map.addLayer(customGridLayer);
                    //map.removeLayer("background");
                    
                    map.addLayer({
                        'id': 'houtenGrid1',
                        'type': 'fill',
                        'source': {
                            'type': 'geojson',
                            'data': {
                                'type': 'Feature',
                                'geometry': {
                                    'type': 'Polygon',
                                    'coordinates': gridData2
                                }
                            }
                        },
                        'layout': {},
                        'paint': {
                            'fill-color': '#088',
                            'fill-opacity': 0.8
                        }
                    });
                });
            },
            getPolygon(lat_start, lon_start, x, y) {
                //1 degree latitude ~ 111.2 km.
                // 1km = 0.089 in degrees.
                var lat_change = 1 / 111.2;
                var lon_change = 1 / (Math.abs(111.2 * Math.cos(lat_start * Math.PI / 180)));

                var result = [
                    [lon_start + (x * lon_change), lat_start + (y * lat_change)],
                    [lon_start + (x * lon_change) + lon_change, lat_start + (y * lat_change)],
                    [lon_start + (x * lon_change) + lon_change, lat_start + (y * lat_change) + lat_change],
                    [lon_start + (x * lon_change), lat_start + (y * lat_change)+ lat_change],
                    [lon_start + (x * lon_change), lat_start + (y * lat_change)]];
                return result;
            },
        }
    }
</script>

<style>
    #map-container {
        height: 400px;
    }
</style>

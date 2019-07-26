<template>
    <div class="dashboard">
        <h2>Welkom {{name}}</h2>
        <div class="horizontalLine"></div>
        <div class="alert alert-info alert-dismissible fade show" role="alert" v-if="dashboardMessage !== ''">
            {{dashboardMessage}}
            <button type="button" class="close" aria-label="Close" @click="closeAlert">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <p class="container">
            <div class="row">
                <div class="gameStatus col-2">Status:</div><div class="col-10">{{statusMessage}}</div>
            </div>
        </p>
        <div class="horizontalLine"></div>
        <template v-if="!inRunningGame">
            <router-link to="/listGames">
                <button class="btn btn-secondary">Toon spellen</button>
            </router-link>
            <router-link to="/startNewGame" class="float-right">
                <button class="btn btn-secondary">Begin nieuw spel</button>
            </router-link>
        </template>
        <template v-if="inRunningGame">
            <div class="countdown-clock">
                <div class="countdown-clockTime">{{countDown}}</div>
            </div>
            <div class="team-points">Team <span v-bind:style="{'color': currentTeam.color}">{{currentTeam.name}}</span> heeft <span class="team-points-score">{{currentTeam.score}}</span> punten</div>
            <p>
                <h3>Locatie</h3>
                <div>{{locationName}}</div>
                <!--The div in which the map will be created-->
                <div id="map-container"></div>
            </p>
        </template>
    </div>
</template>

<script>
    import GameItem from './GameItem'
    import shared from './shared'
    import moment from 'moment';

    export default {        
        data() {
            return { dateNow: '', }
        },
        components: { GameItem },
        mounted() {
            this.$store.dispatch('getAllGames').then(this.showMap).then(this.getLocation);
            this.interval = setInterval(this.setGameTime, 1000);
        },
        computed: {
            name() {
                return this.$store.state.userName;
            },            
            games() {
                return this.$store.state.games;
            },
            dashboardMessage() {
                return this.$store.state.dashboardMessage;
            },
            myCurrentGame() {
                var userName = this.$store.state.userName;
                var myCurrentGame = null;
                this.games.forEach(function (game) {
                    if (shared.isPlayerInTeams(userName, game.teams)) {
                        myCurrentGame = game;
                    }
                });
                return myCurrentGame;
            },
            statusMessage() {
                if (this.myCurrentGame !== null) {
                    if (this.myCurrentGame.gameState === 'Startup') {
                        return "Je doet mee aan '" + this.myCurrentGame.name + "', er wordt druk gezocht naar deelnemers.";
                    }
                    if (this.myCurrentGame.gameState === 'Running') {
                        return "Je doet mee aan '" + this.myCurrentGame.name + "'. Trek er op uit en verover de quadranten!";
                    }
                }
                return "Je doet op dit moment niet mee aan een spel. Wil je je inschrijven voor het volgende spel?";
            },
            inRunningGame() {
                return this.myCurrentGame !== null && this.myCurrentGame.gameState === 'Running';
            },
            currentTeam() {
                var userName = this.$store.state.userName;
                if (this.myCurrentGame !== null) {
                    return shared.getTeamFromCurrentUser(userName, this.myCurrentGame);
                }                
            },            
            countDown() {
                if (this.myCurrentGame !== null) {
                    var test = this.dateNow;
                    var startDate = moment(this.myCurrentGame.startDate + " " + this.myCurrentGame.startTime, "DD-MM-YYYY HH:mm");
                    var toNow = moment(startDate).diff(moment(), 'seconds');
                    var duration = moment.duration(toNow, 'seconds').asMilliseconds();
                    var timeLeft = moment.utc(duration).format("HH:mm:ss");
                    return timeLeft;
                }
                return "";
            },
            locationName() {
                return this.$store.state.locationName;
            },
        },
        methods: {
            closeAlert() {
                this.$store.state.dashboardMessage = "";
            },
            setGameTime() {
                if (this.myCurrentGame !== null) {
                    var today = new Date();
                    this.dateNow = today.toLocaleString('nl-NL', { timeZone: 'Europe/Amsterdam' });
                }
            },
            showMap() {
                if (this.myCurrentGame !== null) {
                    var center = this.myCurrentGame.centerLocationCoords;
                    var map = new mapboxgl.Map({
                        container: 'map-container',
                        style: 'https://geodata.nationaalgeoregister.nl/beta/topotiles-viewer/styles/achtergrond.json',
                        zoom: 13.2,
                        center: center,
                        interactive: false,
                    });
                    // Add zoom and rotation controls to the map.
                    //map.addControl(new mapboxgl.NavigationControl(), "top-left");
                    var componentContext = this;
                    map.on('load', function () {
                        componentContext.addMapSource(this);
                    });
                    componentContext.mapboxMap = map;
                }
            },
            addMapSource(map) {
                var features = this.createFeatureData();
                var gridDataJson = this.getGridDataJson(features);
                //Source with the feature data in GeoJson format.
                map.addSource("gridData", gridDataJson);
                //Draw all the polygons with an color based on the color property of the feature.
                map.addLayer({
                    "id": "TestPolyGons",
                    "type": "fill",
                    "source": "gridData",
                    "paint": {
                        'fill-color': ['get', 'color'],
                        "fill-opacity": 0.4,
                        "fill-outline-color": "#333333"
                    },
                    "filter": ["==", "$type", "Polygon"]
                });

                //TEMP does not work yet
                //map.addLayer({
                //    "id": "points",
                //    "type": "symbol",
                //    "source": {
                //        "type": "geojson",
                //        "data": {
                //            "type": "FeatureCollection",
                //            "features": [{
                //                "type": "Feature",
                //                "geometry": {
                //                    "type": "Point",
                //                    "coordinates": [this.$store.state.locationCoords.longtitude, this.$store.state.locationCoords.latitude]
                //                },
                //                "properties": {
                //                    "icon": {
                //                        "iconUrl": 'https://www.mapbox.com/mapbox.js/assets/images/astronaut2.png',
                //                        "iconSize": [50, 50], // size of the icon
                //                        "iconAnchor": [25, 25], // point of the icon which will correspond to marker's location
                //                        "popupAnchor": [0, -25], // point from which the popup should open relative to the iconAnchor
                //                        "className": 'dot'
                //                    }
                //                }
                //            }]
                //        }
                //    }
                //});

                //var geojson = [
                //    {
                //        type: 'Feature',
                //        geometry: {
                //            type: 'Point',
                //            coordinates: [this.$store.state.locationCoords.longtitude, this.$store.state.locationCoords.latitude]
                //        }
                //    }                    
                //];

                //var mapGeo = L.mapbox.map('map-container')
                //    //.setView([37.8, -96], 4)
                //    .addLayer(L.mapbox.styleLayer('mapbox://styles/mapbox/light-v10'));
                //var mapGeo = map.addLayer(L.mapbox.styleLayer('mapbox://styles/mapbox/light-v10'));
                //var myLayer = L.mapbox.featureLayer().setGeoJSON(geojson).addTo(mapGeo);
            },
            updateMapSource() {
                if (this.mapboxMap != undefined && this.mapboxMap != null) {
                    var features = this.createFeatureData();
                    var newData = {
                        "type": "FeatureCollection",
                        "features": features
                    };
                    var test = this.mapboxMap.getSource("gridData");
                    test.setData(newData);
                }
            },
            createFeatureData() {
                var features = [];
                if (this.myCurrentGame !== null) {
                    var colors = ["#FF4747", "#FF9A47", "#35BDBD", "#3EDE3E", "#35BD79"];
                    var colorCount = 0;
                    for (var i = 0; i < this.myCurrentGame.quadrants.length; i++) {
                        var quadrant = this.myCurrentGame.quadrants[i];
                        var polygonColor = "#000000";

                        if (this.eventItems != null && this.eventItems.length > 0) {
                            var currentControlEvent = this.eventItems[this.eventCounter];
                            var quadrantId = currentControlEvent.quadrantId;
                            var teamColor = currentControlEvent.teamColor;

                            if (quadrantId == quadrant.id) {
                                polygonColor = teamColor;
                            }
                        }

                        var newFeature = {
                            type: "Feature",
                            properties: {
                                "color": polygonColor
                            },
                            geometry: {
                                type: "Polygon",
                                coordinates: [this.getPolygon(quadrant)]
                            }
                        };
                        features.push(newFeature);
                        colorCount++;
                        if (colorCount == 5) {
                            colorCount = 0;
                        }
                    }

                    //Add current location point.
                    var newFeature = {
                        type: "Feature",
                        properties: {
                            "color": polygonColor
                        },
                        geometry: {
                            type: "Point",
                            coordinates: [this.$store.state.locationCoords]
                        }
                    };
                    features.push(newFeature);
                }
                return features;
            },
            getGridDataJson(features) {
                return {
                    "type": "geojson",
                    "data": {
                        "type": "FeatureCollection",
                        "features": features
                    }
                };
            },
            getPolygon(quadrant) {
                var result = [];
                if (quadrant !== null && quadrant !== undefined) {
                    for (var i = 0; i < quadrant.border.length; i++) {
                        var longtitude = quadrant.border[i].borderPoint[0];
                        var latitude = quadrant.border[i].borderPoint[1];
                        result.push([longtitude, latitude]);
                    }
                }
                return result;
            },
            getLocation() {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(this.savePosition);

                } else {
                    x.innerHTML = "Geolocation is not supported by this browser.";
                }
            },
            savePosition(position) {
                this.$store.dispatch('setPostion', position);
            },
        },
        beforeDestroy() {
            clearInterval(this.interval)
        }
    }
</script>
<style>
    .gameStatus {
        color: white;
    }
    .countdown-clock {
        width: 100%;
        text-align: center;
        color: #daf6ff;
        font-family: 'Share Tech Mono', monospace;
        font-size: 20px;
    }
    .countdown-clockTime {
        font-size: 40px;
        letter-spacing: 0.05rem;
        text-shadow: 0 0 20px rgba(10, 175, 230, 1), 0 0 20px rgba(10, 175, 230, 0);
    }
    .team-points {
        font-size: 20px;
        width: 100%;
        text-align: center;
    }
    .team-points-score {
        font-size: 30px;
        color: #daf6ff;
        text-shadow: 0 0 20px rgba(10, 175, 230, 1), 0 0 20px rgba(10, 175, 230, 0);
    }
</style>

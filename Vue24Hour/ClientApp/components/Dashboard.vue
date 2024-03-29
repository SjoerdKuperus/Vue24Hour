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
                <h3>Kaart <button class="btn btn-secondary float-right" @click="claimPolygon">verover kwadrant</button></h3>                
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
        watch: {
            countDown(newValue) {
                //When the time changes. Set the location again.
                //TODO: for now leave off.
                this.getLocation();
                this.updateMapPointerSource();
                this.updateMapSource();
            }
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
            currentLocation() {
                return [this.$store.state.locationCoords.longtitude, this.$store.state.locationCoords.latitude];
            }
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

                var center = this.myCurrentGame.centerLocationCoords;
                map.addSource("centerPoint", {
                    "type": "geojson",
                    "data": {
                        "type": "FeatureCollection",
                        "features": [{
                            "type": "Feature",
                            "geometry": {
                                "type": "Point",
                                "coordinates": center
                            }
                        }]
                    }
                });
                map.loadImage('images/icon-pointer-test.png', function (error, image) {
                    if (error) throw error;
                    map.addImage('pointer', image);
                    map.addLayer({
                        "id": "centerPointLayer",
                        "type": "symbol",
                        "source": "centerPoint",
                        "layout": {
                            "icon-image": "pointer",
                            "icon-size": 1,
                            "icon-offset": [0, -19], //Image is 37px height.
                        }
                    });
                });
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
            updateMapPointerSource() {
                if (this.mapboxMap != undefined && this.mapboxMap != null) {
                    //Update location pointer
                    var newCenterPointSource = {
                        "type": "FeatureCollection",
                        "features": [{
                            "type": "Feature",
                            "geometry": {
                                "type": "Point",
                                "coordinates": this.currentLocation
                            }
                        }]
                    };
                    var centerPointSource = this.mapboxMap.getSource("centerPoint");
                    if (centerPointSource !== undefined) {
                        centerPointSource.setData(newCenterPointSource);
                    }
                }
            },
            createFeatureData() {
                var features = [];
                if (this.myCurrentGame !== null) {
                    for (var i = 0; i < this.myCurrentGame.quadrants.length; i++) {
                        var quadrant = this.myCurrentGame.quadrants[i];
                        var polygonColor = "#000000";
                        var currentEvents = this.getCurrentEventsFromGame(this.myCurrentGame);
                        if (currentEvents.length > 0) {
                            for (var k = 0; k < currentEvents.length; k++) {
                                var currentControlEvent = currentEvents[k];
                                var quadrantId = currentControlEvent.quadrantId;
                                var teamColor = currentControlEvent.teamColor;
                                if (quadrantId == quadrant.id) {
                                    polygonColor = teamColor;
                                }
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
            getRandomColor() {
                var letters = '0123456789ABCDEF';
                var color = '#';
                for (var i = 0; i < 6; i++) {
                    color += letters[Math.floor(Math.random() * 16)];
                }
                return color;
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
            getCurrentEventsFromGame(game) {
                var resultList = [];
                var now = moment();
                for (var i = 0; i < game.controlEvents.length; i++) {
                    var controlEvent = game.controlEvents[i];
                    var startDateTime = moment(controlEvent.startDateTime);
                    var endDateTime = moment(controlEvent.endDateTime);
                    if (startDateTime < now && endDateTime > now) {
                        resultList.push(controlEvent);
                    }
                }
                return resultList;
            },
            savePosition(position) {
                this.$store.dispatch('setPostion', position);
            },
            claimPolygon() {
                this.$store.dispatch('claimPostion', {
                    position: this.$store.state.locationCoords,
                    myCurrentGame: this.myCurrentGame,
                    username: this.$store.state.userName
                });
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

<template>
    <div class="dashboard container-fluid">
        <h2>{{game.name}} ({{game.startDate}})</h2>
        <div class="horizontalLine"></div>
        <p>
            <div class="row">
                <div class="col-3 label">Naam</div><div class="col-9" v-text="game.name"></div>
            </div>
            <div class="row">
                <div class="col-3 label">Locatie</div><div class="col-9" v-text="game.location"></div>
            </div>
            <div class="row">
                <div class="col-3 label">Datum</div><div class="col-9" v-text="game.startDate"></div>
            </div>
            <div class="row">
                <div class="col-3 label">Deelnemers</div><div class="col-9">{{game.activeParticipants}}/{{game.maximumParticipants}}</div>
            </div>
            <div class="row">
                <div class="col-3 label">Game status</div><div class="col-9" v-text="game.gameState"></div>
            </div>
            <div class="row">
                <div class="col-3 label">Kwadranten</div><div class="col-9" v-text="game.quadrantCount"></div>
            </div>
            <div class="row">
                <div class="col-3 label">Middenpunt</div><div class="col-9" v-text="game.gameCenter"></div>
            </div>
        </p>
        <p>
            <h4>Teams</h4>
            <div class="team-container">
                <team-item v-for="(team, teamIndex) in teams" :key="team.id" :item="team" v-bind:gameState="game.gameState" v-bind:maxTeamSize="maxTeamSize"></team-item>
            </div>
            <div class="clearfix"></div>
        </p>
        <div class="horizontalLine"></div>
        <p>
            <h3>Kaart</h3>
            <!--The div in which the map will be created-->
            <div id="map-container"></div>
        </p>
        <div class="horizontalLine"></div>
        <p>
            <div class="row">
                <div v-if="gameStateStartup" class="col-12"><button v-on:click="activateGame" class="btn btn-primary">Activate game</button></div>
                <div v-else class="col-12"><button v-on:click="createTestEvents" class="btn btn-primary">Create test events</button></div>
            </div>
            <div class="row eventHeader">
                <h3 class="col-lg-2">Events</h3>
                <div class="col-lg-10 eventControls" v-if="eventItems.length > 0">
                    <button class="btn btn-primary btn-sm" v-on:click="prevEvent" v-bind:disabled="this.eventCounter === 0">prev</button>
                    <button class="btn btn-primary btn-sm" v-on:click="nextEvent" v-bind:disabled="this.eventCounter === this.eventItems.length - 1">next</button>
                    <span>{{eventCounter}}</span>
                </div>
            </div>
            <event-item v-for="(eventItem, index) in eventItems" :key="eventItem.id" :item="eventItem" :index="index" :eventCounter="eventCounter"></event-item>
        </p>
        <div class="horizontalLine"></div>
        <router-link to="/listGames">
            <button class="btn btn-primary">Terug</button>
        </router-link>
    </div>
</template>

<script>
    import GameItem from './GameItem'
    import TeamItem from './TeamItem'
    import EventItem from './EventItem'

    export default {
        components: { GameItem, TeamItem, EventItem},
        mounted() {
            this.$store.dispatch('getGame', this.$route.params.id).then(this.showMap);
        },
        data: function () {
            return {
                eventCounter: 0,
                mapboxMap : null
            }            
        },
        computed: {
            name() {
                return this.$store.state.userName
            },
            game() {
                var game = this.$store.state.game || { 'startDate': "" };
                return game;
            },
            gameStateStartup() {
                return (this.$store.state.game && this.$store.state.game.gameState === "Startup");
            },
            teams() {
                return this.$store.state.game != null ? this.$store.state.game.teams : [];
            },
            eventItems() {
                return this.$store.state.game != null ? this.$store.state.game.controlEvents : [];
            },
            maxTeamSize() {
                return Math.ceil((this.game.maximumParticipants / this.teams.length));
            }
        },
        created() {},
        updated() {},
        methods: {
            activateGame() {
                this.$store.dispatch('activateGame', {
                    id: this.$store.state.game.id
                });
            },
            createTestEvents() {
                this.$store.dispatch('createTestEvents', {
                    id: this.$store.state.game.id
                });
            },
            prevEvent() {
                if (this.eventCounter > 0) {
                    this.eventCounter -= 1;
                    this.updateMapSource();
                }
            },
            nextEvent() {
                if (this.eventCounter < this.eventItems.length - 1) {
                    this.eventCounter += 1;
                    this.updateMapSource();
                }
            },
            showMap() {
                var center = this.$store.state.game.centerLocationCoords;
                var map = new mapboxgl.Map({
                    container: 'map-container',
                    style: 'https://geodata.nationaalgeoregister.nl/beta/topotiles-viewer/styles/achtergrond.json',
                    zoom: 13,
                    center: center
                });
                // Add zoom and rotation controls to the map.
                map.addControl(new mapboxgl.NavigationControl(), "top-left");
                var componentContext = this;
                map.on('load', function () {
                    componentContext.addMapSource(this);                    
                });
                componentContext.mapboxMap = map;
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
                var colors = ["#FF4747", "#FF9A47", "#35BDBD", "#3EDE3E", "#35BD79"];
                var colorCount = 0;
                for (var i = 0; i < this.$store.state.game.quadrants.length; i++) {
                    var polygonColor = "#000000";

                    if (this.eventItems != null && this.eventItems.length > 0) {
                        var currentControlEvent = this.eventItems[this.eventCounter];
                        var quadrantId = currentControlEvent.quadrantId;
                        var teamColor = currentControlEvent.teamColor;

                        if (quadrantId == this.$store.state.game.quadrants[i].id) {
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
                            coordinates: [this.getPolygon(this.$store.state.game.quadrants[i])]
                        }
                    };
                    features.push(newFeature);
                    colorCount++;
                    if (colorCount == 5) {
                        colorCount = 0;
                    }
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
                for (var i = 0; i < quadrant.border.length; i++) {
                    var longtitude = quadrant.border[i].borderPoint[0];
                    var latitude = quadrant.border[i].borderPoint[1];
                    result.push([longtitude, latitude]);
                }
                return result;
            },
        }
    }
</script>
<style>
    .eventHeader {
        margin-bottom: 10px;
    }
    .eventControls {
        padding-top: 20px;
    }
    .label{
        color: #EEE;
    }
    .team-container {
        
    }
</style>

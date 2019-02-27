<template>
    <div class="dashboard">
        <h2>Game: {{game.startDate}} </h2>
        <hr />
        <p>
            <h3>Informatie over deze game:</h3>
            <div class="row">
                <div class="col-sm-3">Naam:</div><div class="col-sm-9" v-text="game.name"></div>
            </div>
            <div class="row">
                <div class="col-sm-3">Datum:</div><div class="col-sm-9" v-text="game.startDate"></div>
            </div>
            <div class="row">
                <div class="col-sm-3">Game status:</div>
                <div class="col-sm-3" v-text="game.gameState"></div>
                <div v-if="gameStateStartup" class="col-sm-6"><button v-on:click="activateGame" class="btn">Activate game</button></div>
            </div>
            <div class="row">
                <div class="col-sm-3">Aantal kwadranten:</div><div class="col-sm-9" v-text="game.quadrantCount"></div>
            </div>
            <div class="row">
                <div class="col-sm-3">Middenpunt:</div><div class="col-sm-9" v-text="game.gameCenter"></div>
            </div>
        </p>
        <hr />
        <p>
            <h3>Teams</h3>
        </p>
        <hr />
        <p>
            <h3>Kaart</h3>
            <!--MapboxGLjs CSS-->
            <link href='https://api.tiles.mapbox.com/mapbox-gl-js/v0.44.0/mapbox-gl.css' rel='stylesheet' />
            <!--The div in which the map will be created-->
            <div id="map-container"></div>
        </p>
        <router-link to="/">Terug</router-link>
    </div>
</template>

<script>
    import GameItem from './GameItem'

    export default {
        components: {GameItem},
        mounted() {
            this.$store.dispatch('getGame', this.$route.params.id);
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
            }
        },
        created() {
            let mapBoxScript = document.createElement('script')
            mapBoxScript.setAttribute('src', 'https://api.tiles.mapbox.com/mapbox-gl-js/v0.44.0/mapbox-gl.js')
            document.head.appendChild(mapBoxScript)
        },
        updated() {
            this.showMap();
        },
        methods: {
            activateGame() {
                this.$store.dispatch('activateGame', {
                    id: this.$store.state.game.id                    
                })
            },
            showMap() {
                var center = this.$store.state.game.centerLocationCoords;
                var map = new mapboxgl.Map({
                    container: 'map-container',
                    style: 'https://geodata.nationaalgeoregister.nl/beta/topotiles-viewer/styles/achtergrond.json',
                    zoom: 15,
                    center: center
                });
                // Add zoom and rotation controls to the map.
                map.addControl(new mapboxgl.NavigationControl(), "top-left");

                var features = [];
                var colors = ["#FF4747", "#FF9A47", "#35BDBD", "#3EDE3E"];
                var colorCount = 0;
                for (var i = 0; i < this.$store.state.game.quadrants.length; i++) {
                    var newFeature = {
                        type: "Feature",
                        properties: {
                            "color": colors[colorCount]
                        },
                        geometry: {
                            type: "Polygon",
                            coordinates: [this.getPolygon(this.$store.state.game.quadrants[i])]
                        }
                    };
                    features.push(newFeature);
                    colorCount++;
                    if (colorCount == 3) {
                        colorCount = 0;
                    }
                }
                //console.log(JSON.stringify(features));

                map.on('load', function () {
                    //Source with the feature data in GeoJson format.
                    map.addSource("gridData", {
                        "type": "geojson",
                        "data": {
                            "type": "FeatureCollection",
                            "features": features 
                        }
                    });

                    //Draw all the polygons with an color based on the color property of the feature.
                    map.addLayer({
                        "id": "TestPolyGons",
                        "type": "fill",
                        "source": "gridData",
                        "paint": {
                            'fill-color': ['get', 'color'],
                            "fill-opacity": 0.8
                        },
                        "filter": ["==", "$type", "Polygon"]
                    });
                });
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
<style></style>

<template>
    <div class="dashboard">
        <h2>Game: {{game.startDate}} </h2>
        <hr />
        <p>
            Informatie over deze game:
        </p>
        <hr />
        <p>
            <div class="row">
                <div class="col-sm-3">Naam:</div><div class="col-sm-9" v-text="game.name"></div>
            </div>
            <div class="row">
                <div class="col-sm-3">Datum:</div><div class="col-sm-9"  v-text="game.startDate"></div>
            </div>
            <div class="row">
                <div class="col-sm-3">Aantal kwadranten:</div><div class="col-sm-9"  v-text="game.quadrantCount"></div>
            </div>
            <div class="row">
                <div class="col-sm-3">Middenpunt:</div><div class="col-sm-9"  v-text="game.gameCenter"></div>
            </div>
        </p>
        <p>
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
            showMap() {
                var center = this.$store.state.game.centerLocationCoords;
                console.log("center = " + center);
                var map = new mapboxgl.Map({
                    container: 'map-container',
                    style: 'https://geodata.nationaalgeoregister.nl/beta/topotiles-viewer/styles/achtergrond.json',
                    zoom: 16,
                    center: center
                });
                // Add zoom and rotation controls to the map.
                map.addControl(new mapboxgl.NavigationControl(), "top-left");

                var gridData2 = [];
                for (var i = 0; i < this.$store.state.game.quadrants.length; i++) {
                    gridData2.push(this.getPolygon(this.$store.state.game.quadrants[i]));
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
</style>

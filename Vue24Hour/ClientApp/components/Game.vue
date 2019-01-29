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
        methods: {            
        }
    }
</script>

<style>
</style>

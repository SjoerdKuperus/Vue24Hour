<template>
    <div>
        <h2>Open en lopende spellen</h2>
        <div class="horizontalLine"></div>
        <div class="game-list container">
            <div class="row game-list-header">
                <div class="col-3 col-sm-2 font-weight-bold">Datum</div>
                <div class="col-3 font-weight-bold">Naam</div>
                <div class="col-3 font-weight-bold d-none d-sm-block">Locatie</div>
                <div class="col-3 col-sm-2 font-weight-bold">Deelnemers</div>
                <div class="col-3 col-sm-2 font-weight-bold">Status</div>
            </div>
            <game-item v-for="(game, index) in games" :key="index" :item="game"></game-item>
        </div>
        <router-link to="/">
            <button class="btn btn-secondary">Terug naar overzicht</button>
        </router-link>
    </div>
</template>

<script>
    import GameItem from './GameItem'
    export default {
        components: { GameItem},
        mounted() {
            this.$store.dispatch('getAllGames');
        },
        computed: {
            name() {
                return this.$store.state.userName
            },            
            games() {
                return this.$store.state.games
            },
        },
        methods: {}
    }
</script>
<style>
    .game-list {
        margin-bottom: 50px;
    }
    .game-list-header {
        line-height: 40px;
        border-bottom: 1px solid #3A3F44;
    }
</style>

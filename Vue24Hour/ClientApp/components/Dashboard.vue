<template>
    <div class="dashboard">
        <h2>Welkom {{name}}</h2>
        <div class="horizontalLine"></div>
        <p>
            <div class="gameStatus">Status:</div>
            Je doet op dit moment niet mee aan een spel. Wil je je inschrijven voor het volgende spel?
        </p>
        <div class="horizontalLine"></div>
        <router-link to="/startNewGame">
            <button class="btn btn-secondary">Begin nieuw spel</button>
        </router-link>
        <router-link to="/listGames">
            <button class="btn btn-secondary">Toon spellen</button>
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
    .gameStatus {
        color: white;
    }
</style>

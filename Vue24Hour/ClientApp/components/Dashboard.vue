<template>
    <div class="dashboard">
        <h2>Welkom {{name}}, veel plezier met het 24 uur spel.</h2>
        <hr />
        <p>
            Je doet op dit moment niet mee aan een spel. Wil je je inschrijven voor het volgende spel?
        </p>
        <hr />
        <p>Actiefe spellen</p>
        <ul class="account-list">
            <game-item v-for="(game, index) in games" :key="index" :item="game"></game-item>
        </ul>
        <router-link to="/startNewGame">Nieuw spel starten</router-link>
        <hr />
        <br />
        <br />
        <br />
        <br />
        <br />
        <router-link to="/logout">Uitloggen</router-link>
        <hr />
        <p>
            Test stuff
        </p>
        <router-link to="/location">Locatie</router-link>
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
</style>

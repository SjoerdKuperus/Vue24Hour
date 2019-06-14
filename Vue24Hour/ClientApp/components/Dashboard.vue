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
        <router-link to="/listGames">
            <button class="btn btn-secondary">Toon spellen</button>
        </router-link>
        <router-link to="/startNewGame" class="float-right">
            <button class="btn btn-secondary">Begin nieuw spel</button>
        </router-link>
    </div>
</template>

<script>
    import GameItem from './GameItem'
    import shared from './shared'

    export default {
        components: { GameItem},
        mounted() {
            this.$store.dispatch('getAllGames');
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
        },
        methods: {
            closeAlert() {
                this.$store.state.dashboardMessage = "";
            },
        }
    }
</script>
<style>
    .gameStatus {
        color: white;
    }
</style>

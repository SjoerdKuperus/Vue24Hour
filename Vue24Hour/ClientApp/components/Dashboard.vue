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
            <div>{{gameTimeMessage}}</div>
            <div>Team {{currentTeam.name}} heeft {{currentTeam.score}} punten</div>            
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
            this.$store.dispatch('getAllGames');
            this.interval = setInterval(this.setGameTime, 1000)
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
            gameTimeMessage() {
                if (this.myCurrentGame !== null) {
                    var startDate = moment(this.myCurrentGame.startDate + " " + this.myCurrentGame.startTime, "DD-MM-YYYY HH:mm");
                    var toNow = moment(startDate).diff(moment(), 'seconds');
                    var duration = moment.duration(toNow, 'seconds').asMilliseconds();
                    var timeLeft = moment.utc(duration).format("HH:mm:ss")
                    return "Het is nu: " + this.dateNow + ". Het spel duurt nog: " + timeLeft;                    
                }
                return "hh:mm:ss";
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
</style>

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
                return this.$store.state.userName;
            },            
            games() {
                return this.$store.state.games;
            },
            dashboardMessage() {
                return this.$store.state.dashboardMessage;
            }
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

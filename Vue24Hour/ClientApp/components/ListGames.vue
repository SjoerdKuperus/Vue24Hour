<template>
    <div>
        <h2>Lopende spellen</h2>
        <div class="horizontalLine"></div>
        <ul class="account-list">
            <game-item v-for="(game, index) in games" :key="index" :item="game"></game-item>
        </ul>
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
</style>

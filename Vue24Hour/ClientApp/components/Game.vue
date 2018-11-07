<template>
    <div class="dashboard">
        <h2>Game: {{game.startDate}} </h2>
        <hr />
        <p>
            Informatie over deze game:
        </p>
        <hr />
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

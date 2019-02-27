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
    import TodoItem from './TodoItem'
    import GameItem from './GameItem'

    export default {
        components: { TodoItem, GameItem},
        mounted() {
            this.$store.dispatch('getAllTodos');
            this.$store.dispatch('getAllGames');
        },
        computed: {
            name() {
                return this.$store.state.userName
            },
            todos() {
                return this.$store.state.todos
            },
            games() {
                return this.$store.state.games
            },
        },
        methods: {
            addTodo(e) {
                var text = e.target.value || ''
                text = text.trim()

                if (text.length) {
                    this.$store.dispatch('addTodo', { text })
                }

                e.target.value = ''
            },
        }
    }
</script>

<style>
    .new-todo {
        width: 100%;
        font-size: 18px;
        margin-bottom: 15px;
        border-top-width: 0;
        border-left-width: 0;
        border-right-width: 0;
        border-bottom: 1px solid rgba(0, 0, 0, 0.2);
    }
</style>

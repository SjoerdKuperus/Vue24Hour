<template>
    <div class="dashboard">
        <h2>Nieuw spel beginnen</h2>
        <div class="horizontalLine"></div>
        <p>Hier kunt je een nieuw 24 uur spel opstarten. Kies een naam en startdatum. En voeg de teams toe die mee spelen.</p>
        <form @submit.prevent="createGame" autocomplete="off">
            <div class="form-group">
                <label for="name">Naam</label>
                <input id="name" v-model="name" class="form-control">
            </div>
            <div class="form-group">
                <label for="name">Locatie</label>
                <input id="location" v-model="location" class="form-control">
            </div>
            <div class="form-group">
                <label for="startDate">Start datum</label>
                <input id="startDate" type="date" v-model="startDate" class="form-control">
            </div>
            <div class="form-group">
                <label for="name">Maximaal aantal deelnemers</label>
                <input id="maximumParticipants" type="number" v-model="maximumParticipants" class="form-control">
            </div>
            <div class="form-group">
                <label for="password">Teams</label>
                <select v-model="selectedTeams" multiple>
                    <option v-for="team in teams" v-bind:value="team.id">
                        {{ team.name }}
                    </option>
                </select>
            </div>
            <div>
                <button type="submit" class="btn btn-secondary">Start nieuw spel</button>
                <router-link to="/">
                    <button type="submit" class="btn btn-primary">Annuleren</button>
                </router-link>
                <div v-if="createGameError" class="error alert alert-info float-right">{{createGameError}}</div>
                <div v-if="formError" class="error alert alert-info float-right">{{formError}}</div>
            </div>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                name: '',
                location: '',
                maximumParticipants: '',
                startDate: '',
                selectedTeams: [],
                error: false,
                formError: "",
            }
        },
        mounted() {
            this.$store.state.createGameError = ""
            this.$store.dispatch('getAllTeams');
        },
        computed: {
            createGameError() {
                return this.$store.state.createGameError
            },
            teams() {
                return this.$store.state.teams
            },
        },
        methods: {
            createGame() {
                if (this.name === "") {
                    this.formError = "Naam mag niet leeg zijn";
                }
                else if (this.startDate === "") {
                    this.formError = "Start datum mag niet leeg zijn";
                }
                else {
                    this.formError = "";
                    this.$store.dispatch('createGame', {
                        name: this.name,
                        location: this.location,
                        maximumParticipants: this.maximumParticipants,
                        startDate: this.startDate,
                        teams: this.selectedTeams
                    })
                }
            }
        }
    }
</script>
<style>
    .error {
        color: red;
        margin-bottom: 0px;
    }
</style>

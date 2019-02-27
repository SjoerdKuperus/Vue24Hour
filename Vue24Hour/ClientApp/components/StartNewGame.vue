<template>
    <div class="dashboard">
        <h2>Hier kunt u een nieuw 24 uur spel opstarten</h2>
        <form @submit.prevent="createGame" autocomplete="off">
            <div class="form-group">
                <label for="name">Naam</label>
                <input id="name" v-model="name" class="form-control">
            </div>
            <div class="form-group">
                <label for="startDate">Start datum</label>
                <input id="startDate" type="date" v-model="startDate" class="form-control">
            </div>
            <div class="form-group">
                <label for="password">Team</label>
                <select name="teams" multiple>
                    <option value="rood">Team Rood</option>
                    <option value="geel">Team Geel</option>
                    <option value="paars">Team Paars</option>
                    <option value="blauw">Team Blauw</option>
                </select>
            </div>
            <button type="submit" class="btn">Start nieuw spel</button>
            <br />
            <br />
            <p v-if="createGameError" class="alert alert-danger">{{createGameError}}</p>
            <p v-if="formError" class="alert alert-danger">{{formError}}</p>
        </form>
        <hr />
        <router-link to="/">Terug naar overzicht</router-link>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                name: '',
                startDate: '',
                teams: '',
                error: false,
                formError: ""
            }
        },
        computed: {
            createGameError() {
                return this.$store.state.createGameError
            }
        },
        methods: {
            createGame() {
                if (this.name === "") {
                    this.formError = "Naam mag niet leeg zijn";
                }
                else {
                    this.formError = "";
                    this.$store.dispatch('createGame', {
                        name: this.name,
                        startDate: this.startDate,
                        teams: this.teams
                    })
                }
            }
        }
    }
</script>
<style></style>

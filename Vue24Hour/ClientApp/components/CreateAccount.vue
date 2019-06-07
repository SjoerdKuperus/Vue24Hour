<template>
    <div>
        <h2>Aanmaken nieuw account</h2>
        <div class="horizontalLine"></div>
        <p>
            Vul alle gegevens hieronder in. Deze zijn nodig om een account voor je te maken.
        </p>
        <p>
            Als je hiermee klaar bent kan je deelnemen aan het 24 uur spel!
        </p>
        <form @submit.prevent="createAccount" autocomplete="off">
            <div class="form-group">
                <label for="phone">Naam</label>
                <input id="name" v-model="name" class="form-control">
            </div>
            <div class="form-group">
                <label for="phone">Telefoonnummer</label>
                <input id="phone" v-model="phone" placeholder="06-12312311" class="form-control">
            </div>
            <div class="form-group">
                <label for="password">Wachtwoord</label>
                <input id="password" v-model="password" placeholder="" class="form-control" type="password">
            </div>
            <div class="form-group">
                <label for="password2">Herhaal wachtwoord</label>
                <input id="password2" v-model="password2" placeholder="" class="form-control" type="password">
            </div>
            <button type="submit" class="btn btn-secondary">Aanmaken</button>
            <router-link to="/">
                <button type="submit" class="btn btn-primary">Annuleren</button>
            </router-link>
            <p v-if="createAccountError" class="error">{{createAccountError}}</p>
            <p v-if="formError" class="error">{{formError}}</p>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                name: '',
                phone: '',
                password: '',
                password2: '',
                error: false,
                formError: ""
            }
        },
        computed: {
            createAccountError() {
                return this.$store.state.createAccountError
            }
        },
        methods: {
            createAccount() {
                if (this.password != this.password2) {
                    this.formError = "Wachtwoorden komen niet overeen.";
                }
                else {
                    this.formError = "";
                    this.$store.dispatch('createAccount', {
                        name: this.name,
                        phone: this.phone,
                        password: this.password,
                        password2: this.password2
                    })
                }
            }
        }
    }
</script>

<style scoped>
</style>

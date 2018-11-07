<template>
    <div>
        <h2>24 uur spel</h2>
        <hr />
        <p>
            Welkom bij het 24 uur spel. Om mee te doen moet je eerst een account aanmaken. Dat kan <router-link to="/CreateAccount">Hier</router-link>.
            Als je al een account hebt kan je inloggen.
        </p>
        <hr />
        <div v-if="$route.query.redirect" class="alert alert-warning" role="alert">Je moet eerst inloggen.</div>
        <form @submit.prevent="login" autocomplete="off">
            <div class="form-group">
                <label for="phone">Telefoonnummer</label>
                <input id="phone" v-model="phone" placeholder="06-12312311" class="form-control">
            </div>
            <div class="form-group">
                <label for="password">Wachtwoord</label>
                <input id="password" v-model="password" placeholder="" type="password" class="form-control">
            </div>
            <button type="submit" class="btn">login</button>
            <p v-if="loginError" class="error">{{loginError}}</p>
        </form>
        <hr />
        <p>
            <router-link to="/ListAccounts">Lijst van alle accounts</router-link>
        </p>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                phone: '',
                password: '',
                error: false
            }
        },
        computed: {
            loginError() {
                return this.$store.state.loginError
            }
        },
        methods: {
            login() {
                this.$store.dispatch('login', {
                    phone: this.phone,
                    password: this.password
                })
            }
        }
    }
</script>

<style scoped>
    .error {
        color: red;
    }
    label {
        display: block;
    }
    input {
        display: block;
        margin-bottom: 10px;
    }
</style>

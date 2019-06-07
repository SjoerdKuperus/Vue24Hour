<template>
    <div>
        <h2 id="testGame1">24 uur spel</h2>
        <div class="horizontalLine"></div>
        <p>
            Welkom bij het 24 uur spel. Om mee te doen moet je eerst een account aanmaken.
        </p>
        <router-link to="/CreateAccount">
            <button id="myButton" class="btn btn-secondary">Aanmelden</button>
        </router-link>
        <br />
        <br />
        <div class="horizontalLine"></div>
        <br />
        <br />
        <h2>
            Inloggen
        </h2>
        
        <form @submit.prevent="login" autocomplete="off">
            <div class="form-group">
                <label for="phone">Telefoonnummer</label>
                <input id="phone" v-model="phone" placeholder="06-12312311" class="form-control">
            </div>
            <div class="form-group">
                <label for="password">Wachtwoord</label>
                <input id="password" v-model="password" placeholder="" type="password" class="form-control">
            </div>
            <div>
                <button type="submit" class="btn btn-secondary">login</button>
                <div v-if="loginError" class="error alert alert-info float-right">{{loginError}}</div>
                <div v-if="$route.query.redirect && !loginError" class="alert alert-info float-right" role="alert">Je moet eerst inloggen.</div>
            </div>
        </form>
        <div class="horizontalLine"></div>
        <p>
            <br />
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
        mounted() {
            //reset errors
            this.$store.state.loginError = "";
            //$("#testGame1").html("jquery Works!");
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

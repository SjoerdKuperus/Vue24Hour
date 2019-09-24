<template>
    <div>
        <div class="w-100">
            <div class="logo float-left"></div>
            <h2 class="float-left router-header">24 uur spel</h2>
        </div>
        <div class="clearfix"></div>
        <div class="horizontalLine"></div>
        <p class="introText">
            Welkom bij het 24 uur spel. Een spel waarbij je in 24 uur zo veel mogelijk gebieden inneemt. In kleine groepen strijd je in verschillende teams. Wie heeft er na 24 uur de meeste punten?
        </p>
        <div class="introButtons">
            <router-link to="/CreateAccount">
                <button id="signupButton" class="btn btn-secondary">Account maken</button>
            </router-link>
            <button id="showSignIn" class="btn btn-secondary" @click="showSignIn" v-if="!isShowSignIn">Inloggen</button>
        </div>       
        <div id="installPrompt" class="alert alert-info" role="alert">
            <span>
                Instaleer 24uur op homescherm
            </span>
            <button id="installPromptAddButton" class="btn btn-sm btn-outline-info float-right">Instaleren</button>
        </div>        
        <br />
        <div class="horizontalLine"></div>
        <div class="signInContainer">
            <div v-bind:class="[isShowSignIn ? signInSlideClass: '', signInClass, loginError ? shakeErrorClass: '']">
                <div v-if="isShowSignIn">
                    <br />
                    <h2 class="header">
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
                        <div class="loginButtons">
                            <div>
                                <div v-if="loginError" class="error alert alert-warning">{{loginError}}</div>
                                <div v-if="$route.query.redirect && !loginError" class="alert alert-warning" role="alert">Je moet eerst inloggen.</div>
                            </div>
                            <div>
                                <button type="submit" class="btn btn-secondary">login</button>
                            </div>                           
                        </div>
                    </form>
                    <div class="horizontalLine"></div>
                </div>
            </div>
        </div>
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
                error: false,
                isShowSignIn: false,
                signInClass: 'signIn',
                signInSlideClass: 'signInSlide',
                shakeErrorClass: 'shakeError'
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
            },
            showSignIn() {
                this.isShowSignIn = true;
                this.signInAnimationTop = "5vh";
            }
        }
    }
</script>

<style scoped>
    .logo {
        width: 50px;
        height: 50px;
        background: url("/images/MoonLogo192.png");
        background-size: 50px 50px;
    }
    .router-header{
        width: calc(100% - 100px);
        margin-right:50px;
        color: white;
    }
    .header{
        color: white;
    }
    .introText {
        margin: 30px 0;
        text-align:center;
        font-size: 16px;
        color: white;
    }
    .introButtons {
        display: flex;
        justify-content:center;
    }
    .introButtons .btn {
        margin: 0 5px;
    }
    .signInContainer{
        width:100%;
        height:100%;
        overflow: hidden;
    }
    .signIn {
        transition: all 0.2s ease-out;
        position: relative;
        top: -50vh;
        height:100%;
        width:100%;
    }
    .signInSlide {
        top:0;
    }
    .loginButtons{
        display:flex;
        justify-content: space-between;
    }
    .shakeError {
        animation: shakeAnimation 500ms linear 0s 1 normal;
    }
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
    #installPrompt {
        margin-top: 10px;
        line-height: 30px;
        display:none;
    }
    @keyframes shakeAnimation{
        0% { left: 0px; }
        10% { left: -10px; }
        30% { left: 10px; }
        50% { left: -10px; }
        70% { left: 10px; }
        90% { left: -10px; }
        100% { left: 0px; }
    }
</style>

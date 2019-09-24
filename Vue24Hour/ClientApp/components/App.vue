<template>
    <div class="app-container">
        <div class="app-view">
            <div class="pos-f-t" v-if="loggedIn">
                <nav class="navbar navbar-dark bg-dark float-right">
                    <button class="navbar-toggler" v-on:click="toggleMenu">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                </nav>
                <div class="collapse float-right mt-5" v-bind:class="{ show: openMenu }" id="navbarToggleExternalContent">
                    <div class="card position-absolute" style="min-width: 200px; right: 20px; z-index:100;">
                        <div class="card-header">
                            {{name}}
                        </div>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">
                                <router-link to="/">Home</router-link>
                            </li>
                            <li class="list-group-item">
                                <router-link to="/location">Mijn locatie</router-link>
                            </li>
                            <li class="list-group-item">
                                <router-link to="/logout">Uitloggen</router-link>
                            </li>
                        </ul>
                    </div>                    
                </div>
            </div>  
            <template v-if="$route.matched.length">
                <router-view></router-view>
            </template>
        </div>
    </div>
</template>

<script>
    export default {
        computed: {
            openMenu() {
                return this.$store.state.openMenu;
            },
            name() {
                return this.$store.state.userName
            }, 
            loggedIn() {
                return this.$store.state.loggedIn
            }
        },
        methods: {
            toggleMenu: function (event) {
                this.$store.state.openMenu = !this.$store.state.openMenu;
            }
        }
    }
</script>

<style>
    :root {
        --main-background-color: #F1F1F1;
    }
    html, body {
        margin: 0;
        padding: 0;
    }
    body {       
        background-color: var(--main-background-color);
    }
    ul {
        padding: 0;
    }
    h1, h2 {
        text-align: center;
    }
    .app-container {
        display: flex;
        align-items: center;
        justify-content: center;
        overflow: hidden;
    }
    .app-view {
        background: #272B30;
        position: relative;
        box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 5px 10px 0 rgba(0, 0, 0, 0.1);
    }    

    /* Very small devices */
    @media (min-width: 100px) {
        .app-view {
            min-width: 300px;
            width:100%;
            max-width: 575px;
            margin: 0px;
            padding: 5px 5px 5px 5px;
        }
    }

    /* Small devices (landscape phones, 576px and up) */
    @media (min-width: 576px) {
        .app-view {
            min-width: 400px;
            padding: 20px 25px 15px 25px;
        }
    }

    /* Medium devices (tablets, 768px and up) */
    @media (min-width: 768px) {
        .app-view {
            min-width: 600px;
        }
    }

    /* Large devices (desktops, 992px and up) */
    @media (min-width: 992px) {
        .app-view {
            min-width: 800px;
        }
    }

    /* Extra large devices (large desktops, 1200px and up) */
    @media (min-width: 1200px) {
        .app-view {
            min-width: 800px;
        }
    }
</style>

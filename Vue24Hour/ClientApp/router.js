import Vue from 'vue'
import Router from 'vue-router'
import store from './store'
import Dashboard from './components/Dashboard.vue'
import Login from './components/Login.vue'
import Location from './components/Location.vue'
import CreateAccount from './components/CreateAccount.vue'
import ListAccounts from './components/ListAccounts.vue'
import ListGames from './components/ListGames.vue'
import Game from './components/Game.vue'
import StartNewGame from './components/StartNewGame.vue'

Vue.use(Router);

function requireAuth(to, from, next) {
    if (!store.state.loggedIn) {
        next({
            path: '/login',
            query: { redirect: to.path }
        });
    } else {
        next();
    }
};

export default new Router({
    mode: 'history',
    base: __dirname,
    routes: [
        { path: '/', component: Dashboard, beforeEnter: requireAuth },
        { path: '/login', component: Login },
        { path: '/listAccounts', component: ListAccounts },
        { path: '/location', component: Location, beforeEnter: requireAuth },
        { path: '/createAccount', component: CreateAccount, beforeEnter: requireAuth },
        { path: '/listGames', component: ListGames, beforeEnter: requireAuth },
        { path: '/game/:id', component: Game, beforeEnter: requireAuth },
        { path: '/startNewGame', component: StartNewGame, beforeEnter: requireAuth },
        {
            path: '/logout',
            async beforeEnter(to, from, next) {
                await store.dispatch('logout');
            }
        }
    ]
});

import axios from 'axios'
import router from '../router'

const sleep = ms => {
    return new Promise(resolve => setTimeout(resolve, ms));
}

export const actions = {
    checkLoggedIn({ commit }) {
        // Todo: commit('loggedIn') if the user is already logged in
    },

    async setPostion({ commit }, position) {
        if (position.coords != undefined) {
            var latitude = position.coords.latitude;
            var longtitude = position.coords.longitude;
            commit('setLocation', latitude + ", " + longtitude);
            commit('setLocationCoords', { latitude, longtitude });
        }
    },

    async login({ dispatch, commit }, data) {
        await axios.post('/api/login', data)
            .then((res) => {
                commit('loggedIn', res.data);
            }).catch((error) => {
                commit('loginError', error.response.data);
            });
    },

    async logout({ commit }) {
        // Todo: log the user out
        commit('loggedOut');
    },

    async loginFailed({ commit }, message) {
        commit('loginError', message);
        await sleep(3000);
        commit('loginError', null);
    },

    async createAccount({ dispatch, commit }, data) {
        await axios.post('/api/account', data)
            .then((res) => {
                dispatch('logout');
            }).catch((error) => {
                commit('createAccountError', error.response.data);
            });
    },

    async createGame({ dispatch, commit }, data) {
        await axios.post('/api/game', data)
            .then((res) => {
                router.push('/');
            }).catch((error) => {
                commit('createGameError', error.response.data);
            });
    },

    async activateGame({ dispatch, commit }, data) {
        await axios.post('/api/game/activate', data)
            .then((response) => {
                let game = response.data;
                commit('loadGame', game);
            });
    },

    async createTestEvents({ dispatch, commit }, data) {
        await axios.post('/api/game/createTestEvents', data)
            .then((response) => {
                let game = response.data;
                commit('loadGame', game);
            });
    },

    async getAllAccounts({ commit }) {
        let response = await axios.get('/api/account');

        if (response && response.data) {
            let updatedAccounts = response.data;
            commit('loadAccounts', updatedAccounts);
        }
    },

    async getAllGames({ commit }) {
        let response = await axios.get('/api/game');

        if (response && response.data) {
            let updatedGames = response.data;
            commit('loadGames', updatedGames);
        }
    },

    async getGame({ commit }, id) {
        let response = await axios.get('/api/game/' + id);
        if (response && response.data) {
            let game = response.data;
            commit('loadGame', game);
        }
    },

    async getAllTeams({ commit }) {
        let response = await axios.get('/api/team');

        if (response && response.data) {
            let updatedTeams = response.data;
            commit('loadTeams', updatedTeams);
        }
    },
}

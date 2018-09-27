import axios from 'axios'

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
        // Todo: log the user in
        commit('loggedIn', { userName: data.email });
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

    async getAllTodos({ commit }) {
        let response = await axios.get('/api/todo');

        if (response && response.data) {
            let updatedTodos = response.data;
            commit('loadTodos', updatedTodos);
        }
    },

    async addTodo({ dispatch }, data) {
        await axios.post('/api/todo', { text: data.text });
        await dispatch('getAllTodos');
    },

    async toggleTodo({ dispatch }, data) {
        await axios.post('/api/todo/' + data.id, { completed: data.completed });
        await dispatch('getAllTodos');
    },

    async deleteTodo({ dispatch }, id) {
        await axios.delete('/api/todo/' + id);
        await dispatch('getAllTodos');
    }
}

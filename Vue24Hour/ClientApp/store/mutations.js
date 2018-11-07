import router from '../router'

export const state = {
    todos: [],
    loggedIn: false,
    loginError: null,
    createAccountError: null,
    userName: null,
    locationName: "test1",
    locationCoords: {},
    accounts: []
}

export const mutations = {
    loggedIn(state, data) {
        state.loggedIn = true;
        state.userName = (data.name || '').split(' ')[0] || 'Hello';

        let redirectTo = state.route.query.redirect || '/';
        router.push(redirectTo);
    },

    loggedOut(state) {
        state.loggedIn = false;
        router.push('/login');
    },

    loginError(state, message) {
        state.loginError = message;
    },
    createAccountError(state, message) {
        state.createAccountError = message;
    },

    loadTodos(state, todos) {
        state.todos = todos || [];
    },
    setLocation(state, locationName) {
        state.locationName = locationName;
    },
    setLocationCoords(state, location) {
        state.locationCoords = location;
    },
    loadAccounts(state, accounts) {
        state.accounts = accounts || [];
    },
}

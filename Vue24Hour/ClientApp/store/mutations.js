import router from '../router'

export const state = {
    loggedIn: false,
    loginError: null,
    createAccountError: null,
    createGameError: null,
    userName: null,
    openMenu: false,
    locationName: "test1",
    locationCoords: {},
    accounts: [],
    games: [],
    game: null,
    teams: [],
    dashboardMessage: ""
}

export const mutations = {
    loggedIn(state, data) {
        state.loggedIn = true;
        state.userName = data.name; //(data.name || '').split(' ')[0] || 'Hello';

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
    createGameError(state, message) {
        state.createGameError = message;
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
    loadGames(state, games) {
        state.games = games || [];
    },
    loadGame(state, game) {
        state.game = game || null;
    },
    loadTeams(state, teams) {
        state.teams = teams || [];
    },
    setDashboardMessage(state, message) {
        state.dashboardMessage = message;
    },
}

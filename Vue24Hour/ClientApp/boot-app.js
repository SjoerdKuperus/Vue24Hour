import Vue from 'vue'
import App from './components/App'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'

// Sync Vue router and the Vuex store
sync(store, router);

// Set the global menu state to closed.
router.afterEach((to, from) => { store.state.openMenu = false; });

new Vue({
    el: '#app',
    store,
    router,
    template: '<App/>',
    components: { App }
});
store.dispatch('checkLoggedIn');


// PWA service worker stuff
if ('serviceWorker' in navigator) {
    window.addEventListener('load',
        function() {
            navigator.serviceWorker.register('/sw.js').then(function(registration) {
                    // Registration was successful
                    console.log('ServiceWorker registration successful with scope: ', registration.scope);
                },
                function(err) {
                    // registration failed :(
                    console.log('ServiceWorker registration failed: ', err);
                });
        });
}

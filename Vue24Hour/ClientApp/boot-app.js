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

// Install prompt 
let deferredPrompt;
window.addEventListener('beforeinstallprompt', (e) => {
    // Prevent Chrome 67 and earlier from automatically showing the prompt
    e.preventDefault();
    // Stash the event so it can be triggered later.
    deferredPrompt = e;
    console.log("Install prompt fired!");
    var installPrompt = document.getElementById("installPrompt");
    installPrompt.style.display = 'block';
});

installPromptAddButton.addEventListener('click', (e) => {
    var installPrompt = document.getElementById("installPrompt");
    installPrompt.style.display = 'none';
    // Show the prompt
    deferredPrompt.prompt();
    // Wait for the user to respond to the prompt
    deferredPrompt.userChoice
        .then((choiceResult) => {
            if (choiceResult.outcome === 'accepted') {
                console.log('User accepted the install prompt');
            } else {
                console.log('User dismissed the install prompt');
            }
            deferredPrompt = null;
        });
});

/* Service worker
 *
 *  Warning! This is an experiment.
 *
 *  Will cache all request to the server. And on same request will get the result from the cache.  
 */

var CACHE_NAME = "24HourGame-cache";
var urlsToCache = ["/", "/dist/vendor.css"];

self.addEventListener("install", function(event) {
    // Perform install steps
    //event.waitUntil(
    //    caches.open(CACHE_NAME)
    //    .then(function(cache) {
    //        console.log("Opened cache");
    //        return cache.addAll(urlsToCache).then(function() {
    //            //cache.keys().then(function (cacheNames) {
    //            //    return Promise.all(
    //            //        cacheNames.map(function(cacheName) {
    //            //            console.log(cacheName.url);
    //            //        })
    //            //    );
    //            //});
    //        });
    //    })
    //);
});
self.addEventListener("fetch", function(event) {
    //console.log("Fetch, event:" + event);
    //event.respondWith(
    //    caches.match(event.request)
    //    .then(function(response) {
    //            // Cache hit - return response
    //            if (response) {
    //                console.log("Response found in cache: " + response);
    //                return response;
    //            }
    //            return fetch(event.request);
    //        }
    //    )
    //);
});

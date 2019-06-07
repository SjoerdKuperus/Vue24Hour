///* Service worker
// *
// *  Warning! This is an experiment.
// *
// *  Will cache all request to the server. And on same request will get the result from the cache.  
// */

//var CACHE_NAME = '24HourGame-cache';
//var urlsToCache = ['/'];

//self.addEventListener('install', function(event) {
//    // Perform install steps
//    event.waitUntil(
//        caches.open(CACHE_NAME)
//        .then(function(cache) {
//            console.log('Opened cache');
//            return cache.addAll(urlsToCache).then(function() {
//                //cache.keys().then(function (cacheNames) {
//                //    return Promise.all(
//                //        cacheNames.map(function(cacheName) {
//                //            console.log(cacheName.url);
//                //        })
//                //    );
//                //});
//            });
//        })
//    );
//});

//self.addEventListener('fetch', function (event) {
//    event.respondWith(
//        caches.match(event.request)
//            .then(function (response) {
//                return response;
//            //// Cache hit - return response
//            //if (response) {
//            //    console.log("Found request in cache:" + event.request.url);
//            //    return response;
//            //}

//            //return fetch(event.request).then(
//            //    function (response) {
//            //        // Check if we received a valid response
//            //        if (!response || response.status !== 200 || response.type !== 'basic') {
//            //            return response;
//            //        }

//            //        // IMPORTANT: Clone the response. A response is a stream
//            //        // and because we want the browser to consume the response
//            //        // as well as the cache consuming the response, we need
//            //        // to clone it so we have two streams.
//            //        var responseToCache = response.clone();

//            //        caches.open(CACHE_NAME)
//            //            .then(function (cache) {
//            //                cache.put(event.request, responseToCache);
//            //            });

//            //        return response;
//            //    }
//            //);
//        })
//    );
//});

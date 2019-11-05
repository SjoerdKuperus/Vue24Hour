Uitzoek project van Sjoerd Kuperus over het 24uur spel waarbij gebruikt gemaakt wordt van nieuwe technieken.

Functionele doelen:
* 24uur spel kunnen opzetten in een site.
    -game engine met update elke minuut.
    -spelers loggen in met hun telefoon, geven gps coordinaten door.
    -teams controleren kwadranten (1 bij 1 km) op de kaart.
* Teams, spelers en games kunnen aanmaken, beheren en verwijderen via de site.

Technische doelen:
* vue.js als client side framework.
* kaart met eigen dynamisch layer voor game informatie.
* Progressive Web App intergratie.

Results:

(3 juli 2018)
-- Handleiding van https://scotch.io/tutorials/build-a-secure-to-do-app-with-vuejs-aspnet-core-and-okta gevolgd
* locale git repository van gemaakt.

-- Nieuw project gemaakt volgens https://github.com/MarkPieszak/aspnetcore-Vue-starter
* let op eerst eigen folder maken om project in aan te maken.
* Uitzoeken hoe van developement naar productie te komen. Wat moet je dan doen met webpack.

* launchSettings.json bevat hoe de applicatie start bij F5.
* https://github.com/vuejs/vue-devtools voor Chrome developement tools specifiek voor vue.js
* VuePack extensie in VS2017 voor betere syntax highlighting (https://marketplace.visualstudio.com/items?itemName=MadsKristensen.VuejsPack-18329#overview) 
* scoped-css: https://vue-loader.vuejs.org/guide/scoped-css.html

* gebruik van vuex voor state : https://vuex.vuejs.org/
** State is immutable -- it can’t be changed except by a mutation.
** Mutations can change state, but they must be synchronous. Async code (like API calls) must run in an action instead.
** Actions run asynchronous code, then commit mutations, which change state.

* gebruik location Guard om locatie in firefox te spoofen.


(14 augustus 2018)
* Maken van een map met 3djs en topojson. https://bost.ocks.org/mike/map/

* Kaarten van nederland + datasets: https://www.pdok.nl/viewer/
- https://geodata.nationaalgeoregister.nl/beta/topotiles-viewer/ Voor tiles. Voor detail heb je BGT nodig. voor straten top10nl.

* Map van huidige locatie in vue.js aan de praat gekregen.

* Hoe eigen layer te maken met grid + gekleurde kwadranten van de teams?
https://www.mapbox.com/mapbox-gl-js/example/animate-a-line/

Via mapbox en de data van pdok een map aan de praat gekregen. Samen met raster van polygonen in extra layer. 

Vervolg stappen.
* Game opzet maken voor teams, teamleden en de game engine om elke minuut score op te tellen.
	- Persistance laag, opslag naar memory in node of in sql?
	- Rest service laag tussen vue.js voorkant en persistance.
* Raster moet game state kunnen tonen (in kleuren van polygonen die van dat team zijn)
* Pagina's voor toevoegen teams, teamleden, opstarten nieuw spel, ect.

(?????)

(29 januari 2019)
Doelen:
* Uitzoeken opzet teams/spelers
* Game state running in background.
* PWA intergratie

Acties:
* Opzet van domein in eigen project (met repository structuur)
* Fake Game, Quadranten data toegevoegd.
* Tonen van game en quadrant informatie in een map.

Todos:
* Game loop.
* PWA intergratie.


(27 februari 2019)
Doelen:
* Opzet meerdere kwadranten in een game.
* Game loop

(25 maart 2019)
Acties:
* 4 vaste teams toegevoegd, met eigen kleur
* Geojson bestand (dat door mapbox wordt gegenereerd) in gelezen met 16 quadranten
* Eerste opzet met controlEvents

(9 april)
Doelen:
* PWA
* Remove todo app parts
* Continue with events (Replay game)

Acties:
* Removed todo parts.
* Replay events, with correct color for team on map.

(6 mei)
Doelen:
* Create persistance
* Deploy to test envrionment
* PWA 

Acties:
* Added jsonDatabase serialisation for persistance.
* Accounts are also serialized.
* Deployment to azure.

(21 mei)
Doelen:
* PWA
* Responsive design

Acties:
* Use checklist
https://developers.google.com/web/progressive-web-apps/checklist
* How javascript workers work.
https://www.html5rocks.com/en/tutorials/workers/basics/
* Lighthouse audit now passes all checks!


(6 & 7 juni)
Doelen:
* Improve design

(23 juli)
* Update all npm packages to latest version and fix breaking changes.

(15 okt)
* Migrate to github
* Migrate to .netcore 3.0

(5 nov)
* Add claim quadrant functionality

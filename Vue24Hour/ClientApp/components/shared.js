export default {
    isPlayerInTeams: function(userName, teams) {
        var alreadyInTeam = false;
        teams.forEach(function(team) {
            team.players.forEach(function(player) {
                if (player.accountName === userName) {
                    alreadyInTeam = true;
                }
            });
        });
        return alreadyInTeam;
    },
    isPlayerInTeam: function (userName, team) {
        var playerInTeam = false;
        team.players.forEach(function (player) {
            if (player.accountName === userName) {
                playerInTeam = true;
            }
        });
        return playerInTeam;
    },
    getTeamFromCurrentUser: function(username, game) {
        var returnTeam = null;
        var self = this;
        game.teams.forEach(function(team) {
            if (self.isPlayerInTeam(username, team)) {
                returnTeam = team;
            }
        });
        return returnTeam;
    }
}

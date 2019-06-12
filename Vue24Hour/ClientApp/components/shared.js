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
    }
}

<template>
    <div class="team card" v-bind:style="{'border-color': item.color}">
        <div class="card-header">
            <div class="team-flag" v-bind:style="{ 'background-color': item.color}"></div>
            <div>Team {{item.name}} ({{item.players.length}}/{{maxTeamSize}})</div>
        </div>
        <div class="card-body">
            <div v-for="player in item.players">{{player.name}}</div>
            <div v-if="gameState === 'Startup' && !showAreYouSure && !alreadyInTeam && !teamIsFull">
                <button class="btn btn-outline-secondary btn-sm" @click="joinTeam(item.id)">Join team</button>
            </div>
            <div v-if="showAreYouSure && !alreadyInTeam && !teamIsFull">
                Weet je zeker dat je dit team wil joinen?
                <button class="btn btn-outline-secondary btn-sm" @click="joinTeam(item.id)">Join team {{item.name}}</button>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                showAreYouSure: false
            };
        },
        props: ['item', 'gameState', 'maxTeamSize', 'alreadyInTeam'],
        computed: {
            teamIsFull() {
                return this.item.players.length >= this.maxTeamSize;
            },
        },
        methods: {
            joinTeam(itemId) {
                if (this.showAreYouSure) {
                    var username = this.$store.state.userName;
                    var gameId = this.$store.state.game.id;
                    this.$store.dispatch('joinTeam', {
                        id: itemId,
                        userName: username,
                        gameId: gameId
                    });
                }
                this.showAreYouSure = !this.showAreYouSure;
            }
        }
    }
</script>

<style>
    .team {
        float: left;
        width: calc(50% - 10px);
        margin: 5px;
        min-height: 50px;
        border: 1px solid #fff;
        border-radius: 4px;
    }
    .team:hover {
    }
    .team-flag {
        height: 20px;
        margin-right: 5px;
        width: 40px;
        float: left;
    }     
</style>

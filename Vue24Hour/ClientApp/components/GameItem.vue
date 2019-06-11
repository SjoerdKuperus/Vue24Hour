<template>
    <div class="row game" @click="openGame(item.id)">
        <div class="col-2">{{item.startDate}}</div>
        <div class="col-3">{{item.name}}</div>
        <div class="col-3">{{item.location}}</div>
        <div class="col-2">({{item.activeParticipants}}/{{item.maximumParticipants}})</div>
        <div class="col-2">{{item.gameState}}</div>
    </div>
</template>

<script>
    export default {
        props: ['item'],        
        methods: {
            openGame(itemId) {
                this.$router.push('/game/' + itemId);
            }            
        }
    }
</script>

<style>
    .game {
        line-height: 40px;
        border-bottom: 1px solid #fd7e14;
    }
    .game:hover {
        background-color: #fd7e14;
        cursor: pointer;
        color: #3A3F44;
    }
</style>

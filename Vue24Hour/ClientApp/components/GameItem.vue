<template>
    <li class="game" @click="openGame(item.id)">
        <label v-text="item.name"></label>
        <label v-text="item.startDate"></label>
    </li>
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
        list-style-type: none;
    }
    .game:hover{
        background-color: gray;
        cursor: pointer;
    }

</style>

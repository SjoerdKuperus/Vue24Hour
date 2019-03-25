<template>
    <div class="team" v-bind:style="{ color: item.color}">
        <label v-text="item.name"></label>
    </div>
</template>

<script>
    export default {
        props: ['item'],
        methods: {}
    }
</script>

<style>
    .team {
    }
    .team:hover {
    }
</style>

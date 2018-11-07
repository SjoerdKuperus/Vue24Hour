<template>
    <li class="account">
        <label v-text="item.phone"></label>
    </li>
</template>

<script>
    export default {
        props: ['item'],
        methods: {}
    }
</script>

<style>
    .account {
        list-style-type: none;
    }
</style>

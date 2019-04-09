<template>
    <div class="row event">
        <div class="col-lg-1 eventMarker" v-if="index === eventCounter">*</div>
        <label class="col-lg-11 eventDescription" v-text="item.description"></label>
    </div>
</template>

<script>
    export default {
        props: ['item', 'index', 'eventCounter'],
        methods: {}
    }
</script>

<style>
    .event {
        height: 26px;
    }
    .eventMarker {
        color: red;
        text-align: end;
    }
    .eventDescription {

    }
</style>

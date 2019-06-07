<template>
    <div>
        <h2>Lijst van alle accounts</h2>
        <div class="horizontalLine"></div>
        <ul class="account-list">
            <account-item v-for="(account, index) in accounts" :key="index" :item="account"></account-item>
        </ul>
        <router-link to="/Login">
            <button type="submit" class="btn btn-secondary">Terug naar inlogscherm</button>            
        </router-link>
    </div>
</template>

<script>
    import AccountItem from './AccountItem'

    export default {
        components: { AccountItem },
        mounted() {
            this.$store.dispatch('getAllAccounts')
        },
        computed: {
            accounts() {
                return this.$store.state.accounts
            },
        },
        methods: {}
    }
</script>

<style scoped>
</style>

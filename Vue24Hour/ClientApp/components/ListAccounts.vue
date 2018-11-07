<template>
    <div>
        <h2>Lijst van alle accounts</h2>
        <hr />
        <ul class="account-list">
            <account-item v-for="(account, index) in accounts" :key="index" :item="account"></account-item>
        </ul>
        <hr />
        <p><router-link to="/Login">Terug naar inlogscherm</router-link></p>
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

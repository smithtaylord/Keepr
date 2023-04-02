<template>
    <div class="component">
        THIS IS THE VAULT PAGE YO!

    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import { watchEffect } from 'vue';

export default {
    setup() {
        const route = useRoute()
        const router = useRouter()
        async function getVault() {
            try {
                await vaultsService.getVault(route.params.vaultId)
            } catch (error) {
                Pop.error(error, '[get vault]')
                router.push({ name: "Home" })
            }
        }
        async function getVaultKeeps() {
            try {
                await vaultKeepsService.getVaultKeeps(route.params.vaultId)
            } catch (error) {
                Pop.error(error, '[get vault keeps]')
            }
        }

        watchEffect(() => {
            if (route.params.vaultId) {
                getVault()
                getVaultKeeps()
            }
        })
        return {}
    }
}
</script>


<style lang="scss" scoped></style>
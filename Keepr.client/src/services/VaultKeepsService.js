import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
    async getVaultKeeps(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        logger.log('[get vault keeps]', res.data)
        AppState.vaultKeeps = res.data
    }

    async createVaultKeep(vkData) {
        const res = await api.post('api/vaultkeeps', vkData)
        logger.log('[create vault keeps]', res.data)
        AppState.vaultKeeps.push(res.data)
    }

    async removeFromVault(vkId) {
        const res = await api.delete('api/vaultKeeps/' + vkId)
        logger.log('[removed vault keep]', res.data)
        const index = AppState.vaultKeeps.findIndex(vk => vk.vaultKeepId == vkId)
        logger.log('[index]', index)
        AppState.vaultKeeps.splice(index, 1)
    }
}

export const vaultKeepsService = new VaultKeepsService()
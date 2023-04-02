import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
    async getMyVaults() {
        const res = await api.get('account/vaults')
        logger.log('[get my vaults]', res.data)
        AppState.myVaults = res.data.map(v => new Vault(v))
    }

    async getProfileVaults(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        logger.log('[get profile vaults]', res.data)
        AppState.profileVaults = res.data.map(v => new Vault(v))
    }

    async getVault(id) {
        const res = await api.get('api/vaults/' + id)
        logger.log('[get vault by id]', res.data)
        AppState.vault = new Vault(res.data)
    }

    async createVault(formData) {
        const res = await api.post('api/vaults', formData)
        logger.log('[creating vault', res.data)
        AppState.myVaults.push(new Vault(res.data))
    }

    async deleteVault(id) {
        const res = await api.delete('api/vaults/' + id)
        const index = AppState.myVaults.findIndex(v => v.id == id)
        logger.log(res.data, '[deleted vault]')
        AppState.myVaults.splice(index, 1)
    }

}

export const vaultsService = new VaultsService();
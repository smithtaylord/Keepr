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

}

export const vaultsService = new VaultsService();
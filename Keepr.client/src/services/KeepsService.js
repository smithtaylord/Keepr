import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {

    async createKeep(formData) {
        const res = await api.post('api/keeps', formData)
        logger.log('[creating keep]', res.data)
        AppState.allKeeps.push(new Keep(res.data))
    }
    async getAllKeeps() {
        const res = await api.get('api/keeps')
        logger.log('[all keeps]', res.data)
        AppState.allKeeps = res.data.map(k => new Keep(k))
    }


}

export const keepsService = new KeepsService();
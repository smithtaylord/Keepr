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

    async getOneKeep(id) {
        const res = await api.get('api/keeps/' + id)
        logger.log('[one keep]', res.data)
        AppState.activeKeep = new Keep(res.data)
    }
    async deleteKeep(id) {
        const res = await api.delete('api/keeps/' + id)
        const index = AppState.allKeeps.findIndex(k => k.id == id)
        logger.log(res.data, '[deleted keep]')
        AppState.allKeeps.splice(index, 1)
    }

}

export const keepsService = new KeepsService();
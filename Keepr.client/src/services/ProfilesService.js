import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class ProfilesService {
    async getProfile(profileId) {
        const res = await api.get('api/profiles/' + profileId)
        logger.log(res.data, '[get profile]')
        AppState.profile = res.data
    }
}

export const profilesService = new ProfilesService();
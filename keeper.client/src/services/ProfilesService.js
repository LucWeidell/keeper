import { AppState } from '../AppState'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getKeepsInProfile(id) {
    const res = await api.get('/api/profiles/' + id + '/keeps')
    // logger.log('Data for get keeps: ', res.data)
    AppState.keeps = res.data
    return res.data
  }

  async getVaultsInProfile(id) {
    const res = await api.get('/api/profiles/' + id + '/vaults')
    // logger.log('Data for get keeps: ', res.data)
    AppState.activeProfileVaults = res.data
    return res.data
  }

  async getById(id) {
    const res = await api.get('/api/profiles/' + id)
    // logger.log('Data for get profile by id: ', res.data)
    AppState.activeProfile = res.data
    return res.data
  }
}
export const profilesService = new ProfilesService()

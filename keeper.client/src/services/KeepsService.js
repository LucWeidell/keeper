import { AppState } from '../AppState'
import Keep from '../models/Keep'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getKeeps() {
    const res = await api.get('/api/keeps')
    logger.log('Data for get keeps: ', res.data)
    AppState.keeps = res.data
    return res.data
  }

  async getKeepById(id) {
    const res = await api.get('/api/keeps' + id)
    logger.log('Data for get keep by id: ', res.data)
    // AppState.keeps = res.data
    return res.data
  }

  async createKeep(rawKeep) {
    const res = await api.post('/api/keeps', new Keep(rawKeep))
    logger.log('Daa create Keep: ', res.data)
    AppState.keeps.push(res.data)
    return res.data
  }

  // FIXME add account authentication
  async removeKeep(id) {
    const res = await api.delete('/api/keeps' + id)
    logger.log('Data for remove keep by id: ', res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
    return res.data
  }

  // FIXME add account authentication
  async editKeep(rawKeep) {
    const res = await api.put('/api/keeps' + rawKeep.id, new Keep(rawKeep))
    logger.log('Data for edit keep by id: ', res.data)
    const found = AppState.keeps.find(k => rawKeep.id === k.id)
    Object.assign(found, res.data)
    return res.data
  }
}

export const keepsService = new KeepsService()

import { AppState } from '../AppState'
import Keep from '../models/Keep'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getKeeps() {
    const res = await api.get('/api/keeps')
    // logger.log('Data for get keeps: ', res.data)
    AppState.keeps = res.data
    return res.data
  }

  async getKeepById(id) {
    const res = await api.get('/api/keeps/' + id)
    // logger.log('Data for get keep by id: ', res.data)
    AppState.activeKeep = res.data
    return res.data
  }

  async addKeepDownload(id) {
    const res = await api.get('/api/keeps/' + id + '/keeps')
    // logger.log('Data for get keep by id with new view: ', res.data)
    const found = AppState.keeps.find(k => id === k.id)
    if (!found) {
      const found = AppState.activeVaultKeeps.find(k => id === k.id)
      Object.assign(found.keeps, res.data.keeps)
    } else {
      Object.assign(found.keeps, res.data.keeps)
    }
    return res.data
  }

  async createKeep(rawKeep) {
    const res = await api.post('/api/keeps', rawKeep)
    // logger.log('Data create Keep: ', res.data)
    AppState.keeps.push(res.data)
    return res.data
  }

  async removeKeep(id) {
    await this.isYourAccount(id)
    const res = await api.delete('/api/keeps/' + id)
    // logger.log('Data for remove keep by id: ', res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
    return res.data
  }

  async editKeep(rawKeep) {
    await this.isYourAccount()
    const res = await api.put('/api/keeps/' + rawKeep.id, new Keep(rawKeep))
    // logger.log('Data for edit keep by id: ', res.data)
    const found = AppState.keeps.find(k => rawKeep.id === k.id)
    Object.assign(found, res.data)
    return res.data
  }

  async isYourAccount(editingId) {
    const item = await this.getKeepById(editingId)
    if (AppState.account.id !== item.creator.id) {
      throw new Error('Not Valid Keep for you to Edit!')
    }
  }
}

export const keepsService = new KeepsService()

import { AppState } from '../AppState'
import VaultKeep from '../models/VaultKeep'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepsService {
  async getVaultKeepById(id) {
    const res = await api.get('/api/vaultKeeps/' + id)
    // logger.log('Data for getting vaultKeep by id: ', res.data)
    // AppState.activeVault = res.data
    return res.data
  }

  async createVaultKeep(rawVaultKeep) {
    const res = await api.post('/api/vaultKeeps', new VaultKeep(rawVaultKeep))
    // logger.log('Data create VaultKeep: ', res.data)
    return res.data
  }

  async removeVaultKeep(id) {
    await this.isYourAccount(id)
    const res = await api.delete('/api/vaultKeeps/' + id)
    // logger.log('Data for remove vaultKeep by id: ', res.data)
    AppState.activeVaultKeeps = AppState.activeVaultKeeps.filter(k => k.vaultKeepId !== id)
    return res.data
  }

  async isYourAccount(editingId) {
    const item = await this.getVaultKeepById(editingId)
    if (AppState.account.id !== item.creatorId) {
      throw new Error('Not Valid VaultKeep for you to Edit!')
    }
  }
}
export const vaultKeepsService = new VaultKeepsService()

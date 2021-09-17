
import { AppState } from '../AppState'
import Vault from '../models/Vault'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getVaults() {
    const res = await api.get('/api/vaults')
    logger.log('Data for get vaults: ', res.data)
    AppState.vaults = res.data
    return res.data
  }

  async getVaultById(id) {
    // await this.isYourAccount(id)
    const res = await api.get('/api/vaults/' + id)
    logger.log('Data for get vault by id: ', res.data)
    AppState.activeVault = res.data
    return res.data
  }

  async getKeepsInVault(id) {
    await this.getVaultById
    const res = await api.get('/api/vaults/' + id + '/keeps')
    logger.log('Data for get keeps in vault: ', res.data)
    AppState.activeVaultKeeps = res.data
    return res.data
  }

  async createVault(rawVault) {
    const res = await api.post('/api/vaults', rawVault)
    logger.log('Data create Vault: ', res.data)
    AppState.vaults.push(new Vault(res.data))
    AppState.activeProfileVaults.push(new Vault(res.data))
    return res.data
  }

  async removeVault(id) {
    await this.isYourAccount(id)
    const res = await api.delete('/api/vaults/' + id)
    logger.log('Data for remove keep by id: ', res.data)
    AppState.vaults = AppState.vaults.filter(k => k.id !== id)
    return res.data
  }

  async editVault(rawVault) {
    await this.isYourAccount()
    const res = await api.put('/api/vaults/' + rawVault.id, new Vault(rawVault))
    logger.log('Data for edit keep by id: ', res.data)
    const found = AppState.vaults.find(k => rawVault.id === k.id)
    Object.assign(found, res.data)
    return res.data
  }

  async isYourAccount(editingId) {
    const item = await this.getVaultById(editingId)
    if (AppState.account.id !== item.creator.id) {
      throw new Error('Not Valid Vault for you to Edit!')
    }
  }
}
export const vaultsService = new VaultsService()

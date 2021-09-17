import { AppState } from '../AppState'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
      return res.data
    } catch (err) {
      // logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getVaultsInAccount() {
    const account = await this.getAccount()
    const res = await api.get('/api/profiles/' + account.id + '/vaults')
    // logger.log('Data for get keeps: ', res.data)
    AppState.vaults = res.data
    return res.data
  }
}

export const accountService = new AccountService()

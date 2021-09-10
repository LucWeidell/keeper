import { reactive } from 'vue'
import Keep from './models/Keep'
import Profile from './models/Profile'
import Vault from './models/Vault'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {Profile} */
  account: {},
  /** @type {Profile} */
  activeProfile: {},
  /** @type {[Keep]} */
  keeps: [],
  /** @type {[Vault]} */
  vaults: []
})

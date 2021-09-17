import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  // /** @type {Profile} */
  account: {},

  /** @type {Profile} */
  activeProfile: {},
  /** @type {[Vault]} */
  activeProfileVaults: [],

  /** @type {[Keep]} */
  keeps: [],

  /** @type {[Vault]} */
  vaults: [],

  /** @type {Vault} */
  activeVault: {},

  /** @type {Keep} */
  activeKeep: {},
  activeVaultKeeps: []
})

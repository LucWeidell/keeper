import { reactive } from 'vue'
import Keep from './models/Keep'
import Profile from './models/Profile'
import Vault from './models/Vault'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  // /** @type {Profile} */
  account: {},

  /** @type {Profile} */
  activeProfile: {},

  /** @type {[Keep]} */
  keeps: [new Keep({
    Id: 1,
    CreatorId: '6132aa76b7da62ee06dc46e5',
    Name: 'First Keep',
    Description: 'My first Keep ever',
    Views: 20,
    Shares: 50,
    Keeps: 12
  }), new Keep({
    Id: 2,
    CreatorId: '6132aa76b7da62ee06dc46e5',
    Name: 'Second Keep',
    Description: 'My Second Keep ever',
    Views: 10,
    Shares: 10,
    Keeps: 3
  })],

  /** @type {[Vault]} */
  vaults: [new Vault({
    Id: 1,
    CreatorId: '6132aa76b7da62ee06dc46e5',
    Name: 'First Vault',
    Description: 'My First Vault I made ever',
    IsPrivate: false
  }), new Vault({
    Id: 2,
    CreatorId: '6132aa76b7da62ee06dc46e5',
    Name: 'Second Private Vault',
    Description: 'My Second Vault I made its private',
    IsPrivate: true
  })],

  /** @type {Vault} */
  activeVault: {}
})

import { AppState } from '../AppState'

export default class VaultKeep {
  /** Constructor for a VaultKeep
   *
   * @param {Object} vaultKeep A POJO vaultKeep should be given for the constructor from Db
   */
  constructor(vaultKeep) {
    /** @type {Int} id of the many to many */
    this.id = vaultKeep.Id || -1
    /** @type {String} The Id reference to a profile/account creator */
    this.creatorId = vaultKeep.CreatorId || AppState.account.id
    /** @type {String} The Id reference to a vault  */
    this.vaultId = vaultKeep.VaultId
    /** @type {String} The Id reference to a keep */
    this.keepId = vaultKeep.KeepId
  }
}

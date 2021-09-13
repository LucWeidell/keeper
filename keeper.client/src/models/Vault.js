import Profile from './Profile'

export default class Vault {
  /** Constructor for a Vault
   *
   * @param {Object} vault A POJO vault should be given for the constructor from Db
   */
  constructor(vault) {
    /** @type {Int} */
    this.id = vault.Id || null
    /** @type {String} */
    this.creatorId = vault.CreatorId
    /** @type {String} */
    this.name = vault.Name
    /** @type {String} */
    this.description = vault.Description
    // REVIEW
    /** @type {Boolean} */
    this.isPrivate = vault.isPrivate || null
    /** @type {Profile} */
    this.Creator = (vault.Creator) ? new Profile(vault.Creator) : null
  }
}

import Profile from './Profile'
export default class Keep {
  /** Constructor for a Keep
   *
   * @param {Object} keep A POJO Keep should be given for the constructor from Db
   */
  constructor(keep) {
    /** @type {Int} */
    this.id = keep.Id || null
    /** @type {String} */
    this.creatorId = keep.CreatorId
    /** @type {String} */
    this.name = keep.Name
    /** @type {String} */
    this.description = keep.Description
    /** @type {String} */
    this.img = keep.Img || 'http://placebeard.it/200x300'
    /** @type {Int} */
    this.views = keep.Views
    /** @type {Int} */
    this.shares = keep.Shares
    /** @type {Int} */
    this.keeps = keep.Keeps
    /** @type {Profile} */
    this.Creator = (keep.Creator) ? new Profile(keep.Creator) : null
  }
}

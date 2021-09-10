export default class Profile {
  /** Constructor for Profiles
   *
   * @param {Object} profile A POJO profile should be given for the constructor
   */
  constructor(profile) {
    /** @type {String} Id of a profile: really an account */
    this.id = profile.Id
    /** @type {String}  Name of the profile: defaults to the email if none given */
    this.name = profile.Name
    /** @type {String} Email address of the profile */
    this.email = profile.email
    /** @type {String} The profile picture that is on every profile */
    this.picture = profile.Picture
  }
}

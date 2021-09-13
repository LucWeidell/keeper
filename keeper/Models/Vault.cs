using System.ComponentModel.DataAnnotations;

namespace keeper.Models
{
    public class Vault
    {
        public int Id { get; set; }
        [Required]
        public string creatorId { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        public Profile Creator { get; set; }
    }
}
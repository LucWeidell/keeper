using System.ComponentModel.DataAnnotations;

namespace keeper.Models
{
    public class VaultKeep
    {
        public int Id { get; set; }
        [Required]
        public string CreatorId { get; set; }
        public int keepId { get; set; }
        public int vaultId { get; set; }
    }
}
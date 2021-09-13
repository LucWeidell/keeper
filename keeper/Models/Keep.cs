using System.ComponentModel.DataAnnotations;

namespace keeper.Models
{
    public class Keep
    {
        public int Id { get; set; }
        [Required]
        public string creatorId { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Img { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        public int Keeps { get; set; }
        public Profile Creator { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minstrels.Models
{
    public class AppUser
    {
        [Key]
        public string  Id { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Rehearsal> Rehearsals { get; set; }
        public ICollection<Show> Shows { get; set; }
    }
}

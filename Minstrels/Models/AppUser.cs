using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Minstrels.Enum;

namespace Minstrels.Models
{
    public class AppUser:IdentityUser
    {
       
        public string? City { get; set; }
        public string? State { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public MinstrelType MinstrelType { get; set; }
        public string UserDescription { get; set; }
        public ICollection<Rehearsal> Rehearsals { get; set; }
        public ICollection<Show> Shows { get; set; }
        
    }
}

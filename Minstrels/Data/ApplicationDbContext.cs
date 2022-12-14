using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Minstrels.Models;

namespace Minstrels.Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {


        }
        public DbSet<Rehearsal> Rehearsals { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Address> Address { get; set; }


    }
}

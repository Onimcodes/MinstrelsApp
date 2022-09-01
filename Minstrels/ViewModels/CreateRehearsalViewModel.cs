using Minstrels.Models;

namespace Minstrels.ViewModels
{
    public class CreateRehearsalViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public DateTime DateAndTime { get; set; }   
    }
}

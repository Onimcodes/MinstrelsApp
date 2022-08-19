using Minstrels.Models;

namespace Minstrels.ViewModels
{
    public class CreateShowViewModel
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }

    }
}

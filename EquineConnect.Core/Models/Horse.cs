using System.ComponentModel.DataAnnotations;

namespace EquineConnect.Core.Models
{
    public class Horse
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Breed { get; set; }
        public string OwnerId { get; set; }
        public string Dicipline { get; set; }
    }
}

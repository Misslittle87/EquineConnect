using System.ComponentModel.DataAnnotations;

namespace EquineConnect.Mobile.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int Salt { get; set; }
    }
}

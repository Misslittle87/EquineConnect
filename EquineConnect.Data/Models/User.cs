using EquineConnect.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace EquineConnect.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<Horse> Horses { get; set; } = new List<Horse>();
    }
}

using EquineConnect.Core.Models;

namespace EquineConnect.Core.Interfaces
{
    public interface IHorseService
    {
        Task<Horse?> AddHorse(Horse horse, string ownerId);
    }
}

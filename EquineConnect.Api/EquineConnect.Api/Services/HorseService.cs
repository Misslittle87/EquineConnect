using EquineConnect.Core.Interfaces;
using EquineConnect.Core.Models;
using EquineConnect.Data.Context;

namespace EquineConnect.Core.Services
{
    public class HorseService : IHorseService
    {
        private readonly ApplicationDbContext _context;
        public HorseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Horse?> AddHorse(Horse horse, string ownerId)
        {
            var newHorse = new Horse
            {
                Name = horse.Name,
                DateOfBirth = horse.DateOfBirth,
                Breed = horse.Breed,
                Dicipline = horse.Dicipline,
                OwnerId = ownerId
            };

            _context.Horses.Add(newHorse);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? newHorse : null;
        }
    }
}

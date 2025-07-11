using Microsoft.EntityFrameworkCore;
using ProjektniMenadzment.Data;
using ProjektniMenadzment.Models.Domain;
using ProjektniMenadzment.Repositories.Interfaces;

namespace ProjektniMenadzment.Repositories
{
    public class KorisniciRepository : IKorisniciRepository
    {
        private readonly PMDbContext _context;
        public KorisniciRepository(PMDbContext context)
        {
            _context = context;
        }

        public async Task<List<Korisnici>> GetAllAsync()
        {
            return await _context.Korisnicis.ToListAsync();
        }
    }
}

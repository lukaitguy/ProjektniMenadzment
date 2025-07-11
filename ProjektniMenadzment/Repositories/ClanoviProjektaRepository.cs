using ProjektniMenadzment.Data;
using ProjektniMenadzment.Models.Domain;
using ProjektniMenadzment.Repositories.Interfaces;

namespace ProjektniMenadzment.Repositories
{
    public class ClanoviProjektaRepository : IClanoviProjektaRepository
    {
        private readonly PMDbContext _context;
        public ClanoviProjektaRepository(PMDbContext context)
        {
            _context = context;
        }

        public async Task<ClanoviProjektum> AddAsync(ClanoviProjektum clan)
        {
            await _context.ClanoviProjekta.AddAsync(clan);
            await _context.SaveChangesAsync();
            return clan;
        }
    }
}

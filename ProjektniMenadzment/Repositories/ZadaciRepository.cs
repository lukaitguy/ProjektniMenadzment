using Microsoft.EntityFrameworkCore;
using ProjektniMenadzment.Data;
using ProjektniMenadzment.Models;
using ProjektniMenadzment.Repositories.Interfaces;

namespace ProjektniMenadzment.Repositories
{
    public class ZadaciRepository : IZadaciRepository
    {
        private readonly PMDbContext _context;
        public ZadaciRepository(PMDbContext context)
        {
            _context = context;
        }

        public async Task<Zadaci> AddAsync(Zadaci zadatak)
        {
            if (zadatak == null)
                throw new ArgumentNullException(nameof(zadatak));

            await _context.Zadacis.AddAsync(zadatak);
            await _context.SaveChangesAsync();

            return zadatak;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var zadatak = await _context.Zadacis.FindAsync(id);
            if (zadatak == null) return false;

            _context.Zadacis.Remove(zadatak);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Zadaci?> GetByIdAsync(Guid id)
        {
            return await _context.Zadacis
                .Include(z => z.KreiraoKorisnik)
                .Include(z => z.DodeljenKorisniku)
                .Include(z => z.KomentariZadataks)
                .FirstOrDefaultAsync(z => z.Id == id);
        }

        public async Task<IEnumerable<Zadaci>> GetByProjekatIdAsync(Guid projekatId)
        {
            return await _context.Zadacis
                .Include(z => z.DodeljenKorisniku)
                .Where(z => z.ProjekatId == projekatId)
                .ToListAsync();
        }

        public async Task<Zadaci> UpdateAsync(Zadaci zadatak)
        {
            var postojiZadatak = await _context.Zadacis.FirstOrDefaultAsync(z => z.Id == zadatak.Id);

            if(postojiZadatak == null)
            {
                return null;
            }

            postojiZadatak.Naslov = zadatak.Naslov;
            postojiZadatak.Opis = zadatak.Opis;
            postojiZadatak.Status = zadatak.Status;
            postojiZadatak.Prioritet = zadatak.Prioritet;
            postojiZadatak.DodeljenKorisnikuId = zadatak.DodeljenKorisnikuId;
            postojiZadatak.Rok = zadatak.Rok;

            await _context.SaveChangesAsync();

            return postojiZadatak;
        }

        public async Task<bool> UpdateStatusAsync(Guid zadatakId, Guid korisnikId, string noviStatus)
        {
            var zadatak = await _context.Zadacis.FirstOrDefaultAsync(z => z.Id == zadatakId && z.DodeljenKorisnikuId == korisnikId);
            if (zadatak == null)
            {
                return false;
            }
            zadatak.Status = noviStatus;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProjektniMenadzment.Data;
using ProjektniMenadzment.Models.Domain;
using ProjektniMenadzment.Models.ViewModels;
using ProjektniMenadzment.Repositories.Interfaces;

namespace ProjektniMenadzment.Repositories
{
    public class ProjektiRepository : IProjektiRepository
    {
        private readonly PMDbContext _context;

        public ProjektiRepository(PMDbContext context)
        {
            _context = context;
        }

        public async Task<Projekti> AddAsync(Projekti projekat)
        {
            await _context.Projektis.AddAsync(projekat);
            await _context.SaveChangesAsync();
            return projekat;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var postojeciProjekat = await _context.Projektis.FindAsync(id);

            if(postojeciProjekat == null)
            {
                return false;
            }

            _context.Projektis.Remove(postojeciProjekat);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Projekti>> GetAllAsync()
        {
            var projekti = await _context.Projektis
                .Include(p=> p.KreiraoKorisnik)                
                .ToListAsync();

            return projekti;
        }

        public async Task<PregledProjektaViewModel?> GetByIdAsync(Guid projekatId)
        {
            var projekat = await _context.Projektis
                .Include(p => p.KreiraoKorisnik)
                .Include(p => p.Zanr)
                .FirstOrDefaultAsync(p => p.Id == projekatId);

            if (projekat == null)
                return null;

            var clanovi = await _context.ClanoviProjekta
                .Include(cp => cp.Korisnik)
                .Where(cp => cp.ProjekatId == projekatId)
                .ToListAsync();

            var zadaci = await _context.Zadacis
                .Include(z => z.DodeljenKorisniku)
                .Where(z => z.ProjekatId == projekatId)
                .ToListAsync();

            return new PregledProjektaViewModel
            {
                Id = projekat.Id,
                Naziv = projekat.Naziv,
                Opis = projekat.Opis ?? "Nema opisa",
                Zanr = projekat.Zanr?.Naziv ?? "Nema zanra",
                Status = projekat.Status,
                Budzet = projekat.Budzet ?? 0,
                DatumPocetka = projekat.DatumPocetka,
                Rok = projekat.Rok,
                KreiraoKorisnikIme = $"{projekat.KreiraoKorisnik.Ime} {projekat.KreiraoKorisnik.Prezime}",

                Clanovi = clanovi.Select(cp => new ClanTimaViewModel
                {
                    Ime = cp.Korisnik.Ime,
                    Prezime = cp.Korisnik.Prezime,
                    Uloga = cp.Uloga
                }).ToList(),

                Zadaci = zadaci.Select(z => new ZadaciViewModel
                {
                    Naslov = z.Naslov,
                    Status = z.Status,
                    Prioritet = z.Prioritet,
                    Rok = z.Rok,
                    DodeljenKorisnikuIme = z.DodeljenKorisniku != null
                        ? $"{z.DodeljenKorisniku.Ime} {z.DodeljenKorisniku.Prezime}"
                        : "Nije dodeljen"
                }).ToList()
            };
        }

        public Task<IEnumerable<Projekti>> GetByKorisnikIdAsync(Guid korisnikId)
        {
            throw new NotImplementedException();
        }

        public async Task<Projekti> UpdateAsync(Projekti projekat)
        {
            var postojeciProjekat = await _context.Projektis.FirstOrDefaultAsync(p => p.Id == projekat.Id);

            if(postojeciProjekat == null)
            {
                throw new KeyNotFoundException("Projekat nije pronađen.");
            }

            postojeciProjekat.Naziv = projekat.Naziv;
            postojeciProjekat.Opis = projekat.Opis;
            postojeciProjekat.Status = projekat.Status;
            postojeciProjekat.Budzet = projekat.Budzet;
            postojeciProjekat.DatumPocetka = projekat.DatumPocetka;
            postojeciProjekat.Rok = projekat.Rok;
            postojeciProjekat.ZanrId = projekat.ZanrId;

            await _context.SaveChangesAsync();
            return postojeciProjekat;
        }
    }
}

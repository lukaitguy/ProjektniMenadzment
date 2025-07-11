using ProjektniMenadzment.Models.Domain;
using ProjektniMenadzment.Models.ViewModels;

namespace ProjektniMenadzment.Repositories.Interfaces
{
    public interface IProjektiRepository
    {
        Task<IEnumerable<Projekti>> GetAllAsync();
        Task<PregledProjektaViewModel?> GetByIdAsync(Guid id);
        Task<Projekti> AddAsync(Projekti projekat);
        Task<Projekti> UpdateAsync(Projekti projekat);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Projekti>> GetByKorisnikIdAsync(Guid korisnikId);
    }
}

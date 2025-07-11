using ProjektniMenadzment.Models.Domain;

namespace ProjektniMenadzment.Repositories.Interfaces
{
    public interface IKorisniciRepository
    {
        Task<List<Korisnici>> GetAllAsync();
    }
}

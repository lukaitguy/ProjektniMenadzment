using ProjektniMenadzment.Models;

namespace ProjektniMenadzment.Repositories.Interfaces
{
    public interface IZanroviRepository
    {
        Task<IEnumerable<Zanrovi>> GetAllAsync(); 
    }
}

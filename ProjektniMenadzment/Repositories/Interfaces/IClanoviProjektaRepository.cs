using ProjektniMenadzment.Models.Domain;

namespace ProjektniMenadzment.Repositories.Interfaces
{
    public interface IClanoviProjektaRepository
    {
        Task<ClanoviProjektum> AddAsync(ClanoviProjektum clan);

    }
}

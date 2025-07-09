namespace ProjektniMenadzment.Models.ViewModels
{
    public class CreateZadatakRequest
    {
        public Guid ProjekatId { get; set; }

        public string Naslov { get; set; } = null!;
        public string? Opis { get; set; }
        public string Prioritet { get; set; } = null!;
        public DateOnly? Rok { get; set; }
    }
}
